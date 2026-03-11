using Queene.Core.Consts;
using Queene.Core.Enums;
using System.Text;

namespace Queene.Core.MovesGenerating.PiecesList
{
    public class PieceTypeList
    {
        public const byte MAX_PIECES_NUMBER = 32;

        private int _count;
        private byte[] _squares;
        private PieceTypeEnum _pieceType;

        public PieceTypeList(PieceTypeEnum pieceType)
        {
            _pieceType = pieceType;
            _squares = new byte[MAX_PIECES_NUMBER];

            _count = 0;
        }

        public void Add(byte square)
        {
            _squares[_count++] = square;
        }

        public byte GetAtIndex(int index)
        {
            return _squares[index];
        }

        public void SetAtIndex(int index, byte square)
        {
            _squares[index] = square;
        }

        public byte RemoveAtIndex(int index)
        {
            return _squares[index] = _squares[--_count];
        }

        public void Clear()
        {
            _count = 0;
        }

        public int Count()
        {
            return _count;
        }

        public int GetLastIndex()
        {
            return _count - 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _count; i++)
                sb.Append(BoardConsts.SQUARES_NAMES[_squares[i]] + ", ");

            return $"{_pieceType}: [{sb.ToString().TrimEnd(',')}]";
        }
    }
}
