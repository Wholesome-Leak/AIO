using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x02000075 RID: 117
	internal class Buffs : BaseRotation
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00011754 File Offset: 0x0000F954
		internal Buffs() : base(true, true, false, false)
		{
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00011764 File Offset: 0x0000F964
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Unending Breath", false, 0, 0), 1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Unending Breath", false, 0, 0), 2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Fel Armor", false, 0, 0), 3f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.WarlockSkin, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Demon Armor", false, 0, 0), 4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.WarlockSkin, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Demon Skin", false, 0, 0), 5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.WarlockSkin, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Soul Link", false, 0, 0), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lifetap", false), 7f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsResting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}
	}
}
