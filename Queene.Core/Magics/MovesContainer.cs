using Queene.Core.Enums;
using Queene.Core.Magics;
using QueeneEngine.Engine.Magics.Generators;

namespace QueeneEngine.Engine.Magics
{
    public class MovesContainer
    {
        private IMagicsBinaryPersisterService _magicsBinaryPersister;

        public  MagicResult[] BishopMagics { get; private set; }
        public MagicResult[] RookMagics { get; private set; }
        public ulong[] KingMoves { get; private set; }
        public ulong[] KnightMoves { get; private set; }
        public ulong[] PawnWhiteMoves { get; private set; }
        public ulong[] PawnWhiteCaptures { get; private set; }
        public ulong[] PawnBlackMoves { get; private set; }
        public ulong[] PawnBlackCaptures { get; private set; }

        public MovesContainer(IMagicsBinaryPersisterService magicsBinaryPersister)
        {
            _magicsBinaryPersister = magicsBinaryPersister;
            InitMovesMasks();
        }

        private void InitMovesMasks()
        {
            if (BishopMagics == null)
                BishopMagics = _magicsBinaryPersister.LoadMagics(SliderTypeEnum.Bishop);

            if (RookMagics == null)
                RookMagics = _magicsBinaryPersister.LoadMagics(SliderTypeEnum.Rook);

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
