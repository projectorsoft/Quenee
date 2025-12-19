using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quenee.ConsoleApp.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Quenee.ConsoleApp.Commands.HelpCommand
{
    public class HelpCommand : ICommand<HelpCommandRequest, SetPositionCommandResponse>
    {
        public string Name => "help";

        public List<CommandParameterBase> Params => [];

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HelpCommand> _logger;

        public HelpCommand(IServiceProvider serviceProvider,
            ILogger<HelpCommand> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public SetPositionCommandResponse Execute(HelpCommandRequest request)
        {
            var interfaceType = typeof(ICommand<,>);

            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.IsPublic && !x.IsAbstract);

            foreach (var type in types)
            {
                var iface = type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);

                if (iface == null)
                    continue;

                Type toCreate = type;

                var command = ActivatorUtilities.CreateInstance(_serviceProvider, toCreate);

                if (command != null)
                {
                    var commandNameProperty = type.GetProperties().FirstOrDefault(x => x.Name == nameof(ICommand<,>.Name)).GetValue(command).ToString();
                    var commandParamsProperty = type.GetProperties().FirstOrDefault(x => x.Name == nameof(ICommand<,>.Params));
                    var commandParameters = commandParamsProperty.GetValue(command) as List<CommandParameterBase>;

                    var parameters = "";

                    foreach (var param in commandParameters.OrderBy(x => x.Order))
                        parameters += $"-{param.Name} ";

                    _logger.Log(LogLevel.Information, $"{commandNameProperty} {parameters}");
                }
            }

            return new SetPositionCommandResponse();
        }
    }
}
