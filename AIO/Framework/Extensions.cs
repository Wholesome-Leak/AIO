using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AIO.Lists;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000045 RID: 69
	public static class Extensions
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public static bool HasDebuffType(this WoWUnit unit, params string[] types)
		{
			return RotationCombatUtil.ExecuteActionOnUnit<bool>(unit, delegate(string luaUnitId)
			{
				string text = (from type in types
				select "(debuffType == \"" + type + "\")").Aggregate((string current, string next) => current + " or " + next);
				string text2 = string.Concat(new string[]
				{
					"\r\n                local hasDebuff = false;\r\n                for i=1,10 do\r\n                    local name, rank, iconTexture, count, debuffType, duration, timeLeft = UnitDebuff(\"",
					luaUnitId,
					"\", i);\r\n                    if (",
					text,
					") then\r\n                        hasDebuff = true;\r\n                        break;\r\n                    end\r\n                end\r\n                return hasDebuff;"
				});
				return Lua.LuaDoString<bool>(text2, "");
			});
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D870 File Offset: 0x0000BA70
		public static bool IsCasting(this WoWUnit unit)
		{
			return RotationCombatUtil.ExecuteActionOnUnit<bool>(unit, delegate(string luaUnitId)
			{
				string text = string.Concat(new string[]
				{
					"return (UnitCastingInfo(\"",
					luaUnitId,
					"\") ~= nil or UnitChannelInfo(\"",
					luaUnitId,
					"\") ~= nil)"
				});
				return Lua.LuaDoString<bool>(text, "");
			});
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public static bool IsCreatureType(this WoWUnit unit, string creatureType)
		{
			return Extensions.CreatureTypeCache.GetOrAdd(unit.Entry, (int k) => RotationCombatUtil.ExecuteActionOnUnit<string>(unit, delegate(string luaUnitId)
			{
				string text = "return UnitCreatureType(\"" + luaUnitId + "\")";
				return Lua.LuaDoString<string>(text, "");
			})) == creatureType;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public static bool HasMana(this WoWUnit unit)
		{
			WoWPlayer woWPlayer = unit as WoWPlayer;
			return (woWPlayer != null && woWPlayer.PowerType == null) || (!(unit is WoWPlayer) && unit.MaxMana > 1U);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D922 File Offset: 0x0000BB22
		public static IEnumerable<Aura> GetMyAuras(this WoWUnit unit)
		{
			return from a in BuffManager.GetAuras(unit.GetBaseAddress)
			where a.Owner == Constants.Me.Guid
			select a;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D954 File Offset: 0x0000BB54
		public static IEnumerable<Aura> GetMyBuffs(this WoWUnit unit, params string[] names)
		{
			return from a in unit.GetMyAuras()
			where names.Any(new Func<string, bool>(a.GetSpell.Name.Equals))
			select a;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D988 File Offset: 0x0000BB88
		public static bool HaveMyBuffStack(this WoWUnit unit, string name, int stack)
		{
			return unit.GetMyBuffs(new string[]
			{
				name
			}).Any((Aura b) => b.Stack == stack);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D9C3 File Offset: 0x0000BBC3
		public static bool HaveMyBuff(this WoWUnit unit, params string[] names)
		{
			return unit.GetMyBuffs(names).Any<Aura>();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D9D1 File Offset: 0x0000BBD1
		public static bool IsEnemy(this WoWUnit unit)
		{
			return unit != null && unit.Reaction <= 3;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		public static bool IsResting(this WoWUnit unit)
		{
			return unit.HaveBuff("Food") || unit.HaveBuff("Drink");
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000DA02 File Offset: 0x0000BC02
		public static bool HaveImportantPoison(this WoWUnit unit)
		{
			return SpecialSpells.ImportantPoison.Any(new Func<string, bool>(unit.HaveBuff));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		public static bool HaveImportantCurse(this WoWUnit unit)
		{
			return SpecialSpells.ImportantCurse.Any(new Func<string, bool>(unit.HaveBuff));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000DA32 File Offset: 0x0000BC32
		public static bool HaveImportantDisease(this WoWUnit unit)
		{
			return SpecialSpells.ImportantDisease.Any(new Func<string, bool>(unit.HaveBuff));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		public static bool HaveImportantMagic(this WoWUnit unit)
		{
			return SpecialSpells.ImportantMagic.Any(new Func<string, bool>(unit.HaveBuff));
		}

		// Token: 0x040001AA RID: 426
		private static readonly ConcurrentDictionary<int, string> CreatureTypeCache = new ConcurrentDictionary<int, string>();
	}
}
