using System.Text;
using System.Text.Json;

class DiscordJson
{
	public string content {get;set;}

	public DiscordJson(string c)
	{
		this.content = c;
	}
}

class Program
{
	public static void Main(string[] args)
	{
		Random rnd = new Random();

		string message = @$"〈今日の奥山〉
- {GenerateOkuyama(rnd)}
- {GenerateOkuyama(rnd)}
- {GenerateOkuyama(rnd)}
- {GenerateOkuyama(rnd)}
- {GenerateOkuyama(rnd)}
今日は世界豚汁デーなんだな，はー！";
		Console.WriteLine(message);

		// 投稿処理
		var dj = new DiscordJson(message);
		string jsonMsg = JsonSerializer.Serialize(dj);
		using (var client = new HttpClient())  
		{  
			var content = new StringContent(jsonMsg, Encoding.UTF8, "application/json");  
			var response = client.PostAsync(Secret.discordUrl, content).Result;  
			Console.WriteLine(response);
		}
	}
}
