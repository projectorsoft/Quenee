using Microsoft.Extensions.DependencyInjection;
using Quenee.ConsoleApp.Commands.Abstract;
using Quenee.ConsoleApp.Commands.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Quenee.ConsoleApp.Commands
{
    public class CommandParser
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandParser(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Execute(string commandLine)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commandLine))
                    throw new CommandParserException(new ArgumentException("Invalid command provided"));

                var args = SplitCommand(commandLine.Trim(' '));

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
                        var commandNameProperty = type.GetProperties().FirstOrDefault(x => x.Name == nameof(ICommand<,>.Name));

                        //check command name matches
                        if (!commandNameProperty.GetValue(command).ToString().Equals(args[0], StringComparison.InvariantCultureIgnoreCase))
                            continue;

                        var requestType = iface.GenericTypeArguments[0];
                        var requestProperties = requestType.GetProperties().ToList();
                        var requestInstance = Activator.CreateInstance(requestType);

                        var commandParamsProperty = type.GetProperties().FirstOrDefault(x => x.Name == nameof(ICommand<,>.Params));
                        var commandParameters = commandParamsProperty.GetValue(command) as List<CommandParameterBase>;

                        var index = 1;

                        foreach (var param in commandParameters.OrderBy(x => x.Order))
                        {
                            var requestProperty = requestProperties
                                .Where(x => x.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase))
                                .FirstOrDefault();

                            if (param.IsRequired && requestProperty == null)
                                throw new CommandParserException(new ArgumentException($"Parameter {param.Name} is required"));

                            if (param.IsRequired && index > args.Length - 1)
                                throw new CommandParserException(new ArgumentException($"Parameter {param.Name} is required"));

                            if (index > args.Length - 1)
                                break;

                            switch (requestProperty.PropertyType.Name)
                            {
                                case "Int32":
                                    if (Int32.TryParse(args[index++], out var number))
                                        requestProperty.SetValue(requestInstance, number);
                                    else
                                        throw new CommandParserException(new ArgumentException($"Invalid property {requestProperty.Name}"));

                                    break;
                                case "String":
                                    requestProperty.SetValue(requestInstance, args[index++].Trim('\''));
                                    break;
                            }
                        }

                        var executeMethos = toCreate.GetMethod(nameof(ICommand<,>.Execute), [requestType]);
                        executeMethos.Invoke(command, [requestInstance]);

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CommandParserException(ex);
            }
        }

        private static string[] SplitCommand(string commandLine)
        {
            string pattern = @"'((?:\\'|[^'])*)'|(\S+)";
            var matches = Regex.Matches(commandLine, pattern);

            var tokens = new List<string>();

            foreach (Match m in matches)
            {
                if (m.Groups[1].Success)
                {
                    string inside = m.Groups[1].Value;

                    inside = inside.Replace("\\'", "'");
                    tokens.Add(inside);
                }
                else
                    tokens.Add(m.Groups[2].Value);
            }

            return tokens.ToArray();
        }
    }
}
