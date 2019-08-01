using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGenerator.Models.Relationships;

namespace DataGenerator.Services.Relationships
{
    public class DataShuffle
    {
        private RelationshipEntity entityCardinalityMany;
        private List<string> dataToPopulateFK;
        private Random random = new Random();

        public DataShuffle(RelationshipEntity entityCardinalityMany, List<string> dataToPopulateFK)
        {
            this.entityCardinalityMany = entityCardinalityMany;
            this.dataToPopulateFK = dataToPopulateFK;
        }

        public List<string> ShuffleData(int rowCount)
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
            for (int i = 0; i < answer.Count*convertFromPercent; i++)
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
        }
    }
}
