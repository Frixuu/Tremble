using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Tremble.Utilities;

public static class Reflection
{
    public static ImmutableList<Type> FindAllTypesAnnotatedWith(Type attributeType)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.CustomAttributes.Any(d => d.AttributeType == attributeType))
            .ToImmutableList();
    }
}
