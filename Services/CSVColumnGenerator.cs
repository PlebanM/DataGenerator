using com.sun.net.httpserver;
using DataGenerator.Models;
using DataGenerator.Models.Errors;
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
        private Dictionary<string, Func<Dictionary<string, string>, long, List<string>>> generatorFunctions;

        public CSVColumnGenerator(DataContext dataContext)
        {
            this.dataContext = dataContext;
            this.generatorFunctions = new Dictionary<string, Func<Dictionary<string, string>, long, List<string>>>();
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
            generatorFunctions["randomWord"] = GenerateRandomString;
            generatorFunctions["date"] = GenerateRandomDate;
            generatorFunctions["ID"] = GenerateID;


        }

        private List<string> GenerateID(Dictionary<string, string> options, long length)
        {
            List<string> IDList = new List<string>();
            for (int i = 1; i <= length; i++)
            {
                IDList.Add(i.ToString());
            }
            return IDList;
        }

        private List<string> GenerateEmails(Dictionary<string, string> options, long length)
        {

            HashSet<String> result = new HashSet<string>();

            StringBuilder sb = new StringBuilder();
            Random rand = new Random();

            int firstNameDBSize = dataContext.FirstNames.Count();
            int lastNameDBSize = dataContext.LastNames.Count();
            int domainsDBSize = dataContext.Domains.Count();
            var firstnameDB = GenerateFirstNames(options, firstNameDBSize);
            var lastNameDB = GenerateLastNames(options, lastNameDBSize);
            var domainsDB = GenerateDomains(options, domainsDBSize);

            string sbToString;

            int toSkipFirstName, toSkipLastName, toSkipDomain = 0;
            int numberToUniqueChangeEmail = 0;
            while (result.Count < length)
            {
                toSkipFirstName = rand.Next(0, firstNameDBSize);
                toSkipLastName = rand.Next(0, lastNameDBSize);
                toSkipDomain = rand.Next(0, domainsDBSize);

                sb.Append(firstnameDB.ElementAt(toSkipFirstName));
                sb.Append(".");
                if (lastNameDB.ElementAt(toSkipLastName).Contains(' '))
                {
                    sb.Append(lastNameDB.ElementAt(toSkipLastName).Replace(' ','_').Trim());
                }
                else
                {
                    sb.Append(lastNameDB.ElementAt(toSkipLastName).Trim());
                }
                sb.Append("@");
                sb.Append(domainsDB.ElementAt(toSkipDomain));

                sbToString = sb.ToString();

                if (result.Contains(sbToString))
                {
                    int atIndex = sbToString.IndexOf("@");
                    sb.Insert(atIndex - 1, numberToUniqueChangeEmail++);
                }

                result.Add(sb.ToString());
                sb.Clear();
            }

            return result.ToList();
        }

        public List<string> GenerateDomains(Dictionary<string, string> options, long length)
        {
            var result = new List<string>();
            while (result.Count < length)
            {
                result.AddRange(from domains in dataContext.Domains
                                select domains.Name);
            }
            return result;
        }

        public List<string> GenerateLastNames(Dictionary<string, string> options, long length)
        {
            var result = new List<string>();
            while (result.Count < length)
            {
                result.AddRange(from lastName in dataContext.LastNames
                                select lastName.Name);
            }
            return result;
        }

        public List<string> GenerateFirstNames(Dictionary<string, string> options, long length)
        {
            var result = new List<string>();

            
            while (result.Count < length)
            {
                result.AddRange(from firstName in dataContext.FirstNames
                                select firstName.Name);
            }
            return result;
        }

        public List<string> GenerateIntegers(Dictionary<string, string> options, long length)
        {
            
            var optionsForm = int.TryParse(options["from"], out int from);
            var optionsGap = int.TryParse(options["gap"], out int gap);

            var result = new List<string>();

            while (result.Count() < length)

            {
                result.Add(from.ToString());
                from += gap;
            }
            
            return result;
        }


        public List<string> GenerateRandomString(Dictionary<string, string> options, long length)
        {


            var answer = new List<string>();

            Random random = new Random();
            var lettersAndNumbers = "abcdefghijklmnopqrstuvwxyz1234567890".ToArray();
            StringBuilder sb = new StringBuilder();

            int optionLength = int.Parse(options.GetValueOrDefault("length", "6"));

            if (optionLength == 0)
            {
                throw new BaseCustomException("To small", "Word must be longer than 0 signs. Check options - length.", 400);
            }
            int optionUnique = int.Parse(options.GetValueOrDefault("unique", "0"));
            if (optionUnique == 1)
            {
                new HashSet<string>(answer);
            }
            int optionLetters = int.Parse(options.GetValueOrDefault("letters", "1"));
            int optionNumbers = int.Parse(options.GetValueOrDefault("numbers", "1"));
            if (optionLetters == 0 && optionNumbers == 0)
            {
                throw new BaseCustomException("No letter, no numbers.", "Can't create word without letters and numbers. Check options.", 400);
            }

            int optionsSpacesCount = int.Parse(options.GetValueOrDefault("whiteSigns", "0"));
            if (optionsSpacesCount > (int)Math.Floor(Math.Sqrt(optionLength)))
            {
                throw new BaseCustomException("Too many whitespaces", "Can't create string with so many whitespaces, only round down sqrt(wordLength) whitespaces accepted. " +
                    "You can extend word or don't creat so many whitespace. Change option - whiteSign.", 400);
            }

            while (answer.Count() <= length)
            {
                for (int i = 0; i < optionLength; i++)
                {
                    if (optionLetters == 1 && optionNumbers == 0)
                    {
                        sb.Append((char)random.Next(97, 123));
                    }
                    else if (optionLetters == 0 && optionNumbers == 1)
                    {
                        sb.Append(random.Next(0, 10));
                    }
                    else
                    {
                        sb.Append(lettersAndNumbers[random.Next(lettersAndNumbers.Length)]);
                    }
                }

                if (optionsSpacesCount != 0)
                {
                    for (int i = 0; i < optionsSpacesCount; i++)
                    {
                        int charIndexToReplace = random.Next(1, optionLength - 1);
                        if (sb[charIndexToReplace - 1] != ' ' && sb[charIndexToReplace + 1] != ' ' && sb[charIndexToReplace] != ' ')
                        {
                            sb.Remove(charIndexToReplace, 1);
                            sb.Insert(charIndexToReplace, ' ');

                        }
                        else
                        {
                            --i;
                        }
                    }
                }

                answer.Add(sb.ToString());
                sb.Clear();
            }

            return answer.ToList();
            
        }

        public List<string> GenerateRandomDate(Dictionary<string, string> options, long length)
        {
            DateTime fromDate;
            DateTime toDate;
            if (options["fromDate"].Length != 8 || options["toDate"].Length != 8)
            {
                throw new BaseCustomException("To short date.", "Proper date format is RRRRMMDD", 400);
            }

            var dateFromOptions = options["fromDate"].Insert(4, "/").Insert(7, "/");
            var dateToOptions = options["toDate"].Insert(4, "/").Insert(7, "/");


            if (DateTime.TryParse(dateFromOptions, out fromDate) && DateTime.TryParse(dateToOptions, out toDate))
            {
                if (toDate < fromDate)
                {
                    throw new BaseCustomException("ToDate is earlier than fromDate", "Wrong dates in options", 400);
                }
            }
            else
            {
                throw new BaseCustomException("Wrong date", "Check dates in OPTION. Proper date format is RRRRMMDD", 400);
            }

            Func<DateTime> RandomDayFunc()
            {
                DateTime start = fromDate;
                Random gen = new Random();
                int range = ((TimeSpan)(toDate - start)).Days;
                return () => start.AddDays(gen.Next(range));
            }

            var getRandomDate = RandomDayFunc();

            var result = new List<string>();

            while (result.Count() < length)

            {
                result.Add(getRandomDate().ToShortDateString());
            }
            return result;
        }
    }
}
