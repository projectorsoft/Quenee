namespace Quenee.ConsoleApp.Commands.Abstract
{
    public interface ICommand<TRequest, TResponse>
        where TRequest: CommandRequestBase
        where TResponse: CommandResponseBase
    {
        TResponse Execute(TRequest request);
    }
}
