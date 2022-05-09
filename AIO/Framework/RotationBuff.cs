using System;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000037 RID: 55
	public class RotationBuff : RotationSpell
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000C251 File Offset: 0x0000A451
		public RotationBuff(string name, bool ignoresGlobal = false, int minimumStacks = 0, int minimumRefreshTimeLeft = 0) : base(name, ignoresGlobal)
		{
			this.MinimumStacks = minimumStacks;
			this.MinimumRefreshTimeLeft = minimumRefreshTimeLeft;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C26C File Offset: 0x0000A46C
		public override ValueTuple<bool, bool> Should(WoWUnit target)
		{
			IEnumerable<Aura> auras = BuffManager.GetAuras(target.GetBaseAddress, this.Spell.Ids);
			Aura aura = auras.FirstOrDefault<Aura>();
			bool flag = aura == null;
			ValueTuple<bool, bool> result;
			if (flag)
			{
				result = new ValueTuple<bool, bool>(true, true);
			}
			else
			{
				IEnumerable<Aura> source = from b in auras
				where b.Owner == Constants.Me.Guid
				select b;
				Aura aura2 = source.FirstOrDefault<Aura>();
				bool item = ((aura2 != null) ? aura2.Stack : 0) < this.MinimumStacks || ((aura2 != null) ? aura2.TimeLeft : 0) < this.MinimumRefreshTimeLeft;
				bool item2 = aura2 != null;
				result = new ValueTuple<bool, bool>(item, item2);
			}
			return result;
		}

		// Token: 0x04000161 RID: 353
		private readonly int MinimumStacks;

		// Token: 0x04000162 RID: 354
		private readonly int MinimumRefreshTimeLeft;
	}
}
