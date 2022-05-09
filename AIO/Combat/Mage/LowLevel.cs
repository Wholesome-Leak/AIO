using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Mage
{
	// Token: 0x020000CF RID: 207
	internal class LowLevel : BaseRotation
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001E3A4 File Offset: 0x0001C5A4
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<MageLevelSettings>.Current.UseWand && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fire Blast", false), 2.1f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fireball", false), 3f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh) && !SpellManager.KnowSpell("Frostbolt"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Frostbolt", false), 4f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage > (ulong)((long)BasePersistentSettings<MageLevelSettings>.Current.UseWandTresh), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
