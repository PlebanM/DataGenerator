using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Options
{
    public class ColumnTypeOption
    {
        public long ColumnTypeId { get; set; }
        public ColumnType ColumnType { get; set; }
        public long OptionId { get; set; }
        public Option Option { get; set; }
    }
}
