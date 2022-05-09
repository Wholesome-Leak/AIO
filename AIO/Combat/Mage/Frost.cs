using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Mage
{
	// Token: 0x020000CC RID: 204
	internal class Frost : BaseRotation
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001D96C File Offset: 0x0001BB6C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mana Shield", false), 1.1f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent <= 60.0 && Constants.Me.ManaPercentage >= 30U && !Constants.Me.HaveBuff("Mana Shield"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<MageLevelSettings>.Current.UseWand && RotationFramework.Enemies.Count<WoWUnit>() == 1 && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Polymorph", false), 2.1f, delegate(IRotationAction s, WoWUnit t)
				{
					if (BasePersistentSettings<MageLevelSettings>.Current.Sheep && !t.IsMyTarget)
					{
						if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe) > 1)
						{
							if (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 30f && o.HaveBuff("Polymorph")) < 1 && !t.IsCreatureType("Elemental") && !t.IsCreatureType("Demon") && !t.IsCreatureType("Undead"))
							{
								return !t.IsCreatureType("Dragon");
							}
						}
					}
					return false;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyTargetingMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Frost Nova", false), 2.1f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 6f && t.HealthPercent > 30.0 && !Constants.Me.IsInGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Ice Barrier", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 95.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Ice Block", false), 4f, (IRotationAction s, WoWUnit t) => (t.HealthPercent < 15.0 && !t.HaveMyBuff(new string[]
				{
					"Ice Barrier"
				})) || (Constants.Me.IsInGroup && Constants.Me.HealthPercent < 85.0), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cold Snap", false), 5f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 95.0 && !t.HaveMyBuff(new string[]
				{
					"Ice Barrier"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Counterspell", false), 6f, (IRotationAction s, WoWUnit t) => t.IsCast, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blizzard", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<MageLevelSettings>.Current.AOEInstance && BasePersistentSettings<MageLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Frostfire Bolt", false), 8f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveMyBuff(new string[]
				{
					"Fireball!"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cold Snap", false), 9f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Ice Barrier") && Constants.Me.HealthPercent < 95.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Evocation", false), 10f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 15.0 && RotationFramework.Enemies.Count<WoWUnit>() >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mirror Image", false), 11f, (IRotationAction s, WoWUnit t) => (!Constants.Me.IsInGroup && RotationFramework.Enemies.Count<WoWUnit>() >= 3) || BossList.isboss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icy Veins", false), 12f, (IRotationAction s, WoWUnit t) => (!Constants.Me.IsInGroup && RotationFramework.Enemies.Count<WoWUnit>() >= 2) || BossList.isboss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Summon Water Elemental", false), 13f, (IRotationAction s, WoWUnit t) => (!Constants.Me.IsInGroup && RotationFramework.Enemies.Count<WoWUnit>() >= 2) || BossList.isboss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Deep Freeze", false), 14f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Ice Lance", false), 15f, (IRotationAction s, WoWUnit t) => ((ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && Constants.Me.HaveBuff("Fingers of Frost")) || t.HaveMyBuff(new string[]
				{
					"Frost Nova"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fireball", false), 16f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !SpellManager.KnowSpell("Frostbolt"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fire Blast", false), 17f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && t.HealthPercent < (double)BasePersistentSettings<MageLevelSettings>.Current.FrostFireBlast && !t.HaveBuff("Frost Nova"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Frostbolt", false), 18f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000F279 File Offset: 0x0000D479
		public Frost() : base(true, false, false, false)
		{
		}
	}
}
