using DataGenerator.Data;
using DataGenerator.Models.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class OptionsProvider : IOptionsProvider
    {
        private OptionsContext optionsContext;

        public OptionsProvider(OptionsContext optionsContext)
        {
            this.optionsContext = optionsContext;
        }
        public List<OptionsRepresentation> getOptionsRepresentetion()
        {
            var columnTypes = GetColumnTypes();
            return GetOptionsRepresentationsFor(columnTypes);
        }

        private List<ColumnType> GetColumnTypes()
        {
            return optionsContext.ColumnTypes
                .Include(ct => ct.ColumnTypeOptions)
                .ThenInclude(cto => cto.Option)
                .ToList();
        }

        private List<OptionsRepresentation> GetOptionsRepresentationsFor(List<ColumnType> columnTypes)
        {
            var representations = new List<OptionsRepresentation>();

            foreach (var columnType in columnTypes)
            {
                var options = new List<Option>();
                foreach (var option in columnType.ColumnTypeOptions.Select(e => e.Option))
                {
                    options.Add(option);
                }
                representations.Add(new OptionsRepresentation { Type = columnType, Options = options });
            }

            return representations;
        }
    }
}
