using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x0200008A RID: 138
	internal class HealOOC : BaseRotation
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00013A42 File Offset: 0x00011C42
		internal HealOOC() : base(false, true, false, false)
		{
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00013A50 File Offset: 0x00011C50
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Riptide", false), 1f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<ShamanLevelSettings>.Current.HealOOC && t.HealthPercent < 95.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<ShamanLevelSettings>.Current.HealOOC && t.HealthPercent < 70.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				return list;
			}
		}
	}
}
