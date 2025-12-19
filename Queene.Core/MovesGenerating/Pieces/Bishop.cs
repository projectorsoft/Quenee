using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Bishop : Slider, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Bishop;
        public const int Value = 350;

        public Bishop(BoardContext bitBoardContext,
            IList<Move> movesList,
            IPiecesListService piecesListService,
            ZorbistHash zorbistHash)
            : base(bitBoardContext, movesList, piecesListService, zorbistHash)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            GenerateMoves(MovesContainer.BishopMagics, generationType, SliderTypeEnum.Bishop);
        }
    }
}
