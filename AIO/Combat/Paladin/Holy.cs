using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000B7 RID: 183
	internal class Holy : BaseRotation
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0001AD49 File Offset: 0x00018F49
		public Holy() : base(true, false, BasePersistentSettings<PaladinLevelSettings>.Current.UseSyntheticCombatEvents, false)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001AD60 File Offset: 0x00018F60
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new DebugSpell("Pre-Calculations", 2.14748365E+09f, false), 0f, (IRotationAction action, WoWUnit me) => Holy.DoPreCalculations(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Plea", false), 3f, (IRotationAction s, WoWUnit t) => (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.GeneralDivinePlea), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hand of Freedom", false), 4f, (IRotationAction s, WoWUnit t) => Constants.Me.Rooted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lay on Hands", false), 4.1f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PaladinLevelSettings>.Current.HolyLoH && t.HealthPercent < (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyLoHTresh && t.InCombat, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Purify", false), 5f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && (t.HasDebuffType(new string[]
				{
					"Disease"
				}) || t.HasDebuffType(new string[]
				{
					"Poison"
				})) && BasePersistentSettings<PaladinLevelSettings>.Current.HolyPurify, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Beacon of Light", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.InCombat && !t.HaveMyBuff(new string[]
				{
					"Beacon of Light"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Sacred Shield", false), 7f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= 99.0 && !t.HaveMyBuff(new string[]
				{
					"Sacred Shield"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Shock", false), 8f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyHS, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 9f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyHL, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Holy Light", false), 9.1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyHL, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 10f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyFL, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Flash of Light", false), 10.1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && t.HealthPercent <= (double)BasePersistentSettings<PaladinLevelSettings>.Current.HolyFL, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Judgement of Light", false), 11f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Judgement of Light"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001B148 File Offset: 0x00019348
		private static WoWUnit FindTank(Func<WoWUnit, bool> predicate)
		{
			return (Holy._tank != null && predicate(Holy._tank)) ? Holy._tank : null;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001B168 File Offset: 0x00019368
		private static WoWUnit FindExplicitPartyMemberByName(string name)
		{
			return RotationFramework.PartyMembers.FirstOrDefault((WoWPlayer partyMember) => partyMember.Name.ToLower().Equals(name.ToLower()));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B198 File Offset: 0x00019398
		private static bool DoPreCalculations()
		{
			WoWUnit tank;
			if ((tank = Holy.FindExplicitPartyMemberByName(BasePersistentSettings<PaladinLevelSettings>.Current.HolyCustomTank)) == null)
			{
				tank = RotationCombatUtil.FindTank((WoWUnit unit) => true);
			}
			Holy._tank = tank;
			return false;
		}

		// Token: 0x040003B7 RID: 951
		private static WoWUnit _tank;
	}
}
