using System;
using System.Collections.Generic;
using System.Linq;
using DataGenerator.Models;
using DataGenerator.Models.Relationships;

namespace DataGenerator.Services.Relationships
{
    internal class ManyToManyRelations
    {
        private Relationship relation;
        private List<FakeDataTable> fakeDataTables;
        Random random = new Random();


        public ManyToManyRelations(Relationship relation, List<FakeDataTable> fakeDataTables)
        {
            this.relation = relation;
            this.fakeDataTables = fakeDataTables;
        }

        public void CreateJunctionTable()
        {
            List<string> dataFromFirstTable = TakeColumnToCreateJunctionTable(relation.EntityOne);
            List<string> dataFromSecondTable = TakeColumnToCreateJunctionTable(relation.EntityTwo);

            

        }

        private List<string> TakeColumnToCreateJunctionTable(RelationshipEntity entityCardinalityOne)
        {
            return fakeDataTables.Find(x => x.Name == entityCardinalityOne.TableName)
                .FakeDataColumns.Find(y => y.Name == entityCardinalityOne.ColumnName).Data;
        }

        public List<(string, string)> createDataForNewTable(List<string> dataFromFirstTable, List<string> dataFromSecondTable)
        {
            var modalityFirst = relation.EntityOne.Modality;
            var modalitySecond = relation.EntityTwo.Modality;

            if (modalityFirst =="zero" && modalityFirst == modalitySecond)
            {
                return createTupleRandomly(dataFromFirstTable, dataFromSecondTable);
            }
            else if (modalityFirst == "one" && modalityFirst == modalitySecond)
            {
                return createTupleEvryDataAtLeastOneTime(dataFromFirstTable, dataFromSecondTable);
            }
            else
            {
                return createTupleOneZeroModality(dataFromFirstTable, dataFromSecondTable);
            }

        }

        private List<(string, string)> createTupleEveryDataAtLeastOneTime(List<string> dataFromFirstTable, List<string> dataFromSecondTable)
        {
            
        }

        private List<(string,string)> createTupleRandomly(List<string> dataFromFirstTable, List<string> dataFromSecondTable)
        {
            
            int firstTableCount = dataFromFirstTable.Count();
            int secondTableCount = dataFromSecondTable.Count();
            var howLongTable =(decimal) firstTableCount > secondTableCount ? firstTableCount * 1.5 : secondTableCount * 1.5;
            var answer = new HashSet<(string, string)>();

            var randomNumberFromTable1 = random.Next(0, firstTableCount);
            int? addToTuple = randomNumberFromTable1 == 0 ?  (int?)null : randomNumberFromTable1;

            var randomNumberFromTable2 = random.Next(0, secondTableCount);
            int? addToTuple2 = randomNumberFromTable2 == 0 ? (int?)null : randomNumberFromTable2;

            while (answer.Count() < howLongTable)
            {
                
                answer.Add((dataFromFirstTable[addToTuple.GetValueOrDefault()], dataFromSecondTable[addToTuple2.GetValueOrDefault()])) ;
            }
            
            return answer.ToList();
        }
    }
}