using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tremble.Chat.Commands;
using Tremble.Utilities;

namespace Tremble;

/// <summary>
/// A helper for creating new Tremble apps.
/// </summary>
public class TrembleBuilder
{
    private readonly IServiceCollection _serviceCollection;

    private readonly List<string> _channelsToJoin = new();
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

    /// <summary>
    /// Describes which channels should the bot connect to on startup.
    /// </summary>
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

    /// <summary>
    /// This method checks whether a Tremble instance can be built.
    /// If not, throws an appropriate exception.
    /// </summary>
    private void CheckStateIntegrity()
    {
        if (_identity == null)
            throw BuilderException.NoUsername;

        if (_oauth == null)
            throw BuilderException.NoOauth;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Tremble"/> bot application.
    /// </summary>
    public ITremble Build()
    {
        CheckStateIntegrity();

        var commandTypes = Reflections.FindTypesAnnotatedWith<CommandAttribute>();
        foreach (var commandType in commandTypes)
        {
            _serviceCollection.TryAddSingleton(commandType);
        }

        var client = new Tremble(_serviceCollection, commandTypes, _identity!, _oauth!);
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
