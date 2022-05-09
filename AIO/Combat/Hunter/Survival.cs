using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000E0 RID: 224
	internal class Survival : BaseRotation
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00020674 File Offset: 0x0001E874
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Hunter's Mark", false), 6f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Hunter's Mark"
				}) && t.IsAlive && t.GetDistance >= 5f && t.HealthPercent > 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Kill Command", false), 6.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.HaveBuff("Kill Command"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Serpent Sting", false), 7f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Serpent Sting"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Black Arrow", false), 8f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Explosive Shot", false), 9f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Aimed Shot", false), 10f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Steady Shot", false), 11f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Raptor Strike", false), 12f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Disengage", false), 13f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.Dis, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Feign Death", false), 14f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f && Constants.Me.HealthPercent < 50.0 && t.IsTargetingMe && Constants.Pet.IsAlive && BasePersistentSettings<HunterLevelSettings>.Current.FD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000F279 File Offset: 0x0000D479
		public Survival() : base(true, false, false, false)
		{
		}
	}
}
