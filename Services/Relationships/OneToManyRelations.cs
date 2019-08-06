using System;
using System.Collections.Generic;
using DataGenerator.Models;
using DataGenerator.Models.Relationships;

namespace DataGenerator.Services.Relationships
{
    internal class OneToManyRelations
    {
        private Relationship relation;
        private List<FakeDataTable> fakeDataTables;

        public OneToManyRelations(Relationship relation, List<FakeDataTable> fakeDataTables)
        {
            this.relation = relation;
            this.fakeDataTables = fakeDataTables;
        }
        public void AddColumnToFakeTable()
        {

            var entityCardinalityMany = FindEntityWithMany();
            var entityCardinalityOne = FindEntityWithOne();

            var modalityInCardinalityMany = entityCardinalityMany.Modality;
            var modalityInCardinalityOne = entityCardinalityOne.Modality;

            List<string> dataToPopulateFK = TakeColumnWithPK(entityCardinalityOne);
            int rowCount = unchecked((int)fakeDataTables.Find(x => x.Name == entityCardinalityMany.TableName).RowCount);


            if (entityCardinalityMany.Modality == "one")
            {

                if (entityCardinalityOne.Modality == "zero")
                {
                    dataToPopulateFK = new DataShuffle(entityCardinalityOne, dataToPopulateFK).CreateDataForC1M0CManyM1(rowCount);

                }
                else if (entityCardinalityOne.Modality=="one")
                {
                    dataToPopulateFK = new DataShuffle(entityCardinalityOne, dataToPopulateFK).CreateDataForC1M1CManyM1(rowCount);
                }
            }
            else if(entityCardinalityMany.Modality == "zero")
            {
                if (entityCardinalityOne.Modality == "zero")
                {
                    dataToPopulateFK = new DataShuffle(entityCardinalityOne, dataToPopulateFK).CreateDataForC1M0CManyM0(rowCount);

                }
                else if (entityCardinalityOne.Modality == "one")
                {
                    dataToPopulateFK = new DataShuffle(entityCardinalityOne, dataToPopulateFK).CreateDataForC1M1CManyM0(rowCount);
                }
            }

            var newColumnName = entityCardinalityOne.ColumnName;

            fakeDataTables.Find(x => x.Name == entityCardinalityMany.TableName)
                .FakeDataColumns.Add(new FakeDataColumn(newColumnName, dataToPopulateFK));

        }


        private RelationshipEntity FindEntityWithOne()
        {
            return relation.EntityOne.Cardinality == "one" ? relation.EntityOne : relation.EntityTwo;
        }

        private RelationshipEntity FindEntityWithMany()
        {
            return relation.EntityOne.Cardinality == "many" ? relation.EntityOne : relation.EntityTwo;
        }

        private List<string> TakeColumnWithPK(RelationshipEntity entityCardinalityMany)
        {
            return fakeDataTables.Find(x => x.Name == entityCardinalityMany.TableName)
                .FakeDataColumns.Find(y => y.Name == entityCardinalityMany.ColumnName).Data;
        }

        /*  var entityCardinalityMany = FindEntityWithMany();
          var entityCardinalityOne = FindEntityWithOne();

          var modalityInCardinalityMany = entityCardinalityMany.Modality;
          var modalityInCardinalityOne = entityCardinalityOne.Modality;

          List<string> dataToPopulateFK = TakeColumnWithPK(entityCardinalityOne);
          int rowCount = unchecked((int)fakeDataTables.Find(x => x.Name == entityCardinalityMany.TableName).RowCount);

          dataToPopulateFK = new DataShuffle(entityCardinalityMany, dataToPopulateFK).ShuffleData(rowCount);
          var newColumnName = entityCardinalityMany.ColumnName;

          fakeDataTables.Find(x => x.Name == entityCardinalityMany.TableName)
              .FakeDataColumns.Add(new FakeDataColumn(newColumnName, dataToPopulateFK));
      }



      private List<string> TakeColumnWithPK(RelationshipEntity entityCardinalityOne)
      {
          return fakeDataTables.Find(x => x.Name == entityCardinalityOne.TableName)
              .FakeDataColumns.Find(y => y.Name == entityCardinalityOne.ColumnName).Data;
      }

      private RelationshipEntity FindEntityWithOne()
      {
          return relation.EntityOne.Cardinality == "one" ? relation.EntityOne : relation.EntityTwo;
      }

      private RelationshipEntity FindEntityWithMany()
      {
          return relation.EntityOne.Cardinality == "many" ? relation.EntityOne : relation.EntityTwo;
      }
  }*/
    }
}