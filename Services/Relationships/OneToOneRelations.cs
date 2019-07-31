using System.Collections.Generic;
using DataGenerator.Models;
using DataGenerator.Models.Relationships;

namespace DataGenerator.Services.Relationships
{
    internal class OneToOneRelations
    {
        private Relationship relation;
        private List<FakeDataTable> fakeDataTables;

        public OneToOneRelations(Relationship relation, List<FakeDataTable> fakeDataTables)
        {
            this.relation = relation;
            this.fakeDataTables = fakeDataTables;
        }
    }
}