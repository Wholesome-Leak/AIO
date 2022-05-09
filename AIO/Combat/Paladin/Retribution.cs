using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000C0 RID: 192
	internal class Retribution : BaseRotation
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001C07C File Offset: 0x0001A27C
		private bool UseConsecration(IRotationAction s, WoWUnit t)
		{
			int num = RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f);
			return num >= BasePersistentSettings<PaladinLevelSettings>.Current.GeneralConsecration && (Constants.Me.Level >= 43U || t.HealthPercent > 25.0);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001C0EC File Offset: 0x0001A2EC
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Divine Plea", false), 1.1f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.GeneralDivinePlea), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Freedom", false), 1.2f, (IRotationAction s, WoWUnit t) => Constants.Me.Rooted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Protection", false), 1.3f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit u) => u.IsTargetingMe) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Purify", false), 2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && (Constants.Me.HasDebuffType(new string[]
				{
					"Disease"
				}) || Constants.Me.HasDebuffType(new string[]
				{
					"Poison"
				})), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Purify", false), 3f, (IRotationAction s, WoWUnit t) => (t.HasDebuffType(new string[]
				{
					"Disease"
				}) || t.HasDebuffType(new string[]
				{
					"Poison"
				})) && BasePersistentSettings<PaladinLevelSettings>.Current.RetributionPurify, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 4f, (IRotationAction s, WoWUnit t) => (!Constants.Me.IsInGroup && Constants.Me.HaveBuff("The Art of War") && Constants.Me.HealthPercent <= 60.0) || (Constants.Me.IsInGroup && Constants.Me.HaveBuff("The Art of War") && Constants.Me.HealthPercent <= 60.0 && BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHealInCombat), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Justice", false), 5f, (IRotationAction s, WoWUnit t) => t.IsCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Justice", false), 5.1f, (IRotationAction s, WoWUnit t) => t.Fleeing, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Justice", false), 6f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 5f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Wrath", false), 7f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 20.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Wrath", false), 8f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 9f, (IRotationAction s, WoWUnit t) => (!Constants.Me.IsInGroup && Constants.Me.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHL) || (Constants.Me.IsInGroup && Constants.Me.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHL && BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHealInCombat), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 10f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.RetributionFL, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Reckoning", false), 11f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 25f && !t.IsTargetingMe && !Constants.Me.IsInGroup && BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHOR, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Avenger's Shield", false), 12f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 25f && Constants.Me.ManaPercentage > 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Avenging Wrath", false), 13f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 20f) >= 3 && BasePersistentSettings<PaladinLevelSettings>.Current.AvengingWrathRetribution, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Light", false), 14f, (IRotationAction s, WoWUnit t) => !SpellManager.KnowSpell("Judgement of Wisdom"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Wisdom", false), 15f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Storm", false), 16f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Crusader Strike", false), 17f, (IRotationAction s, WoWUnit t) => true, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Consecration", false), 18f, new Func<IRotationAction, WoWUnit, bool>(this.UseConsecration), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Exorcism", false), 19f, (IRotationAction s, WoWUnit t) => (Constants.Me.HaveBuff("The Art of War") && (t.HealthPercent > 10.0 || BossList.isboss)) || !TalentsManager.HaveTalent(3, 17), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Exorcism", false), 20f, (IRotationAction s, WoWUnit t) => !TalentsManager.HaveTalent(3, 17), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Wrath", false), 21f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000F279 File Offset: 0x0000D479
		public Retribution() : base(true, false, false, false)
		{
		}
	}
}
