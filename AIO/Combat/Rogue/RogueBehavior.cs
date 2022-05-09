using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Settings;
using robotManager.Helpful;

namespace AIO.Combat.Rogue
{
	// Token: 0x020000A3 RID: 163
	internal class RogueBehavior : BaseCombatClass
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000164C1 File Offset: 0x000146C1
		public override float Range
		{
			get
			{
				return this.CombatRange;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000164CC File Offset: 0x000146CC
		private float SwapRange(float range)
		{
			float combatRange = this.CombatRange;
			this.CombatRange = range;
			return combatRange;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000164F0 File Offset: 0x000146F0
		internal RogueBehavior() : base(BasePersistentSettings<RogueLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"LowLevel",
				new LowLevel()
			},
			{
				"Combat",
				new Combat()
			}
		}, Array.Empty<ICycleable>())
		{
			base.Addons.Add(new ConditionalCycleable(() => BasePersistentSettings<RogueLevelSettings>.Current.PullRanged, new RangedPull("Throw", new Func<float, float>(this.SwapRange))));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00016587 File Offset: 0x00014787
		protected override void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			PoisonHelper.CheckPoison();
		}

		// Token: 0x040002FA RID: 762
		private float CombatRange = 5f;
	}
}
