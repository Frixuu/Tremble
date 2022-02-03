using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tremble.Chat.Commands;
using Tremble.Chat.Commands.Attributes;
using Tremble.Utilities;

namespace Tremble;

public class TrembleBuilder
{
    private readonly IServiceCollection _serviceCollection;

    public TrembleBuilder()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddOptions();
    }

    public ITremble Build()
    {
        var commandTypes = Reflection.FindAllTypesAnnotatedWith<CommandAttribute>();
        commandTypes.ForEach(type => _serviceCollection.TryAddSingleton(type));

        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var executors = commandTypes.ToDictionary(
            type => Reflection.GetAttributeOfType<CommandAttribute>(type)!.Literal,
            type =>
            {
                var command = serviceProvider.GetService(type) as Command;
                return new CommandExecutor(command!) as ICommandExecutor;
            });

        return new Tremble(executors);
    }
}
