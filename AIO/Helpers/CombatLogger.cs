using System;
using System.Collections.Generic;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;

namespace AIO.Helpers
{
	// Token: 0x0200002A RID: 42
	public static class CombatLogger
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000AB68 File Offset: 0x00008D68
		public static void ParseCombatLog(string eventId, List<string> args)
		{
			bool flag = !eventId.Equals("COMBAT_LOG_EVENT_UNFILTERED") || args.Count < 5 || Convert.ToUInt64(args[2], 16) != ObjectManager.Me.Guid;
			if (!flag)
			{
				string text = args[1];
				string a = text;
				if (a == "SPELL_HEAL" || a == "SPELL_PERIODIC_HEAL")
				{
					CombatLogger.LogData(Convert.ToUInt32(args[8]), Convert.ToInt32(args[11]));
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000ABF9 File Offset: 0x00008DF9
		public static double GetAverage(this Spell spell)
		{
			return CombatLogger.GetAverage(spell.Id);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AC06 File Offset: 0x00008E06
		public static double GetAverage(uint spellId)
		{
			return CombatLogger.GetStatisticEntry(spellId).Average;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AC13 File Offset: 0x00008E13
		public static StatisticEntry GetStatisticEntry(this Spell spell)
		{
			return CombatLogger.GetStatisticEntry(spell.Id);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AC20 File Offset: 0x00008E20
		public static StatisticEntry GetStatisticEntry(uint spellId)
		{
			object locker = CombatLogger.Locker;
			StatisticEntry result;
			lock (locker)
			{
				StatisticEntry statisticEntry;
				result = (CombatLogger._statistics.TryGetValue(spellId, out statisticEntry) ? statisticEntry : null);
			}
			return result;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AC74 File Offset: 0x00008E74
		public static Dictionary<uint, StatisticEntry> GetDictionary()
		{
			object locker = CombatLogger.Locker;
			Dictionary<uint, StatisticEntry> statistics;
			lock (locker)
			{
				statistics = CombatLogger._statistics;
			}
			return statistics;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public static void ForceAverage(uint spellId, int average)
		{
			object locker = CombatLogger.Locker;
			lock (locker)
			{
				StatisticEntry statisticEntry;
				bool flag2 = !CombatLogger._statistics.TryGetValue(spellId, out statisticEntry);
				if (flag2)
				{
					CombatLogger._statistics.Add(spellId, new StatisticEntry(average));
				}
				else
				{
					CombatLogger._statistics[spellId] = new StatisticEntry(average);
				}
				CombatLogger._statistics[spellId].Add(average);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AD44 File Offset: 0x00008F44
		private static void LogData(uint spellId, int amount)
		{
			object locker = CombatLogger.Locker;
			lock (locker)
			{
				StatisticEntry statisticEntry;
				bool flag2 = !CombatLogger._statistics.TryGetValue(spellId, out statisticEntry);
				if (flag2)
				{
					CombatLogger._statistics.Add(spellId, new StatisticEntry(amount));
				}
				else
				{
					statisticEntry.Add(amount);
				}
			}
		}

		// Token: 0x0400012A RID: 298
		private const byte Type = 1;

		// Token: 0x0400012B RID: 299
		private const byte ActiveGuid = 2;

		// Token: 0x0400012C RID: 300
		private const byte SpellId = 8;

		// Token: 0x0400012D RID: 301
		private const byte AmountHealed = 11;

		// Token: 0x0400012E RID: 302
		private static readonly object Locker = new object();

		// Token: 0x0400012F RID: 303
		private static readonly Dictionary<uint, StatisticEntry> _statistics = new Dictionary<uint, StatisticEntry>();
	}
}
