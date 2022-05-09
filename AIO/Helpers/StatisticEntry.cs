using System;
using System.Linq;

namespace AIO.Helpers
{
	// Token: 0x0200002F RID: 47
	public class StatisticEntry
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B6AB File Offset: 0x000098AB
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000B6B3 File Offset: 0x000098B3
		public double Average { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B6BC File Offset: 0x000098BC
		public int Count
		{
			get
			{
				return this.HistoricalData.Count;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B6C9 File Offset: 0x000098C9
		public StatisticEntry(int startValue)
		{
			this.HistoricalData = new RingBuffer<int>(10, true);
			this.Add(startValue);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B6EC File Offset: 0x000098EC
		public void Add(int value)
		{
			bool flag = value <= 0;
			if (!flag)
			{
				this.HistoricalData.Add(value);
				this.Average = this.HistoricalData.Average();
			}
		}

		// Token: 0x0400013E RID: 318
		private readonly RingBuffer<int> HistoricalData;
	}
}
