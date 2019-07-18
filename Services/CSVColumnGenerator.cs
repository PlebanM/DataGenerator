using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class CSVColumnGenerator
    {
        private DataContext dataContext;
        private Dictionary<string, Func<Dictionary<string, int>, long, List<string>>> generatorFunctions;

        public CSVColumnGenerator(DataContext dataContext)
        {
            this.dataContext = dataContext;
            this.generatorFunctions = new Dictionary<string, Func<Dictionary<string, int>, long, List<string>>>();
            RegisterFunctions();
            
        }

        internal List<string> GenerateColumn(ColumnStructure columnStructure, long length)
        {
            if (generatorFunctions.ContainsKey(columnStructure.Type))
            {
                return generatorFunctions[columnStructure.Type](columnStructure.Options, length);
            }

            throw new ArgumentException();
            
        }

        private void RegisterFunctions()
        {
            generatorFunctions["lastName"] = GenerateLastNames;
            generatorFunctions["integer"] = GenerateIntegers;
        }

        public List<string> GenerateLastNames(Dictionary<string, int> options, long length)
        {
            List<String> result = new List<string>();
            while (result.Count < length)
            {
                result.AddRange(from lastName in dataContext.LastNames
                                select lastName.Name);
            }
            return result;
        }

        public List<string> GenerateIntegers(Dictionary<string, int> options, long length)
        {
            List<string> result = new List<string>();
            for (long i = 1; i <= length; i++)
            {
                result.Add(i.ToString());
            }
            return result;
        }
    }
}
