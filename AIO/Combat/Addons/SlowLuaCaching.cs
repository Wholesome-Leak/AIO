using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers.Caching;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Addons
{
	// Token: 0x0200010F RID: 271
	public class SlowLuaCaching : ICycleable
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x00027374 File Offset: 0x00025574
		private static void Update()
		{
			List<WoWPlayer> list = new List<WoWPlayer>
			{
				ObjectManager.Me
			};
			list.AddRange(Party.GetParty());
			list.AddRange(Party.GetRaidMembers());
			list = (from player in list
			where player.Name.Length > 0
			select player).ToList<WoWPlayer>();
			bool flag = list.Count <= 0 || !Conditions.InGameAndConnected;
			if (!flag)
			{
				string arg = "{" + string.Join(",", from player in list
				select "'" + player.Name + "'") + "}";
				List<int> list2 = Lua.LuaDoString<List<int>>(string.Format("\r\n            players = {0}\r\n            threats = {{}}\r\n            for i = 1, {1} do\r\n                threat = UnitThreatSituation(players[i])\r\n                if threat == nil then threat = 0 end\r\n                table.insert(threats, threat)\r\n            end\r\n            return unpack(threats)", arg, list.Count), "");
				bool flag2 = list2.Count != list.Count;
				if (flag2)
				{
					Logging.WriteError("Mismatch in SlowLua threat function. If this is not being spammed, it can safely be ignored.", true);
				}
				else
				{
					object lockThreat = LuaCache.LockThreat;
					lock (lockThreat)
					{
						LuaCache.UnitThreatSituations.Clear();
						for (int i = 0; i < list.Count; i++)
						{
							try
							{
								LuaCache.UnitThreatSituations.Add(list[i].GetBaseAddress, list2[i]);
							}
							catch (ArgumentException)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002750C File Offset: 0x0002570C
		public static void UpdateThreatSituations()
		{
			List<WoWPlayer> list = (from player in RotationFramework.PartyMembers
			where player.Name.Length > 0
			select player).ToList<WoWPlayer>();
			string arg = "{" + string.Join(",", from player in list
			select "'" + player.Name + "'") + "}";
			List<int> list2 = Lua.LuaDoString<List<int>>(string.Format("\r\n            players = {0}\r\n            threats = {{}}\r\n            for i = 1, {1} do\r\n                threat = UnitThreatSituation(players[i])\r\n                if threat == nil then threat = 0 end\r\n                table.insert(threats, threat)\r\n            end\r\n            return unpack(threats)", arg, list.Count), "");
			bool flag = list2.Count != list.Count;
			if (flag)
			{
				Logging.WriteError("Mismatch in SlowLua threat function. If this is not being spammed, it can safely be ignored.", true);
			}
			else
			{
				object lockThreat = LuaCache.LockThreat;
				lock (lockThreat)
				{
					LuaCache.UnitThreatSituations.Clear();
					for (int i = 0; i < list.Count; i++)
					{
						Logging.Write("Adding " + list[i].Name);
						LuaCache.UnitThreatSituations.Add(list[i].GetBaseAddress, list2[i]);
					}
				}
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002766C File Offset: 0x0002586C
		public void Initialize()
		{
			Task.Factory.StartNew(delegate()
			{
				while (!this.CancellationTokenSource.Token.IsCancellationRequested)
				{
					Logging.WriteDebug("Starting SlowLuaCaching Thread!");
					Task task = Task.Factory.StartNew(delegate()
					{
						while (!this.CancellationTokenSource.Token.IsCancellationRequested)
						{
							try
							{
								SlowLuaCaching.Update();
							}
							catch (Exception ex)
							{
								Logging.WriteError("Something went wrong while updating the Lua Cache\n" + ex.Message, true);
							}
							Thread.Sleep(10);
						}
					}, this.CancellationTokenSource.Token);
					while (task.Status != TaskStatus.RanToCompletion)
					{
						Thread.Sleep(10);
					}
					Logging.WriteDebug("SlowLuaCaching has ran to completion.");
					task.Dispose();
				}
			}, this.CancellationTokenSource.Token);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00027691 File Offset: 0x00025891
		public void Dispose()
		{
			this.CancellationTokenSource.Cancel();
		}

		// Token: 0x04000599 RID: 1433
		private readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
	}
}
