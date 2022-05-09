using System;
using System.Collections.Generic;
using System.Threading;
using AIO.Framework;
using wManager.Wow.Helpers;

// Token: 0x02000003 RID: 3
public class ItemsHelper
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
	public static float GetItemCooldown(string itemName)
	{
		string text = "\r\n        for bag=0,4 do\r\n            for slot=1,36 do\r\n                local itemLink = GetContainerItemLink(bag,slot);\r\n                if (itemLink) then\r\n                    local itemString = string.match(itemLink, \"item[%-?%d:]+\");\r\n                    if (GetItemInfo(itemString) == \"" + itemName + "\") then\r\n                        local start, duration, enabled = GetContainerItemCooldown(bag, slot);\r\n                        if enabled == 1 and duration > 0 and start > 0 then\r\n                            return (duration - (GetTime() - start));\r\n                        end\r\n                    end\r\n                end;\r\n            end;\r\n        end\r\n        return 0;";
		return Lua.LuaDoString<float>(text, "");
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
	public static float GetItemCooldown(uint id)
	{
		return ItemsHelper.GetItemCooldown(ItemsManager.GetNameById(id));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
	public static void DeleteItems(uint itemID, int leaveAmount)
	{
		int num = ItemsManager.GetItemCountById(itemID) - leaveAmount;
		RotationLogger.Debug(string.Format("Found Items to delete: {0}", num));
		RotationLogger.Debug(string.Format("Found Items to delete: {0}", itemID));
		bool flag = num > 0;
		if (flag)
		{
			ItemsHelper.Delete((int)itemID);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
	public static void Delete(int item)
	{
		List<int> itemContainerBagIdAndSlot = Bag.GetItemContainerBagIdAndSlot(item);
		Lua.LuaDoString(string.Format("PickupContainerItem({0}, {1}); DeleteCursorItem()", itemContainerBagIdAndSlot[0], itemContainerBagIdAndSlot[1]), false);
		Thread.Sleep(10);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
	public static int GetItemCountSave(uint itemId)
	{
		int itemCountById = ItemsManager.GetItemCountById(itemId);
		bool flag = itemCountById > 0;
		int result;
		if (flag)
		{
			result = itemCountById;
		}
		else
		{
			Thread.Sleep(250);
			result = ItemsManager.GetItemCountById(itemId);
		}
		return result;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002294 File Offset: 0x00000494
	public static int GetItemCountSave(string itemName)
	{
		int itemCount = ItemsHelper.GetItemCount(itemName);
		bool flag = itemCount > 0;
		int result;
		if (flag)
		{
			result = itemCount;
		}
		else
		{
			Thread.Sleep(250);
			result = ItemsHelper.GetItemCount(itemName);
		}
		return result;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000022CC File Offset: 0x000004CC
	public static int GetItemCount(string itemName)
	{
		string text = "\r\n        local fullCount = 0;\r\n        for bag=0,4 do\r\n            for slot=1,36 do\r\n                local itemLink = GetContainerItemLink(bag, slot);\r\n                if (itemLink) then\r\n                    local itemString = string.match(itemLink, \"item[%-?%d:]+\");\r\n                    if (GetItemInfo(itemString) == \"" + itemName + "\") then\r\n                        local texture, count = GetContainerItemInfo(bag, slot);\r\n                        fullCount = fullCount + count;\r\n                    end\r\n                end\r\n            end\r\n        end\r\n        return fullCount;";
		return Lua.LuaDoString<int>(text, "");
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000022FC File Offset: 0x000004FC
	public static int CountItemStacks(string itemArg)
	{
		return Lua.LuaDoString<int>("local count = GetItemCount('" + itemArg + "'); return count", "");
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002328 File Offset: 0x00000528
	public static void LuaDeleteItem(string item)
	{
		Lua.LuaDoString("for bag = 0, 4, 1 do for slot = 1, 32, 1 do local name = GetContainerItemLink(bag, slot); if name and string.find(name, \"" + item + "\") then PickupContainerItem(bag, slot); DeleteCursorItem(); end; end; end", false);
	}
}
