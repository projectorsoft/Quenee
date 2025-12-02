using System.Collections.Generic;

namespace Quenee.ConsoleApp.Commands.Abstract
{
    public abstract class CommandRequestBase
    {
        public abstract string Name { get; }

        protected readonly List<CommandParameterBase> _params;

        public CommandRequestBase()
        {
            _params = new List<CommandParameterBase>();
        }
    }
}
