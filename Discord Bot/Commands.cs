namespace Dottik.Bot.Commands
{
    internal class MainCommands
    {
        /// <summary>
        /// Send Ping! + Bot Latency!
        /// </summary>
        /// <param name="command">Command Context.</param>
        /// <returns>Nothing :D</returns>
        public static async Task PingBot(SocketSlashCommand command)
        {
            //Respond as a message!
            await command.RespondAsync($"Pong! {Program.client.Latency}");
        }
        /// <summary>
        /// Send a Hello!
        /// </summary>
        /// <param name="command">Command Context.</param>
        /// <returns>Nothing :D</returns>
        public static async Task SayHello(SocketSlashCommand command)
        {
            Random rand = new();
            EmbedBuilder embed = new();
            //Embed title!
            embed.WithTitle("Hello!");
            embed.AddField("Dottik Note: ", "How is it going man!", false);

            await command.RespondAsync(embed: embed.Build());
        }
        
        public static async Task Meme(SocketSlashCommand command)
        {
              var Result = JObject.Parse(
                JArray.Parse(

                    new HttpClient()

                    .GetStringAsync("https://reddit.com/r/memes/random.json?limit=1").GetAwaiter().GetResult()

                    )[0]["data"]["children"][0]["data"]
                    .ToString()
                
                );
            EmbedBuilder embed = new();
            //Embed title!
            embed.WithTitle(Result["title"].ToString());
            embed.WithImageUrl(Result["url"].ToString());
            embed.AddField("Dottik Note: ", "This Meme Is From r/memes", false);

            await command.RespondAsync(embed: embed.Build());
        }
    }
}
