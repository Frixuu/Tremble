using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tremble.Chat;
using Tremble.Chat.Commands;
using Tremble.Chat.Commands.Attributes;
using Tremble.Utilities;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Tremble;

internal class Tremble : ITremble
{
    private readonly IServiceCollection _serviceCollection;
    private IServiceProvider _serviceProvider;

    private readonly IList<Type> _commandTypes;
    private Dictionary<string, ICommandExecutor> _executors = new();
    internal ITwitchClient? _twitchChatClient;
    private string _username;
    private string _oauth;

    internal Tremble
    (
        IServiceCollection serviceCollection,
        IList<Type> commandTypes,
        string username,
        string oauth
    )
    {
        _serviceCollection = serviceCollection;
        _commandTypes = commandTypes;
        _username = username;
        _oauth = oauth;
    }

    internal void Initialize(List<string> channels)
    {
        _serviceCollection.TryAddSingleton(new TwitchChat(this));
        _serviceCollection.TryAddSingleton<TwitchChat>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();

        var credentials = new ConnectionCredentials(_username, _oauth);
        var clientOptions = new ClientOptions()
        {
            MessagesAllowedInPeriod = 750,
            ThrottlingPeriod = TimeSpan.FromSeconds(30),
        };

        _twitchChatClient = new TwitchClient(new WebSocketClient(clientOptions));
        _twitchChatClient.Initialize(credentials, channels);

        _executors = _commandTypes.ToDictionary(
            type => Reflection.GetAttributeOfType<CommandAttribute>(type)!.Literal.ToLowerInvariant(),
            type =>
            {
                var command = _serviceProvider.GetService(type) as Command;
                return new CommandExecutor(command!) as ICommandExecutor;
            });

        _twitchChatClient.OnChatCommandReceived += (sender, args) =>
        {
            var trigger = args.Command.CommandText.ToLowerInvariant();
            if (_executors.TryGetValue(trigger, out var executor))
            {
                var message = args.Command.ChatMessage;
                var invocation = new CommandInvocation
                {
                    Message = message.Message.AsSpan(),
                    ChannelName = message.Channel,
                    Sender = new User
                    {
                        Id = Convert.ToInt64(message.UserId),
                        Name = message.DisplayName,
                    },
                };
                executor.Execute(in invocation);
            }
        };
    }

    /// <summary>
    /// Runs the bot application.
    /// </summary>
    public async Task Run()
    {
        Task.Run(() => _twitchChatClient?.Connect()).ConfigureAwait(false).GetAwaiter();
        while (true)
        {
            await Task.Delay(500);
            var input = Console.ReadLine();
            if (input == ":q")
            {
                _twitchChatClient?.Disconnect();
                break;
            }
        }
    }
}
