namespace Tremble.Chat.Commands;

/// <summary>
/// Provides a convenient abstraction for handling commands.
/// </summary>
public abstract class Command
{
    public abstract void Execute(in CommandInvocation invocation);
}
