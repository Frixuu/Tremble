using Tremble.Chat;
using Tremble.Chat.Commands;
using Tremble.Chat.Commands.Attributes;

namespace HelloWorld;

[Command("hello")]
public class HelloCommand : Command
{
    private readonly TwitchChat _twitchChat;

    public HelloCommand(TwitchChat twitchChat)
    {
        _twitchChat = twitchChat;
    }

    public override void Execute(in CommandInvocation invocation)
    {
        _twitchChat.SendMessage(invocation.ChannelName, $"Hello, {invocation.Sender.Name}!");
    }
}
