using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tremble.Chat.Commands;
using Tremble.Chat.Commands.Attributes;
using Tremble.Utilities;

namespace Tremble;

public class TrembleBuilder
{
    private readonly IServiceCollection _serviceCollection;

    private List<string> _channelsToJoin = new();
    private string? _identity;
    private string? _oauth;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrembleBuilder"/> class.
    /// </summary>
    public TrembleBuilder()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddOptions();
    }

    public TrembleBuilder WithIdentity(string username)
    {
        _identity = username;
        return this;
    }

    public TrembleBuilder WithOauth(string oauth)
    {
        _oauth = oauth;
        return this;
    }

    public TrembleBuilder OnChannels(params string[] channels)
    {
        _channelsToJoin.AddRange(channels);
        return this;
    }

    /// <summary>
    /// Configures Tremble's service colection.
    /// </summary>
    public TrembleBuilder ConfigureServices(Action<IServiceCollection> configure)
    {
        configure.Invoke(_serviceCollection);
        return this;
    }

    public ITremble Build()
    {
        if (_identity == null)
            throw BuilderException.NoUsername;

        if (_oauth == null)
            throw BuilderException.NoOauth;

        var commandTypes = Reflection.FindAllTypesAnnotatedWith<CommandAttribute>();
        commandTypes.ForEach(type => _serviceCollection.TryAddSingleton(type));

        var client = new Tremble(_serviceCollection, commandTypes, _identity, _oauth);
        client.Initialize(_channelsToJoin);
        return client;
    }

    private sealed class BuilderException : ApplicationException
    {
        internal static BuilderException NoUsername { get; } = new("Bot username not provided");
        internal static BuilderException NoOauth { get; } = new("OAuth token not provided");

        private BuilderException(string message) : base(message)
        {
        }
    }
}
