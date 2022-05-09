using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Mage
{
	// Token: 0x020000C2 RID: 194
	internal class Arcane : BaseRotation
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001CA58 File Offset: 0x0001AC58
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<MageLevelSettings>.Current.UseWand && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icy Veins", false), 3f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(36032U) >= 1 && t.IsBoss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Power", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(36032U) >= 1 && t.IsBoss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mirror Image", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(36032U) >= 1 && t.IsBoss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Presence of Mind", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(36032U) >= 2 && t.IsBoss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Missiles", false), 7f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && Constants.Me.BuffStack(36032U) >= 3 && Constants.Me.HaveBuff(44401U), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Arcane Blast", false), 8f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000F279 File Offset: 0x0000D479
		public Arcane() : base(true, false, false, false)
		{
		}
	}
}
