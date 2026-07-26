
# WelcomeBot


Small Discord bot that drops a custom welcome card image whenever someone joins.

Instead of the usual "draw an image with code" approach, this thing renders an actual HTML page and screenshots it with a headless browser (Puppeteer/Chromium). So the card is just HTML + CSS, which can be easily modified without coding in C#.

## How it works
1. A new member hops in
2. Bot detects the new member's name, avatar, and member count.
3. Injects the values in the HTML template (`pipelines/welcomer/welcome.html`)
4. Renders the final image using a headless browser.
5. The screenshot goes straight to the welcome channel, no temp files, nothing saved to disk

## Why HTML

Most bots draw the card with something like `System.Drawing`, `SkiaSharp` or even some `Python` libraries, so every little tweak means editing code and rebuilding. Doing it in HTML means:

-   restyle with plain CSS, no rebuild needed
-   don't need to touch C# at all to change the look
-   way less effort to redesign later

If you can make a webpage you can make this card look however you want.

## Available themes

[![welcome-test](https://i.imgur.com/QBidaxR.png)](https://i.imgur.com/QBidaxR.png)
[![welcome-tesr](https://i.imgur.com/Z5Vt6XD.png)](https://i.imgur.com/Z5Vt6XD.png)

feel free to pull new themes requests or whatever you like



## Getting started

There's two ways to run this: grab a prebuilt release, or build it from source. Prebuilt is way easier if you're not planning on touching the code.

### Option 1: prebuilt release (no .NET needed)

**1. download**

Grab the zip for your OS from the [releases page](https://github.com/metch1/WelcomeBot/releases)

**2. unzip and set up your env**

Unzip it, open `.env` and fill it in:

```
BOT_TOKEN=your_bot_token_here
WELCOME_CHANNEL_ID=your_channel_id_here
```

**3. permissions**

Invite the bot with at least:

-   View Channel
-   Send Messages
-   Attach Files

**4. run it**

```bash
./WelcomeBot        # linux / mac
WelcomeBot.exe       # windows
```

First run takes a bit longer since it's downloading Chromium in the background, after that it's fast.

### Option 2: build from source

**1. clone it**

```bash
git clone https://github.com/metch1/WelcomeBot.git
cd WelcomeBot
```

**2. requirements**

-   .NET SDK 10 or higher
-   [Bot Token](https://discord.com/developers/applications)
- And A brain OFC

**3. restore & build**

```bash
dotnet restore
dotnet build
```

**4. set up your env**

Rename `.envFAKE` to `.env` and fill it in same as above.

**5. permissions**

Same as Option A, View Channel / Send Messages / Attach Files.

**6. run it**

```bash
dotnet run
```

## Notes

- Single server only for now, no multi guild config yet.
- Browser stays open the whole time the bot runs, so it doesn't choke if a bunch of people join at once
- renders are loaded in ram for speed reasons

## TODO

Since this was a small part of my main bot... I didn't add the multi guild system yet
soon I'll be adding:

- multi-guild support
- public hosting
- slash commands
- more themes
- custom guild themes/requests
