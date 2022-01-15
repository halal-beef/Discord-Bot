namespace Rocket.Main
{
	public class Program
	{
		private static string Token = ""; //DISCORD BOT TOKEN HERE.

		public static DiscordSocketClient client;
		
		public static ulong ServerGID = ulong.MinValue; //SERVER GUILD ID HERE.

		public static Task Main(string[] args) => new Program().MainAsync();

		public async Task MainAsync()
		{
			//After any key get's pressed log out and close connections.
			Thread stop = new Thread(() => CloseConnections());
			stop.Start();
			var config = new DiscordSocketConfig()
			{
				GatewayIntents = GatewayIntents.All
			};
			client = new DiscordSocketClient();

			//Make Logs!1
			client.Log += Log;
			client.SlashCommandExecuted += SlashCommandHandler;
			client.Ready += () =>
			{
				Console.WriteLine($"Bot Connected! running with {client.Latency}ms. \nLoading Commands...");
				//Add commands on every execution, to lazy to thing of something more efficient lol
				AddCommands().GetAwaiter().GetResult();
				return Task.CompletedTask;
			};

			//Start the Bot!
			await client.LoginAsync(TokenType.Bot, Token);
			await client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}
		private async Task AddCommands()
        {
			var guild = client.GetGuild(ServerGID);

			SlashCommandBuilder guildCm = new();

			guildCm.WithName("hello");
			guildCm.WithDescription("Get a hello from the bot :D");

			SlashCommandBuilder globalCm = new();
			globalCm.WithName("ping");
			globalCm.WithDescription("receive Pong! and the bot's latency!");

            try 
			{
				await guild.CreateApplicationCommandAsync(guildCm.Build());
				await client.CreateGlobalApplicationCommandAsync(globalCm.Build());
			}
            catch (ApplicationCommandException exception)
			{ 
				// If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
				var json = JsonConvert.SerializeObject(exception.Message, Formatting.Indented);

				// You can send this error somewhere or just print it to the console, for this example we're just going to print it.
				Console.WriteLine(json);
			}
		}

        private Task _client_JoinedGuild(SocketGuild arg)
        {
            throw new NotImplementedException();
        }

        private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
		private async Task CloseConnections()
        {
			Console.ReadKey();
			Console.WriteLine("Stopping Client...");
			await client.LogoutAsync();
			await client.StopAsync();
			Console.WriteLine("Client Stopped.");
			Environment.Exit(0);
        }
		private async Task SlashCommandHandler(SocketSlashCommand command)
		{
			//Read the command name and act upon it!
			if (command.Data.Name == "ping")
			{
				await MainCommands.PingBot(command);
			}
			else if (command.Data.Name == "hello")
			{
				await MainCommands.SayHello(command);
			}
		}
	}
}