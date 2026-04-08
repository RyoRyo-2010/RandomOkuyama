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

		string[] okuyamas = new string[5];
		int okuyamaCount = 0;
		for(int i = 0;i < okuyamas.Length;i++)
		{
			okuyamas[i] = Okuyama.GenerateOkuyama(rnd);
			if(okuyamas[i] == "奥山だな")
			{
				okuyamaCount++;
			}
		}
		string message = @$"〈今日の奥山〉
- {okuyamas[0]}
- {okuyamas[1]}
- {okuyamas[2]}
- {okuyamas[3]}
- {okuyamas[4]}
今日は世界豚汁デーなんだな，はー！";
		Console.WriteLine(message);
		if(okuyamaCount == 5)
		{
			Console.WriteLine("奥山が揃った！");
		}

#if DISCORD_POST
		// 投稿処理
		var dj = new DiscordJson(message);
		string jsonMsg = JsonSerializer.Serialize(dj);
		using (var client = new HttpClient())  
		{  
			var content = new StringContent(jsonMsg, Encoding.UTF8, "application/json");  
			var response = client.PostAsync(Secret.discordUrl, content).Result;  
			Console.WriteLine(response);
		}
#endif
	}
}
