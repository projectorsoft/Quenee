using Queene.Core.Enums;
using Queene.Core.Magics;
using QueeneEngine.Engine.Magics.Generators;

namespace QueeneEngine.Engine.Magics
{
    public static class MovesContainer
    {
        public static MagicResult[] BishopMagics { get; private set; }
        public static MagicResult[] RookMagics { get; private set; }
        public static ulong[] KingMoves { get; private set; }
        public static ulong[] KnightMoves { get; private set; }
        public static ulong[] PawnWhiteMoves { get; private set; }
        public static ulong[] PawnWhiteCaptures { get; private set; }
        public static ulong[] PawnBlackMoves { get; private set; }
        public static ulong[] PawnBlackCaptures { get; private set; }

        public static void InitMovesMasks(IMagicsBinaryPersisterService magicsBinaryPersister)
        {
            if (BishopMagics == null)
                BishopMagics = magicsBinaryPersister.LoadMagics(SliderTypeEnum.Bishop);

            if (RookMagics == null)
                RookMagics = magicsBinaryPersister.LoadMagics(SliderTypeEnum.Rook);

            if (KingMoves == null)
                KingMoves = KingMovesGenerator.GeneratetMoves();

            if (KnightMoves == null)
                KnightMoves = KnightMovesGenerator.GeneratetMoves();

            if (PawnWhiteMoves == null)
                PawnWhiteMoves = PawnMovesGenerator.GeneratetWhiteMoves();

            if (PawnWhiteCaptures == null)
                PawnWhiteCaptures = PawnMovesGenerator.GeneratetWhiteCaptures();

            if (PawnBlackMoves == null)
                PawnBlackMoves = PawnMovesGenerator.GeneratetBlackMoves();

            if (PawnBlackCaptures == null)
                PawnBlackCaptures = PawnMovesGenerator.GeneratetBlackCaptures();
        }
    }
}
