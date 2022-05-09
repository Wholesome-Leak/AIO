using System;
using System.Collections.Generic;
using System.Linq;
using robotManager.Helpful;
using wManager.Wow;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Helpers.Caching
{
	// Token: 0x02000030 RID: 48
	public static class Cache
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000B728 File Offset: 0x00009928
		private static object[] GetCUnit(WoWObject unit)
		{
			uint getBaseAddress = unit.GetBaseAddress;
			object[] array;
			bool flag = !Cache.Units.TryGetValue(getBaseAddress, out array);
			if (flag)
			{
				array = new object[12];
				Cache.Units.Add(getBaseAddress, array);
			}
			return array;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B770 File Offset: 0x00009970
		private static T GetProperty<T>(this WoWUnit unit, Entry entry)
		{
			object[] cunit = Cache.GetCUnit(unit);
			bool flag = cunit[(int)entry] != null;
			T t;
			if (flag)
			{
				t = (T)((object)cunit[(int)entry]);
			}
			else
			{
				t = (T)((object)Cache.Access[(int)entry](unit));
				cunit[(int)entry] = t;
			}
			return t;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B7C3 File Offset: 0x000099C3
		public static void Reset()
		{
			Cache.Units.Clear();
			Cache._frameTimeCached = false;
			Cache._inGroupCached = false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public static int CGetFrameTime()
		{
			bool flag = !Cache._frameTimeCached;
			if (flag)
			{
				Cache._frameTime = Usefuls.FrameTime_GetCurTimeMs();
				Cache._frameTimeCached = true;
			}
			return Cache._frameTime;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B815 File Offset: 0x00009A15
		public static uint CManaPercentage(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.ManaPercentage);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B81E File Offset: 0x00009A1E
		public static Aura[] CGetAuras(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.GetAuras);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B827 File Offset: 0x00009A27
		public static bool CHaveBuff(this WoWUnit unit, string spellName)
		{
			return unit.CGetAuraByName(spellName) != null;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B833 File Offset: 0x00009A33
		public static bool CInCombat(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.InCombat);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B83C File Offset: 0x00009A3C
		public static long CHealth(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.Health);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B845 File Offset: 0x00009A45
		public static long CMaxHealth(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.MaxHealth);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B84F File Offset: 0x00009A4F
		public static double CHealthPercent(this WoWUnit unit)
		{
			return (double)unit.CHealth() * 100.0 / (double)unit.CMaxHealth();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B86A File Offset: 0x00009A6A
		public static bool CIsAlive(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.IsAlive);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B873 File Offset: 0x00009A73
		public static float CGetDistance(this WoWUnit unit)
		{
			return ObjectManager.Me.CGetPosition().DistanceTo(unit.CGetPosition());
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B88A File Offset: 0x00009A8A
		public static Vector3 CGetPosition(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.PositionWithoutType);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B893 File Offset: 0x00009A93
		public static uint CRage(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.Rage);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B89C File Offset: 0x00009A9C
		public static string CName(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.Name);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		public static bool CIsTargetingMe(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.IsTargetingMe);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B8AF File Offset: 0x00009AAF
		public static int CCastingSpellId(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.CastingSpellId);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public static bool CIsCast(this WoWUnit unit)
		{
			return unit.CCastingSpellId() > 0;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		public static bool CCanInterruptCasting(this WoWUnit unit)
		{
			return unit.CIsCast() && Convert.ToBoolean((int)(Memory.WowMemory.Memory.ReadByte(unit.GetBaseAddress + 2612U) & 8));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B8F2 File Offset: 0x00009AF2
		public static bool CIsTargetingMeOrMyPetOrPartyMember(this WoWUnit unit)
		{
			return unit.GetProperty(Entry.IsTargetingMeOrMyPetOrPartyMember);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public static bool CHaveMyBuff(this WoWUnit unit, string name)
		{
			List<uint> list = SpellListManager.SpellIdByName(name);
			ulong guid = ObjectManager.Me.Guid;
			foreach (Aura aura in unit.CGetAuras())
			{
				bool flag = aura.Owner == guid && list.Contains(aura.SpellId);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B96C File Offset: 0x00009B6C
		public static int CMyBuffStack(this WoWUnit unit, string name)
		{
			List<uint> list = SpellListManager.SpellIdByName(name);
			ulong guid = ObjectManager.Me.Guid;
			foreach (Aura aura in unit.CGetAuras())
			{
				bool flag = aura.Owner == guid && list.Contains(aura.SpellId);
				if (flag)
				{
					return aura.Stack;
				}
			}
			return 0;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		public static int CBuffTimeLeft(this WoWUnit unit, string name)
		{
			Aura aura = unit.CGetAuraByName(name);
			bool flag = aura == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = aura.TimeEnd - Cache.CGetFrameTime();
				result = ((num > 0) ? num : 0);
			}
			return result;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public static int CMyBuffTimeLeft(this WoWUnit unit, string name)
		{
			List<uint> list = SpellListManager.SpellIdByName(name);
			ulong guid = ObjectManager.Me.Guid;
			foreach (Aura aura in unit.CGetAuras())
			{
				bool flag = aura.Owner == guid && list.Contains(aura.SpellId);
				if (flag)
				{
					int num = aura.TimeEnd - Cache.CGetFrameTime();
					return (num > 0) ? num : 0;
				}
			}
			return 0;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public static Aura CGetAuraByName(this WoWUnit unit, string name)
		{
			List<uint> list = SpellListManager.SpellIdByName(name);
			foreach (Aura aura in unit.CGetAuras())
			{
				bool flag = list.Contains(aura.SpellId);
				if (flag)
				{
					return aura;
				}
			}
			return null;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static int CBuffStack(this WoWUnit unit, string name)
		{
			Aura aura = unit.CGetAuraByName(name);
			return (aura != null) ? aura.Stack : 0;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BB0D File Offset: 0x00009D0D
		public static bool CIsResting(this WoWUnit unit)
		{
			return unit.CHaveBuff("Food") || unit.CHaveBuff("Drink");
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BB2C File Offset: 0x00009D2C
		public static bool CIsInGroup(this WoWLocalPlayer player)
		{
			bool flag = !Cache._inGroupCached;
			if (flag)
			{
				Cache._inGroup = player.IsInGroup;
				Cache._inGroupCached = true;
			}
			return Cache._inGroup;
		}

		// Token: 0x04000140 RID: 320
		private const byte CacheSize = 12;

		// Token: 0x04000141 RID: 321
		private static readonly Dictionary<uint, object[]> Units = new Dictionary<uint, object[]>();

		// Token: 0x04000142 RID: 322
		private static bool _frameTimeCached;

		// Token: 0x04000143 RID: 323
		private static int _frameTime;

		// Token: 0x04000144 RID: 324
		private static bool _inGroup;

		// Token: 0x04000145 RID: 325
		private static bool _inGroupCached;

		// Token: 0x04000146 RID: 326
		private static readonly Func<WoWUnit, object>[] Access = new Func<WoWUnit, object>[]
		{
			(WoWUnit unit) => unit.ManaPercentage,
			(WoWUnit unit) => BuffManager.GetAuras(unit.GetBaseAddress).ToArray<Aura>(),
			(WoWUnit unit) => unit.InCombat,
			(WoWUnit unit) => unit.Health,
			(WoWUnit unit) => unit.IsAlive,
			(WoWUnit unit) => unit.PositionWithoutType,
			(WoWUnit unit) => unit.Rage,
			(WoWUnit unit) => unit.IsTargetingMe,
			(WoWUnit unit) => unit.CastingSpellId,
			(WoWUnit unit) => unit.IsTargetingMeOrMyPetOrPartyMember,
			(WoWUnit unit) => unit.MaxHealth,
			(WoWUnit unit) => unit.Name
		};
	}
}
