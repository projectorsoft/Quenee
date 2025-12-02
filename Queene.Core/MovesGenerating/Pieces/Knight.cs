using Queene.Core.Consts;
using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.PiecesList;
using QueeneEngine.Engine.Magics;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating.Pieces
{
    public class Knight : PieceBase, IPiece
    {
        public override PieceTypeEnum PieceType => PieceTypeEnum.Knight;

        public Knight(BitBoardContext bitBoardContext,
            MovesContainer movesContainer,
            IList<Move> movesList,
            IPiecesListService piecesListService)
            : base(bitBoardContext, movesContainer, movesList, piecesListService)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenerateMoves(MoveGenerationTypeEnum generationType)
        {
            for (int i = 0; i < _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceType].Count(); i++)
            {
                _square = _bitBoardContext.PieceTypeList[_bitBoardContext.Player.Current][(byte)PieceType].GetAtIndex(i);

                if (generationType == MoveGenerationTypeEnum.All)
                    _moves = _movesContainer.KnightMoves[_square] & _bitBoardContext.EmptySquares;

                _captures = _movesContainer.KnightMoves[_square] & _bitBoardContext.Pieces[_bitBoardContext.Player.Oponnent][(byte)PieceTypeEnum.All];

                if (_bitBoardContext.Attackers != 0)
                {
                    _moves &= _bitBoardContext.CheckedSquares;
                    _captures &= _bitBoardContext.Attackers;
                }

                if (_bitBoardContext.PinnedSquares != 0)
                {
                    if ((_bitBoardContext.PinnedSquares & Powers.powersOfTwo[_square]) != 0)
                    {
                        _moves = 0;
                        _captures = 0;
                    }
                }

                AddMoves(_moves | _captures, _square, MoveTypeEnum.Move);
            }
        }
    }
}
