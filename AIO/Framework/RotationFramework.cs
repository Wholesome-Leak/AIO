using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AIO.Combat.Common;
using AIO.Helpers.Caching;
using robotManager.Events;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x0200003F RID: 63
	public class RotationFramework : ICycleable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000307 RID: 775 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		// (remove) Token: 0x06000308 RID: 776 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler OnCacheUpdated;

		// Token: 0x06000309 RID: 777 RVA: 0x0000CA27 File Offset: 0x0000AC27
		public static void Setup(bool framelock = true, int losCreditsPlayers = 5, int losCreditsNPCs = 10, int scanRange = 50)
		{
			RotationFramework.UseFramelock = framelock;
			RotationFramework.ScanRange = scanRange;
			RotationFramework.LoSCreditsPlayers = losCreditsPlayers;
			RotationFramework.LoSCreditsNPCs = losCreditsNPCs;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public void Initialize()
		{
			ObjectManagerEvents.OnObjectManagerPulsed += new SimpleHandler(this.OnObjectManagerPulsed);
			EventsLuaWithArgs.OnEventsLuaStringWithArgs += new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.UpdatePartyMembers);
			EventsLua.AttachEventLua("RAID_ROSTER_UPDATE", delegate(object _)
			{
				LuaCache.UpdateRaidGroups();
			});
			LuaCache.UpdateRaidGroups();
			this.UpdatePartyMembers("INSTANCE_BOOT_START", null);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000CAB3 File Offset: 0x0000ACB3
		public void Dispose()
		{
			ObjectManagerEvents.OnObjectManagerPulsed -= new SimpleHandler(this.OnObjectManagerPulsed);
			EventsLuaWithArgs.OnEventsLuaStringWithArgs -= new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.UpdatePartyMembers);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000CADC File Offset: 0x0000ACDC
		private void OnObjectManagerPulsed()
		{
			bool cacheDirectTransmission = RotationFramework.CacheDirectTransmission;
			if (cacheDirectTransmission)
			{
				RotationFramework.Run(new Action(RotationFramework.UpdateCache), false);
			}
			else
			{
				this.UpdateTimer.RunAdaptive(delegate
				{
					RotationFramework.Run(new Action(RotationFramework.UpdateCache), false);
				}, this.UpdateCacheMaxDelay, 8);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000CB40 File Offset: 0x0000AD40
		private void UpdatePartyMembers(string name, List<string> args)
		{
			bool flag = name != "INSTANCE_BOOT_START";
			if (!flag)
			{
				string text = Lua.LuaDoString<string>("\r\n                            for i = 1, 4 do \r\n                                local isTank,_,_ = UnitGroupRolesAssigned('party' .. i)\r\n                                if isTank then                                    \r\n                                    return UnitName('party' .. i);\r\n                                end\r\n                            end", "").Split(new string[]
				{
					"#||#"
				}, StringSplitOptions.None).FirstOrDefault<string>();
				bool flag2 = text != RotationFramework.TankName;
				if (flag2)
				{
					Logging.Write("Tank name: " + text);
					RotationFramework.TankName = text;
				}
				string text2 = Lua.LuaDoString<string>("\r\n                            for i = 1, 4 do \r\n                                local _,isHeal,_ = UnitGroupRolesAssigned('party' .. i)\r\n                                if isHeal then                                    \r\n                                    return UnitName('party' .. i);\r\n                                end\r\n                            end", "").Split(new string[]
				{
					"#||#"
				}, StringSplitOptions.None).FirstOrDefault<string>();
				bool flag3 = text2 != RotationFramework.HealName;
				if (flag3)
				{
					Logging.Write("Heal name: " + text2);
					RotationFramework.HealName = text2;
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		private static void UpdateCache()
		{
			List<WoWUnit> objectWoWUnit = ObjectManager.GetObjectWoWUnit();
			List<WoWPlayer> objectWoWPlayer = ObjectManager.GetObjectWoWPlayer();
			Vector3 myPosition = Constants.Me.PositionWithoutType;
			int num = RotationFramework.LoSCreditsPlayers;
			int num2 = RotationFramework.LoSCreditsNPCs;
			List<WoWUnit> list = new List<WoWUnit>(objectWoWUnit.Count + objectWoWPlayer.Count);
			Func<WoWUnit, float> <>9__0;
			for (int i = 0; i < objectWoWUnit.Count; i++)
			{
				WoWUnit woWUnit = objectWoWUnit[i];
				bool flag = RotationFramework.ScanRange != 0 && myPosition.DistanceTo(woWUnit.PositionWithoutType) > (float)RotationFramework.ScanRange;
				if (!flag)
				{
					bool flag2 = num2 > 0;
					if (flag2)
					{
						num2--;
						bool flag3 = TraceLine.TraceLineGo(woWUnit.Position);
						if (flag3)
						{
							goto IL_D8;
						}
					}
					List<WoWUnit> list2 = list;
					WoWUnit item = woWUnit;
					Func<WoWUnit, float> pred;
					if ((pred = <>9__0) == null)
					{
						pred = (<>9__0 = ((WoWUnit u) => myPosition.DistanceTo(u.PositionWithoutType)));
					}
					list2.AddSorted(item, pred);
				}
				IL_D8:;
			}
			List<WoWPlayer> list3 = new List<WoWPlayer>(objectWoWPlayer.Count + 1)
			{
				Constants.Me
			};
			Func<WoWUnit, float> <>9__1;
			Func<WoWPlayer, float> <>9__2;
			for (int j = 0; j < objectWoWPlayer.Count; j++)
			{
				WoWPlayer woWPlayer = objectWoWPlayer[j];
				bool flag4 = RotationFramework.ScanRange != 0 && myPosition.DistanceTo(woWPlayer.PositionWithoutType) > (float)RotationFramework.ScanRange;
				if (!flag4)
				{
					bool flag5 = num > 0;
					if (flag5)
					{
						num--;
						bool flag6 = TraceLine.TraceLineGo(woWPlayer.Position);
						if (flag6)
						{
							goto IL_1C8;
						}
					}
					List<WoWUnit> list4 = list;
					WoWUnit item2 = woWPlayer;
					Func<WoWUnit, float> pred2;
					if ((pred2 = <>9__1) == null)
					{
						pred2 = (<>9__1 = ((WoWUnit p) => myPosition.DistanceTo(p.PositionWithoutType)));
					}
					list4.AddSorted(item2, pred2);
					List<WoWPlayer> list5 = list3;
					WoWPlayer item3 = woWPlayer;
					Func<WoWPlayer, float> pred3;
					if ((pred3 = <>9__2) == null)
					{
						pred3 = (<>9__2 = ((WoWPlayer p) => myPosition.DistanceTo(p.PositionWithoutType)));
					}
					list5.AddSorted(item3, pred3);
				}
				IL_1C8:;
			}
			List<WoWUnit> list6 = new List<WoWUnit>(list.Count);
			Func<WoWUnit, float> <>9__3;
			for (int k = 0; k < list.Count; k++)
			{
				WoWUnit woWUnit2 = list[k];
				bool flag7 = !woWUnit2.IsEnemy() || (!RotationFramework.UseSynthetic && !woWUnit2.IsAttackable);
				if (!flag7)
				{
					List<WoWUnit> list7 = list6;
					WoWUnit item4 = woWUnit2;
					Func<WoWUnit, float> pred4;
					if ((pred4 = <>9__3) == null)
					{
						pred4 = (<>9__3 = ((WoWUnit u) => myPosition.DistanceTo(u.PositionWithoutType)));
					}
					list7.AddSorted(item4, pred4);
				}
			}
			List<ulong> partyGUIDHomeAndInstance = Party.GetPartyGUIDHomeAndInstance();
			List<WoWPlayer> list8 = new List<WoWPlayer>(list3.Count);
			for (int l = 0; l < list3.Count; l++)
			{
				WoWPlayer woWPlayer2 = list3[l];
				bool flag8 = !partyGUIDHomeAndInstance.Contains(woWPlayer2.Guid) && !woWPlayer2.IsLocalPlayer;
				if (!flag8)
				{
					list8.AddSorted(woWPlayer2, (WoWPlayer p) => p.HealthPercent);
				}
			}
			RotationFramework.PlayerUnits = list3.ToArray();
			RotationFramework.Enemies = list6.ToArray();
			RotationFramework.AllUnits = list.ToArray();
			RotationFramework.PartyMembers = list8.ToArray();
			bool cacheDirectTransmission = RotationFramework.CacheDirectTransmission;
			if (cacheDirectTransmission)
			{
				EventHandler onCacheUpdated = RotationFramework.OnCacheUpdated;
				if (onCacheUpdated != null)
				{
					onCacheUpdated(null, EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000CF68 File Offset: 0x0000B168
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000CF6F File Offset: 0x0000B16F
		public static string HealName { get; private set; } = "";

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000CF77 File Offset: 0x0000B177
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000CF7E File Offset: 0x0000B17E
		public static string TankName { get; private set; } = "";

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000CF86 File Offset: 0x0000B186
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000CF8D File Offset: 0x0000B18D
		public static WoWUnit[] AllUnits { get; private set; } = new WoWUnit[0];

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000CF95 File Offset: 0x0000B195
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000CF9C File Offset: 0x0000B19C
		public static WoWUnit[] Enemies { get; private set; } = new WoWUnit[0];

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000CFAB File Offset: 0x0000B1AB
		public static WoWPlayer[] PlayerUnits { get; private set; } = new WoWPlayer[0];

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000CFBA File Offset: 0x0000B1BA
		public static WoWPlayer[] PartyMembers { get; private set; } = new WoWPlayer[0];

		// Token: 0x0600031B RID: 795 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		public static void RunRotation(string caller, List<RotationStep> rotation, bool alreadyLocked = false)
		{
			int globalCooldown = RotationFramework.GetGlobalCooldown();
			bool gcdEnabled = globalCooldown != 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			RotationFramework.Run(delegate
			{
				RotationFramework.RunRotation(rotation, gcdEnabled);
			}, alreadyLocked);
			stopwatch.Stop();
			bool flag = stopwatch.ElapsedMilliseconds > 64L;
			if (flag)
			{
				Logging.WriteDebug(string.Format("[{0}] Iteration took {1} ms", caller, stopwatch.ElapsedMilliseconds));
				RotationLogger.Debug(string.Format("[{0}] Iteration took {1} ms", caller, stopwatch.ElapsedMilliseconds));
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D058 File Offset: 0x0000B258
		private static void Run(Action action, bool alreadyLocked = false)
		{
			if (alreadyLocked)
			{
				action();
			}
			else
			{
				bool useFramelock = RotationFramework.UseFramelock;
				if (useFramelock)
				{
					RotationFramework.RunInFrameLock(action);
				}
				else
				{
					RotationFramework.RunInLock(action);
				}
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D094 File Offset: 0x0000B294
		private static void RunInLock(Action action)
		{
			object locker = ObjectManager.Locker;
			lock (locker)
			{
				action();
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		private static void RunInFrameLock(Action action)
		{
			object lockFrameLocker = Memory.WowMemory.LockFrameLocker;
			lock (lockFrameLocker)
			{
				try
				{
					Memory.WowMemory.LockFrame();
					action();
				}
				finally
				{
					Memory.WowMemory.UnlockFrame(false);
				}
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000D150 File Offset: 0x0000B350
		private static void PrintStats()
		{
			foreach (KeyValuePair<ushort, List<long>> keyValuePair in from stat in RotationFramework.Stats
			where stat.Value.Max() >= 1L
			select stat)
			{
				Logging.Write(string.Concat(new string[]
				{
					string.Format("--- Step {0} ---\n", (int)(keyValuePair.Key + 1)),
					string.Format("Average: {0}ms\n", Math.Round(keyValuePair.Value.Average(), 2)),
					string.Format("Min: {0}ms\n", keyValuePair.Value.Min()),
					string.Format("Max: {0}ms\n", keyValuePair.Value.Max()),
					string.Format("Count: {0} ticks", keyValuePair.Value.Count)
				}));
			}
			string[] array = new string[5];
			array[0] = "Where step ";
			array[1] = string.Format("{0} ", (int)((from stat in RotationFramework.Stats
			orderby stat.Value.Average() descending
			select stat).First<KeyValuePair<ushort, List<long>>>().Key + 1));
			array[2] = "has the highest average and step ";
			array[3] = string.Format("{0} ", (int)((from stat in RotationFramework.Stats
			orderby stat.Value.Max() descending
			select stat).First<KeyValuePair<ushort, List<long>>>().Key + 1));
			array[4] = "has the highest maximum.";
			Logging.Write(string.Concat(array));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D32C File Offset: 0x0000B52C
		private static void RunRotation(IReadOnlyList<RotationStep> rotation, bool gcdEnabled)
		{
			Exclusives exclusives = new Exclusives();
			ushort num = 0;
			while ((int)num < rotation.Count)
			{
				RotationStep rotationStep = rotation[(int)num];
				try
				{
					bool flag = rotationStep.Execute(gcdEnabled, exclusives);
					if (flag)
					{
						break;
					}
				}
				finally
				{
				}
				num += 1;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D388 File Offset: 0x0000B588
		public static int GetGlobalCooldown()
		{
			foreach (RotationFramework.SpellCooldown spellCooldown in RotationFramework.SpellCooldownTimeLeft())
			{
				bool flag = spellCooldown.Duration == 1500;
				if (flag)
				{
					return spellCooldown.TimeLeft;
				}
			}
			return 0;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public static bool SpellReady(uint spellid)
		{
			return RotationFramework.SpellCooldownTimeLeft(spellid) <= 0;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D414 File Offset: 0x0000B614
		public static int SpellCooldownTimeLeft(uint spellid)
		{
			foreach (RotationFramework.SpellCooldown spellCooldown in RotationFramework.SpellCooldownTimeLeft())
			{
				bool flag = spellCooldown.SpellId == spellid;
				if (flag)
				{
					return spellCooldown.TimeLeft;
				}
			}
			return 0;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D47C File Offset: 0x0000B67C
		public static IEnumerable<RotationFramework.SpellCooldown> SpellCooldownTimeLeft()
		{
			int now = Memory.WowMemory.Memory.ReadInt32(13465260U);
			uint currentListObject = Memory.WowMemory.Memory.ReadPtr(13890996U);
			while (currentListObject != 0U && (currentListObject & 1U) == 0U)
			{
				uint currentSpellId = Memory.WowMemory.Memory.ReadPtr(currentListObject + 8U);
				int start = Memory.WowMemory.Memory.ReadInt32(currentListObject + 16U);
				int cd = Memory.WowMemory.Memory.ReadInt32(currentListObject + 20U);
				int cd2 = Memory.WowMemory.Memory.ReadInt32(currentListObject + 32U);
				int length = cd + cd2;
				int globalLength = Memory.WowMemory.Memory.ReadInt32(currentListObject + 44U);
				int cdleft = Math.Max(Math.Max(length, globalLength) - (now - start), 0);
				bool flag = cdleft > 0;
				if (flag)
				{
					yield return new RotationFramework.SpellCooldown(currentSpellId, cdleft, start, Math.Max(length, globalLength));
				}
				currentListObject = Memory.WowMemory.Memory.ReadPtr(currentListObject + 4U);
			}
			yield break;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D488 File Offset: 0x0000B688
		public static float GetItemCooldown(string itemName)
		{
			string text = "\r\n\t        for bag=0,4 do\r\n\t            for slot=1,36 do\r\n\t                local name = GetContainerItemLink(bag,slot);\r\n\t                if (name and name == \"" + itemName + "\") then\r\n\t                    local start, duration, enabled = GetContainerItemCooldown(bag, slot);\r\n\t                    if enabled then\r\n\t                        return (duration - (GetTime() - start)) * 1000;\r\n\t                    end\r\n\t                end;\r\n\t            end;\r\n\t        end\r\n\t        return 0;";
			return Lua.LuaDoString<float>(text, "");
		}

		// Token: 0x0400017C RID: 380
		public static bool CacheDirectTransmission = false;

		// Token: 0x0400017D RID: 381
		public static bool UseSynthetic = false;

		// Token: 0x0400017E RID: 382
		private static bool UseFramelock = true;

		// Token: 0x0400017F RID: 383
		private static int ScanRange = 50;

		// Token: 0x04000180 RID: 384
		private static int LoSCreditsPlayers = 5;

		// Token: 0x04000181 RID: 385
		private static int LoSCreditsNPCs = 10;

		// Token: 0x04000182 RID: 386
		private readonly TimeSpan UpdateCacheMaxDelay = new TimeSpan(0, 0, 3);

		// Token: 0x04000183 RID: 387
		private readonly Timer UpdateTimer = new Timer();

		// Token: 0x0400018A RID: 394
		private static Dictionary<ushort, List<long>> Stats = new Dictionary<ushort, List<long>>();

		// Token: 0x0400018B RID: 395
		private static ushort _ticks = 0;

		// Token: 0x02000040 RID: 64
		public struct SpellCooldown
		{
			// Token: 0x06000328 RID: 808 RVA: 0x0000D55B File Offset: 0x0000B75B
			public SpellCooldown(uint spellId, int timeLeft, int startTime, int duration)
			{
				this.SpellId = spellId;
				this.TimeLeft = timeLeft;
				this.StartTime = startTime;
				this.Duration = duration;
			}

			// Token: 0x0400018C RID: 396
			public uint SpellId;

			// Token: 0x0400018D RID: 397
			public int TimeLeft;

			// Token: 0x0400018E RID: 398
			public int StartTime;

			// Token: 0x0400018F RID: 399
			public int Duration;
		}
	}
}
