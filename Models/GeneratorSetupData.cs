using DataGenerator.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class GeneratorSetupData
    {
        public List<Relationship> Relationships { get; set; }
        public Settings Settings { get; set; }
        public DesiredTableStructure[] Tables { get; set; }
    }
}
