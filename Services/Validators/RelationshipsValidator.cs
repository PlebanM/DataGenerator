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
        
        public WrongInputData ValidateRelationships(DesiredTableStructure[] structures, List<Relationship> relationships)
        {
            foreach(Relationship relationship in relationships)
            {
                var wrongInput = TablesInRelationExist(structures, relationship);
                if (wrongInput != null)
                {
                    return wrongInput;
                }
            }
            return null;
        }

        private WrongInputData TablesInRelationExist(DesiredTableStructure[] structures, Relationship relationship)
        {
            return TableOfNameExists(relationship.EntityOne.TableName, structures) ?? TableOfNameExists(relationship.EntityTwo.TableName, structures);
        }

        private WrongInputData TableOfNameExists(string name, DesiredTableStructure[] structures)
        {
            if (structures.Any(s => s.Name == name))
            {
                return null;
            } else
            {
                string subject = $"Table specified in references as {name} does not exist";
                string description = "Each table used in references must exists in tables context";
                return new WrongInputData(subject, description);
            }
        }
    }
}
