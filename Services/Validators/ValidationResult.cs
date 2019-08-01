using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Validators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsValid = true;
            Subject = "";
            Description = "";
        }
        [JsonIgnore]
        public bool IsValid { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
