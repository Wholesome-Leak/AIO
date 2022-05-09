using System;
using System.Collections.Generic;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Settings;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000BC RID: 188
	internal class PaladinBehavior : BaseCombatClass
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B5E4 File Offset: 0x000197E4
		public override float Range
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001B5EC File Offset: 0x000197EC
		internal PaladinBehavior()
		{
			BaseSettings settings = BasePersistentSettings<PaladinLevelSettings>.Current;
			Dictionary<string, BaseRotation> dictionary = new Dictionary<string, BaseRotation>();
			dictionary.Add("LowLevel", new LowLevel());
			dictionary.Add("Holy", new Holy());
			dictionary.Add("Protection", new Protection());
			dictionary.Add("Retribution", new Retribution());
			ICycleable[] array = new ICycleable[2];
			array[0] = new AutoPartyResurrect("Redemption", false, true);
			array[1] = new ConditionalCycleable(() => BasePersistentSettings<PaladinLevelSettings>.Current.HealOOC, new HealOOC());
			base..ctor(settings, dictionary, array);
			base.Addons.Add(new ConditionalCycleable(() => BasePersistentSettings<PaladinLevelSettings>.Current.Buffing, new Buffs(this)));
		}
	}
}
