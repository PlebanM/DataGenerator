using DataGenerator.Models;
using DataGenerator.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Relationships
{
    public class RelationshipController
    {
        
        public RelationshipController(List<Relationship> relationships, List<FakeDataTable> fakeDataTables)
        {
            CreateRelation(relationships, fakeDataTables);
        }

        public void CreateRelation(List<Relationship> relationships, List<FakeDataTable> fakeDataTables)
        {
            foreach (var relation in relationships)
            {
                if (relation.EntityOne.Cardinality=="many" && relation.EntityTwo.Cardinality=="many")
                {
                    ManyToManyRelations ManyToManyRelation = new ManyToManyRelations(relation, fakeDataTables);
                }
                else if (relation.EntityOne.Cardinality == "one" && relation.EntityTwo.Cardinality == "one")
                {
                    OneToOneRelations OneToOneRelation = new OneToOneRelations(relation, fakeDataTables);
                }
                else
                {
                    OneToManyRelations ManyToOneRelation = new OneToManyRelations(relation, fakeDataTables);
                    ManyToOneRelation.AddColumnToFakeTable();
                }
            }
        }
    }
}
