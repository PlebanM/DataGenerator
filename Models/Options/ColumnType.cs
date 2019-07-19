using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Options
{
    public class ColumnType
    {
        public long id { get; set; }
        public string type { get; set; }
        public ICollection<ColumnTypeOption> ColumnTypeOptions { get; set; }
    }
}
