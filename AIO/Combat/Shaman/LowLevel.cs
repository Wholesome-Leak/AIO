using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x02000093 RID: 147
	internal class LowLevel : BaseRotation
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00014550 File Offset: 0x00012750
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 40.0 && t.HealthPercent > 10.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 3f, (IRotationAction s, WoWUnit t) => !Constants.Me.InCombatFlagOnly && t.HealthPercent == 100.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage > 50U && t.GetDistance > 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Earth Shock", false), 5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Earth Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
