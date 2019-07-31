using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Options
{
    public class ColumnType
    {
        [JsonIgnore]
        public long id { get; set; }
        public string type { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<ColumnTypeOption> ColumnTypeOptions { get; set; }
    }
}
