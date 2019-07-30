using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Relationships
{
    public class RelationshipEntity
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Modality { get; set; }
        public string Cardinality { get; set; }
    }
}
