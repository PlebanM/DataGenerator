using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerator.Models;
using DataGenerator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {

        private DataContext dataContext;

        public GeneratorController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        // GET: api/Generator
        [HttpGet]
        public ActionResult Get(GeneratorSetupData generatorSetupData)
        {
            CSVColumnGenerator columnGenerator = new CSVColumnGenerator(dataContext);
            CSVTableGenerator tableGenerator = new CSVTableGenerator(columnGenerator);
            byte[] csvFile = tableGenerator.GenerateTable(generatorSetupData.tables[0], generatorSetupData.settings);
            return File(csvFile, "application/csv", "my_file.csv");
        }
    }
}
