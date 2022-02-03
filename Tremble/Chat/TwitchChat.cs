namespace Tremble.Chat;

public class TwitchChat
{
    private readonly Tremble _tremble;

    internal TwitchChat(Tremble tremble)
    {
        _tremble = tremble;
    }

    public void SendMessage(string where, string message)
    {
        _tremble!._twitchChatClient!.SendMessage(where, message);
    }
}
