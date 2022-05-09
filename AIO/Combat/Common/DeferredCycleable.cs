using System;

namespace AIO.Combat.Common
{
	// Token: 0x02000107 RID: 263
	internal class DeferredCycleable : ICycleable
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x00026903 File Offset: 0x00024B03
		public DeferredCycleable(Func<ICycleable> cycleable)
		{
			this.Cycleable = cycleable;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00026913 File Offset: 0x00024B13
		public void Dispose()
		{
			ICycleable cycleable = this.Cycleable();
			if (cycleable != null)
			{
				cycleable.Dispose();
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002692C File Offset: 0x00024B2C
		public void Initialize()
		{
			ICycleable cycleable = this.Cycleable();
			if (cycleable != null)
			{
				cycleable.Initialize();
			}
		}

		// Token: 0x04000581 RID: 1409
		private readonly Func<ICycleable> Cycleable;
	}
}
