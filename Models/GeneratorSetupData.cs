using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class GeneratorSetupData
    {
        public Settings settings { get; set; }
        public DesiredTableStructure[] tables { get; set; }
    }
}
