using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class GeneratorSetupData
    {
        public Settings Settings { get; set; }
        public DesiredTableStructure[] Tables { get; set; }
    }
}
