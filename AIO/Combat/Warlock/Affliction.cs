using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x0200006F RID: 111
	internal class Affliction : BaseRotation
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00010D10 File Offset: 0x0000EF10
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseWand && t.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Drain Soul", false), 2.5f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= 25.0 && ItemsHelper.GetItemCount("Soul Shard") <= 3, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Demonic Empowerment", false), 3f, (IRotationAction s, WoWUnit t) => !Constants.Pet.HaveBuff("Demonic Empowerment") && Constants.Pet.IsAlive && Constants.Pet.IsMyPet, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Life Tap", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent > 50.0 && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<WarlockLevelSettings>.Current.Lifetap), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Seed of Corruption", false), 4.4f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseSeedGroup && Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Seed of Corruption"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroupAndMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 8f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseCorruptionGroup && Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroupAndMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rain of Fire", false), 4.5f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Shadow Trance"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Health Funnel", false), 6f, (IRotationAction s, WoWUnit t) => !Constants.Pet.HaveBuff("Health Funnel") && Constants.Pet.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.HealthfunnelPet && Constants.Me.HealthPercent > (double)BasePersistentSettings<WarlockLevelSettings>.Current.HealthfunnelMe && Constants.Pet.IsAlive && Constants.Pet.IsMyPet, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Haunt", false), 7.5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Haunt"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 8.1f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEOutsideInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOEOutside, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroupAndMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Curse of Agony", false), 10f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Curse of Agony"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 11f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Drain Life", false), 12f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.Drainlife, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Unstable Affliction", false), 13f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Unstable Affliction"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Immolate", false), 14f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Immolate"
				}) && !SpellManager.KnowSpell("Unstable Affliction"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 16f, (IRotationAction s, WoWUnit t) => t.HealthPercent > (double)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh && !BasePersistentSettings<WarlockLevelSettings>.Current.ShadowboltWand, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 17f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.ShadowboltWand, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F279 File Offset: 0x0000D479
		public Affliction() : base(true, false, false, false)
		{
		}
	}
}
