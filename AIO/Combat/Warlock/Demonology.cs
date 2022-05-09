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
	// Token: 0x02000077 RID: 119
	internal class Demonology : BaseRotation
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011948 File Offset: 0x0000FB48
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseWand && t.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Drain Soul", false), 2.5f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= 25.0 && ItemsHelper.GetItemCount("Soul Shard") <= 3, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Metamorphosis", false), 3f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Metamorphosis"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Demonic Empowerment", false), 4f, (IRotationAction s, WoWUnit t) => !Constants.Pet.HaveBuff("Demonic Empowerment") && Constants.Pet.IsAlive && Constants.Pet.IsMyPet, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Life Tap", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent > 50.0 && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<WarlockLevelSettings>.Current.Lifetap), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 8f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Seed of Corruption"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rain of Fire", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<WarlockLevelSettings>.Current.AOEInstance && BasePersistentSettings<WarlockLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Health Funnel", false), 7f, (IRotationAction s, WoWUnit t) => !Constants.Pet.HaveBuff("Health Funnel") && Constants.Pet.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.HealthfunnelPet && Constants.Me.HealthPercent > (double)BasePersistentSettings<WarlockLevelSettings>.Current.HealthfunnelMe && Constants.Pet.IsAlive && Constants.Pet.IsMyPet, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Immolate", false), 8f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Immolate"
				}) && !SpellManager.KnowSpell("Unstable Affliction"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 9f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Curse of Agony", false), 10f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Curse of Agony"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Drain Life", false), 12f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent < (double)BasePersistentSettings<WarlockLevelSettings>.Current.Drainlife, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 16f, (IRotationAction s, WoWUnit t) => t.HealthPercent > (double)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh && !BasePersistentSettings<WarlockLevelSettings>.Current.ShadowboltWand, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 17f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.ShadowboltWand, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000F279 File Offset: 0x0000D479
		public Demonology() : base(true, false, false, false)
		{
		}
	}
}
