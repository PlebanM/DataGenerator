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
        private IOptionsProvider optionsProvider;

        public GeneratorController(IOptionsProvider optionsProvider, ITableGenerator tableGenerator)
        {
            this.tableGenerator = tableGenerator;
            this.optionsProvider = optionsProvider;
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
            return optionsProvider.getOptionsRepresentetion();
        }
    }
}
