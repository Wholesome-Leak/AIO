using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Paladin
{
	// Token: 0x020000B3 RID: 179
	internal class Buffs : BaseRotation
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001A354 File Offset: 0x00018554
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001A361 File Offset: 0x00018561
		internal Buffs(BaseCombatClass combatClass) : base(true, true, false, false)
		{
			this.CombatClass = combatClass;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001A378 File Offset: 0x00018578
		private bool IsBoM(IRotationAction s, WoWUnit t)
		{
			WoWClass wowClass = t.WowClass;
			WoWClass woWClass = wowClass;
			return woWClass - 1 <= 3 || woWClass == 6;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001A3A8 File Offset: 0x000185A8
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Seal of Command", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => this.Spec == "Retribution" && BasePersistentSettings<PaladinLevelSettings>.Current.Sealret == "Seal of Command", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Righteousness", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => this.Spec == "Retribution" && BasePersistentSettings<PaladinLevelSettings>.Current.Sealret == "Seal of Righteousness", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Justice", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => this.Spec == "Retribution" && BasePersistentSettings<PaladinLevelSettings>.Current.Sealret == "Seal of Justice", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Righteous Fury", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Wisdom", false, 0, 0), 5f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection" && (ulong)Constants.Me.ManaPercentage < (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.ProtectionSoW), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Wisdom", false, 0, 0), 5.1f, (IRotationAction s, WoWUnit t) => this.Spec == "Holy", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Light", false, 0, 0), 6f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection" && (ulong)Constants.Me.ManaPercentage >= (ulong)((long)BasePersistentSettings<PaladinLevelSettings>.Current.ProtectionSoL), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Seal of Righteousness", false, 0, 0), 7f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection" && !SpellManager.KnowSpell("Seal of Light"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Might", false, 0, 0), 7.1f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && this.Spec == "Retribution" && !Constants.Me.HaveBuff("Battle Shout"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Might", false, 0, 0), 7.2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && this.Spec == "Protection" && !SpellManager.KnowSpell("Blessing of Sanctuary"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Crusader Aura", false, 0, 0), 7.3f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsOnTaxi && Constants.Me.IsMounted && BasePersistentSettings<PaladinLevelSettings>.Current.Crusader, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Retribution Aura", false, 0, 0), 8f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PaladinLevelSettings>.Current.Aura == "Retribution Aura", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Devotion Aura", false, 0, 0), 9f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PaladinLevelSettings>.Current.Aura == "Devotion Aura", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Concentration Aura", false, 0, 0), 10f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PaladinLevelSettings>.Current.Aura == "Concentration Aura", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Sanctuary", false, 0, 0), 12f, (IRotationAction s, WoWUnit t) => this.Spec == "Protection", new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.PaladinBlessing, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Wisdom", false, 0, 0), 13f, (IRotationAction s, WoWUnit t) => t.WowClass != 7, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindHeal), Exclusive.PaladinBlessing, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Might", false, 0, 0), 14f, new Func<IRotationAction, WoWUnit, bool>(this.IsBoM), (IRotationAction s) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.WowClass == 1) == 0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), Exclusive.PaladinBlessing, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Kings", false, 0, 0), 15f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), Exclusive.PaladinBlessing, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Wisdom", false, 0, 0), 16f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), Exclusive.PaladinBlessing, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Blessing of Might", false, 0, 0), 17f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), Exclusive.PaladinBlessing, false, true, false));
				return list;
			}
		}

		// Token: 0x040003A8 RID: 936
		private readonly BaseCombatClass CombatClass;
	}
}
