using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
	// Token: 0x020000F6 RID: 246
	internal class Restoration : BaseRotation
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00023AA3 File Offset: 0x00021CA3
		public Restoration() : base(true, false, BasePersistentSettings<DruidLevelSettings>.Current.UseSyntheticCombatEvents, false)
		{
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00023ABC File Offset: 0x00021CBC
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tree of Life", false, 0, 0), 1.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Tree of Life"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Innervate", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage <= 15U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wild Growth", false), 2.1f, (IRotationAction s, WoWUnit t) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.IsAlive && o.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationWildGrowth && o.GetDistance <= 40f) >= BasePersistentSettings<DruidLevelSettings>.Current.RestorationWildGrowthCount, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Abolish Poison", false), 5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Abolish Poison"
				}) && t.HasDebuffType(new string[]
				{
					"Poison"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Remove Curse", false), 6f, (IRotationAction s, WoWUnit t) => t.HasDebuffType(new string[]
				{
					"Curse"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Swiftmend", false), 6.1f, (IRotationAction s, WoWUnit t) => t.HaveMyBuff(new string[]
				{
					"Rejuvenation"
				}) || t.HaveMyBuff(new string[]
				{
					"Regrowth"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Nature's Swiftness", false), 8f, (IRotationAction s, WoWUnit t) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.IsAlive && o.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationHealingTouch && o.GetDistance <= 40f) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Touch", false), 9f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationHealingTouch, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Lifebloom", false, 3, 2000), 10f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationLifebloom, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Nourish", false), 11f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationNourish, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Regrowth", false), 12f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationRegrowth, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Rejuvenation", false, 0, 0), 13f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<DruidLevelSettings>.Current.RestorationRejuvenation, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				return list;
			}
		}
	}
}
