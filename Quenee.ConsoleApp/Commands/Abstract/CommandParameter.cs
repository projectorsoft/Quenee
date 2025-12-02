namespace Quenee.ConsoleApp.Commands.Abstract
{
    public class CommandParameter<T> : CommandParameterBase
    {
        public override string Name { get; protected set; }

        public T Value { get; private set; }

        public bool Required { get; private set; }

        public CommandParameter(string name, T value, bool required)
        {
            Name = name;
            Value = value;
            Required = required;
        }
    }
}
