using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x0200007B RID: 123
	internal class Destruction : BaseRotation
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00011F6C File Offset: 0x0001016C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 2f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarlockLevelSettings>.Current.UseWand && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<WarlockLevelSettings>.Current.UseWandTresh) && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Drain Soul", false), 2.5f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= 25.0 && ItemsHelper.GetItemCount("Soul Shard") <= 3, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Curse of the Elements", false), 3f, (IRotationAction s, WoWUnit t) => !t.HaveBuff("Curse of the Elements"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Immolate", false), 4f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Immolate"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Corruption", false), 5f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Corruption"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chaos Bolt", false), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Incinerate", false), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Conflagrate", false), 8f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Bolt", false), 9f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000F279 File Offset: 0x0000D479
		public Destruction() : base(true, false, false, false)
		{
		}
	}
}
