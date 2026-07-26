namespace WelcomeBot.src;

public partial class Bot
{
    private DiscordSocketClient? Client;
    public bool Ready { get; set; }
    public async Task RunAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers,
            AlwaysDownloadUsers = false,
            LogGatewayIntentWarnings = false
        };
        Client = new DiscordSocketClient(config);
        Env.Load();

        var token = Env.GetString("BOT_TOKEN");
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("missing token");

        await BrowserManager.InitAsync();

        JoinQueue.StartWorker();

        RegisterEvents();
        await Client.LoginAsync(TokenType.Bot, token);
        await Client.StartAsync();

        await Task.Delay(-1);
    }
}
