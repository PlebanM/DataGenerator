using DataGenerator.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public interface IOptionsProvider
    {
        List<OptionsRepresentation> getOptionsRepresentetion();
    }
}
