using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class FakeDataColumn
    {
        public FakeDataColumn(string name, List<string> data)
        {
            Name = name;
            Data = data;
        }
        public string Name { get; private set; }
        public List<string> Data { get; private set; }
    }
}
