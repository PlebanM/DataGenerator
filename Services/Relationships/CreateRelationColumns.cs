using DataGenerator.Models;
using DataGenerator.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Relationships
{
    public class CreateRelationColumns
    {

        ColumnGenerator _columnGenerator;
        public CreateRelationColumns(ColumnGenerator columnGenerator)
        {
            _columnGenerator = columnGenerator;
        }

        public void CreateRelations(List<FakeDataTable> fakeDataTables, List<Relationship> relationships)
        {
            foreach (var relation in relationships)
            {
                if (relation.EntityOne.Cardinality=="many")
                {
                    FindTableToAddColumn(relation.EntityOne, fakeDataTables, relationships);
                }
                else if(relation.EntityTwo.Cardinality == "many")
                {
                    FindTableToAddColumn(relation.EntityTwo, fakeDataTables, relationships);

                }
            }
        }

        private void FindTableToAddColumn(RelationshipEntity entity, List<FakeDataTable> fakeDataTables, List<Relationship> relationships)
        {
            foreach (var fakeTable in fakeDataTables)
            {
                if (fakeTable.Name==entity.TableName)
                {
                    addColumn(fakeTable, entity, relationships);
                }
            }
        }

        private void addColumn(FakeDataTable fakeTable, RelationshipEntity entity, List<Relationship> relationships)
        {
            List<string> argumentsToAdd = new List<string>();
            if (entity.Modality=="zero")
            {
                //TODO: create intigers with null,
            }
            else if (entity.Modality == "one")
            {
                argumentsToAdd.AddRange(_columnGenerator.)
            }
            fakeTable.FakeDataColumns.Add(new FakeDataColumn(entity.ColumnName, ))
        }
    }
}
