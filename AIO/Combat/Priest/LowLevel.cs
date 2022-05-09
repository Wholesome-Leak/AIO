using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Priest
{
	// Token: 0x020000AC RID: 172
	internal class LowLevel : BaseRotation
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00018D3C File Offset: 0x00016F3C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => Extension.HaveRangedWeaponEquipped && BasePersistentSettings<PriestLevelSettings>.Current.UseWand && Constants.Target.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.UseWandTresh && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Power Word: Fortitude", false, 0, 0), 2.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Power Word: Fortitude") && Constants.Me.ManaPercentage > 50U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Power Word: Shield", false), 3f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 99.0 && !Constants.Me.HaveBuff("Power Word: Shield"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Power Word: Shield", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseShieldTresh && !t.HaveBuff("Power Word: Shield") && BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseShieldParty, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lesser Heal", false), 5f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Lesser Heal") && Constants.Me.HealthPercent < 75.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lesser Heal", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Lesser Heal"
				}) && Constants.Me.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseFlashTresh && BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 90.0 && !Constants.Me.HaveBuff("Renew"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 8f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Renew"
				}) && t.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseRenewTresh && BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash Heal", false), 9f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && Constants.Me.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseFlashTresh && BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Word: Pain", false), 11f, (IRotationAction s, WoWUnit t) => (Constants.Target.HealthPercent > (double)BasePersistentSettings<PriestLevelSettings>.Current.UseWandTresh || Constants.Me.ManaPercentage < 5U) && !t.HaveMyBuff(new string[]
				{
					"Shadow Word: Pain"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Smite", false), 12f, (IRotationAction s, WoWUnit t) => Constants.Target.HealthPercent > (double)BasePersistentSettings<PriestLevelSettings>.Current.UseWandTresh || Constants.Me.ManaPercentage < 5U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
