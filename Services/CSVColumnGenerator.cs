using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            generatorFunctions["firstName"] = GenerateFirstNames;
            generatorFunctions["integer"] = GenerateIntegers;
            generatorFunctions["email"] = GenerateEmails;

        }

        private List<string> GenerateEmails(Dictionary<string, int> options, long length)
        {
            List<String> result = new List<string>();
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();

            int firstNameDBSize = dataContext.FirstNames.Count();
            int lastNameDBSize = dataContext.LastNames.Count();
            int domainsDBSize = dataContext.Domains.Count();
            var firstnameDB = GenerateFirstNames(options, firstNameDBSize);
            var lastNameDB = GenerateLastNames(options, lastNameDBSize);
            var domainsDB = GenerateDomains(options, domainsDBSize);

            int toSkipFirstName, toSkipLastName, toSkipDomain = 0;
           
            while (result.Count < length)
            {
                toSkipFirstName = rand.Next(0, firstNameDBSize);
                toSkipLastName = rand.Next(0, lastNameDBSize);
                toSkipDomain = rand.Next(0, domainsDBSize);

                sb.Append(firstnameDB.ElementAt(toSkipFirstName));
                sb.Append(".");
                sb.Append(lastNameDB.ElementAt(toSkipLastName));
                sb.Append("@");
                sb.Append(domainsDB.ElementAt(toSkipDomain));

                result.Add(sb.ToString());
                sb.Clear();
            }

            return result;
        }

        public List<string> GenerateDomains(Dictionary<string, int> options, long length)
        {
            List<String> result = new List<string>();
            while (result.Count < length)
            {
                result.AddRange(from domains in dataContext.Domains
                                select domains.Name);
            }
            return result;
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

        public List<string> GenerateFirstNames(Dictionary<string, int> options, long length)
        {
            List<String> result = new List<string>();
            while (result.Count < length)
            {
                result.AddRange(from firstName in dataContext.FirstNames
                                select firstName.Name);
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
