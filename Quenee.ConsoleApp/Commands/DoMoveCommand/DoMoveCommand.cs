using Microsoft.Extensions.Logging;
using Queene.Core;
using Queene.Core.Consts;
using Quenee.ConsoleApp.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Quenee.ConsoleApp.Commands.DoMoveCommand
{
    public class DoMoveCommand : ICommand<DoMoveCommandRequest, DoMoveCommandResponse>
    {
        public string Name => "Move";

        public List<CommandParameterBase> Params => [
            new CommandParameter<string>(nameof(DoMoveCommandRequest.From), 0),
            new CommandParameter<string>(nameof(DoMoveCommandRequest.To), 1)
        ];

        private readonly IQueeneGame _game;
        private readonly ILogger<DoMoveCommand> _logger;

        public DoMoveCommand(IQueeneGame game,
            ILogger<DoMoveCommand> logger)
        {
            _game = game;
            _logger = logger;
        }

        public DoMoveCommandResponse Execute(DoMoveCommandRequest request)
        {
            var from = BoardConsts.SQUARES_NAMES.IndexOf(request.From.ToUpper(CultureInfo.InvariantCulture));
            var to = BoardConsts.SQUARES_NAMES.IndexOf(request.To.ToUpper(CultureInfo.InvariantCulture));
            var hash = _game.Context.Hash;

            var moves = _game.GenerateMoves();
            var move = moves.FirstOrDefault(m => m.GetFromSquare() == from && m.GetToSquare() == to);

            if (!move.IsNullMove())
            {
                var madeMove = _game.MakeMove(move);

                if (madeMove != null)
                {
                    _logger.Log(LogLevel.Information, $"Move done \r\n");
                    _logger.Log(LogLevel.Information, $"Fen: {_game.Context.ToString()}");
                    _logger.Log(LogLevel.Information, $"Pieces: {_game.Context.PrintPieces()}");
                    //_logger.Log(LogLevel.Information, $"Indexes: {_game.Context.PrintPiecesIndicies()} \r\n");
                    _logger.Log(LogLevel.Information, $"Hash: {hash} => {_game.Context.Hash} \r\n \r\n");
                }
            }

            return new DoMoveCommandResponse();
        }
    }
}
