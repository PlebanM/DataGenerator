using CsvHelper;
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
            var dataColumns = GetAllDataColumns(structure, settings.RowNumbers);
            return CreateCSVFileContentFrom(dataColumns, structure, settings.RowNumbers);
        }

        private List<List<String>> GetAllDataColumns(DesiredTableStructure structure, long rowCount)
        {
            var columns = new List<List<String>>();
            foreach (ColumnStructure columnStructure in structure.ColumnStructures)
            {
                columns.Add(columnGenerator.GenerateColumn(columnStructure, rowCount));
            }
            return columns;
        }

        private byte[] CreateCSVFileContentFrom(List<List<String>> dataColumns, DesiredTableStructure structure, long rowCount)
        {
            using (MemoryStream table = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(table))
            using (CsvWriter csvWriter = new CsvWriter(writer)) {
                AddColumnNamesToContent(csvWriter, structure);
                AddDataRowsToContent(csvWriter, dataColumns, rowCount);
                writer.Flush();
                table.Position = 0;
                return table.ToArray();
            }
        }

        private void AddColumnNamesToContent(CsvWriter csvWriter, DesiredTableStructure structure)
        {
            foreach (var columnStructure in structure.ColumnStructures)
            {
                csvWriter.WriteField(columnStructure.Name);
            }
            csvWriter.NextRecord();
        }

        private void AddDataRowsToContent(CsvWriter csvWriter, List<List<String>> dataColumns, long rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < dataColumns.Count; j++)
                {
                    csvWriter.WriteField(dataColumns[j][i]);
                }
                csvWriter.NextRecord();
            }
        }

    }
}
