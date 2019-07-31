using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class FakeDataTable
    {
        public FakeDataTable(string name, long rowCount, List<FakeDataColumn> fakeDataColumns)
        {
            Name = name;
            RowCount = rowCount;
            FakeDataColumns = fakeDataColumns;
        }
        public string Name { get; private set; }
        public long RowCount { get; private set; }
        public List<FakeDataColumn> FakeDataColumns { get; private set; }
    }
}
