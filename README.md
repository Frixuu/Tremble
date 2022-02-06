# Tremble

![Nuget](https://img.shields.io/nuget/v/Tremble) ![GitHub](https://img.shields.io/github/license/Frixuu/Tremble)

A high-level .NET 6 framework for creating your own private Twitch chatbots.

## Quickstart

After you create a Twitch.tv account for your bot and generate the OAuth token, create a new .NET console app:

```csharp
using Tremble;

await new TrembleBuilder()
    .WithIdentity("my-bot-name")
    .WithOauth("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
    .OnChannels("join-me", "also-join-me")
    .Build()
    .Run();
```

The commands you create are automatically picked up by Tremble.
For example, here's how to make a command that will answer to whoever's calling ```!hello```:

```csharp
using Tremble.Chat;
using Tremble.Chat.Commands;
using Tremble.Chat.Commands.Attributes;

namespace HelloWorld;

[Command("hello")]
public class HelloCommand : Command
{
    private readonly ITwitchChat _twitchChat;

    // Important modules will get injected through constructor
    public HelloCommand(ITwitchChat twitchChat)
    {
        _twitchChat = twitchChat;
    }

    public override void Execute(in CommandInvocation invocation)
    {
        _twitchChat.SendMessage(invocation.ChannelName, $"Hello, {invocation.Sender.Name}!");
    }
}
```
