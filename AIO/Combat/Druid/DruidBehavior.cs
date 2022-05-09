using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
	// Token: 0x020000E8 RID: 232
	internal class DruidBehavior : BaseCombatClass
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x000219C1 File Offset: 0x0001FBC1
		public override float Range
		{
			get
			{
				return this.CombatRange;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000219CC File Offset: 0x0001FBCC
		internal DruidBehavior() : base(BasePersistentSettings<DruidLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"LowLevel",
				new LowLevel()
			},
			{
				"FeralCombat",
				new FeralCombat()
			},
			{
				"Balance",
				new Balance()
			},
			{
				"Restoration",
				new Restoration()
			}
		}, new ICycleable[]
		{
			new Buffs(),
			new AutoPartyResurrect("Revive", false, true),
			new AutoPartyResurrect("Rebirth", true, BasePersistentSettings<DruidLevelSettings>.Current.RestorationRebirthAuto)
		})
		{
			base.Addons.Add(new ConditionalCycleable(() => BasePersistentSettings<DruidLevelSettings>.Current.HealOOC, new HealOOC(this)));
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00021A9C File Offset: 0x0001FC9C
		public override void Initialize()
		{
			base.Initialize();
			string specialisation = base.Specialisation;
			string a = specialisation;
			if (!(a == "FeralCombat"))
			{
				this.CombatRange = 29f;
			}
			else
			{
				this.CombatRange = ((SpellManager.KnowSpell("Growl") || SpellManager.KnowSpell("Cat Form")) ? 5f : 29f);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00021B02 File Offset: 0x0001FD02
		protected override void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
			DruidBehavior.HealingTouchValue = RotationSpell.GetSpellCost("Healing Touch");
			DruidBehavior.RejuvenationValue = RotationSpell.GetSpellCost("Rejuvenation");
			DruidBehavior.RegrowthValue = RotationSpell.GetSpellCost("Regrowth");
			DruidBehavior.TransformValue = RotationSpell.GetSpellCost("Bear Form");
		}

		// Token: 0x040004BD RID: 1213
		public static int HealingTouchValue;

		// Token: 0x040004BE RID: 1214
		public static int RegrowthValue;

		// Token: 0x040004BF RID: 1215
		public static int RejuvenationValue;

		// Token: 0x040004C0 RID: 1216
		public static int TransformValue;

		// Token: 0x040004C1 RID: 1217
		private float CombatRange;
	}
}
