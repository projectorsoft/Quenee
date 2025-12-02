using Quenee.ConsoleApp.Commands.Abstract;
using System;

namespace Quenee.ConsoleApp.Commands.PerftCommand
{
    public class PerftCommand : ICommand<PerftCommandRequest, PerftCommandResponse>
    {
        public PerftCommandResponse Execute(PerftCommandRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
