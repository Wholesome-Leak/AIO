using System;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Helpers
{
	// Token: 0x0200002B RID: 43
	public class DebugSpell : IRotationAction
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000ADCA File Offset: 0x00008FCA
		public DebugSpell(string name, float maxRange = 2.14748365E+09f, bool ignoresGlobal = false)
		{
			this.Name = name;
			this.MaxRange = maxRange;
			this.IgnoresGlobal = ignoresGlobal;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public bool Execute(WoWUnit target, bool force = false)
		{
			return true;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000ADFF File Offset: 0x00008FFF
		public float MaxRange { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000AE07 File Offset: 0x00009007
		public bool IgnoresGlobal { get; }

		// Token: 0x0600027B RID: 635 RVA: 0x0000AE10 File Offset: 0x00009010
		public ValueTuple<bool, bool> Should(WoWUnit target)
		{
			return new ValueTuple<bool, bool>(true, true);
		}

		// Token: 0x04000130 RID: 304
		private readonly string Name;
	}
}
