using System;
using wManager.Wow.ObjectManager;

namespace AIO
{
	// Token: 0x0200001A RID: 26
	internal static class Constants
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00008FA3 File Offset: 0x000071A3
		internal static WoWUnit Target
		{
			get
			{
				return ObjectManager.Target;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00008FAA File Offset: 0x000071AA
		internal static WoWLocalPlayer Me
		{
			get
			{
				return ObjectManager.Me;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00008FB1 File Offset: 0x000071B1
		internal static WoWUnit Pet
		{
			get
			{
				return ObjectManager.Pet;
			}
		}
	}
}
