using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
	// Token: 0x020000F3 RID: 243
	internal class HealOOC : BaseRotation
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002364E File Offset: 0x0002184E
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002365B File Offset: 0x0002185B
		internal HealOOC(BaseCombatClass combatClass) : base(false, true, false, false)
		{
			this.CombatClass = combatClass;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00023670 File Offset: 0x00021870
		protected override List<RotationStep> Rotation
		{
			get
			{
				return new List<RotationStep>
				{
					new RotationStep(new RotationSpell("Rejuvenation", false), 1f, (IRotationAction s, WoWUnit t) => this.Spec == "FeralCombat" && Constants.Me.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.FeralRejuvenation && Constants.Me.ManaPercentage > 15U && !Constants.Me.HaveBuff("Rejuvenation"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false),
					new RotationStep(new RotationSpell("Regrowth", false), 2f, (IRotationAction s, WoWUnit t) => this.Spec == "FeralCombat" && Constants.Me.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.FeralRegrowth && Constants.Me.ManaPercentage > 15U && !Constants.Me.HaveBuff("Regrowth") && Constants.Me.HaveBuff("Rejuvenation"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false),
					new RotationStep(new RotationSpell("Rejuvenation", false), 3f, (IRotationAction s, WoWUnit t) => this.Spec == "Balance" && Constants.Me.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.BalanceRejuvenation && Constants.Me.ManaPercentage > 15U && !Constants.Me.HaveBuff("Rejuvenation"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false),
					new RotationStep(new RotationSpell("Regrowth", false), 4f, (IRotationAction s, WoWUnit t) => this.Spec == "Balance" && Constants.Me.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.BalanceRegrowth && Constants.Me.ManaPercentage > 15U && !Constants.Me.HaveBuff("Regrowth") && Constants.Me.HaveBuff("Rejuvenation"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false)
				};
			}
		}

		// Token: 0x040004F8 RID: 1272
		private readonly BaseCombatClass CombatClass;
	}
}
