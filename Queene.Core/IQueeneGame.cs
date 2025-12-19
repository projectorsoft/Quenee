using Queene.Core.Enums;
using Queene.Core.Models;

namespace Queene.Core
{
    public interface IQueeneGame
    {
        void NewGame();
        void SetupPosition(string fen);
        Move[] GenerateMoves(MoveGenerationTypeEnum generationType = MoveGenerationTypeEnum.All);
        ExtendedMove MakeMove(Move move);
        void UnmakeMove(ExtendedMove move);
        string GetFen();
        Board CloneBoard();
    }
}
