using System;
using System.Collections.Generic;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000036 RID: 54
	public class Exclusives
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000C154 File Offset: 0x0000A354
		public bool Add(WoWUnit unit, Exclusive exclusive)
		{
			bool flag = exclusive == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				HashSet<Exclusive> hashSet;
				bool flag2 = !this.Tokens.TryGetValue(unit.Guid, out hashSet);
				if (flag2)
				{
					hashSet = new HashSet<Exclusive>();
					this.Tokens[unit.Guid] = hashSet;
				}
				result = hashSet.Add(exclusive);
			}
			return result;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public bool Remove(WoWUnit unit, Exclusive exclusive)
		{
			bool flag = exclusive == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				HashSet<Exclusive> hashSet;
				bool flag2 = !this.Tokens.TryGetValue(unit.Guid, out hashSet);
				result = (!flag2 && hashSet.Remove(exclusive));
			}
			return result;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		public bool Contains(WoWUnit unit, Exclusive exclusive)
		{
			bool flag = exclusive == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				HashSet<Exclusive> hashSet;
				bool flag2 = !this.Tokens.TryGetValue(unit.Guid, out hashSet);
				result = (!flag2 && hashSet.Contains(exclusive));
			}
			return result;
		}

		// Token: 0x04000160 RID: 352
		private readonly Dictionary<ulong, HashSet<Exclusive>> Tokens = new Dictionary<ulong, HashSet<Exclusive>>();
	}
}
