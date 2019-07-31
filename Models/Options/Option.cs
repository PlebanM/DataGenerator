using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Options
{
    public class Option
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<ColumnTypeOption> ColumnTypeOptions { get; set; }
    }
}
