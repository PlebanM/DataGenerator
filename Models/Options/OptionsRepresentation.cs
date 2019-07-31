using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Options
{
    public class OptionsRepresentation
    {
        public ColumnType Type { get; set; }
        public List<Option> Options { get; set; }
    }
}
