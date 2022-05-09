using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x020000FD RID: 253
	internal class Frost : BaseRotation
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00025008 File Offset: 0x00023208
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Mind Freeze", false), 2f, (IRotationAction s, WoWUnit t) => t.IsCast, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Strangulate", false), 3f, (IRotationAction s, WoWUnit t) => t.IsCast && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Howling Blast", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Killing Machine") || Constants.Me.HaveBuff("Freezing Fog"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death and Decay", false), 5f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 15f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.DnD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icebound Fortitude", false), 6f, delegate(IRotationAction s, WoWUnit t)
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
				list.Add(new RotationStep(new RotationSpell("Frost Strike", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.RunicPower > 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icy Touch", false), 8f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Plague Strike", false), 9f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Blood Plague"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pestilence", false), 10f, delegate(IRotationAction s, WoWUnit t)
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
				list.Add(new RotationStep(new RotationSpell("Obliberate", false), 11f, (IRotationAction s, WoWUnit t) => t.HaveMyBuff(new string[]
				{
					"Blood Plague",
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Strike", false), 12f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) == BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heart Strike", false), 13f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.HearthStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Boil", false), 14f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) > BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodBoil, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Strike", false), 15f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0000F279 File Offset: 0x0000D479
		public Frost() : base(true, false, false, false)
		{
		}
	}
}
