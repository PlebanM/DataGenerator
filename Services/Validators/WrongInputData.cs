using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Validators
{
    public class WrongInputData
    {
        public WrongInputData(string subject, string description)
        {
            Subject = subject;
            Description = description;
        }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
