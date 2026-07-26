namespace WelcomeBot.src;

public partial class Bot
{
    private Task OnUserJoinedAsync(SocketGuildUser user)
    {
        JoinQueue.Enqueue(user);
        return Task.CompletedTask;
    }

    private Task OnReadyAsync()
    {
        Ready = true;
        Console.WriteLine("bot's up");
        return Task.CompletedTask;
    }

}
