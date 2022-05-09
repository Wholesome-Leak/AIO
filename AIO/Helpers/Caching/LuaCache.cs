using System;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Helpers.Caching
{
	// Token: 0x02000033 RID: 51
	public static class LuaCache
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000BD14 File Offset: 0x00009F14
		public static int GetCachedThreatSituation(this WoWUnit unit)
		{
			object lockThreat = LuaCache.LockThreat;
			int result;
			lock (lockThreat)
			{
				int num;
				result = (LuaCache.UnitThreatSituations.TryGetValue(unit.GetBaseAddress, out num) ? num : -1);
			}
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public static void UpdateRaidGroups()
		{
			List<string> source = Lua.LuaDoString<List<string>>("\r\n                outTable = {}\r\n                for i=1, GetNumRaidMembers() do\r\n                    name, _, subgroup, _, _, _, _, _, _, _, _ = GetRaidRosterInfo(i)\r\n                    outTable[i] = name .. \"=\" .. subgroup\r\n                end\r\n                return unpack(outTable)", "");
			object lockGroup = LuaCache.LockGroup;
			lock (lockGroup)
			{
				LuaCache.RaidGroups.Clear();
				foreach (string[] array in from luaGroup in source
				select luaGroup.Split(new char[]
				{
					'='
				}) into split
				where split.Length == 2
				select split)
				{
					LuaCache.RaidGroups.Add(array[0], Convert.ToByte(array[1]));
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public static byte GetRaidGroup(this WoWPlayer unit)
		{
			return LuaCache.GetRaidGroup(unit.Name);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BE69 File Offset: 0x0000A069
		public static byte CGetRaidGroup(this WoWPlayer unit)
		{
			return LuaCache.GetRaidGroup(unit.CName());
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BE78 File Offset: 0x0000A078
		public static byte GetRaidGroup(string name)
		{
			object lockGroup = LuaCache.LockGroup;
			byte result;
			lock (lockGroup)
			{
				byte b;
				result = (LuaCache.RaidGroups.TryGetValue(name, out b) ? b : 0);
			}
			return result;
		}

		// Token: 0x04000155 RID: 341
		public static readonly object LockThreat = new object();

		// Token: 0x04000156 RID: 342
		private static readonly object LockGroup = new object();

		// Token: 0x04000157 RID: 343
		public static readonly Dictionary<uint, int> UnitThreatSituations = new Dictionary<uint, int>();

		// Token: 0x04000158 RID: 344
		private static readonly Dictionary<string, byte> RaidGroups = new Dictionary<string, byte>();
	}
}
