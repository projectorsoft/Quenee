using Queene.Core;
using Quenee.ConsoleApp.Commands.Abstract;
using System.Collections.Generic;

namespace Quenee.ConsoleApp.Commands.SetPositionCommand
{
    public class SetPositionCommand : ICommand<SetPositionCommandRequest, SetPositionCommandResponse>
    {
        private readonly IQueeneGame _game;

        public string Name => "position";

        public List<CommandParameterBase> Params => [ new CommandParameter<string>(nameof(SetPositionCommandRequest.Fen), 0) ];

        public SetPositionCommand(IQueeneGame game)
        {
            _game = game;
        }

        public SetPositionCommandResponse Execute(SetPositionCommandRequest request)
        {
            _game.SetupPosition(request.Fen);

            return new SetPositionCommandResponse();
        }
    }
}
