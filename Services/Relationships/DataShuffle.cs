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

        public List<string> TakeDataForModalityOne(int rowCount)
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

        internal List<string> TakeDataForModalityZero(int rowCount)
        {
            var listToAddNulls = TakeDataForModalityOne(rowCount);

            if (true)
            {

            }

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
