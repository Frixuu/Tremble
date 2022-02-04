using System;
using System.Collections.Immutable;
using System.Linq;

namespace Tremble.Utilities;

/// <summary>
/// Provides various utility methods for dealing with reflection in .NET.
/// </summary>
public static class Reflections
{
    /// <summary>
    /// Finds all types in current domain that have a specified attribute attached to them.
    /// </summary>
    /// <typeparam name="T">Type of the desired attribute.</typeparam>
    /// <returns>Immutable set of all found types.</returns>
    public static IImmutableSet<Type> FindTypesAnnotatedWith<T>()
        where T : Attribute
    {
        return FindTypesAnnotatedWith(typeof(T));
    }

    /// <summary>
    /// Finds all types in current domain that have a specified attribute attached to them.
    /// </summary>
    /// <param name="attributeType">Type of the wanted attribute.</param>
    /// <returns>Immutable set of all found types.</returns>
    public static IImmutableSet<Type> FindTypesAnnotatedWith(Type attributeType)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.CustomAttributes.Any(d => d.AttributeType == attributeType))
            .ToImmutableHashSet();
    }

    /// <summary>
    /// Gets attribute from a type, if one exists.
    /// </summary>
    /// <param name="elementType">Type of the element queried for attributes.</param>
    /// <typeparam name="T">The type of the requested attribute.</typeparam>
    /// <returns>Requested attribute object, if type has an attribute attached.</returns>
    public static T? GetAttributeOfType<T>(Type elementType)
        where T : Attribute
    {
        return Attribute.GetCustomAttribute(elementType, typeof(T)) as T;
    }
}
