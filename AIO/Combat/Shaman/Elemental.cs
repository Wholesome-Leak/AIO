using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x02000086 RID: 134
	internal class Elemental : BaseRotation
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00013480 File Offset: 0x00011680
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cure Toxins", false), 2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && (Constants.Me.HasDebuffType(new string[]
				{
					"Disease"
				}) || Constants.Me.HasDebuffType(new string[]
				{
					"Poison"
				})), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cure Toxins", false), 3f, (IRotationAction s, WoWUnit t) => (t.HasDebuffType(new string[]
				{
					"Disease"
				}) || t.HasDebuffType(new string[]
				{
					"Poison"
				})) && BasePersistentSettings<ShamanLevelSettings>.Current.ElementalCureToxin, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 4f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent < 40.0 && t.HealthPercent > 10.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wind Shear", false), 15f, (IRotationAction s, WoWUnit t) => t.IsCasting() && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flame Shock", false), 16f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<ShamanLevelSettings>.Current.ElementalFlameShock && !t.HaveMyBuff(new string[]
				{
					"Flame Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Earth Shock", false), 16.1f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<ShamanLevelSettings>.Current.ElementalEarthShock && !t.HaveMyBuff(new string[]
				{
					"Earth Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Elemental Mastery", false, 0, 0), 17f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lava Burst", false), 18f, (IRotationAction s, WoWUnit t) => t.HaveMyBuff(new string[]
				{
					"Flame Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chain Lightning", false), 19f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<ShamanLevelSettings>.Current.ElementalChainlightningTresshold, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 19.1f, (IRotationAction s, WoWUnit t) => !SpellManager.KnowSpell("Chain Lightning"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 20f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) <= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F279 File Offset: 0x0000D479
		public Elemental() : base(true, false, false, false)
		{
		}
	}
}
