using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class TableGenerator : ITableGenerator
    {
        private ColumnGenerator columnGenerator;
        public TableGenerator(ColumnGenerator columnGenerator)
        {
            this.columnGenerator = columnGenerator;
        }

        public FakeDataTable GenerateTable(DesiredTableStructure structure)
        {
            var columns = GetAllFakeDataColumns(structure);
            return new FakeDataTable(structure.Name, structure.RowCount, columns);
        }

        private List<FakeDataColumn> GetAllFakeDataColumns(DesiredTableStructure structure)
        {
            var columns = new List<FakeDataColumn>();
            foreach (ColumnStructure columnStructure in structure.ColumnStructures)
            {
                columns.Add(columnGenerator.GenerateColumn(columnStructure, structure.RowCount));
            }
            return columns;
        }
    }
}
