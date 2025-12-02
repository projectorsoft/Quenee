using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Queen : Slider, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Queen;

        public Queen(BitBoardContext bitBoardContext,
            MovesContainer movesContainer,
            IList<Move> movesList,
            IPiecesListService piecesListService)
            : base(bitBoardContext, movesContainer, movesList, piecesListService)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateMoves(_movesContainer.BishopMagics, generationType, SliderTypeEnum.Bishop);
            GenerateMoves(_movesContainer.RookMagics, generationType, SliderTypeEnum.Rook);
        }
    }
}
