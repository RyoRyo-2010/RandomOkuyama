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

struct OkuyamaChar
{
	public char c;
	public int trueIndex;
	public int falseIndex;

	public OkuyamaChar(char c, int trueIndex, int falseIndex)
	{
		this.c = c;
		this.trueIndex = trueIndex;
		this.falseIndex = falseIndex;
	}
}

class Program
{
	static OkuyamaChar[] okuyamas = 
		{
			new OkuyamaChar('奥',1,2),
			new OkuyamaChar('山',2,0),
			new OkuyamaChar('だ',3,2),
			new OkuyamaChar('な',-1,0)
		};
	static string GenerateOkuyama(Random rnd)
	{
		int index = 0;
		var sb = new StringBuilder(15);
		while(true)
		{
			bool rndIsTrue = (rnd.Next(0,256) < 128);
			sb.Append(okuyamas[index].c);
			index = (rndIsTrue ? okuyamas[index].trueIndex : okuyamas[index].falseIndex);
			if(index == -1)
			{
				break;
			}
		}
		return sb.ToString();
	}

	public static void Main(string[] args)
	{
		Random rnd = new Random();

		string[] okuyamas = new string[5];
		for(int i = 0;i < 5;i++)
		{
			okuyamas[i] = GenerateOkuyama(rnd);
		}
		string message = @$"〈今日の奥山〉
- {okuyamas[0]}
- {okuyamas[1]}
- {okuyamas[2]}
- {okuyamas[3]}
- {okuyamas[4]}
今日は世界豚汁デーなんだな，はー！";
		Console.WriteLine(message);
		var dj = new DiscordJson(message);
		string jsonMsg = JsonSerializer.Serialize(dj);
		using (var client = new HttpClient())  
		{  
			var content = new StringContent(jsonMsg, Encoding.UTF8, "application/json");  
			var response = client.PostAsync(Secret.discordUrl, content).Result;  
		}
	}
}
