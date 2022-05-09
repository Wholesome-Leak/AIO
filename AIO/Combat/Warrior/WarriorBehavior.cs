using System;
using System.Collections.Generic;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Settings;

namespace AIO.Combat.Warrior
{
	// Token: 0x0200006D RID: 109
	internal class WarriorBehavior : BaseCombatClass
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00010BFE File Offset: 0x0000EDFE
		public override float Range
		{
			get
			{
				return this.CombatRange;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010C08 File Offset: 0x0000EE08
		private float SwapRange(float range)
		{
			float combatRange = this.CombatRange;
			this.CombatRange = range;
			return combatRange;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010C2C File Offset: 0x0000EE2C
		internal WarriorBehavior() : base(BasePersistentSettings<WarriorLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"LowLevel",
				new LowLevel()
			},
			{
				"Arms",
				new Arms()
			},
			{
				"Protection",
				new Protection()
			},
			{
				"Fury",
				new Fury()
			}
		}, Array.Empty<ICycleable>())
		{
			base.Addons.Add(new Buffs(this));
			base.Addons.Add(new ConditionalCycleable(() => BasePersistentSettings<WarriorLevelSettings>.Current.PullRanged, new RangedPull("Throw", new Func<float, float>(this.SwapRange))));
		}

		// Token: 0x04000226 RID: 550
		private float CombatRange = 5f;
	}
}
