namespace WelcomeBot.src.UI.UserInteraction;

public static class BrowserManager
{
    public static IBrowser? Instance { get; private set; }

    public static async Task InitAsync()
    {
        var fetcher = new BrowserFetcher();
        await fetcher.DownloadAsync();

        Instance = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = ["--no-sandbox"]
        });

        Console.WriteLine("browser's up, ready to render cards");
    }

    public static async Task<IPage> GetPageAsync()
    {
        if (Instance is null)
            throw new Exception("browser isn't running yet");

        return await Instance.NewPageAsync();
    }
}
