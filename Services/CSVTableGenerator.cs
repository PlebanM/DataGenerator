using CsvHelper;
using DataGenerator.Models;
using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class CSVTableGenerator
    {
        private CSVColumnGenerator columnGenerator;
        public CSVTableGenerator(CSVColumnGenerator columnGenerator)
        {
            this.columnGenerator = columnGenerator;
        }

        public byte[] GenerateTable(DesiredTableStructure structure, Settings settings)
        {
            List<List<String>> columns = new List<List<String>>();
            foreach (ColumnStructure columnStructure in structure.ColumnStructures)
            {
                columns.Add(columnGenerator.GenerateColumn(columnStructure, settings.RowNumbers));
            }

            byte[] csvFile;

            using (MemoryStream table = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(table))
            using (CsvWriter csvWriter = new CsvWriter(writer)) {
                foreach (var columnStructure in structure.ColumnStructures)
                {
                    csvWriter.WriteField(columnStructure.Name);
                }
                csvWriter.NextRecord();
                for (int i = 0; i < settings.RowNumbers; i++)
                {
                    for (int j = 0; j < columns.Count; j++)
                    {
                        csvWriter.WriteField(columns[j][i]);
                    }
                    csvWriter.NextRecord();
                }
                writer.Flush();
                table.Position = 0;
                csvFile = table.ToArray();
            }
            return csvFile;
        }

    }
}
