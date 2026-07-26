namespace WelcomeBot;

internal abstract class Program
{
    private static async Task Main()
    {
        var bot = new Bot();
        await bot.RunAsync();
    }
}
