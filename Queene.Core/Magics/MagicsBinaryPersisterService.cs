using MemoryPack;
using Queene.Core.Enums;
using Queene.Core.Exceptions;
using System;
using System.IO;

namespace Queene.Core.Magics
{
    public class MagicsBinaryPersisterService : IMagicsBinaryPersisterService
    {
        private const string MAGICS_PREFIX = "Magics_";

        public MagicResult[] LoadMagics(SliderTypeEnum sliderType)
        {
            try
            {
                return ReadFromBinaryFile($"Assets/{MAGICS_PREFIX}{sliderType}.bin");
            }
            catch (Exception ex)
            {
                throw new MagicsException($"Error during loading magic keys for '{sliderType}'", ex);
            }
        }

        public void SaveMagics(MagicResult[] magics, SliderTypeEnum sliderType)
        {
            try
            {
                byte[] data = MemoryPackSerializer.Serialize(magics);

                SaveToBinaryFile($"{MAGICS_PREFIX}{sliderType}.bin", data);
            }
            catch (Exception ex)
            {
                throw new MagicsException($"Error during saving magic keys for '{sliderType}'", ex);
            }
        }

        private static void SaveToBinaryFile(string fileName, byte[] data)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Append);
            using BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
        }

        private static MagicResult[] ReadFromBinaryFile(string fileName)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using MemoryStream br = new MemoryStream();
            new StreamReader(fs).BaseStream.CopyTo(br);
            var data = br.GetBuffer();

            return MemoryPackSerializer.Deserialize<MagicResult[]>(data)!;
        }
    }
}