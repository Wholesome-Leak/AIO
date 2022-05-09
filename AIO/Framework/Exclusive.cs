using System;

namespace AIO.Framework
{
	// Token: 0x02000061 RID: 97
	public class Exclusive
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000EE70 File Offset: 0x0000D070
		private Exclusive(string Name)
		{
			this.Name = Name;
		}

		// Token: 0x040001D9 RID: 473
		internal string Name;

		// Token: 0x040001DA RID: 474
		public static readonly Exclusive HunterAspect = new Exclusive("HunterAspect");

		// Token: 0x040001DB RID: 475
		public static readonly Exclusive WarlockSkin = new Exclusive("WarlockSkin");

		// Token: 0x040001DC RID: 476
		public static readonly Exclusive MageArmor = new Exclusive("MageArmor");

		// Token: 0x040001DD RID: 477
		public static readonly Exclusive ShamanShield = new Exclusive("ShamanShield");

		// Token: 0x040001DE RID: 478
		public static readonly Exclusive PaladinBlessing = new Exclusive("PaladinBlessing");
	}
}
