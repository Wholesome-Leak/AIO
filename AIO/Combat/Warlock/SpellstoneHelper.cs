using System;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x0200007F RID: 127
	internal static class SpellstoneHelper
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00012484 File Offset: 0x00010684
		private static bool HasEnchant
		{
			get
			{
				return Lua.LuaDoString<bool>("local hasMainHandEnchant, _, _, _, _, _, _, _, _ = GetWeaponEnchantInfo()\r\n                if (hasMainHandEnchant) then \r\n                    return '1'\r\n                else\r\n                    return '0'\r\n                end", "");
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00012498 File Offset: 0x00010698
		private static string FindSpellStone()
		{
			List<WoWItem> bagItem = Bag.GetBagItem();
			foreach (WoWItem woWItem in bagItem)
			{
				bool flag = !SpellstoneHelper.Spellstones.Contains(woWItem.Name);
				if (!flag)
				{
					return woWItem.Name;
				}
			}
			return "";
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00012518 File Offset: 0x00010718
		public static void Refresh()
		{
			bool hasEnchant = SpellstoneHelper.HasEnchant;
			if (!hasEnchant)
			{
				string text = SpellstoneHelper.FindSpellStone();
				bool flag = text == "";
				if (flag)
				{
					bool flag2 = !SpellstoneHelper.CreateSpellstone.KnownSpell || !SpellstoneHelper.CreateSpellstone.IsSpellUsable;
					if (flag2)
					{
						return;
					}
					bool flag3 = Bag.GetContainerNumFreeSlotsNormalType < 1;
					if (flag3)
					{
						return;
					}
					bool flag4 = ItemsManager.GetItemCountByIdLUA(6265U) == 0;
					if (flag4)
					{
						return;
					}
					SpellstoneHelper.CreateSpellstone.Launch();
					Usefuls.WaitIsCasting();
					text = SpellstoneHelper.FindSpellStone();
					bool flag5 = text == "";
					if (flag5)
					{
						return;
					}
				}
				ItemsManager.UseItemByNameOrId(text);
				Thread.Sleep(10);
				Lua.RunMacroText("/use 16");
				Usefuls.WaitIsCasting();
			}
		}

		// Token: 0x04000262 RID: 610
		private static readonly List<string> Spellstones = new List<string>
		{
			"Spellstone",
			"Greater Spellstone",
			"Major Spellstone",
			"Master Spellstone",
			"Demonic Spellstone",
			"Grand Spellstone"
		};

		// Token: 0x04000263 RID: 611
		private static readonly Spell CreateSpellstone = new Spell("Create Spellstone");
	}
}
