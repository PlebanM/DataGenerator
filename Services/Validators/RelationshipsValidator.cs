using DataGenerator.Models;
using DataGenerator.Models.Relationships;
using DataGenerator.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class RelationshipsValidator
    {
        
        public ValidationResult ValidateRelationships(DesiredTableStructure[] structures, List<Relationship> relationships)
        {
            var validationResult = new ValidationResult();
            foreach (Relationship relationship in relationships)
            {
                validationResult = TablesInRelationExist(structures, relationship);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
                validationResult = TablesHaveCorrectLengthForManyToOneRelation(structures, relationship);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
                validationResult = CheckIfModalityIsCorrect(relationship);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
                validationResult = CheckIfCardinalityIsCorrect(relationship);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
                validationResult = CheckIfReferencedColumnIsCorrectForManyToOneRelation(structures, relationship);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
            }
            return validationResult;
        }

        private ValidationResult CheckIfReferencedColumnIsCorrectForManyToOneRelation(DesiredTableStructure[] structures, Relationship relationship)
        {
            var result = new ValidationResult();
            RelationshipEntity many = GetEntityWithCardinality("many", relationship);
            RelationshipEntity one = GetEntityWithCardinality("one", relationship);
            if (many == null || one == null)
            {
                return result;
            }
            DesiredTableStructure oneTable = GetTableStructureByName(structures, one.TableName);
            if (oneTable.ColumnStructures.Select(c => c.Name).Contains(one.ColumnName))
            {
                return result;
            }

            result.IsValid = false;
            result.Subject = "Wrong reference";
            result.Description = $"Column of name {one.ColumnName} does not exist in table {one.TableName}";
            return result;

        }

        private ValidationResult CheckIfCardinalityIsCorrect(Relationship relationship)
        {
            var result = new ValidationResult();
            if (!(result = CheckIfCardinalityIsCorrect(relationship.EntityOne)).IsValid)
            {
                return result;
            }
            else if (!(result = CheckIfCardinalityIsCorrect(relationship.EntityTwo)).IsValid)
            {
                return result;
            }
            return result;
        }

        private ValidationResult CheckIfCardinalityIsCorrect(RelationshipEntity entity)
        {
            var result = new ValidationResult();
            var cardinalities = new string[] { "one", "many" };
            if (!cardinalities.Contains(entity.Cardinality))
            {
                result.IsValid = false;
                result.Subject = $"Wrong cardinality: {entity.Cardinality}";
                result.Description = "Cardinality can be set only to one or many";
            }
            return result;
        }

        private ValidationResult CheckIfModalityIsCorrect(Relationship relationship)
        {
            var result = new ValidationResult();
            if (!(result = CheckIfModalityIsCorrect(relationship.EntityOne)).IsValid)
            {
                return result;
            } else if (!(result = CheckIfModalityIsCorrect(relationship.EntityTwo)).IsValid)
            {
                return result;
            }
            return result;
        }

        private ValidationResult CheckIfModalityIsCorrect(RelationshipEntity entity)
        {
            var result = new ValidationResult();
            var modalities = new string[] { "one", "zero" };
            if (!modalities.Contains(entity.Modality))
            {
                result.IsValid = false;
                result.Subject = $"Wrong modality: {entity.Modality}";
                result.Description = "Modality can be set only to one or zero";
            }
            return result;
        }

        private ValidationResult TablesHaveCorrectLengthForManyToOneRelation(DesiredTableStructure[] structures, Relationship relationship)
        {
            var result = new ValidationResult();
            RelationshipEntity many = GetEntityWithCardinality("many", relationship);
            RelationshipEntity one = GetEntityWithCardinality("one", relationship);
            if (many == null || one == null)
            {
                return result;
            }
            
            if (many.Modality == "one")
            {
                var tableMany = GetTableStructureByName(structures, many.TableName);
                var tableOne = GetTableStructureByName(structures, one.TableName);
                if (tableMany.RowCount < tableOne.RowCount)
                {
                    result.IsValid = false;
                    result.Subject = $"Table {tableMany.Name} is too short";
                    result.Description = "Table with cardinality many and modality one has to be at least as long as" +
                        "the other table in one to many relationship";
                    return result;
                }
            }
            return result;
        }

        private DesiredTableStructure GetTableStructureByName(DesiredTableStructure[] structures, string name)
        {
            foreach (var structure in structures)
            {
                if (structure.Name == name)
                {
                    return structure;
                }
            }
            return null;
        }

        private RelationshipEntity GetEntityWithCardinality(string cardinality, Relationship relationship)
        {
            string cardOne = relationship.EntityOne.Cardinality;
            string cardTwo = relationship.EntityTwo.Cardinality;

            RelationshipEntity entity = null;
            if (cardOne == cardinality)
            {
                return relationship.EntityOne;
            }
            else if (cardTwo == cardinality)
            {
                entity = relationship.EntityTwo;
            }
            return entity;
        }

        private ValidationResult TablesInRelationExist(DesiredTableStructure[] structures, Relationship relationship)
        {
            var result = TableOfNameExists(relationship.EntityOne.TableName, structures);
            return result.IsValid ? TableOfNameExists(relationship.EntityTwo.TableName, structures) : result;
        }

        private ValidationResult TableOfNameExists(string name, DesiredTableStructure[] structures)
        {
            var result = new ValidationResult();
            if (!structures.Any(s => s.Name == name))
            {
                result.IsValid = false;
                result.Subject = $"Table specified in references as {name} does not exist";
                result.Description = "Each table used in references must exists in tables context";
            } 
            return result;
        }
    }
}
