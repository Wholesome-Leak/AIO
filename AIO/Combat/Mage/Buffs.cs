using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Mage
{
	// Token: 0x020000C4 RID: 196
	internal class Buffs : BaseRotation
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00011754 File Offset: 0x0000F954
		internal Buffs() : base(true, true, false, false)
		{
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001CD98 File Offset: 0x0001AF98
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Molten Armor", false, 0, 0), 1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.MageArmor, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Mage Armor", false, 0, 0), 2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.MageArmor, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Ice Armor", false, 0, 0), 3f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.MageArmor, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Frost Armor", false, 0, 0), 4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.MageArmor, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Arcane Intellect", false, 0, 0), 5f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Fel Intelligence") && !t.HaveBuff("Arcane Brilliance"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Arcane Intellect", false, 0, 0), 6f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Fel Intelligence") && !t.HaveBuff("Arcane Brilliance"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Combustion", false, 0, 0), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}
	}
}
