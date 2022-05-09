using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using robotManager.Helpful;
using wManager;
using wManager.Wow.Helpers;

// Token: 0x0200000B RID: 11
public static class Update
{
	// Token: 0x0600003B RID: 59 RVA: 0x00003520 File Offset: 0x00001720
	public static void CheckUpdate()
	{
		try
		{
			string b = "3.1.40";
			string address = "https://github.com/Talamin/AIO-Release/raw/master/AIO.dll";
			string address2 = "https://raw.githubusercontent.com/Talamin/AIO-Release/master/Version.txt";
			string text = new WebClient
			{
				Encoding = Encoding.UTF8
			}.DownloadString(address2);
			bool flag = text == null || text.Length > 10 || text == b;
			if (!flag)
			{
				string path = Others.GetCurrentDirectory + "\\FightClass\\" + wManagerSetting.CurrentSetting.CustomClass;
				byte[] array = new WebClient
				{
					Encoding = Encoding.UTF8
				}.DownloadData(address);
				bool flag2 = array != null && array.Length != 0;
				if (flag2)
				{
					Logging.Write("OnlineVersionContent " + text);
					Logging.Write("New version found, try to update file");
					File.WriteAllBytes(path, array);
					Thread.Sleep(3000);
					new Thread(new ThreadStart(CustomClass.ResetCustomClass)).Start();
				}
			}
		}
		catch (Exception ex)
		{
			string str = "Auto update: ";
			Exception ex2 = ex;
			Logging.WriteError(str + ((ex2 != null) ? ex2.ToString() : null), true);
		}
	}
}
