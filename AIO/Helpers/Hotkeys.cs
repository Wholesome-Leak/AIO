using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Helpers
{
	// Token: 0x0200002C RID: 44
	public static class Hotkeys
	{
		// Token: 0x0600027C RID: 636
		[DllImport("user32.dll")]
		private static extern ushort GetAsyncKeyState(int vKey);

		// Token: 0x0600027D RID: 637 RVA: 0x0000AE2C File Offset: 0x0000902C
		public static void CheckKeyPress()
		{
			bool flag = (Hotkeys.GetAsyncKeyState(97) & 1) > 0;
			if (flag)
			{
				Hotkeys.ForceNewTank();
			}
			bool flag2 = (Hotkeys.GetAsyncKeyState(98) & 1) > 0;
			if (flag2)
			{
				Hotkeys.LogCurrentData();
			}
			bool flag3 = (Hotkeys.GetAsyncKeyState(99) & 1) > 0;
			if (flag3)
			{
				Hotkeys.ForceStatisticalData();
			}
			bool flag4 = (Hotkeys.GetAsyncKeyState(100) & 1) > 0;
			if (flag4)
			{
				Hotkeys.ToggleRangeCircles();
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AE94 File Offset: 0x00009094
		private static void PrintCircles()
		{
			Vector3 positionWithoutType = ObjectManager.Me.PositionWithoutType;
			Radar3D.DrawCircle(positionWithoutType, 40f, Color.Yellow, false, 24);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AEC4 File Offset: 0x000090C4
		private static void ToggleRangeCircles()
		{
			bool flag = !Hotkeys._rangeCircleOn;
			if (flag)
			{
				Logging.Write("Turning Range Circles on.");
				Radar3D.OnDrawEvent += new Radar3D.OnDrawHandler(Hotkeys.PrintCircles);
				Radar3D.Pulse();
				Hotkeys._rangeCircleOn = true;
			}
			else
			{
				Hotkeys.DisableRangeCircles();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AF14 File Offset: 0x00009114
		public static void DisableRangeCircles()
		{
			bool rangeCircleOn = Hotkeys._rangeCircleOn;
			if (rangeCircleOn)
			{
				Logging.Write("Turning Range Circles off.");
				Hotkeys._rangeCircleOn = false;
				Radar3D.OnDrawEvent -= new Radar3D.OnDrawHandler(Hotkeys.PrintCircles);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AF50 File Offset: 0x00009150
		private static void ForceStatisticalData()
		{
			Logging.Write("Forcing statistical data.");
			string text = string.Concat(new string[]
			{
				"Settings/Hotswitch-",
				ObjectManager.Me.Name,
				"-",
				Usefuls.RealmName,
				".txt"
			});
			bool flag = !File.Exists(text);
			if (flag)
			{
				Logging.Write("Creating new hotswitch file at " + text);
				File.Create(text);
			}
			foreach (string text2 in File.ReadLines(text))
			{
				string[] array = text2.Split(new char[]
				{
					'='
				});
				bool flag2 = array.Length != 2;
				if (!flag2)
				{
					Spell spell = new Spell(array[0], false);
					bool flag3 = spell.Id <= 0U;
					if (!flag3)
					{
						try
						{
							int average = Convert.ToInt32(array[1]);
							Logging.Write(text2);
							CombatLogger.ForceAverage(spell.Id, average);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B088 File Offset: 0x00009288
		private static void LogCurrentData()
		{
			Logging.Write("Current custom tank: < " + BasePersistentSettings<PriestLevelSettings>.Current.HolyCustomTank + " >");
			Logging.Write(" ### Logging statistical values ### ");
			foreach (KeyValuePair<uint, StatisticEntry> keyValuePair in CombatLogger.GetDictionary())
			{
				Logging.Write(string.Format("{0}: {1} ({2})", new Spell(keyValuePair.Key).Name, keyValuePair.Value.Average, keyValuePair.Value.Count));
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B148 File Offset: 0x00009348
		private static void ForceNewTank()
		{
			WoWUnit targetObject = ObjectManager.Me.TargetObject;
			string text = ((targetObject != null) ? targetObject.Name : null) ?? "";
			BasePersistentSettings<PriestLevelSettings>.Current.HolyCustomTank = text;
			Logging.Write("[Hotkey] Set custom tank to: < " + text + " >");
		}

		// Token: 0x04000133 RID: 307
		private static bool _rangeCircleOn;
	}
}
