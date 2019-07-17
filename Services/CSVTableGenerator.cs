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

        public MemoryStream GenerateTable(DesiredTableStructure structure, Settings settings)
        {
            List<List<String>> columns = new List<List<String>>();
            foreach (Column columnStructure in structure.Columns)
            {
                columns.Add(columnGenerator.GenerateColumn(columnStructure, settings));
            }

            MemoryStream table = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(table))
            using (CsvWriter csvWriter = new CsvWriter(writer)) {
                foreach (var columnName in structure)
                {
                    csvWriter.WriteField(columnName);
                }
                csvWriter.NextRecord();
                for (int i = 0; i < settings.RowNumber; i++)
                {
                    for (int j = 0; j < columns.Count; j++)
                    {
                        csvWriter.WriteField(columns[j][i]);
                    }
                }
                writer.Flush();
                table.Position = 0;
            }
            return table;
        }

    }
}
