using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000B5 RID: 181
	internal class HealOOC : BaseRotation
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x00013A42 File Offset: 0x00011C42
		internal HealOOC() : base(false, true, false, false)
		{
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001AB28 File Offset: 0x00018D28
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent < 80.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 80.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 3f, (IRotationAction s, WoWUnit t) => t.HealthPercent < 60.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 60.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Plea", false), 5f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.GeneralDivinePlea) && BasePersistentSettings<PaladinLevelSettings>.Current.DivinePleaOOC, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}
	}
}
