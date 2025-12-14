namespace Quenee.ConsoleApp.Commands.Abstract
{
    public class CommandParameter<T> : CommandParameterBase
    {
        public override string Name { get; protected set; }

        public CommandParameter(string name, int order, bool required = true)
        {
            Name = name;
            Order = order;
            IsRequired = required;
        }
    }
}
