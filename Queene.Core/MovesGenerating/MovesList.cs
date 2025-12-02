using Queene.Core.Consts;
using Queene.Core.Models;
using System;
using System.Runtime.CompilerServices;

namespace Queene.Core.MovesGenerating
{
    public class MovesList : IList<Move>
	{
		private Move[] _moves;
		private int _count;

		public int Count => _count;

        public MovesList()
		{
			_count = 0;
			_moves = new Move[BoardConsts.MAX_MOVES_NUMBER];

			for (int i = 0; i < BoardConsts.MAX_MOVES_NUMBER; i++)
				_moves[i] = new Move();
		}

		public void Add(Move move)
		{
			_moves[_count++] = move;
		}

		public void Clear()
		{
			_count = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Move[] Get()
		{
			Span<Move> span = new Span<Move>(_moves);

            return span.Slice(0, _count).ToArray();
		}

        public void RemoveAtIndex(int index)
        {
			_count--;
        }

        public void SetAtIndex(int index, Move value)
        {
        }

        public Move GetAtIndex(int index)
        {
			return _moves[index];
        }
    }
}
