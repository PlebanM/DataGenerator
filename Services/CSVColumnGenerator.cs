using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class CSVColumnGenerator
    {
        private SQLServerContext serverContext;
        private DataContext dataContext;

        public CSVColumnGenerator(SQLServerContext serverContext, DataContext dataContext)
        {
            this.serverContext = serverContext;
            this.dataContext = dataContext;
        }

        internal List<string> GenerateColumn(Column columnStructure, long length)
        {
            throw new NotImplementedException();
        }
    }
}
