namespace WelcomeBot.src;

public static class AppPaths
{
    public static readonly string Home = Directory.GetCurrentDirectory();
    public static readonly string Welcomer = Path.Combine(Home, "pipelines/welcomer/welcome.html");
}
