using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace DataGenerator.Models
{
    public class Settings
    {
        public long Row_numbers { get; set; }
        public string ExtractFileType { get; set; }

       List<ArrayList> cos = new List<ArrayList>();
        
       public void lala()
        {
            cos.Add(new ArrayList()
            {
                "jdjd", 1, "dupa"
            });

            foreach (var item in cos)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2);
                }

            }
        }

       

    }
}
