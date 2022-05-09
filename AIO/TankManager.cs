using System;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x02000005 RID: 5
public static class TankManager
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002514 File Offset: 0x00000714
	public static uint GetThreatStatus(WoWUnit target)
	{
		return Lua.LuaDoString<uint>("return UnitThreatSituation(\"" + target.Name + "\");", "");
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002548 File Offset: 0x00000748
	public static int GetAggroDifferenceFor(WoWUnit target, IEnumerable<WoWPlayer> partyMembers)
	{
		uint threatStatus = TankManager.GetThreatStatus(target);
		uint num = (from p in partyMembers
		let tVal = TankManager.GetThreatStatus(p)
		orderby tVal descending
		select tVal).FirstOrDefault<uint>();
		return (int)(threatStatus - num);
	}
}
