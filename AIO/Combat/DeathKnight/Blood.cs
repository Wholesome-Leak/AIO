using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x020000F8 RID: 248
	internal class Blood : BaseRotation
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00024098 File Offset: 0x00022298
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Dark Command", false), 1f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (Constants.Me.IsInGroup && BasePersistentSettings<DeathKnightLevelSettings>.Current.DarkCommand)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => !o.IsTargetingMe && o.IsTargetingPartyMember) >= 1);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Grip", false), 1.1f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (BasePersistentSettings<DeathKnightLevelSettings>.Current.DeathGrip && Constants.Me.IsInGroup)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => !o.IsTargetingMe && o.IsTargetingPartyMember) >= 1);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mind Freeze", false), 2f, (IRotationAction s, WoWUnit t) => t.IsCast, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Strangulate", false), 2.1f, (IRotationAction s, WoWUnit t) => t.IsCast && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Grip", false), 2.2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<DeathKnightLevelSettings>.Current.DeathGrip && t.IsCast && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Anti Magic Shell", false), 3.1f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsCast && o.IsTargetingMe) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Vampiric Blood", false), 3.2f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent <= 30.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Empower Rune Weapon", false), 3.5f, (IRotationAction s, WoWUnit t) => Constants.Me.RunesReadyCount() <= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chains of Ice", false), 3.7f, (IRotationAction s, WoWUnit t) => t.Fleeing, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death and Decay", false), 4f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 15f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.DnD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Tap", false), 4.1f, (IRotationAction s, WoWUnit t) => Constants.Me.RuneIsReady(1) || Constants.Me.RuneIsReady(2), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icebound Fortitude", false), 5.1f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (Constants.Me.HealthPercent < 80.0)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.GetDistance <= 8f) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mark of Blood", false), 6f, (IRotationAction s, WoWUnit t) => BossList.isboss || (t.IsElite && Constants.Me.IsInGroup), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dancing Rune Weapon", false), 7f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (!BossList.isboss && (!t.IsElite || Constants.Me.IsInGroup))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 2 && !Constants.Me.IsInGroup);
					}
					else
					{
						result = true;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Grip", false), 8.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && t.IsAttackable && !t.IsTargetingMe && t.IsMyTarget && !TraceLine.TraceLineGo(Constants.Me.Position, t.Position, 337) && t.GetDistance >= 7f && BasePersistentSettings<DeathKnightLevelSettings>.Current.DeathGrip, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icy Touch", false), 10f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Plague Strike", false), 11f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Blood Plague"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pestilence", false), 12f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HaveMyBuff(new string[]
					{
						"Blood Plague",
						"Frost Fever"
					}))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 15f && !o.HaveMyBuff(new string[]
						{
							"Blood Plague",
							"Frost Fever"
						})) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Strike", false), 13f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) == BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heart Strike", false), 14f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.HearthStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Boil", false), 15f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) > BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodBoil, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Strike", false), 16f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Coil", false), 17f, (IRotationAction s, WoWUnit t) => Constants.Me.RunicPower >= 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000F279 File Offset: 0x0000D479
		public Blood() : base(true, false, false, false)
		{
		}
	}
}
