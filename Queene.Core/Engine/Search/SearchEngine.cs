using Queene.Core.Models;
using System.Collections.Generic;

namespace Queene.Core.Engine.Search
{
    public class SearchEngine
    {
        public const int Infinity = 999999;
        public const int MaxDepth = 32;

        private readonly Board _board;
        private readonly QuiescenceSearch _quiescenceSearch;
        private readonly int _initialDepth;

        public SearchStats Stats { get; } = new SearchStats();

        public SearchEngine(Board board, int initialDepth)
        {
            _board = board;
            _quiescenceSearch = new QuiescenceSearch(_board);
            _initialDepth = initialDepth;
        }

        public (int, List<ExtendedMove>) Run(int alpha, int beta, int depth)
        {
            var moves = _board.GenerateMoves();

            if (depth == 0)
                return _quiescenceSearch.Run(alpha, beta);

            List<ExtendedMove> pv = [];
            int bestScore = -Infinity;

            for (int i = 0; i < moves.Length; i++)
            {
                var extMove = _board.MakeMove(moves[i]);

                var (currentScore, currentPv) = Run(-beta, -alpha, depth - 1);  

                _board.UnmakeMove(extMove);

                var score = -currentScore;

                if (score > bestScore)
                {
                    bestScore = score;
                    pv = [extMove]; //best move [PV]
                    pv.AddRange(currentPv);
                }

                if (score >= beta)
                    return (beta, pv); //cut off

                if (score > alpha)
                    alpha = score;
            }

            if (moves.Length == 0)
            {
                //shortest path to mate
                if (_board.BoardContext.IsCheck)
                    return (-(Infinity - _initialDepth + depth), []);

                //draw
                return (0, []);
            }


            //Stats.NodesSearched += moves.LongLength;
            // Stats.BestValue = alpha;

            return (alpha, pv);
        }
    }
}
