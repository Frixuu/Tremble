using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tremble.Chat.Commands;

namespace Tremble;

internal class Tremble : ITremble
{
    private IDictionary<string, ICommandExecutor> _executors;

    internal Tremble(IDictionary<string, ICommandExecutor> executors)
    {
        _executors = executors;
    }

    internal void Initialize()
    {
    }

    /// <summary>
    /// Runs the bot application.
    /// </summary>
    public async Task Run()
    {
        await Task.Run(() => Task.Delay(TimeSpan.FromSeconds(3)));
    }
}
