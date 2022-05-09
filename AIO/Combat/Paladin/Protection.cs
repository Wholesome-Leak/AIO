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
	// Token: 0x020000BE RID: 190
	internal class Protection : BaseRotation
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001B6EC File Offset: 0x000198EC
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Lay on Hands", false), 1f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.ProtectionLoH && !Constants.Me.HaveBuff("Forbearance"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Consecration", false), 2f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HealthPercent > 25.0)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 15f) >= BasePersistentSettings<PaladinLevelSettings>.Current.GeneralConsecration);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Righteous Defense", false), 3f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.Name != Constants.Me.Name)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsAttackable && !o.IsTargetingMe && o.IsTargetingPartyMember) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Reckoning", false), 4f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 25f && !t.IsTargetingMe && !Constants.Me.IsInGroup && BasePersistentSettings<PaladinLevelSettings>.Current.RetributionHOR, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Reckoning", false), 4.5f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (Constants.Me.IsInGroup)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.IsAttackable && !o.IsTargetingMe && o.IsTargetingPartyMember) >= 1);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Plea", false), 5f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.GeneralDivinePlea), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Freedom", false), 5.5f, (IRotationAction s, WoWUnit t) => Constants.Me.Rooted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 6f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent <= 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Protection", false), 7f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.GetDistance <= 15f) >= 3 || BossList.isboss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Justice", false), 8f, (IRotationAction s, WoWUnit t) => t.HealthPercent >= 75.0 && RotationCombatUtil.EnemyAttackingCountCluster(20) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Avenger's Shield", false), 9f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Salvation", false), 9.1f, (IRotationAction s, WoWUnit t) => t.InCombatFlagOnly && t.HealthPercent < 99.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindHeal), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Protection", false), 9.2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PaladinLevelSettings>.Current.ProtectionHoP && t.HealthPercent < 75.0 && (t.WowClass == 8 || t.WowClass == 9 || t.WowClass == 5 || t.WowClass == 11), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Avenger's Shield", false), 10f, (IRotationAction s, WoWUnit t) => t.GetDistance <= 25f && Constants.Me.ManaPercentage > 20U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Avenging Wrath", false), 11f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 20f) >= 3 && BasePersistentSettings<PaladinLevelSettings>.Current.AvengingWrathProtection, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Light", false), 12f, (IRotationAction s, WoWUnit t) => !SpellManager.KnowSpell("Judgement of Wisdom"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Wisdom", false), 13f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Judgement of Wisdom"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Light", false), 13.1f, (IRotationAction s, WoWUnit t) => t.HaveBuff("Judgement of Wisdom"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of Wrath", false), 14f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 20.0 && Constants.Me.ManaPercentage > 50U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hammer of the Righteous", false), 16f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shield of Righteousness", false), 17f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Shield", false), 18f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000F279 File Offset: 0x0000D479
		public Protection() : base(true, false, false, false)
		{
		}
	}
}
