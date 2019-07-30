using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class DesiredTableStructure
    {
        public string Name { get; set; }
        public long RowCount { get; set; }
        public ColumnStructure[] ColumnStructures { get; set; }
    }
}
