using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x0200008D RID: 141
	internal class Enhancement : BaseRotation
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00013DD0 File Offset: 0x00011FD0
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
				list.Add(new RotationStep(new RotationSpell("Cure Toxins", false), 3f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && (t.HasDebuffType(new string[]
				{
					"Disease"
				}) || t.HasDebuffType(new string[]
				{
					"Poison"
				})) && BasePersistentSettings<ShamanLevelSettings>.Current.EnhanceCureToxinGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(53817U) >= 4 && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) == 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chain Lightning", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.BuffStack(53817U) >= 4 && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fire Nova", false), 5.1f, (IRotationAction s, WoWUnit t) => Totems.HasAny(new string[]
				{
					"Magma Totem",
					"Searing Totem",
					"Flametongue Totem"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 10f) >= BasePersistentSettings<ShamanLevelSettings>.Current.EnhFireNova, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 6f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && Constants.Me.HealthPercent < 40.0 && t.HealthPercent > 10.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shamanistic Rage", false), 9f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Wind Shear", false), 10f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage >= 40U && t.IsCasting() && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Feral Spirit", false), 11f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 20f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 22f, (IRotationAction s, WoWUnit t) => !Constants.Me.InCombatFlagOnly && t.HealthPercent == 100.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Stormstrike", false), 23f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flame Shock", false), 24f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Flame Shock"
				}) && t.HealthPercent > 15.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Earth Shock", false), 25f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Earth Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lava Lash", false), 26f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F279 File Offset: 0x0000D479
		public Enhancement() : base(true, false, false, false)
		{
		}
	}
}
