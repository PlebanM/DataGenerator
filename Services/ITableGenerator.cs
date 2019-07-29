using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public interface ITableGenerator
    {
        byte[] GenerateTable(DesiredTableStructure structure);
    }
}
