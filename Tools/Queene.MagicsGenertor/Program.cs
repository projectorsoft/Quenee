using Queene.Core.Enums;
using Queene.Core.Magics;
using QueeneEngine.Engine.Magics;
using System;

namespace Queene.MagicsGenertor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Queene chess engine magics generator!");
            Console.WriteLine();

            GenerateMagics(SliderTypeEnum.Bishop);
            GenerateMagics(SliderTypeEnum.Rook);
        }

        private static void GenerateMagics(SliderTypeEnum sliderTypeEnum)
        {
            Console.WriteLine($"Generating magics for {sliderTypeEnum} ...");

            var magics = MagicsGenerator.ComputeMagics(sliderTypeEnum);
            var magicsBinaryPersister = new MagicsBinaryPersisterService();
            magicsBinaryPersister.SaveMagics(magics, sliderTypeEnum);

            Console.WriteLine("Magics generated");
        }
    }
}
