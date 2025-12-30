using Queene.Core.Enums;
using Queene.Core.Models;
using Queene.Core.MovesGenerating;

namespace Queene.Core
{
    public interface IQueeneGame
    {
        BoardContext Context { get; }
        
        void NewGame();
        void SetupPosition(string fen);
        Move[] GenerateMoves(MoveGenerationTypeEnum generationType = MoveGenerationTypeEnum.All);
        ExtendedMove MakeMove(Move move);
        void UnmakeMove(ExtendedMove move);
        string GetFen();
        Board CloneBoard();
    }
}
