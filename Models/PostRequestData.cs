using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class PostRequestData
    {
        public Settings Settings { get; set; }
        public Tables[] Tables { get; set; }
    }
}
