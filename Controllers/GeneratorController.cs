using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator.Data;
using DataGenerator.Models;
using DataGenerator.Models.Options;
using DataGenerator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private ITableGenerator tableGenerator;
        private OptionsContext optionsContext;

        public GeneratorController(OptionsContext optionsContext, ITableGenerator tableGenerator)
        {
            this.tableGenerator = tableGenerator;
            this.optionsContext = optionsContext;
        }
        
        // GET: api/Generator
        [HttpGet]
        public ActionResult Get(GeneratorSetupData generatorSetupData)
        {
            var csvFile = tableGenerator.GenerateTable(generatorSetupData.tables[0], generatorSetupData.settings);
            return File(csvFile, "application/csv", "my_file.csv");
        }

        [HttpGet]
        [Route("Options")]
        public List<OptionsRepresentation> Options()
        {
            var types = optionsContext.ColumnTypes
                .Include(ct => ct.ColumnTypeOptions)
                .ThenInclude(cto => cto.Option)
                .ToList();

            var representations = new List<OptionsRepresentation>();

            foreach (var columnType in types)
            {
                var options = new List<string>();
                foreach (var option in columnType.ColumnTypeOptions.Select(e => e.Option))
                {
                    options.Add(option.Name);
                }
                representations.Add(new OptionsRepresentation { Type = columnType.type, Options = options });
            }
                                           
            return representations;
        }
    }
}
