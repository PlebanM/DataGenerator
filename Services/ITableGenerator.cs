using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public interface ITableGenerator
    {
        List<byte[]> GenerateTables(List<DesiredTableStructure> structures);
        byte[] GenerateTable(DesiredTableStructure structure);
    }
}
