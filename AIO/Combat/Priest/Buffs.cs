using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers.Caching;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Priest
{
	// Token: 0x020000A5 RID: 165
	internal class Buffs : BaseRotation
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000165A7 File Offset: 0x000147A7
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000165B4 File Offset: 0x000147B4
		internal Buffs(BaseCombatClass combatClass) : base(true, true, false, false)
		{
			this.CombatClass = combatClass;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000165C8 File Offset: 0x000147C8
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Inner Fire", false, 2, 0), 1f, (IRotationAction action, WoWUnit me) => !me.IsResting() && me.ManaPercentage > 7U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Power Word: Fortitude", false, 0, 0), 2f, (IRotationAction action, WoWUnit me) => this.Spec == "Holy" && !me.IsResting() && !me.CHaveBuff("Prayer of Fortitude") && !me.CHaveBuff("Holy Word: Fortitude"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Divine Spirit", false, 0, 0), 3f, (IRotationAction action, WoWUnit me) => this.Spec == "Holy" && !me.IsResting() && !me.CHaveBuff("Prayer of Spirit"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Shadow Protection", false, 0, 0), 4f, (IRotationAction action, WoWUnit me) => this.Spec == "Holy" && !me.IsResting() && !me.CHaveBuff("Prayer of Shadow Protection"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Vampiric Embrace", false, 0, 0), 5f, (IRotationAction action, WoWUnit me) => this.Spec == "Shadow" && !me.IsResting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Power Word: Fortitude", false, 0, 0), 6f, (IRotationAction action, WoWUnit me) => this.Spec == "Shadow" && !me.InCombatFlagOnly && !me.InCombat && !me.IsResting() && !me.CHaveBuff("Prayer of Fortitude") && !me.CHaveBuff("Holy Word: Fortitude"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Divine Spirit", false, 0, 0), 7f, (IRotationAction action, WoWUnit me) => this.Spec == "Shadow" && !me.InCombatFlagOnly && !me.InCombat && !me.IsResting() && !me.CHaveBuff("Prayer of Spirit"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Shadow Protection", false, 0, 0), 8f, (IRotationAction action, WoWUnit me) => this.Spec == "Shadow" && !me.InCombatFlagOnly && !me.InCombat && !me.IsResting() && !me.CHaveBuff("Prayer of Shadow Protection"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Shadowform", false, 0, 0), 9f, (IRotationAction action, WoWUnit me) => BasePersistentSettings<PriestLevelSettings>.Current.ShadowForm && !me.IsResting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}

		// Token: 0x040002FD RID: 765
		private readonly BaseCombatClass CombatClass;
	}
}
