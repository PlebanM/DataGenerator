using DataGenerator.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models.Relationships
{
    public class Relationship
    {
        public RelationshipEntity EntityOne { get; set; }
        public RelationshipEntity EntityTwo { get; set; }
    }
}
