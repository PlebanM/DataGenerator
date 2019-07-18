using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class TestService
    {
        public String PrintValues(Tables[] tables)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in tables)
            {
                stringBuilder.Append(item.Name);
                stringBuilder.Append(", type: ");
                stringBuilder.Append(item.Type);
                stringBuilder.Append(", słownik percent: ");
                stringBuilder.Append(item.Options["from"]);


            }

            return stringBuilder.ToString();
        }

    }
}
