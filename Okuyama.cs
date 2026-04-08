using System.Text;

public class Okuyama
{
    private struct OkuyamaChar
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
    /// <summary>
    /// 文字一覧
    /// 1/2の確率で抽選して，成功ならtrueIndexへ，失敗ならfalseIndexへ飛ぶ
    /// -1で終了
    /// </summary>
    private static OkuyamaChar[] okuyamas = 
		{
			new OkuyamaChar('奥',1,2),
			new OkuyamaChar('山',2,0),
			new OkuyamaChar('だ',3,2),
			new OkuyamaChar('な',-1,0)
		};
    /// <summary>
    /// 豚汁のかけらを作る
    /// </summary>
    /// <param name="rnd">グローバルに使うRandom</param>
    /// <returns>豚汁のかけら</returns>
	public static string GenerateOkuyama(Random rnd)
	{
		int index = 0;
		var sb = new StringBuilder(15);
		while(true)
		{
			bool rndIsTrue = rnd.Next(0,256) < 128;
			sb.Append(okuyamas[index].c);
			index = rndIsTrue ? okuyamas[index].trueIndex : okuyamas[index].falseIndex;
			if(index == -1)
			{
				break;
			}
		}
		return sb.ToString();
	}
}
