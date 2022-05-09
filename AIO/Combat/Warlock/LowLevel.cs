using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x0200007D RID: 125
	internal class LowLevel : BaseRotation
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00012290 File Offset: 0x00010490
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseWand && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Immolate", false), 3f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Immolate"
				}) && SpellManager.KnowSpell("Curse of Agony"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Curse of Agony", false), 4f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Curse of Agony"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
