using DataGenerator.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Relationships
{
    public class CreateRelationColumn
    {
        FakeDataTable _fakeDataTable;
        Relationship _relationship;
        public  CreateRelationColumn(FakeDataTable fakeDataTable, Relationship relationship)
        {
            _fakeDataTable = fakeDataTable;
            _relationships = relationship;
        }


    }
}
