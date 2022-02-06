using TwitchLib.Client.Interfaces;

namespace Tremble.Chat;

internal class TwitchChat : ITwitchChat
{
    private readonly ITwitchClient _innerChatClient;

    internal TwitchChat(ITwitchClient innerClient)
    {
        _innerChatClient = innerClient;
    }

    public void SendMessage(string channel, string message)
    {
        _innerChatClient.SendMessage(channel, message);
    }
}
