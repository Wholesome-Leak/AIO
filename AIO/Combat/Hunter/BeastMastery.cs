using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000D7 RID: 215
	internal class BeastMastery : BaseRotation
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001EE0C File Offset: 0x0001D00C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Feign Death", false), 2f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && Constants.Me.HealthPercent < 50.0 && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.FD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Deterrence", false), 2.1f, (IRotationAction s, WoWUnit t) => t.IsTargetingMe && Constants.Me.HealthPercent < 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Intimidation", false), 3f, (IRotationAction s, WoWUnit t) => Constants.Pet.Target == Constants.Me.Target && Constants.Pet.Position.DistanceTo(t.Position) <= 6f && t.IsCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Concussive Shot", false), 3.1f, (IRotationAction s, WoWUnit t) => t.Fleeing && !t.HaveBuff("Concussive Shot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Misdirection", false), 3.2f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (BasePersistentSettings<HunterLevelSettings>.Current.BeastMasteryMisdirection && !Constants.Me.IsInGroup && !Constants.Me.HaveBuff("Misdirection") && Constants.Pet.IsAlive && t.IsMyPet)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit u) => u.IsTargetingMe) >= 1);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Misdirection", false), 3.3f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<HunterLevelSettings>.Current.BeastMasteryMisdirection && Constants.Me.IsInGroup && !Constants.Me.HaveBuff("Misdirection") && t.IsAlive, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Volley", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance && BasePersistentSettings<HunterLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Kill Shot", false), 5f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.HealthPercent < 20.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hunter's Mark", false), 9f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Hunter's Mark"
				}) && t.IsAlive && t.GetDistance >= 5f && t.HealthPercent > 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bestial Wrath", false), 10f, (IRotationAction s, WoWUnit t) => (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPet) >= 2 && !Constants.Me.IsInGroup) || (t.IsElite && !Constants.Me.IsInGroup) || (Constants.Me.IsInGroup && t.IsBoss), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rapid Fire", false), 11f, delegate(IRotationAction s, WoWUnit t)
				{
					if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPet) < BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance || Constants.Me.IsInGroup)
					{
						if ((RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember) < BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance || !Constants.Me.IsInGroup) && (!t.IsElite || Constants.Me.IsInGroup))
						{
							return Constants.Me.IsInGroup && t.IsBoss;
						}
					}
					return true;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Kill Command", false), 12f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Kill Command"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Serpent Sting", false), 13f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Serpent Sting"
				}) && (t.HealthPercent >= 70.0 || (t.IsBoss && t.HealthPercent >= 20.0)), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Shot", false), 14f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Multi-Shot", false), 15f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.GetDistance >= 5f && BasePersistentSettings<HunterLevelSettings>.Current.MultiS)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsAttackable && o.IsTargetingMeOrMyPetOrPartyMember) >= BasePersistentSettings<HunterLevelSettings>.Current.MultiSCount);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Steady Shot", false), 15.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.GetMove && t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Raptor Strike", false), 16f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Disengage", false), 17f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.Dis, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000F279 File Offset: 0x0000D479
		public BeastMastery() : base(true, false, false, false)
		{
		}
	}
}
