using System;
using wManager.Wow.Helpers;

// Token: 0x02000004 RID: 4
public static class PetManager
{
	// Token: 0x06000012 RID: 18 RVA: 0x0000234C File Offset: 0x0000054C
	public static int GetPetSpellIndex(string spellName)
	{
		return Lua.LuaDoString<int>("for i=1,10 do local name, _, _, _, _, _, _ = GetPetActionInfo(i); if name == '" + spellName + "' then return i end end", "");
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000237C File Offset: 0x0000057C
	public static int GetPetSpellCooldown(string spellName)
	{
		return Lua.LuaDoString<int>("local startTime, duration, enable = GetPetActionCooldown(" + PetManager.GetPetSpellIndex(spellName).ToString() + "); return duration - (GetTime() - startTime)", "");
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000023B8 File Offset: 0x000005B8
	public static bool GetPetSpellReady(string spellName)
	{
		return PetManager.GetPetSpellCooldown(spellName) <= 0;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023D8 File Offset: 0x000005D8
	public static void PetSpellCast(string spellName)
	{
		int petSpellIndex = PetManager.GetPetSpellIndex(spellName);
		bool petSpellReady = PetManager.GetPetSpellReady(spellName);
		if (petSpellReady)
		{
			Lua.LuaDoString("CastPetAction(" + petSpellIndex.ToString() + ");", false);
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002418 File Offset: 0x00000618
	public static void PetSpellCastFocus(string spellName)
	{
		int petSpellIndex = PetManager.GetPetSpellIndex(spellName);
		bool petSpellReady = PetManager.GetPetSpellReady(spellName);
		if (petSpellReady)
		{
			Lua.LuaDoString(string.Format("CastPetAction({0}, 'focus');", petSpellIndex), false);
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002450 File Offset: 0x00000650
	public static void TogglePetSpellAuto(string spellName, bool toggle)
	{
		string text = PetManager.GetPetSpellIndex(spellName).ToString();
		bool flag = !text.Equals("0");
		if (flag)
		{
			bool flag2 = Lua.LuaDoString<bool>("local _, autostate = GetSpellAutocast(" + text + ", 'pet'); return autostate == 1", "") || Lua.LuaDoString<bool>("local _, autostate = GetSpellAutocast('" + spellName + "', 'pet'); return autostate == 1", "");
			bool flag3 = (toggle && !flag2) || (!toggle && flag2);
			if (flag3)
			{
				Lua.LuaDoString("ToggleSpellAutocast(" + text + ", 'pet');", false);
				Lua.LuaDoString("ToggleSpellAutocast('" + spellName + "', 'pet');", false);
			}
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000018 RID: 24 RVA: 0x00002503 File Offset: 0x00000703
	public static string CurrentWarlockPet
	{
		get
		{
			return Lua.LuaDoString<string>("for i=1,10 do local name, _, _, _, _, _, _ = GetPetActionInfo(i); if name == 'Firebolt' then return 'Imp' end if name == 'Torment' then return 'Voidwalker' end if name == 'Anguish' then return 'Felguard' end if name == 'Devour Magic' then return 'Felhunter' end end", "");
		}
	}
}
