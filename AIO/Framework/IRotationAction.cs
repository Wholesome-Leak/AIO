using System;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000060 RID: 96
	public interface IRotationAction
	{
		// Token: 0x060003CA RID: 970
		bool Execute(WoWUnit target, bool force = false);

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CB RID: 971
		float MaxRange { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003CC RID: 972
		bool IgnoresGlobal { get; }

		// Token: 0x060003CD RID: 973
		ValueTuple<bool, bool> Should(WoWUnit target);
	}
}
