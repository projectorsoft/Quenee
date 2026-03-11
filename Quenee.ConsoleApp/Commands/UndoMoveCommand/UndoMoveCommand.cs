using Microsoft.Extensions.Logging;
using Queene.Core;
using Quenee.ConsoleApp.Commands.Abstract;
using System.Collections.Generic;

namespace Quenee.ConsoleApp.Commands.UndoMoveCommand
{
    public class UndoMoveCommand : ICommand<UndoMoveCommandRequest, UndoMoveCommandResponse>
    {
        public string Name => "Undo";

        public List<CommandParameterBase> Params => [];

        private readonly IQueeneGame _game;
        private readonly ILogger<UndoMoveCommand> _logger;

        public UndoMoveCommand(IQueeneGame game,
            ILogger<UndoMoveCommand> logger)
        {
            _game = game;
            _logger = logger;
        }

        public UndoMoveCommandResponse Execute(UndoMoveCommandRequest request)
        {
            var hash = _game.Context.Hash;

            if (_game.UnmakeMove())
            {
                _logger.Log(LogLevel.Information, $"Move undone \r\n", "aaaa");
                _logger.Log(LogLevel.Information, $"Board fen: {_game.Context.ToString()}");
                _logger.Log(LogLevel.Information, $"Pieces: {_game.Context.PrintPieces()}");
                //_logger.Log(LogLevel.Information, $"Indexes: {_game.Context.PrintPiecesIndicies()} \r\n");
                _logger.Log(LogLevel.Information, $"Board hash: {hash} => {_game.Context.Hash} \r\n \r\n");
            }

            return new UndoMoveCommandResponse();
        }
    }
}
