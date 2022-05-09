using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AIO;
using AIO.Helpers;
using AIO.Helpers.Caching;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x0200000E RID: 14
internal static class Extension
{
	// Token: 0x06000043 RID: 67 RVA: 0x00006858 File Offset: 0x00004A58
	public static void RunAdaptive(this Timer timer, Action action, TimeSpan maxDuration, int factor = 8)
	{
		bool flag = !timer.IsReady;
		if (!flag)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				action();
			}
			finally
			{
				stopwatch.Stop();
				double num = stopwatch.Elapsed.TotalMilliseconds * (double)factor;
				bool flag2 = num > maxDuration.TotalMilliseconds;
				if (flag2)
				{
					num = maxDuration.TotalMilliseconds;
				}
				timer.Reset(num);
			}
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000068D8 File Offset: 0x00004AD8
	public static long CGetDamage(this WoWUnit unit)
	{
		return unit.CMaxHealth() - unit.CHealth();
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000068E8 File Offset: 0x00004AE8
	public static bool CShouldHeal(this WoWUnit unit, Spell spell, double percentage, double factor = 1.0)
	{
		StatisticEntry statisticEntry = spell.GetStatisticEntry();
		bool flag = statisticEntry == null || statisticEntry.Count < 2 || statisticEntry.Average < 1.0;
		bool result;
		if (flag)
		{
			result = (unit.CHealthPercent() < percentage);
		}
		else
		{
			bool flag2 = unit.CHealthPercent() < percentage * 0.5;
			result = (flag2 || (double)unit.CGetDamage() > statisticEntry.Average * factor);
		}
		return result;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00006960 File Offset: 0x00004B60
	public static bool ContainsAtLeast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, int amount)
	{
		int num = 0;
		foreach (TSource arg in source)
		{
			bool flag = predicate(arg);
			if (flag)
			{
				num++;
			}
			bool flag2 = num >= amount;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000069D0 File Offset: 0x00004BD0
	public static bool HasPoisonDebuff()
	{
		return Lua.LuaDoString<bool>("for i=1,25 do \r\n\t            local _, _, _, _, d  = UnitDebuff('player',i);\r\n\t            if d == 'Poison' then\r\n                return true\r\n                end\r\n            end", "");
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000069F4 File Offset: 0x00004BF4
	public static bool HasDiseaseDebuff()
	{
		return Lua.LuaDoString<bool>("for i=1,25 do \r\n\t            local _, _, _, _, d  = UnitDebuff('player',i);\r\n\t            if d == 'Disease' then\r\n                return true\r\n                end\r\n            end", "");
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00006A18 File Offset: 0x00004C18
	public static int GetItemQuantity(string itemName)
	{
		string text = "local itemCount = 0; for b=0,4 do if GetBagName(b) then for s=1, GetContainerNumSlots(b) do local itemLink = GetContainerItemLink(b, s) if itemLink then local _, stackCount = GetContainerItemInfo(b, s)\t if string.find(itemLink, \"" + itemName + "\") then itemCount = itemCount + stackCount; end end end end end; return itemCount; ";
		return Lua.LuaDoString<int>(text, "");
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00006A48 File Offset: 0x00004C48
	public static string GetSpec()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 1; i <= 3; i++)
		{
			dictionary.Add(Lua.LuaDoString<string>(string.Format("local name, iconTexture, pointsSpent = GetTalentTabInfo({0}); return name", i), ""), Lua.LuaDoString<int>(string.Format("local name, iconTexture, pointsSpent = GetTalentTabInfo({0}); return pointsSpent", i), ""));
		}
		int highestTalents = dictionary.Max((KeyValuePair<string, int> x) => x.Value);
		return dictionary.FirstOrDefault((KeyValuePair<string, int> t) => t.Value == highestTalents).Key.Replace(" ", "");
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00006B10 File Offset: 0x00004D10
	public static void UseFirstMatchingItem(List<string> list)
	{
		List<WoWItem> bagItem = Bag.GetBagItem();
		foreach (WoWItem woWItem in bagItem)
		{
			bool flag = list.Contains(woWItem.Name) && Extension.GetItemCooldown(woWItem.Name) <= 0;
			if (flag)
			{
				ItemsManager.UseItemByNameOrId(woWItem.Name);
				Main.Log("Using " + woWItem.Name);
				break;
			}
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00006BB0 File Offset: 0x00004DB0
	public static void AddSorted<T, TN>(this List<T> list, T item, Func<T, TN> pred) where TN : IComparable<TN>
	{
		bool flag = list.Count == 0;
		if (flag)
		{
			list.Add(item);
		}
		else
		{
			TN tn = pred(list[list.Count - 1]);
			bool flag2 = tn.CompareTo(pred(item)) <= 0;
			if (flag2)
			{
				list.Add(item);
			}
			else
			{
				tn = pred(list[0]);
				bool flag3 = tn.CompareTo(pred(item)) >= 0;
				if (flag3)
				{
					list.Insert(0, item);
				}
				else
				{
					int num = list.BinarySearch(item, new FuncComp<T, TN>(pred));
					bool flag4 = num < 0;
					if (flag4)
					{
						num = ~num;
					}
					list.Insert(num, item);
				}
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00006C78 File Offset: 0x00004E78
	public static bool HaveOneInList(List<string> list)
	{
		List<WoWItem> bagItem = Bag.GetBagItem();
		return bagItem.Any((WoWItem item) => list.Contains(item.Name));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00006CB0 File Offset: 0x00004EB0
	public static int GetItemCooldown(string itemName)
	{
		int entry = Extension.GetItemID(itemName);
		List<WoWItem> bagItem = Bag.GetBagItem();
		bool flag = bagItem.Any((WoWItem item) => entry == item.Entry);
		int result;
		if (flag)
		{
			result = Lua.LuaDoString<int>("local startTime, duration, enable = GetItemCooldown(" + entry.ToString() + "); return duration - (GetTime() - startTime)", "");
		}
		else
		{
			Main.Log("Couldn't find item" + itemName);
			result = 0;
		}
		return result;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00006D2C File Offset: 0x00004F2C
	public static int GetItemID(string itemName)
	{
		return (from item in Bag.GetBagItem()
		where itemName.Equals(item.Name)
		select item.Entry).FirstOrDefault<int>();
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00006D85 File Offset: 0x00004F85
	public static bool HaveRangedWeaponEquipped
	{
		get
		{
			return Constants.Me.GetEquipedItemBySlot(18) > 0U;
		}
	}
}
