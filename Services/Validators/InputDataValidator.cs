using DataGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.Validators
{
    public class InputDataValidator
    {

        public InputDataValidator(RelationshipsValidator relationshipsValidator)
        {
            RelationshipsValidator = relationshipsValidator;
        }
        public RelationshipsValidator RelationshipsValidator { get; private set; }

        public WrongInputData Validate(GeneratorSetupData generatorSetupData)
        {
            return RelationshipsValidator.ValidateRelationships(generatorSetupData.Tables, generatorSetupData.Relationships);
        }
    }
}
