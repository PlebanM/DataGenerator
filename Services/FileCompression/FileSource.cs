using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services.FileCompression
{
    public class FileSource
    {

        public FileSource(byte[] content, string name, string extension)
        {
            Content = content;
            Name = name;
            Extension = extension;
        }

        public byte[] Content { get; private set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }
    }
}
