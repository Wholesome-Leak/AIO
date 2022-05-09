using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x020000FA RID: 250
	internal class Buffs : BaseRotation
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00011754 File Offset: 0x0000F954
		internal Buffs() : base(true, true, false, false)
		{
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00024C18 File Offset: 0x00022E18
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Blood Presence", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<DeathKnightLevelSettings>.Current.Presence == "Blood", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Frost Presence", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<DeathKnightLevelSettings>.Current.Presence == "Frost", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Unholy Presence", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<DeathKnightLevelSettings>.Current.Presence == "Unholy", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Horn of Winter", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Bone Shield", false, 0, 0), 5f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}
	}
}
