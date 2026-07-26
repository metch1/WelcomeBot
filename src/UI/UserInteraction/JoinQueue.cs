using System.Threading.Channels;

namespace WelcomeBot.src.UI.UserInteraction;

public static class JoinQueue
{
    private static readonly Channel<SocketGuildUser> Queue = Channel.CreateUnbounded<SocketGuildUser>();

    public static void Enqueue(SocketGuildUser user)
    {
        Queue.Writer.TryWrite(user);
    }

    public static void StartWorker()
    {
        _ = Task.Run(async () =>
        {
            while (await Queue.Reader.WaitToReadAsync())
            {
                while (Queue.Reader.TryRead(out var user))
                {
                    try
                    {
                        await WelcomeCard.SendAsync(user);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"join queue choked: {ex}");
                    }
                }
            }
        });
    }
}
