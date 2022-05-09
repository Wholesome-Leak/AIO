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
	// Token: 0x020000EA RID: 234
	internal class FeralCombat : BaseRotation
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00021B5C File Offset: 0x0001FD5C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Innervate", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !Constants.Me.HaveBuff("Bear Form") && !Constants.Me.HaveBuff("Dire Bear Form") && !Constants.Me.HaveBuff("Cat Form") && (ulong)Constants.Me.ManaPercentage <= (ulong)((long)BasePersistentSettings<DruidLevelSettings>.Current.Innervate), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Innervate", false, 0, 0), 1.1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && (ulong)t.ManaPercentage <= (ulong)((long)BasePersistentSettings<DruidLevelSettings>.Current.Innervate), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindHeal), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Remove Curse", false), 1.1f, (IRotationAction s, WoWUnit t) => t.HaveBuff("Veil of Shadow") && BasePersistentSettings<DruidLevelSettings>.Current.FeralDecurse, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Barkskin", false, 0, 0), 1.2f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent <= 35.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Regrowth", false, 0, 0), 1.3f, (IRotationAction s, WoWUnit t) => ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue + DruidBehavior.RegrowthValue)) && Constants.Me.ManaPercentage > 15U && Constants.Me.HealthPercent <= 35.0) || ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue / 100 * 80 + DruidBehavior.RegrowthValue)) && Constants.Me.ManaPercentage > 15U && Constants.Me.HealthPercent <= 35.0 && TalentsManager.HaveTalent(2, 25) && Constants.Me.HaveBuff("Enrage")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Rejuvenation", false, 0, 0), 1.4f, (IRotationAction s, WoWUnit t) => ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue + DruidBehavior.RejuvenationValue)) && Constants.Me.ManaPercentage > 15U && Constants.Me.HaveBuff("Regrowth")) || ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue / 100 * 80 + DruidBehavior.RejuvenationValue)) && Constants.Me.ManaPercentage > 15U && Constants.Me.HaveBuff("Regrowth") && TalentsManager.HaveTalent(2, 25) && Constants.Me.HaveBuff("Enrage")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Touch", false), 1.5f, (IRotationAction s, WoWUnit t) => ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue + DruidBehavior.HealingTouchValue)) && !Constants.Me.IsInGroup && Constants.Me.ManaPercentage > 15U && Constants.Me.HealthPercent <= 35.0 && Constants.Me.HaveBuff("Regrowth")) || ((ulong)Constants.Me.Mana > (ulong)((long)(DruidBehavior.TransformValue / 100 * 80 + DruidBehavior.HealingTouchValue)) && !Constants.Me.IsInGroup && Constants.Me.ManaPercentage > 15U && Constants.Me.HealthPercent <= 35.0 && Constants.Me.HaveBuff("Regrowth") && TalentsManager.HaveTalent(2, 25) && Constants.Me.HaveBuff("Enrage")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Berserk", false), 1.6f, (IRotationAction s, WoWUnit t) => (Constants.Me.HaveBuff("Bear Form") || Constants.Me.HaveBuff("Dire Bear Form") || Constants.Me.HaveBuff("Cat Form")) && (t.IsElite || RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 20f) >= 2), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Bear Form", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.Level > 9U && Constants.Me.Level < 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bash", false), 2.1f, (IRotationAction s, WoWUnit t) => t.IsCasting() && Constants.Me.Level > 9U && Constants.Me.Level < 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Demoralizing Roar", false), 3f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Demoralizing Roar"
				}) && Constants.Me.Level > 9U && Constants.Me.Level < 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Swipe (Bear)", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.Level > 9U && Constants.Me.Level < 20U && t.GetDistance < 8f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Growl", false), 6f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.Level > 9U && Constants.Me.Level < 20U && t.GetDistance > 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Maul", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.Rage >= 16U && Constants.Me.Level > 9U && Constants.Me.Level < 20U && t.GetDistance < 8f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Moonfire", false), 8f, (IRotationAction s, WoWUnit t) => !SpellManager.KnowSpell("Bear Form") && Constants.Me.Level > 9U && Constants.Me.Level < 20U && t.HealthPercent == 100.0 && !t.IsTargetingMe, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wrath", false), 9f, (IRotationAction s, WoWUnit t) => !SpellManager.KnowSpell("Bear Form") && Constants.Me.Level > 9U && Constants.Me.Level < 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Regrowth", false), 10f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Predator's Swiftness") && Constants.Me.HealthPercent < 70.0 && Constants.Me.ManaPercentage > 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Dire Bear Form", false, 0, 0), 11f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 20f) >= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Bear Form", false, 0, 0), 12f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !SpellManager.KnowSpell("Dire Bear Form") && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 20f) >= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Faerie Fire (Feral)", false), 12.3f, (IRotationAction s, WoWUnit t) => (Constants.Me.HaveBuff("Bear Form") || Constants.Me.HaveBuff("Dire Bear Form")) && !Constants.Me.HaveBuff("Prowl") && !t.HaveMyBuff(new string[]
				{
					"Faerie Fire (Feral)"
				}) && BasePersistentSettings<DruidLevelSettings>.Current.FFF, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bash", false), 12.1f, (IRotationAction s, WoWUnit t) => t.IsCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Enrage", false), 12.2f, (IRotationAction s, WoWUnit t) => t.HealthPercent >= 35.0 && !Constants.Me.HaveBuff("Enrage"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Frenzied Regeneration", false, 0, 0), 13f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 60.0 && Constants.Me.Rage > 25U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mangle (Bear)", false), 14f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Mangle"
				}) && (Constants.Me.HaveBuff("Dire Bear Form") || Constants.Me.HaveBuff("Bear Form")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Maul", false), 15f, (IRotationAction s, WoWUnit t) => Constants.Me.Rage > 16U && (Constants.Me.HaveBuff("Dire Bear Form") || Constants.Me.HaveBuff("Bear Form")) && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 20f) >= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Demoralizing Roar", false), 16f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Demoralizing Shout") && !t.HaveMyBuff(new string[]
				{
					"Demoralizing Roar"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(t.Position) <= 20f) >= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Cat Form", false, 0, 0), 19f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && (!Constants.Me.HaveBuff("Bear Form") || !Constants.Me.HaveBuff("Dire Bear Form")) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 12f) <= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount - 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Cat Form", false, 0, 0), 19.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && (Constants.Me.HaveBuff("Bear Form") || Constants.Me.HaveBuff("Dire Bear Form")) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.Position.DistanceTo(t.Position) <= 12f) <= BasePersistentSettings<DruidLevelSettings>.Current.FeralBearCount - 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Cat Form", false, 0, 0), 19.2f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Prowl", false), 20f, (IRotationAction s, WoWUnit t) => t.HealthPercent > 99.0 && !Constants.Me.HaveBuff("Prowl") && !t.IsTargetingMe && BasePersistentSettings<DruidLevelSettings>.Current.Prowl, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Feral Charge - Cat", false), 21f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 15f && t.GetDistance <= 25f && Constants.Me.HaveBuff("Prowl") && !t.IsTargetingMe && Constants.Me.HaveBuff("Cat Form"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pounce", false), 22f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Prowl") && t.GetDistance <= 5f && !t.IsTargetingMe, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Faerie Fire (Feral)", false), 22.1f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<DruidLevelSettings>.Current.ForceFaerie && !t.HaveMyBuff(new string[]
				{
					"Faerie Fire (Feral)"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Ravage", false), 23f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Prowl") && Constants.Me.IsBehind(t.Position, 1.8f, 3.141593f) && t.GetDistance <= 6f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Faerie Fire (Feral)", false), 23.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Prowl") && !t.HaveMyBuff(new string[]
				{
					"Faerie Fire (Feral)"
				}) && BasePersistentSettings<DruidLevelSettings>.Current.FFF, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dash", false), 24f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Prowl") && !Constants.Me.HaveBuff("Dash") && BasePersistentSettings<DruidLevelSettings>.Current.Dash && Constants.Me.HaveBuff("Cat Form"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tiger's Fury", false, 0, 0), 25f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 92.0 && Constants.Me.ManaPercentage > 40U && !TalentsManager.HaveTalent(2, 25), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tiger's Fury", false, 0, 0), 26f, (IRotationAction s, WoWUnit t) => Constants.Me.ComboPoint <= 4 && t.HealthPercent >= 40.0 && BasePersistentSettings<DruidLevelSettings>.Current.TF && Constants.Me.HaveBuff("Cat Form") && !TalentsManager.HaveTalent(2, 25), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tiger's Fury", false, 0, 0), 26.1f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 92.0 && Constants.Me.ManaPercentage > 40U && TalentsManager.HaveTalent(2, 25) && Constants.Me.HaveBuff("Cat Form") && Constants.Me.Rage <= 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rake", false), 27f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Prowl") && Constants.Me.ComboPoint <= 4 && !t.HaveBuff("Rake") && (t.HealthPercent >= 35.0 || (t.HealthPercent >= 20.0 && BossList.isboss)) && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rip", false), 28f, (IRotationAction s, WoWUnit t) => Constants.Me.ComboPoint >= BasePersistentSettings<DruidLevelSettings>.Current.FBC && !t.HaveMyBuff(new string[]
				{
					"Rip"
				}) && t.HealthPercent >= (double)BasePersistentSettings<DruidLevelSettings>.Current.FeralRipHealth && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Ferocious Bite", false), 29f, (IRotationAction s, WoWUnit t) => Constants.Me.ComboPoint >= BasePersistentSettings<DruidLevelSettings>.Current.FBC, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mangle (Cat)", false), 30f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Prowl") && Constants.Me.ComboPoint <= 4 && BasePersistentSettings<DruidLevelSettings>.Current.TF && Constants.Me.HaveBuff("Cat Form"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Claw", false), 31f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Prowl"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000F279 File Offset: 0x0000D479
		public FeralCombat() : base(true, false, false, false)
		{
		}
	}
}
