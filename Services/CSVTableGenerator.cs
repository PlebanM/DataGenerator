using CsvHelper;
using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class CSVTableGenerator
    {

        public byte[] CreateCSVFileContentFrom(FakeDataTable fakeDataTable)
        {
            
            var config = new CsvHelper.Configuration.Configuration();
            config.Delimiter = ",";
            config.Encoding = Encoding.UTF8;
            config.InjectionEscapeCharacter = '\n';
            using (MemoryStream table = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(table))
            using (CsvWriter csvWriter = new CsvWriter(writer, config))
            {
                AddColumnNamesToContent(csvWriter, fakeDataTable.FakeDataColumns);
                AddDataRowsToContent(csvWriter, fakeDataTable.FakeDataColumns, fakeDataTable.RowCount);
                writer.Flush();
                table.Position = 0;
                return table.ToArray();
            }
        }

        private void AddColumnNamesToContent(CsvWriter csvWriter, List<FakeDataColumn> fakeDataColumns)
        {
            foreach (var column in fakeDataColumns)
            {
                csvWriter.WriteField(column.Name);
            }
            csvWriter.NextRecord();
        }

        private void AddDataRowsToContent(CsvWriter csvWriter, List<FakeDataColumn> dataColumns, long rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < dataColumns.Count; j++)
                {
                    csvWriter.WriteField(dataColumns[j].Data[i]);
                }
                csvWriter.NextRecord();
            }
        }

    }
}
