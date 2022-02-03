using System;
using System.Collections.Immutable;
using System.Linq;

namespace Tremble.Utilities;

/// <summary>
/// Provides various utility methods for dealing with reflection in .NET.
/// </summary>
public static class Reflection
{
    /// <summary>
    /// Finds all types in current domain that have a specified attribute attached to them.
    /// </summary>
    public static ImmutableList<Type> FindAllTypesAnnotatedWith<T>()
        => FindAllTypesAnnotatedWith(typeof(T));

    /// <summary>
    /// Finds all types in current domain that have a specified attribute attached to them.
    /// </summary>
    /// <param name="attributeType">Type of the wanted attribute.</param>
    public static ImmutableList<Type> FindAllTypesAnnotatedWith(Type attributeType)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.CustomAttributes.Any(d => d.AttributeType == attributeType))
            .ToImmutableList();
    }

    /// <summary>
    /// Gets attribute from a type, if one exists.
    /// </summary>
    /// <param name="elementType">Type of the element queried for attributes.</param>
    /// <typeparam name="T">The type of the wanted attribute.</typeparam>
    public static T? GetAttributeOfType<T>(Type elementType)
        where T : Attribute
    {
        return Attribute.GetCustomAttribute(elementType, typeof(T)) as T;
    }
}
