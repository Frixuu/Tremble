namespace Tremble.Chat.Commands;

/// <summary>
/// Handles incoming chat commands.
/// </summary>
public interface ICommandExecutor
{
    void Execute(in CommandInvocation invocation);
}
