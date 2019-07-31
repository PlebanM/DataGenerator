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
using DataGenerator.Services.FileCompression;
using DataGenerator.Services.Relationships;
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
        private CSVTableGenerator csvTableGenerator;
        private Zipper zipper;

        public GeneratorController(IOptionsProvider optionsProvider, ITableGenerator tableGenerator, Zipper zipper, CSVTableGenerator csvTableGenerator)
        {
            this.tableGenerator = tableGenerator;
            this.optionsProvider = optionsProvider;
            this.csvTableGenerator = csvTableGenerator;
            this.zipper = zipper;
        }
        
        // GET: api/Generator
        [HttpGet]
        public ActionResult Get(GeneratorSetupData generatorSetupData)
        {
            List<FileSource> csvFiles = new List<FileSource>();
            foreach (var table in generatorSetupData.Tables)
            {
                FakeDataTable fakeDataTable = tableGenerator.GenerateTable(table);
                
                csvFiles.Add(new FileSource(
                    csvTableGenerator.CreateCSVFileContentFrom(fakeDataTable),
                    table.Name,
                    generatorSetupData.Settings.ExtractFileType));
            }
            byte[] zipContent = zipper.Pack(csvFiles);
            return File(zipContent, "application/zip", "data.zip");
        }

        [HttpGet]
        [Route("Options")]
        public List<OptionsRepresentation> Options()
        {
            return optionsProvider.getOptionsRepresentetion();
        }
    }
}
