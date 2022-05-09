using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers;
using AIO.Helpers.Caching;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Priest
{
	// Token: 0x020000B0 RID: 176
	internal class Shadow : BaseRotation
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x000193F8 File Offset: 0x000175F8
		public Shadow() : base(true, false, false, BasePersistentSettings<PriestLevelSettings>.Current.CompletelySynthetic)
		{
			Shadow._healSpell = Shadow.FindCorrectHealSpell();
			Shadow._haveShadowWeaving = TalentsManager.HaveTalent(3, 12);
			bool flag = Constants.Me.Level < 20U;
			if (flag)
			{
				this.Rotation.Add(new RotationStep(new RotationSpell("Smite", false), 18.1f, (IRotationAction s, WoWUnit t) => true, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00019498 File Offset: 0x00017698
		protected sealed override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new DebugSpell("Pre-Calculations", 2.14748365E+09f, true), 0f, (IRotationAction action, WoWUnit unit) => Shadow.DoPreCalculations(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(new RotationSpell("Power Word: Shield", false), 1f, (IRotationAction s, WoWUnit t) => (BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp || !Constants.Me.CIsInGroup()) && Constants.Me.CHealthPercent() <= (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseShieldTresh && !Constants.Me.CHaveBuff("Power Word: Shield") && !Constants.Me.CHaveBuff("Weakened Soul"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(Shadow._healSpell, 2f, (IRotationAction s, WoWUnit t) => (BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp || !Constants.Me.CIsInGroup()) && Constants.Me.CHealthPercent() < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHealTresh, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new CancelableSpell("Flash Heal", (WoWUnit unit) => unit.CHealthPercent() > (double)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseFlashTresh + 10), false), 3f, (IRotationAction s, WoWUnit t) => ((BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp || !Constants.Me.CIsInGroup()) && Constants.Me.CHealthPercent() < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseFlashTresh) || (Constants.Me.CHealthPercent() < (double)Math.Min(BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseFlashTresh + 25, 99) && !Constants.Me.CHaveBuff("Shadowform")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 4f, (IRotationAction s, WoWUnit t) => (BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp || !Constants.Me.CIsInGroup()) && !Constants.Me.CHaveBuff("Shadowform") && Constants.Me.CHealthPercent() < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseRenewTresh && Constants.Me.CManaPercentage() > 40U && t.CBuffTimeLeft("Renew") < 1000, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Psychic Scream", false), 5f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (!Constants.Me.CIsInGroup() && Constants.Me.CHealthPercent() < 80.0)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.Target == Constants.Me.Guid && o.CGetDistance() <= 6f) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Power Word: Shield", false), 6f, (IRotationAction s, WoWUnit t) => (BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHeaInGrp || !Constants.Me.CIsInGroup()) && BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseShieldParty && t.CHealthPercent() < (double)BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseShieldTresh && t.GetCachedThreatSituation() > 1 && !t.CHaveBuff("Power Word: Shield") && !t.CHaveBuff("Weakened Soul"), delegate(Func<WoWUnit, bool> pred)
				{
					WoWUnit[] partyMembers = RotationFramework.PartyMembers;
					return partyMembers.CFindInRange((WoWUnit unit) => unit.CIsAlive() && pred(unit), 40f, 5);
				}, null, false, false, true));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 7f, (IRotationAction action, WoWUnit target) => target.IsEnemy() && target.IsAttackable && target.Target == Shadow._tank.Guid, (IRotationAction action) => Shadow._tank != null && (ulong)Constants.Me.CManaPercentage() < (ulong)((long)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowShadowfiend + 10)) && Shadow._tank.CInCombat(), delegate(Func<WoWUnit, bool> predicate)
				{
					WoWUnit tank = Shadow._tank;
					WoWUnit result;
					if (!predicate((tank != null) ? tank.TargetObject : null))
					{
						result = null;
					}
					else
					{
						WoWUnit tank2 = Shadow._tank;
						result = ((tank2 != null) ? tank2.TargetObject : null);
					}
					return result;
				}, null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 8f, (IRotationAction action, WoWUnit target) => target.IsEnemy() && target.IsAttackable, (IRotationAction action) => (ulong)Constants.Me.CManaPercentage() < (ulong)((long)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowShadowfiend + 10)), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.CGetHighestHpPartyMemberTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 9f, (IRotationAction action, WoWUnit target) => target.Target == Constants.Me.Guid && target.IsAttackable, (IRotationAction action) => (ulong)Constants.Me.CManaPercentage() < (ulong)((long)BasePersistentSettings<PriestLevelSettings>.Current.ShadowShadowfiend) && Constants.Me.CInCombat(), (Func<WoWUnit, bool> predicate) => RotationFramework.Enemies.FirstOrDefault(predicate), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Dispersion", false), 10f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if ((ulong)Constants.Me.CManaPercentage() <= (ulong)((long)BasePersistentSettings<PriestLevelSettings>.Current.ShadowDispersion) && !Constants.Me.CHaveBuff("Dispersion"))
					{
						WoWUnit pet = Constants.Pet;
						result = (((pet != null) ? pet.CreatedBySpell : 0) != 34433);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Shadowform", false), 11f, (IRotationAction s, WoWUnit t) => !Constants.Me.CHaveBuff("Shadowform"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Shoot", false), 12f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PriestLevelSettings>.Current.UseWand && (t.CHealthPercent() <= (double)BasePersistentSettings<PriestLevelSettings>.Current.UseWandTresh || Constants.Me.CManaPercentage() < 5U) && Extension.HaveRangedWeaponEquipped && !RotationCombatUtil.IsAutoRepeating("Shoot"), new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Mind Sear", false), 13f, delegate(IRotationAction action, WoWUnit target)
				{
					ushort num = 0;
					Vector3 positionWithoutType = target.PositionWithoutType;
					int num2 = Math.Min(10, RotationFramework.Enemies.Length);
					for (int i = 0; i < num2; i++)
					{
						WoWUnit woWUnit = RotationFramework.Enemies[i];
						bool flag = target.GetBaseAddress != woWUnit.GetBaseAddress && positionWithoutType.DistanceTo(woWUnit.PositionWithoutType) <= 11f;
						if (flag)
						{
							num += 1;
						}
						bool flag2 = num >= 2;
						if (flag2)
						{
							return true;
						}
					}
					return false;
				}, (IRotationAction action) => Constants.Me.CManaPercentage() > 65U && !Constants.Me.GetMove, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Vampiric Touch", false), 14f, (IRotationAction s, WoWUnit t) => t.CMyBuffTimeLeft("Vampiric Touch") < 1300, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Devouring Plague", false), 15f, (IRotationAction s, WoWUnit t) => (t.CHealthPercent() > 40.0 || (t.IsBoss && t.CHealthPercent() > 15.0)) && t.CMyBuffTimeLeft("Devouring Plague") < 2590, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Mind Blast", false), 16f, (IRotationAction s, WoWUnit t) => true, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Shadow Word: Pain", false), 17f, (IRotationAction action, WoWUnit target) => (!Shadow._haveShadowWeaving || Constants.Me.CBuffStack("Shadow Weaving") >= 5) && target.CMyBuffTimeLeft("Shadow Word: Pain") < 2800, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Mind Flay", false), 18f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseMindflay && (t.CHaveMyBuff("Shadow Word: Pain") || t.CHaveMyBuff("Devouring Plague")), (IRotationAction action) => !Constants.Me.GetMove, new Func<Func<WoWUnit, bool>, WoWUnit>(Shadow.CQuickBotTarget), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Vampiric Embrace", false), 19f, (IRotationAction s, WoWUnit t) => t.CBuffTimeLeft("Vampiric Embrace") < 300000, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				return list;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00019B7C File Offset: 0x00017D7C
		private static bool DoPreCalculations()
		{
			Cache.Reset();
			Shadow.PartyGuids.Clear();
			foreach (WoWPlayer woWPlayer in RotationFramework.PartyMembers)
			{
				Shadow.PartyGuids.Add(woWPlayer.Guid);
			}
			Shadow._tank = RotationCombatUtil.CFindTank((WoWUnit unit) => true);
			Shadow._target = ObjectManager.Target;
			WoWUnit target = Shadow._target;
			Shadow._targetAttackable = (target != null && target.IsAttackable);
			return false;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00019C1C File Offset: 0x00017E1C
		private static CancelableSpell FindCorrectHealSpell()
		{
			bool flag = SpellManager.KnowSpell("Greater Heal");
			CancelableSpell result;
			if (flag)
			{
				result = new CancelableSpell("Greater Heal", (WoWUnit unit) => unit.CHealthPercent() > (double)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHealTresh + 10), false);
			}
			else
			{
				CancelableSpell cancelableSpell;
				if (!SpellManager.KnowSpell("Heal"))
				{
					cancelableSpell = new CancelableSpell("Lesser Heal", (WoWUnit unit) => unit.CHealthPercent() > (double)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHealTresh + 10), false);
				}
				else
				{
					cancelableSpell = new CancelableSpell("Heal", (WoWUnit unit) => unit.CHealthPercent() > (double)(BasePersistentSettings<PriestLevelSettings>.Current.ShadowUseHealTresh + 10), false);
				}
				result = cancelableSpell;
			}
			return result;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019CCC File Offset: 0x00017ECC
		private static WoWUnit CQuickBotTarget(Func<WoWUnit, bool> predicate)
		{
			return (Shadow._target != null && Shadow._targetAttackable && predicate(Shadow._target)) ? Shadow._target : null;
		}

		// Token: 0x0400037D RID: 893
		private static readonly HashSet<ulong> PartyGuids = new HashSet<ulong>();

		// Token: 0x0400037E RID: 894
		private static CancelableSpell _healSpell;

		// Token: 0x0400037F RID: 895
		private static WoWUnit _tank;

		// Token: 0x04000380 RID: 896
		private static WoWUnit _target;

		// Token: 0x04000381 RID: 897
		private static bool _haveShadowWeaving;

		// Token: 0x04000382 RID: 898
		private static bool _targetAttackable;
	}
}
