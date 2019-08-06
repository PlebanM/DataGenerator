using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGenerator.Models.Relationships;

namespace DataGenerator.Services.Relationships
{
    public class DataShuffle
    {
        private RelationshipEntity entityCardinalityOne;
        private List<string> dataToPopulateFK;
        private Random random = new Random();

        public DataShuffle(RelationshipEntity entityCardinalityOne, List<string> dataToPopulateFK)
        {
            this.entityCardinalityOne = entityCardinalityOne;
            this.dataToPopulateFK = dataToPopulateFK;
        }

        public List<string> CreateDataForC1M1CManyM1(int rowCount)
        {
            var listWithFK = new List<string>();
            var dataUseToFillFK = new List<string>();
                dataUseToFillFK.AddRange(dataToPopulateFK);
            for (int i = 0; i < rowCount; i++)
            {
                int randomIndex = random.Next(dataUseToFillFK.Count);
                listWithFK.Add(dataUseToFillFK[randomIndex]);
                dataUseToFillFK.RemoveAt(randomIndex);
                if (dataUseToFillFK.Count==0)
                {
                    dataUseToFillFK.AddRange(dataToPopulateFK);
                }
            }

            return listWithFK;
        }

        internal List<string> CreateDataForC1M0CManyM1(int rowCount)
        {
            int countPopulateFK = dataToPopulateFK.Count;
            var listToAddNulls = CreateDataForC1M1CManyM1(rowCount);
            var percentNullInDuplicateValues = 5;
            var percentChangeToNumber = (((double)percentNullInDuplicateValues / 100));
            int howManyValuesChangeToNull = Convert.ToInt32((rowCount)*percentChangeToNumber);

            if (rowCount > countPopulateFK)
            {
                for (int i = 0; i < howManyValuesChangeToNull; i++)
                {
                    var indexChangeToNull = random.Next(rowCount);
                    listToAddNulls.RemoveAt(indexChangeToNull);
                    listToAddNulls.Insert(indexChangeToNull, null);
                }
            }

            return listToAddNulls;
        }

        internal List<string> CreateDataForC1M1CManyM0(int rowCount)
        {
            int countPopulateFK = dataToPopulateFK.Count;
            var listToAddFKOtherThanPKFromParentTable = CreateDataForC1M1CManyM1(rowCount);
            var percentOtherFK = 5;
            var percentChangeToNumber = (((double)percentOtherFK / 100));
            int howManyValuesChangeToOtherFK = Convert.ToInt32((rowCount) * percentChangeToNumber);

            if (rowCount > countPopulateFK)
            {
                for (int i = 0; i < howManyValuesChangeToOtherFK; i++)
                {
                    var indexChangeToRandomNumber = random.Next(countPopulateFK+1, rowCount);
                    var randomPK = random.Next(countPopulateFK, rowCount + 300).ToString();
                    listToAddFKOtherThanPKFromParentTable.RemoveAt(indexChangeToRandomNumber);
                    listToAddFKOtherThanPKFromParentTable.Insert(indexChangeToRandomNumber, randomPK);
                }
            }

            return listToAddFKOtherThanPKFromParentTable;
        }

        internal List<string> CreateDataForC1M0CManyM0(int rowCount)
        {
            throw new NotImplementedException();
        }





        /* public List<string> ShuffleData(int rowCount)
         {
             if (entityCardinalityMany.Modality=="one")
             {
                 return CreateListWithoutNull(rowCount);
             }
             else
             {
                 return CreateListWithNulls(rowCount);
             }
         }

         private List<string> CreateListWithNulls(int rowCount, double nullPercent = 5.0)
         {
             var convertFromPercent = nullPercent / 100;
             List<string> answer = CreateListWithoutNull(rowCount);
             for (int i = 0; i < answer.Count * convertFromPercent; i++)
             {
                 var positionToChange = random.Next(rowCount);
                 answer.RemoveAt(positionToChange);
                 answer.Insert(positionToChange, null);
             }
             return answer;
         }


         private List<string> CreateListWithoutNull(int rowCount)
         {
             List<string> answer = new List<string>();
             while (answer.Count()<rowCount)
             {
                 answer.Add(dataToPopulateFK[random.Next(dataToPopulateFK.Count())]);
             }
             return answer;
         }*/
    }
}
