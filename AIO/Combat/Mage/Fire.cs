using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Mage
{
	// Token: 0x020000C6 RID: 198
	internal class Fire : BaseRotation
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<MageLevelSettings>.Current.UseWand && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Polymorph", false), 2.1f, delegate(IRotationAction s, WoWUnit t)
				{
					if (BasePersistentSettings<MageLevelSettings>.Current.Sheep && !t.IsMyTarget)
					{
						if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe) > 1)
						{
							if (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 40f && o.HaveBuff("Polymorph")) < 1 && !t.IsCreatureType("Elemental") && !t.IsCreatureType("Demon") && !t.IsCreatureType("Undead"))
							{
								return !t.IsCreatureType("Dragon");
							}
						}
					}
					return false;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyTargetingMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Frost Nova", false), 2.2f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 6f && t.HealthPercent > 30.0 && !Constants.Me.IsInGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Ice Block", false), 3f, (IRotationAction s, WoWUnit t) => (t.HealthPercent < 15.0 && !t.HaveMyBuff(new string[]
				{
					"Ice Barrier"
				})) || (Constants.Me.IsInGroup && Constants.Me.HealthPercent < 85.0), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Evocation", false), 3.5f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage < 35U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pyroblast", false), 4f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && Constants.Me.HaveBuff("Hot Streak") && t.HealthPercent > 10.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Living Bomb", false), 5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Living Bomb"
				}) && RotationFramework.Enemies.Count<WoWUnit>() >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flamestrike", false), 6f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<MageLevelSettings>.Current.FlamestrikeWithoutFire && !t.HaveMyBuff(new string[]
				{
					"Flamestrike"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<MageLevelSettings>.Current.FlamestrikeWithoutCountFire && BasePersistentSettings<MageLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blizzard", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<MageLevelSettings>.Current.AOEInstance && BasePersistentSettings<MageLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Scorch", false), 9f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && TalentsManager.HaveTalent(2, 11) && !t.HaveMyBuff(new string[]
				{
					"Improved Scorch"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blast Wave", false), 11f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && t.GetDistance < 7f && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 15f) > 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dragon's Breath", false), 12f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && t.GetDistance < 7f && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 15f) > 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Living Bomb", false), 13f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !t.HaveMyBuff(new string[]
				{
					"Living Bomb"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fire Blast", false), 14f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && t.HealthPercent < 10.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fireball", false), 15f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && (t.HealthPercent > 55.0 || BossList.isboss), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Scorch", false), 16f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && (t.HealthPercent < 35.0 || BossList.isboss), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000F279 File Offset: 0x0000D479
		public Fire() : base(true, false, false, false)
		{
		}
	}
}
