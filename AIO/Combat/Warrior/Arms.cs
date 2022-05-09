using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warrior
{
	// Token: 0x02000063 RID: 99
	internal class Arms : BaseRotation
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000F114 File Offset: 0x0000D314
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Execute", false), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Sudden Death"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rend", false), 3f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Rend"
				}) && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Overpower", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.HaveBuff("Taste for Blood"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mortal Strike", false), 5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F279 File Offset: 0x0000D479
		public Arms() : base(true, false, false, false)
		{
		}
	}
}
