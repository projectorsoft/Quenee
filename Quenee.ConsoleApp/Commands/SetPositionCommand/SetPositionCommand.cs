using Queene.Core;
using Quenee.ConsoleApp.Commands.Abstract;
using System.Collections.Generic;

namespace Quenee.ConsoleApp.Commands.SetPositionCommand
{
    public class SetPositionCommand : ICommand<SetPositionCommandRequest, SetPositionCommandResponse>
    {
        private readonly Board _board;

        public string Name => "position";

        public List<CommandParameterBase> Params => [ new CommandParameter<string>(nameof(SetPositionCommandRequest.Fen), 0) ];

        public SetPositionCommand(Board board)
        {
            _board = board;
        }

        public SetPositionCommandResponse Execute(SetPositionCommandRequest request)
        {
            _board.NewGame(request.Fen);

            return new SetPositionCommandResponse();
        }
    }
}
