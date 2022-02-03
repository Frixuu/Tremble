namespace Tremble.Chat.Commands;

/// <summary>
/// Provides
/// </summary>
public abstract class Command
{
    public abstract void Execute(in CommandInvocation invocation);
}
