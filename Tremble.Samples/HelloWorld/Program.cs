using Tremble;

await new TrembleBuilder()
    .WithIdentity("my-bot-name")
    .WithOauth("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
    .OnChannels("join-me", "also-join-me")
    .Build()
    .Run();
