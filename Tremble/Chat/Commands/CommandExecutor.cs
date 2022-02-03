namespace Tremble.Chat.Commands;

/// <summary>
/// Provides a convenient wrapper for basic command handling.
/// </summary>
public class CommandExecutor : ICommandExecutor
{
    private readonly Command _command;

    public CommandExecutor(Command command)
    {
        _command = command;
    }

    public void Execute(in CommandInvocation invocation)
    {
        _command.Execute(in invocation);
    }
}
