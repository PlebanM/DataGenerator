using System;
using System.Collections.Generic;

namespace DataGenerator.Models
{
    public partial class Options
    {
        public Options()
        {
            TypeOptions = new HashSet<TypeOptions>();
        }

        public int Id { get; set; }
        public string OptionName { get; set; }

        public virtual ICollection<TypeOptions> TypeOptions { get; set; }
    }
}
