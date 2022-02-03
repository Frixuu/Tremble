using System;

namespace Tremble.Chat.Commands;

public readonly ref struct CommandInvocation
{
    public string ChannelName { get; init; }

    public User Sender { get; init; }

    public ReadOnlySpan<char> Message { get; init; }
}
