using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000DC RID: 220
	internal class Marksmanship : BaseRotation
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Feign Death", false), 1f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && Constants.Me.HealthPercent < 50.0 && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.FD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Silencing Shot", false), 6f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.IsCast, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Rapid Fire", false, 0, 0), 7f, delegate(IRotationAction s, WoWUnit t)
				{
					if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPet) < 3 || Constants.Me.IsInGroup)
					{
						if ((RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember) < BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance || !Constants.Me.IsInGroup) && (!t.IsElite || Constants.Me.IsInGroup))
						{
							return Constants.Me.IsInGroup && t.IsBoss;
						}
					}
					return true;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Readiness", false), 8f, delegate(IRotationAction s, WoWUnit t)
				{
					if (!Constants.Me.HaveBuff("Rapid Fire"))
					{
						if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPet) >= 3 && !Constants.Me.IsInGroup)
						{
							goto IL_B5;
						}
					}
					if ((RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember) < BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance || !Constants.Me.IsInGroup) && (!t.IsElite || Constants.Me.IsInGroup))
					{
						return Constants.Me.IsInGroup && t.IsBoss;
					}
					IL_B5:
					return true;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Kill Shot", false), 9f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.HealthPercent < 20.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Volley", false), 9.1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<HunterLevelSettings>.Current.AOEInstance && BasePersistentSettings<HunterLevelSettings>.Current.UseAOE, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hunter's Mark", false), 10f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Hunter's Mark"
				}) && t.IsAlive && t.GetDistance >= 5f && t.HealthPercent > 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Viper Sting", false), 11f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.HasMana() && Constants.Me.ManaPercentage <= 45U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Serpent Sting", false), 12f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Serpent Sting"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chimera Shot", false), 13f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.HaveMyBuff(new string[]
				{
					"Serpent Sting",
					"Viper Sting"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Shot", false), 14f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && BasePersistentSettings<HunterLevelSettings>.Current.MarksmanArcaneShot, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Multi-Shot", false), 15f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.GetDistance >= 5f && BasePersistentSettings<HunterLevelSettings>.Current.MultiS)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember) >= BasePersistentSettings<HunterLevelSettings>.Current.MultiSCount);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Aimed Shot", false), 15.1f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && BasePersistentSettings<HunterLevelSettings>.Current.MarksmanAimedShot, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Steady Shot", false), 16f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && t.HaveMyBuff(new string[]
				{
					"Serpent Sting",
					"Viper Sting"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Raptor Strike", false), 17f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Disengage", false), 18f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.Dis, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000F279 File Offset: 0x0000D479
		public Marksmanship() : base(true, false, false, false)
		{
		}
	}
}
