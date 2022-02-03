using System;

namespace Tremble.Chat.Commands.Attributes;

/// <summary>
/// Marks a command for runtime discovery by Tremble.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CommandAttribute : Attribute
{
    /// <summary>
    /// The literal value the user has to provide to trigger this command.
    /// </summary>
    public string Literal { get; }

    public CommandAttribute(string literal)
    {
        Literal = literal;
    }
}
