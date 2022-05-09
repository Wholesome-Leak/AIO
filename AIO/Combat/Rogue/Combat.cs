using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Rogue
{
	// Token: 0x0200009D RID: 157
	internal class Combat : BaseRotation
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000158F8 File Offset: 0x00013AF8
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Sprint", false), 2f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 15f && !BasePersistentSettings<RogueLevelSettings>.Current.PullRanged, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Kick", false), 3f, (IRotationAction s, WoWUnit t) => t.IsCasting() && t.GetDistance < 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Evasion", false), 3.1f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f && o.IsTargetingMe) >= BasePersistentSettings<RogueLevelSettings>.Current.Evasion || (Constants.Me.HealthPercent <= 30.0 && t.HealthPercent > 70.0), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Evasion", false), 3.2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Target.IsElite, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Riposte", false), 4f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Stealth"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blade Flurry", false), 2f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HealthPercent > 70.0 && !Constants.Me.HaveBuff("Stealth"))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<RogueLevelSettings>.Current.BladeFLurry);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Adrenaline Rush", false), 6f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (!Constants.Me.HaveBuff("Stealth"))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<RogueLevelSettings>.Current.AdrenalineRush);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Adrenaline Rush", false), 6.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Stealth") && Constants.Target.IsElite && !Constants.Me.IsInGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Slice and Dice", false), 7f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Slice and Dice") && Constants.Me.ComboPoint >= 1 && t.HealthPercent > 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Eviscerate", false), 8f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Stealth") && Constants.Me.ComboPoint >= BasePersistentSettings<RogueLevelSettings>.Current.Eviscarate, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Killing Spree", false), 9f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (!Constants.Me.HaveBuff("Adrenaline Rush") && !Constants.Me.HaveBuff("Blade Flurry") && !Constants.Me.HaveBuff("Stealth"))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<RogueLevelSettings>.Current.KillingSpree);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Sinister Strike", false), 10f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Stealth"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000F279 File Offset: 0x0000D479
		public Combat() : base(true, false, false, false)
		{
		}
	}
}
