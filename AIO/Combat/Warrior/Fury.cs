using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warrior
{
	// Token: 0x02000067 RID: 103
	internal class Fury : BaseRotation
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Pummel", false), 2f, (IRotationAction s, WoWUnit t) => t.IsCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hamstring", false), 3f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Hamstring") && t.HealthPercent < 40.0 && t.CreatureTypeTarget == "Humanoid" && !BossList.isboss && BasePersistentSettings<WarriorLevelSettings>.Current.Hamstring, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Piercing Howl", false), 4f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HealthPercent < 40.0)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 3);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bloodrage", false), 5f, (IRotationAction s, WoWUnit t) => t.GetDistance < 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Slam", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Slam!"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bloodthirst", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.Rage > 30U && Constants.Me.HealthPercent <= 80.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Wish", false), 8f, (IRotationAction s, WoWUnit t) => Constants.Me.Rage > 10U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Execute", false), 9f, (IRotationAction s1, WoWUnit t) => t.HealthPercent < 20.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Victory Rush", false), 10f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rend", false), 11f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Rend"
				}) && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Intercept", false), 12f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarriorLevelSettings>.Current.FuryCharge && Constants.Me.Rage > 10U && t.GetDistance > 7f && t.GetDistance <= 24f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Charge", false), 13f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarriorLevelSettings>.Current.FuryCharge && t.GetDistance > 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Thunder Clap", false), 14f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Whirlwind", false), 15f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cleave", false), 16f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 17f, (IRotationAction s, WoWUnit t) => Constants.Me.Rage > 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000F279 File Offset: 0x0000D479
		public Fury() : base(true, false, false, false)
		{
		}
	}
}
