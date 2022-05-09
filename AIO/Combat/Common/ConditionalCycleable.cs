using System;

namespace AIO.Combat.Common
{
	// Token: 0x02000105 RID: 261
	internal class ConditionalCycleable : ICycleable
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00026892 File Offset: 0x00024A92
		public ConditionalCycleable(Func<bool> predicate, ICycleable cycleable)
		{
			this.Predicate = predicate;
			this.Cycleable = cycleable;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000268AC File Offset: 0x00024AAC
		public void Dispose()
		{
			bool flag = this.Predicate();
			if (flag)
			{
				this.Cycleable.Dispose();
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000268D8 File Offset: 0x00024AD8
		public void Initialize()
		{
			bool flag = this.Predicate();
			if (flag)
			{
				this.Cycleable.Initialize();
			}
		}

		// Token: 0x0400057F RID: 1407
		private readonly Func<bool> Predicate;

		// Token: 0x04000580 RID: 1408
		private readonly ICycleable Cycleable;
	}
}
