using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Tremble.Chat.Commands;

namespace Tremble;

public class TrembleBuilder
{
    
    public ITremble Build()
    {
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.CustomAttributes.Any(d => d.AttributeType == typeof(CommandAttribute)));

        return new Tremble();
    }
}
