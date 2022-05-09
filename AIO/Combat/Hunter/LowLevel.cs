using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000DA RID: 218
	internal class LowLevel : BaseRotation
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001F86C File Offset: 0x0001DA6C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hunter's Mark", false), 2f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Hunter's Mark"
				}) && t.IsAlive && t.GetDistance >= 5f && t.HealthPercent > 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Serpent Sting", false), 3f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f && !t.HaveMyBuff(new string[]
				{
					"Serpent Sting"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Shot", false), 4f, (IRotationAction s, WoWUnit t) => t.GetDistance >= 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Raptor Strike", false), 5f, (IRotationAction s, WoWUnit t) => t.GetDistance < 5f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
