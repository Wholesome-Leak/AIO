using System;
using System.Collections.Generic;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x02000016 RID: 22
public static class MageFoodManager
{
	// Token: 0x0600006A RID: 106 RVA: 0x000081FC File Offset: 0x000063FC
	private static List<string> Drink()
	{
		return new List<string>
		{
			"Conjured Water",
			"Conjured Fresh Water",
			"Conjured Purified Water",
			"Conjured Spring Water",
			"Conjured Mineral Water",
			"Conjured Sparkling Water",
			"Conjured Crystal Water",
			"Conjured Mountain Spring Water",
			"Conjured Glacier Water",
			"Conjured Mana Biscuit"
		};
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000828C File Offset: 0x0000648C
	private static List<string> Food()
	{
		return new List<string>
		{
			"Conjured Muffin",
			"Conjured Bread",
			"Conjured Rye",
			"Conjured Pumpernickel",
			"Conjured Sourdough",
			"Conjured Sweet Roll",
			"Conjured Cinnamon Roll",
			"Conjured Croissant",
			"Conjured Mana Pie",
			"Conjured Mana Strudel"
		};
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000831C File Offset: 0x0000651C
	private static List<string> ManaStones()
	{
		return new List<string>
		{
			"Mana Agate",
			"Mana Jade",
			"Mana Citrine",
			"Mana Ruby",
			"Mana Emerald",
			"Mana Sapphire"
		};
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000837C File Offset: 0x0000657C
	public static void CheckIfEnoughFoodAndDrinks()
	{
		MageFoodManager._bagItems = Bag.GetBagItem();
		int num = 0;
		int num2 = 0;
		bool flag = Bag.GetContainerNumFreeSlotsNormalType <= 1;
		if (!flag)
		{
			foreach (WoWItem woWItem in MageFoodManager._bagItems)
			{
				bool flag2 = MageFoodManager.Drink().Contains(woWItem.Name);
				if (flag2)
				{
					num += ItemsManager.GetItemCountByNameLUA(woWItem.Name);
				}
				bool flag3 = MageFoodManager.Food().Contains(woWItem.Name);
				if (flag3)
				{
					num2 += ItemsManager.GetItemCountByNameLUA(woWItem.Name);
				}
			}
			bool flag4 = num < 10 && MageFoodManager.ConjureWater.IsSpellUsable && MageFoodManager.ConjureWater.KnownSpell;
			if (flag4)
			{
				MageFoodManager.ConjureWater.Launch();
				Usefuls.WaitIsCasting();
			}
			bool flag5 = num2 < 10 && MageFoodManager.ConjureFood.IsSpellUsable && MageFoodManager.ConjureFood.KnownSpell && !MageFoodManager.ConjureRefreshement.KnownSpell;
			if (flag5)
			{
				MageFoodManager.ConjureFood.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000084C0 File Offset: 0x000066C0
	public static void CheckIfThrowFoodAndDrinks()
	{
		bool flag = !Fight.InFight;
		if (flag)
		{
			MageFoodManager._bagItems = Bag.GetBagItem();
			int num = 0;
			int num2 = 0;
			foreach (WoWItem woWItem in MageFoodManager._bagItems)
			{
				bool flag2 = MageFoodManager.Drink().Contains(woWItem.Name);
				if (flag2)
				{
					num = ((MageFoodManager.Drink().IndexOf(woWItem.Name) > num) ? MageFoodManager.Drink().IndexOf(woWItem.Name) : num);
				}
				bool flag3 = MageFoodManager.Food().Contains(woWItem.Name);
				if (flag3)
				{
					num2 = ((MageFoodManager.Food().IndexOf(woWItem.Name) > num2) ? MageFoodManager.Food().IndexOf(woWItem.Name) : num2);
				}
			}
			foreach (WoWItem woWItem2 in MageFoodManager._bagItems)
			{
				bool flag4 = MageFoodManager.Drink().Contains(woWItem2.Name) && MageFoodManager.Drink().IndexOf(woWItem2.Name) < num;
				if (flag4)
				{
					MageFoodManager.LuaDeleteItem(woWItem2.Name);
				}
				bool flag5 = MageFoodManager.Food().Contains(woWItem2.Name) && MageFoodManager.Food().IndexOf(woWItem2.Name) < num2;
				if (flag5)
				{
					MageFoodManager.LuaDeleteItem(woWItem2.Name);
				}
			}
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00008680 File Offset: 0x00006880
	public static void CheckIfHaveManaStone()
	{
		bool flag = !Fight.InFight && MageFoodManager.ManaStone == "";
		if (flag)
		{
			MageFoodManager._bagItems = Bag.GetBagItem();
			bool flag2 = false;
			foreach (WoWItem woWItem in MageFoodManager._bagItems)
			{
				bool flag3 = MageFoodManager.ManaStones().Contains(woWItem.Name);
				if (flag3)
				{
					flag2 = true;
					MageFoodManager.ManaStone = woWItem.Name;
				}
			}
			bool flag4 = !flag2 && Bag.GetContainerNumFreeSlotsNormalType > 1;
			if (flag4)
			{
				bool knownSpell = MageFoodManager.ConjureManaGem.KnownSpell;
				if (knownSpell)
				{
					bool isSpellUsable = MageFoodManager.ConjureManaGem.IsSpellUsable;
					if (isSpellUsable)
					{
						MageFoodManager.ConjureManaGem.Launch();
						Usefuls.WaitIsCasting();
					}
				}
			}
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00008770 File Offset: 0x00006970
	public static void UseManaStone()
	{
		ItemsManager.UseItemByNameOrId(MageFoodManager.ManaStone);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00002328 File Offset: 0x00000528
	public static void LuaDeleteItem(string item)
	{
		Lua.LuaDoString("for bag = 0, 4, 1 do for slot = 1, 32, 1 do local name = GetContainerItemLink(bag, slot); if name and string.find(name, \"" + item + "\") then PickupContainerItem(bag, slot); DeleteCursorItem(); end; end; end", false);
	}

	// Token: 0x04000032 RID: 50
	private static List<WoWItem> _bagItems;

	// Token: 0x04000033 RID: 51
	private static Spell ConjureWater = new Spell("Conjure Water");

	// Token: 0x04000034 RID: 52
	private static Spell ConjureFood = new Spell("Conjure Food");

	// Token: 0x04000035 RID: 53
	private static Spell ConjureManaGem = new Spell("Conjure Mana Gem");

	// Token: 0x04000036 RID: 54
	private static Spell ConjureRefreshement = new Spell("Conjure Refreshment");

	// Token: 0x04000037 RID: 55
	public static string ManaStone = "";
}
