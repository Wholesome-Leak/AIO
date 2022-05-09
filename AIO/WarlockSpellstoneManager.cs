using System;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x0200000C RID: 12
public static class WarlockSpellstoneManager
{
	// Token: 0x0600003C RID: 60 RVA: 0x00003644 File Offset: 0x00001844
	private static List<string> Spellstones()
	{
		return new List<string>
		{
			"Spellstone",
			"Greater Spellstone",
			"Major Spellstone",
			"Master Spellstone",
			"Demonic Spellstone",
			"Grand Spellstone"
		};
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000036A4 File Offset: 0x000018A4
	private static void CheckIfHaveManaStone()
	{
		bool flag = !Fight.InFight && ItemsManager.GetItemCountByIdLUA(6265U) > 0;
		if (flag)
		{
			WarlockSpellstoneManager._bagItems = Bag.GetBagItem();
			WarlockSpellstoneManager.haveSpellstone = false;
			foreach (WoWItem woWItem in WarlockSpellstoneManager._bagItems)
			{
				bool flag2 = WarlockSpellstoneManager.Spellstones().Contains(woWItem.Name);
				if (flag2)
				{
					WarlockSpellstoneManager.haveSpellstone = true;
					WarlockSpellstoneManager.SpellstoneinBag = woWItem.Name;
				}
			}
			bool flag3 = !WarlockSpellstoneManager.haveSpellstone && Bag.GetContainerNumFreeSlotsNormalType > 1;
			if (flag3)
			{
				bool knownSpell = WarlockSpellstoneManager.CreateSpellstone.KnownSpell;
				if (knownSpell)
				{
					bool isSpellUsable = WarlockSpellstoneManager.CreateSpellstone.IsSpellUsable;
					if (isSpellUsable)
					{
						WarlockSpellstoneManager.CreateSpellstone.Launch();
						Usefuls.WaitIsCasting();
					}
				}
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0000379C File Offset: 0x0000199C
	private static void UseSpellStone()
	{
		bool flag = !Lua.LuaDoString<bool>("local hasMainHandEnchant, _, _, _, _, _, _, _, _ = GetWeaponEnchantInfo()\r\n            if (hasMainHandEnchant) then \r\n               return '1'\r\n            else\r\n               return '0'\r\n            end", "") && WarlockSpellstoneManager.haveSpellstone;
		if (flag)
		{
			ItemsManager.UseItemByNameOrId(WarlockSpellstoneManager.SpellstoneinBag);
			Thread.Sleep(10);
			Lua.RunMacroText("/use 16");
			Usefuls.WaitIsCasting();
		}
	}

	// Token: 0x04000023 RID: 35
	private static List<WoWItem> _bagItems;

	// Token: 0x04000024 RID: 36
	private static Spell CreateSpellstone = new Spell("Create Spellstone");

	// Token: 0x04000025 RID: 37
	private static string SpellstoneinBag = "";

	// Token: 0x04000026 RID: 38
	private static bool haveSpellstone;
}
