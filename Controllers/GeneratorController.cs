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
using DataGenerator.Services.Validators;
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
        private InputDataValidator validator;

        public GeneratorController(IOptionsProvider optionsProvider,
            ITableGenerator tableGenerator,
            Zipper zipper,
            CSVTableGenerator csvTableGenerator,
            InputDataValidator validator)
        {
            this.tableGenerator = tableGenerator;
            this.optionsProvider = optionsProvider;
            this.csvTableGenerator = csvTableGenerator;
            this.zipper = zipper;
            this.validator = validator;
        }
        
        // GET: api/Generator
        [HttpGet]
        public ActionResult Get(GeneratorSetupData generatorSetupData)
        {
            ValidationResult validationResult = validator.Validate(generatorSetupData);
            if (!validationResult.IsValid)
            {
                return new JsonResult(validationResult);
            }

            List<FileSource> csvFiles = new List<FileSource>();
            List<FakeDataTable> fakeDataTables = new List<FakeDataTable>();
            foreach (var table in generatorSetupData.Tables)
            {
                FakeDataTable fakeDataTable = tableGenerator.GenerateTable(table);
                fakeDataTables.Add(fakeDataTable);
            }
            RelationshipController relationshipController = new RelationshipController(generatorSetupData.Relationships, fakeDataTables);
            foreach (var fakeDataTable in fakeDataTables)
            {
                csvFiles.Add(new FileSource(
                    csvTableGenerator.CreateCSVFileContentFrom(fakeDataTable),

                    fakeDataTable.Name,
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
