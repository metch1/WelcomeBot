namespace WelcomeBot.src.UI.UserInteraction;

public static class WelcomeCard
{
    private static readonly string HtmlPath = AppPaths.Welcomer;
    private static readonly ulong Channel = ulong.Parse(Env.GetString("WELCOME_CHANNEL_ID"));

    private static string? CachedTemplate;

    public static async Task SendAsync(SocketGuildUser user)
    {
        try
        {
            var channel = user.Guild.GetTextChannel(Channel);
            if (channel == null)
            {
                Console.WriteLine("welcome channel not found, check the env var");
                return;
            }

            byte[] imageBytes = await RenderAsync(
                username: user.DisplayName,
                avatarUrl: user.GetDisplayAvatarUrl(size: 256) ?? user.GetDefaultAvatarUrl(),
                memberCount: $"#{user.Guild.MemberCount:N0}",
                user: user
            );

            using var stream = new MemoryStream(imageBytes);
            await channel.SendFileAsync(stream, "welcome.png", text: $"Welcome to the server, {user.Mention}! 🎉");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"welcome card blew up: {ex}");
        }
    }

    private static async Task<byte[]> RenderAsync(string username, string avatarUrl, string memberCount, IGuildUser user)
    {
        CachedTemplate ??= await File.ReadAllTextAsync(HtmlPath);

        string html = CachedTemplate
            .Replace("{{USERNAME}}", System.Net.WebUtility.HtmlEncode(username))
            .Replace("{{AVATAR_URL}}", avatarUrl)
            .Replace("{{MEMBER_COUNT}}", memberCount)
            .Replace("{{USER_ID}}", user.Id.ToString())
            .Replace("{{SERVER_NAME}}", System.Net.WebUtility.HtmlEncode(user.Guild.Name));

        using var page = await BrowserManager.GetPageAsync();

        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = 1000,
            Height = 400,
            DeviceScaleFactor = 2
        });

        await page.SetContentAsync(html);
        await page.WaitForNetworkIdleAsync();

        return await page.ScreenshotDataAsync(new ScreenshotOptions { OmitBackground = false });
    }
}
