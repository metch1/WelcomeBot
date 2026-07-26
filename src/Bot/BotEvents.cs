namespace WelcomeBot.src;

public partial class Bot
{
    private void RegisterEvents()
    {
        Client?.UserJoined += OnUserJoinedAsync;
        Client?.Ready += OnReadyAsync;
    }
}
