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
        string tuplaItemFirstColumnName;
        string tuplaItemSecondColumnName;



        public ManyToManyRelations(Relationship relation, List<FakeDataTable> fakeDataTables)
        {
            this.relation = relation;
            this.fakeDataTables = fakeDataTables;
        }

        public void CreateJunctionTable()
        {
            List<string> dataFromFirstTable = TakeColumnToCreateJunctionTable(relation.EntityOne);
            List<string> dataFromSecondTable = TakeColumnToCreateJunctionTable(relation.EntityTwo);

            var tupleWithDataToCreateJunctionTable = createDataForNewTable(dataFromFirstTable, dataFromSecondTable);
            var firstColumn = tupleWithDataToCreateJunctionTable.Select(x => x.Item1).ToList();
            var secondColumn = tupleWithDataToCreateJunctionTable.Select(x => x.Item2).ToList();

            var junctionTableName = relation.EntityOne.TableName+relation.EntityTwo.TableName;
            var rowCount = tupleWithDataToCreateJunctionTable.Count();
            var fakeDataColumns = new List<FakeDataColumn>();
            fakeDataColumns.Add(new FakeDataColumn(tuplaItemFirstColumnName, firstColumn));
            fakeDataColumns.Add(new FakeDataColumn(tuplaItemSecondColumnName, secondColumn));

            fakeDataTables.Add(new FakeDataTable(junctionTableName, rowCount, fakeDataColumns));
                   
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
                return createTupleEevryDataAtLeastOneTime(dataFromFirstTable, dataFromSecondTable);
            }
            else if(modalityFirst == "one")
            {
                tuplaItemFirstColumnName = relation.EntityOne.ColumnName;
                tuplaItemSecondColumnName = relation.EntityTwo.ColumnName;
                return createTupleOneZeroModality(dataFromFirstTable, dataFromSecondTable);

            }
            else
            {
                tuplaItemFirstColumnName = relation.EntityTwo.ColumnName;
                tuplaItemSecondColumnName = relation.EntityOne.ColumnName;
                return createTupleOneZeroModality( dataFromSecondTable, dataFromSecondTable);

            }

        }

        private List<(string, string)> createTupleOneZeroModality(List<string> dataFromFirstTableModalityOne, List<string> dataFromSecondTableModalityZero)
        {   
            //TODO: extract to new method that return tuple
            var biggerList = new List<string>();
            if (dataFromFirstTableModalityOne.Count() >= dataFromSecondTableModalityZero.Count())
            {
                biggerList.AddRange(dataFromFirstTableModalityOne);               
            }
            else
            {
                biggerList.AddRange(dataFromSecondTableModalityZero);
            }
            
            var answer = new List<(string, string)>();
            var i = 0;
            var j = 0;
          

            while (answer.Count() < (int)biggerList.Count()*1.5)
            {
                if (i >= dataFromFirstTableModalityOne.Count())
                {
                    i = 0;
                }
                answer.Add((dataFromFirstTableModalityOne[i++], dataFromSecondTableModalityZero[random.Next(dataFromSecondTableModalityZero.Count())]));
            }

            return answer;
        }

        

        private List<(string, string)> createTupleEevryDataAtLeastOneTime(List<string> dataFromFirstTable, List<string> dataFromSecondTable)
        {
            //TODO: extract to new method that return tuple
            var smallerList = new List<string>();
            var biggerList = new List<string>();
            if (dataFromFirstTable.Count() >= dataFromSecondTable.Count())
            {
                biggerList.AddRange(dataFromFirstTable);
                smallerList.AddRange(dataFromSecondTable);
                tuplaItemFirstColumnName = relation.EntityTwo.ColumnName;
                tuplaItemSecondColumnName = relation.EntityOne.ColumnName;

            }
            else
            {
                biggerList.AddRange(dataFromSecondTable);
                smallerList.AddRange(dataFromFirstTable);
                tuplaItemFirstColumnName = relation.EntityOne.ColumnName;
                tuplaItemSecondColumnName = relation.EntityTwo.ColumnName;

            }

            var answer = new List<(string, string)>();
            var i = 0;
            var j = 0;
            while (answer.Count() < biggerList.Count())
            {
                if (i >= smallerList.Count())
                {
                    i = 0;
                }
                answer.Add((smallerList[i++], biggerList[j++]));
            }

            return answer;

        }


        private List<(string,string)> createTupleRandomly(List<string> dataFromFirstTable, List<string> dataFromSecondTable)
        {
            
            int firstTableCount = dataFromFirstTable.Count();
            int secondTableCount = dataFromSecondTable.Count();
            var howLongTable =(decimal) firstTableCount > secondTableCount ? firstTableCount * 1.5 : secondTableCount * 1.5;
            var answer = new HashSet<(string, string)>();

            
            tuplaItemFirstColumnName = relation.EntityOne.ColumnName;
            tuplaItemSecondColumnName = relation.EntityTwo.ColumnName;

            while (answer.Count() < howLongTable)
            {
                var randomNumberFromTable1 = random.Next(0, firstTableCount);
                int? addToTuple = randomNumberFromTable1 == 0 ? (int?)null : randomNumberFromTable1;

                var randomNumberFromTable2 = random.Next(0, secondTableCount);
                int? addToTuple2 = randomNumberFromTable2 == 0 ? (int?)null : randomNumberFromTable2;
                answer.Add((dataFromFirstTable[addToTuple.GetValueOrDefault()], dataFromSecondTable[addToTuple2.GetValueOrDefault()])) ;
            }
            
            return answer.ToList();
        }
    }
}