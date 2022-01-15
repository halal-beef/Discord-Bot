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
    }
}
