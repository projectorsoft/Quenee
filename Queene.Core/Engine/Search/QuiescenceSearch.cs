using Queene.Core.Enums;
using Queene.Core.Models;
using System.Collections.Generic;

namespace Queene.Core.Engine.Search
{
    public class QuiescenceSearch
    {
        private readonly Board _board;

        public QuiescenceSearch(Board board)
        {
            _board = board;
        }

        public (int, List<ExtendedMove>) Run(int alpha, int beta)
        {
            var eval = Evaluation.Evaluation.Evaluate(_board.BoardContext);

            int bestScore = eval;

            if (eval >= beta)
                return (bestScore, []);

            if (eval > alpha)
                alpha = bestScore;

            var moves = _board.GenerateMoves(MoveGenerationTypeEnum.OnlyCaptures);

            List<ExtendedMove> pv = [];

            for (int i = 0; i < moves.Length; i++)
            {
                var extMove = _board.MakeMove(moves[i]);

                var (currentScore, currentPv) = Run(-beta, -alpha);
                var score = -currentScore;

                _board.UnmakeMove(extMove);

                if (score >= beta)
                    return (score, []);

                if (score > bestScore)
                    bestScore = score;

                if (score > alpha)
                {
                    alpha = score;

                    pv = [extMove];
                    pv.AddRange(currentPv);
                }
            }

            return (bestScore, pv);
        }
    }
}
