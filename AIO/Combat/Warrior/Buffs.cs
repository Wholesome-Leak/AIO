using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warrior
{
	// Token: 0x02000065 RID: 101
	internal class Buffs : BaseRotation
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000F2DD File Offset: 0x0000D4DD
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F2EA File Offset: 0x0000D4EA
		internal Buffs(BaseCombatClass combatClass) : base(true, true, false, false)
		{
			this.CombatClass = combatClass;
			this.KnowStance = SpellManager.KnowSpell("Berserker Stance");
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000F310 File Offset: 0x0000D510
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Vigilance", false, 0, 0), 1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindHeal), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Battle Shout", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Greater Blessing of Might"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Defensive Stance", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Battle Stance", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => this.Spec == "Arms" || (this.Spec == "Fury" && !this.KnowStance), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Berserker Stance", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => this.Spec == "Fury", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}

		// Token: 0x040001E9 RID: 489
		private readonly BaseCombatClass CombatClass;

		// Token: 0x040001EA RID: 490
		private bool KnowStance;
	}
}
