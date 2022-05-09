using System;
using System.Collections.Generic;

// Token: 0x02000014 RID: 20
internal class FuncComp<T, TN> : IComparer<T> where TN : IComparable<TN>
{
	// Token: 0x0600005D RID: 93 RVA: 0x00006DFA File Offset: 0x00004FFA
	public FuncComp(Func<T, TN> pred)
	{
		this.Pred = pred;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00006E0C File Offset: 0x0000500C
	public int Compare(T entry1, T entry2)
	{
		TN tn = this.Pred(entry1);
		return tn.CompareTo(this.Pred(entry2));
	}

	// Token: 0x0400002F RID: 47
	private readonly Func<T, TN> Pred;
}
