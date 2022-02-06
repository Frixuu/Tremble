namespace Tremble.Chat;

public interface ITwitchChat
{
    void SendMessage(string channel, string message);
}
