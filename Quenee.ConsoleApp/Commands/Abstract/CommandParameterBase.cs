namespace Quenee.ConsoleApp.Commands.Abstract
{
    public abstract class CommandParameterBase
    {
        public abstract string Name { get; protected set; }

        public int Order { get; protected set; }

        public bool IsRequired { get; protected set; }
    }
}
