using System;
using System.Collections.Generic;

// Token: 0x02000019 RID: 25
public static class Consumables
{
	// Token: 0x0600008A RID: 138 RVA: 0x00008EF8 File Offset: 0x000070F8
	public static List<string> HealthStones()
	{
		return new List<string>
		{
			"Minor Healthstone",
			"Lesser Healthstone",
			"Healthstone",
			"Greater Healthstone",
			"Major Healthstone",
			"Master Healthstone",
			"Fel Healthstone",
			"Demonic Healthstone"
		};
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00008F70 File Offset: 0x00007170
	public static bool HaveHealthstone()
	{
		return Extension.HaveOneInList(Consumables.HealthStones());
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00008F95 File Offset: 0x00007195
	public static void UseHealthstone()
	{
		Extension.UseFirstMatchingItem(Consumables.HealthStones());
	}
}
