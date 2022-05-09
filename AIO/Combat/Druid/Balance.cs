using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
	// Token: 0x020000E2 RID: 226
	internal class Balance : BaseRotation
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00020A30 File Offset: 0x0001EC30
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Regrowth", false), 1.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Regrowth") && Constants.Me.HealthPercent < (double)BasePersistentSettings<DruidLevelSettings>.Current.BalanceRegrowth, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Touch", false), 1.2f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < (double)BasePersistentSettings<DruidLevelSettings>.Current.BalanceHealingTouch, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Innervate", false, 0, 0), 1.3f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.Name == RotationFramework.HealName && (ulong)t.ManaPercentage < (ulong)((long)BasePersistentSettings<DruidLevelSettings>.Current.Innervate), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Innervate", false, 0, 0), 1.4f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage <= (ulong)((long)BasePersistentSettings<DruidLevelSettings>.Current.Innervate), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Moonkin Form", false), 2f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Moonkin Form"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Barkskin", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 20f) >= 2 || (!Constants.Me.IsInGroup && Constants.Me.InCombat), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Abolish Poison", false), 3.2f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Abolish Poison"
				}) && t.HaveImportantPoison(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Remove Curse", false), 3.3f, (IRotationAction s, WoWUnit t) => t.HaveImportantCurse(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Typhoon", false), 4f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.GetDistance < 7f)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit u) => u.HealthPercent >= 20.0 && u.IsTargetingMe) > 1);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Starfall", false), 5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction s) => BossList.isboss || (Constants.Me.IsInGroup && RotationCombatUtil.EnemyAttackingCountCluster(30) > 2 && BasePersistentSettings<DruidLevelSettings>.Current.UseStarfall), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Starfall", false), 6.1f, delegate(IRotationAction s, WoWUnit t)
				{
					if (!Constants.Me.IsInGroup && t.HealthPercent >= 50.0 && SpellManager.GetSpellCooldownTimeLeft(33831U) > 1)
					{
						if (RotationFramework.AllUnits.Count((WoWUnit u) => u.IsMyPet) <= 0)
						{
							if (RotationFramework.AllUnits.Count((WoWUnit o) => o.IsAlive && o.Reaction <= 3 && o.IsTargetingMeOrMyPet) >= 2)
							{
								return BasePersistentSettings<DruidLevelSettings>.Current.UseStarfall;
							}
						}
					}
					return false;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Force of Nature", false), 6.5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction s) => BossList.isboss || (Constants.Me.IsInGroup && RotationCombatUtil.EnemyAttackingCountCluster(30) >= BasePersistentSettings<DruidLevelSettings>.Current.AOEInstance), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Force of Nature", false), 6.6f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (!Constants.Me.IsInGroup && t.HealthPercent >= 50.0)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hurricane", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<DruidLevelSettings>.Current.AOEInstance && BasePersistentSettings<DruidLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Starfire", false), 8f, (IRotationAction s, WoWUnit t) => t.HealthPercent == 100.0 && !t.IsTargetingMe, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Insect Swarm", false), 9f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Insect Swarm"
				}) && (t.Health > 35L || t.IsBoss) && !Constants.Me.HaveBuff("Eclipse (Lunar)") && !Constants.Me.HaveBuff("Eclipse (Solar)"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Faerie Fire", false), 0f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Faerie Fire") && t.IsBoss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Moonfire", false), 13f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Moonfire"
				}) && (t.HealthPercent >= 60.0 || t.IsBoss) && !Constants.Me.HaveBuff("Eclipse (Lunar)") && !Constants.Me.HaveBuff("Eclipse (Solar)"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Starfire", false), 14f, (IRotationAction s, WoWUnit t) => (t.HealthPercent >= 10.0 && !Constants.Me.HaveBuff("Eclipse (Solar)") && Constants.Me.HaveBuff("Nature's Grace")) || Constants.Me.HaveBuff("Eclipse (Lunar)"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wrath", false), 15f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Eclipse (Solar)"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wrath", false), 16f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Eclipse (Solar)") && !Constants.Me.HaveBuff("Eclipse (Lunar)"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000F279 File Offset: 0x0000D479
		public Balance() : base(true, false, false, false)
		{
		}
	}
}
