using DataGenerator.Services.FileCompression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Services
{
    public class Zipper
    {
        public byte[] Pack(List<FileSource> fileSources)
        {
            using(var memoryStream = new MemoryStream())
            {
                using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var source in fileSources)
                    {
                        var entry = zip.CreateEntry(source.Name + "." + source.Extension);
                        using (var originalFileMemoryStream = new MemoryStream(source.Content))
                        using (var entryStream = entry.Open())
                        {
                            originalFileMemoryStream.CopyTo(entryStream);
                        }
                    }
                }
                    
                return memoryStream.ToArray();
            }

        }
    }
}
