﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class ColumnStructure
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }
}
