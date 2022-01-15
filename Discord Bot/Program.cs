namespace Rocket.Main
{
	public class Program
	{
		private static string Token = ""; //DISCORD BOT TOKEN HERE.
		public static DiscordSocketClient _client;

		public static Task Main(string[] args) => new Program().MainAsync();

		public async Task MainAsync()
		{
			//Make Logs!1
			_client.Log += Log;

			//Start the Bot!
			await _client.LoginAsync(TokenType.Bot, Token);
			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}
		
		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}