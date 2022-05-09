using System;
using System.Collections.Generic;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Settings;

namespace AIO.Combat.Priest
{
	// Token: 0x020000AE RID: 174
	internal class PriestBehavior : BaseCombatClass
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00012652 File Offset: 0x00010852
		public override float Range
		{
			get
			{
				return 29f;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000192F8 File Offset: 0x000174F8
		internal PriestBehavior() : base(BasePersistentSettings<PriestLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"LowLevel",
				new LowLevel()
			},
			{
				"Holy",
				new Holy()
			},
			{
				"Shadow",
				new Shadow()
			}
		}, new ICycleable[]
		{
			new AutoPartyResurrect("Resurrection", false, true)
		})
		{
			base.Addons.Add(new ConditionalCycleable(() => BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuff, new Buffs(this)));
			base.Addons.Add(new ConditionalCycleable(() => base.Specialisation == "Holy" || base.Specialisation == "Shadow", new SlowLuaCaching()));
		}
	}
}
