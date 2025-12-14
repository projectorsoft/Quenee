using System.Collections.Generic;

namespace Quenee.ConsoleApp.Commands.Abstract
{
    public interface ICommand<TRequest, TResponse>
        where TRequest: CommandRequestBase
        where TResponse: CommandResponseBase
    {
        string Name { get; }
        List<CommandParameterBase> Params { get; }

        TResponse Execute(TRequest request);
    }
}
