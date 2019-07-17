using System;
using System.Collections.Generic;

namespace DataGenerator.Models
{
    public partial class TypeOptions
    {
        public int TypeId { get; set; }
        public int OptionsId { get; set; }

        public virtual Options Options { get; set; }
    }
}
