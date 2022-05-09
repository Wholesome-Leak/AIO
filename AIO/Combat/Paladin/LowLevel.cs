using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000BA RID: 186
	internal class LowLevel : BaseRotation
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001B3C4 File Offset: 0x000195C4
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent < 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 3f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Righteousness", false, 0, 0), 4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Might", false, 0, 0), 5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Devotion Aura", false, 0, 0), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Light", false), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
