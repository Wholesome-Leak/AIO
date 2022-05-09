using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers;
using AIO.Helpers.Caching;
using AIO.Lists;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Priest
{
	// Token: 0x020000A7 RID: 167
	internal class Holy : BaseRotation
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x000169CB File Offset: 0x00014BCB
		public Holy() : base(true, true, BasePersistentSettings<PriestLevelSettings>.Current.UseSyntheticCombatEvents, false)
		{
			Logging.Write("Loading FlXWare's Heal Rotation ...");
			Holy._slowHealSpell = Holy.FindCorrectSlowHealSpell();
			Holy._fastHealSpell = Holy.FindCorrectFastHealSpell();
			Holy._diseaseSpell = Holy.FindCorrectRemoveDiseaseSpell();
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00016A0C File Offset: 0x00014C0C
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new DebugSpell("Pre-Calculations", 2.14748365E+09f, false), 0f, (IRotationAction action, WoWUnit me) => Holy.DoPreCalculations(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Guardian Spirit", false), 1f, (IRotationAction action, WoWUnit tank) => tank.InCombat && tank.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyGuardianSpiritTresh && !tank.CHaveBuff("Guardian Spirit"), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, true, true, false));
				list.Add(new RotationStep(new RotationSpell("Guardian Spirit", false), 1.1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => !Holy._isSpirit && Constants.Me.InCombat && Constants.Me.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyGuardianSpiritTresh && !Constants.Me.CHaveBuff("Guardian Spirit"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(new RotationSpell("Fear Ward", false), 1.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => !Holy._isSpirit && BasePersistentSettings<PriestLevelSettings>.Current.HolyProtectAgainstFear && Constants.Me.BuffTimeLeft("Fear Ward") < 3000 && Holy.anyoneCastingFearSpellOnMe(Holy.CastingOnMeOrGroup), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(new DebugSpell("Abort", 2.14748365E+09f, false), 1.21f, (IRotationAction action, WoWUnit me) => Holy.HandleAbort(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Inner Focus", false), 1.3f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => !Constants.Me.CHaveBuff("Inner Focus") && Holy._castDivineHymn, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Hymn", false), 1.4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => Constants.Me.CHaveBuff("Spirit of Redemption") || Constants.Me.CHaveBuff("Inner Focus") || Holy._castDivineHymn, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fear Ward", false), 1.5f, (IRotationAction action, WoWUnit tank) => tank.BuffTimeLeft("Fear Ward") < 3000 && Holy.anyoneCastingFearSpellOnUnit(tank, Holy.CastingOnMeOrGroup), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyProtectAgainstFear, new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new DebugSpell("Do-NOP", 2.14748365E+09f, false), 1.6f, (IRotationAction action, WoWUnit unit) => Holy.DoNop(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Power Word: Shield", false, 0, 0), 1.7f, (IRotationAction action, WoWUnit me) => me.HealthPercent < 70.0 && !me.CHaveBuff("Weakened Soul") && !me.CHaveBuff("Power Word: Shield"), (IRotationAction action) => !Holy._isSpirit && Constants.Me.InCombat && (Constants.Me.CManaPercentage() > 50U || Constants.Me.HealthPercent < 40.0), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fade", false), 2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction action)
				{
					bool result;
					if (!Holy._isSpirit && Holy.EnemiesTargetingMe.Any<WoWUnit>() && !Constants.Me.CHaveBuff("Fade"))
					{
						result = (RotationCombatUtil.CCountAlivePartyMembers((WoWUnit partyMember) => true) > 0);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mind Soothe", false), 2.1f, (IRotationAction action, WoWUnit enemy) => !enemy.InCombat && !enemy.IsMyTarget && !enemy.PlayerControlled && enemy.IsAttackable && enemy.IsCreatureType("Humanoid") && enemy.BuffTimeLeft("Mind Soothe") < 1000 && enemy.GetDistance - (float)enemy.AggroDistance < (float)BasePersistentSettings<PriestLevelSettings>.Current.HolyMindSootheDistance, (IRotationAction action) => !Holy._isSpirit && Holy._shouldCastOffTank && Constants.Me.CManaPercentage() > 50U && BasePersistentSettings<PriestLevelSettings>.Current.HolyUseMindSoothe, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemy), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Psychic Scream", false), 2.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction action)
				{
					bool result;
					if (!Holy._isSpirit)
					{
						result = (RotationCombatUtil.CountEnemies((WoWUnit enemy) => enemy.IsTargetingMe && enemy.GetDistance < 8f && !enemy.CHaveBuff("Psychic Scream")) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shackle Undead", false), 2.3f, (IRotationAction action, WoWUnit enemy) => enemy.IsCreatureType("Undead") && enemy.GetDistance > enemy.CombatReach, (IRotationAction action) => !Holy._isSpirit && Constants.Me.CManaPercentage() > 50U && Holy._shouldCastOffTank && BasePersistentSettings<PriestLevelSettings>.Current.HolyShackleUndead && Holy.CanShackleNew(), (Func<WoWUnit, bool> predicate) => Holy.EnemiesTargetingMe.FirstOrDefault(predicate), null, false, true, false));
				list.Add(new RotationStep(new CancelableSpell("Prayer of Healing", (WoWUnit me) => !Holy.ShouldCastPrayerOfHealing(), false), 2.99f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank) && Holy.ShouldCastPrayerOfHealing(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(Holy._slowHealSpell, 3f, (IRotationAction action, WoWUnit tank) => tank.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal && ((Holy._groupInCombat && Constants.Me.BuffStack("Serendipity") >= 2) || (Holy._isSpirit && Constants.Me.BuffStack("Serendipity") >= 3) || !Holy._groupInCombat), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Power Word: Shield", false, 0, 0), 1.7f, (IRotationAction action, WoWUnit tank) => tank.HealthPercent < 70.0 && tank.InCombat && !tank.CHaveBuff("Weakened Soul") && !tank.CHaveBuff("Power Word: Shield"), (IRotationAction action) => Constants.Me.CManaPercentage() > 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(Holy._fastHealSpell, 3.01f, (IRotationAction action, WoWUnit tank) => tank.HealthPercent < 80.0, new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Binding Heal", false), 3.03f, (IRotationAction action, WoWUnit tank) => tank.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyBindingHealTresh, (IRotationAction action) => !Holy._isSpirit && Constants.Me.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyBindingHealTresh, new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 3.02f, (IRotationAction action, WoWUnit tank) => tank.InCombat && tank.HealthPercent < 95.0 && !tank.CHaveBuff("Renew"), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(Holy._slowHealSpell, 3.04f, (IRotationAction action, WoWUnit me) => !Holy._isSpirit && me.HealthPercent < (double)(BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal - 5) && ((Holy._groupInCombat && Constants.Me.BuffStack("Serendipity") >= 2) || !Holy._groupInCombat), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(Holy._fastHealSpell, 3.05f, (IRotationAction action, WoWUnit me) => !Holy._isSpirit && me.HealthPercent < 75.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Binding Heal", false), 3.06f, (IRotationAction action, WoWUnit partyMember) => partyMember.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyBindingHealTresh, (IRotationAction action) => !Holy._isSpirit && (Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank) && Constants.Me.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyBindingHealTresh, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 3.07f, (IRotationAction action, WoWUnit me) => !Holy._isSpirit && me.InCombat && me.HealthPercent < 95.0 && !Constants.Me.CHaveBuff("Renew"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Prayer of Mending", false), 3.09f, (IRotationAction action, WoWUnit partyMember) => partyMember.InCombat && partyMember.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyPrayerOfMendingTresh && RotationCombatUtil.CCountAlivePartyMembers((WoWUnit player) => player.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyPrayerOfMendingTresh && partyMember.Position.DistanceTo(player.Position) < 20f) >= 2, delegate(IRotationAction action)
				{
					bool result;
					if (Holy._shouldCastOffTank)
					{
						result = !RotationFramework.PartyMembers.Any((WoWPlayer player) => player.HaveMyBuff(new string[]
						{
							"Prayer of Mending"
						}));
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Circle of Healing", false), 3.1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction action)
				{
					bool result;
					if (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank)
					{
						result = (RotationCombatUtil.CCountAlivePartyMembers((WoWUnit partyMember) => partyMember.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyCircleOfHealingTresh && partyMember.GetDistance < 18f) > 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(Holy._slowHealSpell, 3.11f, (IRotationAction action, WoWUnit partyMember) => partyMember.HealthPercent < (double)(BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal - 15), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank || Constants.Me.BuffStack("Serendipity") >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(Holy._fastHealSpell, 3.12f, (IRotationAction action, WoWUnit partyMember) => partyMember.HealthPercent < 70.0 || (Constants.Me.CHaveBuff("Surge of Light") && partyMember.HealthPercent < 99.0), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Renew", false), 3.13f, (IRotationAction action, WoWUnit partyMember) => partyMember.InCombat && partyMember.HealthPercent < 90.0 && !partyMember.CHaveBuff("Renew"), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 4f, (IRotationAction action, WoWUnit target) => target.IsEnemy() && target.IsAttackable && target.Target == Holy._tank.Guid, (IRotationAction action) => Holy._groupInCombat && Holy._tank != null && Holy._tank.IsAlive && Constants.Me.CManaPercentage() < 40U, delegate(Func<WoWUnit, bool> predicate)
				{
					WoWUnit tank = Holy._tank;
					WoWUnit result;
					if (!predicate((tank != null) ? tank.TargetObject : null))
					{
						result = null;
					}
					else
					{
						WoWUnit tank2 = Holy._tank;
						result = ((tank2 != null) ? tank2.TargetObject : null);
					}
					return result;
				}, null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 4.1f, (IRotationAction action, WoWUnit target) => target.IsEnemy() && target.IsAttackable, (IRotationAction action) => Holy._groupInCombat && Constants.Me.CManaPercentage() < 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.CGetHighestHpPartyMemberTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadowfiend", false), 4.2f, (IRotationAction action, WoWUnit target) => target.IsEnemy() && target.IsAttackable, (IRotationAction action) => Constants.Me.InCombat && Constants.Me.CManaPercentage() < 30U, (Func<WoWUnit, bool> predicate) => Holy.EnemiesTargetingMe.FirstOrDefault<WoWUnit>(), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Hymn of Hope", false), 4.3f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction action)
				{
					bool result;
					if (Constants.Me.InCombat)
					{
						if (Constants.Me.CManaPercentage() >= 20U)
						{
							result = (RotationCombatUtil.CCountAlivePartyMembers((WoWUnit partyMember) => partyMember.ManaPercentage < 30U && partyMember.GetDistance < 40f && partyMember.HasMana()) > 2);
						}
						else
						{
							result = true;
						}
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Fear Ward", false), 5f, (IRotationAction action, WoWUnit partyMember) => partyMember.BuffTimeLeft("Fear Ward") < 3000 && Holy.anyoneCastingFearSpellOnUnit(partyMember, Holy.CastingOnMeOrGroup), (IRotationAction action) => Holy._tank == null && BasePersistentSettings<PriestLevelSettings>.Current.HolyProtectAgainstFear, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Prayer of Fortitude", false), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt && (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank) && ItemsManager.HasItemById(44615U) && RotationCombatUtil.CCountAlivePartyMembers(new Func<WoWUnit, bool>(Holy.CanGiveFortitude)) > 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Power Word: Fortitude", false), 6.1f, (IRotationAction action, WoWUnit partyMember) => BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt && Holy.CanGiveFortitude(partyMember), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Prayer of Spirit", false), 6.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank) && ItemsManager.HasItemById(44615U) && RotationCombatUtil.CCountAlivePartyMembers(new Func<WoWUnit, bool>(Holy.CanGiveSpirit)) > 2 && BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Divine Spirit", false), 6.3f, (IRotationAction action, WoWUnit partyMember) => BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt && Holy.CanGiveSpirit(partyMember), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Prayer of Shadow Protection", false), 6.4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank) && ItemsManager.HasItemById(44615U) && RotationCombatUtil.CCountAlivePartyMembers(new Func<WoWUnit, bool>(Holy.CanGiveShadowProtection)) > 2 && BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shadow Protection", false), 6.5f, (IRotationAction action, WoWUnit partyMember) => BasePersistentSettings<PriestLevelSettings>.Current.UseAutoBuffInt && Holy.CanGiveShadowProtection(partyMember), (IRotationAction action) => Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mass Dispel", false), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction action)
				{
					bool result;
					if (BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._isSpirit || Holy._ignoreManaManagementOoc || Holy._shouldCastOffTank))
					{
						result = (RotationFramework.PartyMembers.Count((WoWPlayer partyMember) => partyMember.HasDebuffType(new string[]
						{
							"Magic"
						})) >= 3);
					}
					else
					{
						result = false;
					}
					return result;
				}, (Func<WoWUnit, bool> predicate) => RotationCombatUtil.GetBestAoETarget(predicate, 30f, 15f, from partyMember in RotationFramework.PartyMembers
				where partyMember.HasDebuffType(new string[]
				{
					"Magic"
				})
				select partyMember, 3), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dispel Magic", false), 7.1f, (IRotationAction action, WoWUnit me) => me.HasDebuffType(new string[]
				{
					"Magic"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._ignoreManaManagementOoc || Constants.Me.CManaPercentage() > 30U), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(Holy._diseaseSpell, 7.2f, (IRotationAction action, WoWUnit me) => !Constants.Me.CHaveBuff("Abolish Disease") && me.HasDebuffType(new string[]
				{
					"Disease"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._ignoreManaManagementOoc || Constants.Me.CManaPercentage() > 30U), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dispel Magic", false), 7.3f, (IRotationAction action, WoWUnit tank) => tank.HasDebuffType(new string[]
				{
					"Magic"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._isSpirit || Holy._ignoreManaManagementOoc || Constants.Me.CManaPercentage() > 40U), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(Holy._diseaseSpell, 7.4f, (IRotationAction action, WoWUnit tank) => !tank.CHaveBuff("Abolish Disease") && tank.HasDebuffType(new string[]
				{
					"Disease"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._isSpirit || Holy._ignoreManaManagementOoc || Constants.Me.CManaPercentage() > 40U), new Func<Func<WoWUnit, bool>, WoWUnit>(Holy.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Dispel Magic", false), 7.5f, (IRotationAction action, WoWUnit partyMember) => partyMember.HasDebuffType(new string[]
				{
					"Magic"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._isSpirit || Holy._ignoreManaManagementOoc || (Holy._shouldCastOffTank && Constants.Me.CManaPercentage() > 40U)), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				list.Add(new RotationStep(Holy._diseaseSpell, 7.6f, (IRotationAction action, WoWUnit partyMember) => !partyMember.CHaveBuff("Abolish Disease") && partyMember.HasDebuffType(new string[]
				{
					"Disease"
				}), (IRotationAction action) => BasePersistentSettings<PriestLevelSettings>.Current.HolyDeDeBuff && (Holy._isSpirit || Holy._ignoreManaManagementOoc || (Holy._shouldCastOffTank && Constants.Me.CManaPercentage() > 40U)), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindExplicitPartyMember), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017BB1 File Offset: 0x00015DB1
		private static WoWUnit FindTank(Func<WoWUnit, bool> predicate)
		{
			return (Holy._tank != null && predicate(Holy._tank)) ? Holy._tank : null;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017BD0 File Offset: 0x00015DD0
		private static bool DoPreCalculations()
		{
			Holy.Reset();
			for (int i = 0; i < RotationFramework.Enemies.Length; i++)
			{
				WoWUnit woWUnit = RotationFramework.Enemies[i];
				bool isTargetingMe = woWUnit.IsTargetingMe;
				if (isTargetingMe)
				{
					Holy.EnemiesTargetingMe.AddLast(woWUnit);
				}
				bool flag = woWUnit.IsCast && woWUnit.IsTargetingMeOrMyPetOrPartyMember;
				if (flag)
				{
					Holy.CastingOnMeOrGroup.AddLast(woWUnit);
				}
			}
			WoWUnit tank;
			if ((tank = Holy.FindExplicitPartyMemberByName(BasePersistentSettings<PriestLevelSettings>.Current.HolyCustomTank)) == null)
			{
				tank = RotationCombatUtil.FindTank((WoWUnit unit) => true);
			}
			Holy._tank = tank;
			Holy._shouldCastOffTank = (Holy._tank == null || Holy._tank.IsDead || TraceLine.TraceLineGo(Holy._tank.Position) || (ulong)Constants.Me.CManaPercentage() > (ulong)((long)BasePersistentSettings<PriestLevelSettings>.Current.HolyOffTankCastingMana));
			Holy._groupInCombat = RotationFramework.PartyMembers.Any((WoWPlayer partyMember) => partyMember.InCombat);
			Holy._ignoreManaManagementOoc = (!Holy._groupInCombat && BasePersistentSettings<PriestLevelSettings>.Current.HolyIgnoreManaManagementOOC);
			Holy._isSpirit = Constants.Me.CHaveBuff("Spirit of Redemption");
			Holy._castDivineHymn = Holy.ShouldCastDivineHymn();
			return false;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017D29 File Offset: 0x00015F29
		private static bool HandleAbort()
		{
			return CancelableSpell.Check();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017D30 File Offset: 0x00015F30
		private static bool ShouldCastPrayerOfHealing()
		{
			return RotationCombatUtil.CCountAlivePartyMembers((WoWUnit partyMember) => partyMember.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyPrayerOfHealingTresh && partyMember.GetDistance < 36f) > 2;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017D69 File Offset: 0x00015F69
		private static void Reset()
		{
			Cache.Reset();
			Holy.CastingOnMeOrGroup.Clear();
			Holy.EnemiesTargetingMe.Clear();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017D88 File Offset: 0x00015F88
		private static WoWUnit FindExplicitPartyMemberByName(string name)
		{
			return RotationFramework.PartyMembers.FirstOrDefault((WoWPlayer partyMember) => partyMember.Name.ToLower().Equals(name.ToLower()));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00017DB8 File Offset: 0x00015FB8
		private static bool DoNop()
		{
			return Constants.Me.IsResting();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00017DC4 File Offset: 0x00015FC4
		private static bool anyoneCastingFearSpellOnMe(IEnumerable<WoWUnit> castingUnits)
		{
			return castingUnits.Any((WoWUnit enemy) => enemy.IsTargetingMe && SpecialSpells.FearInducingWithCast.Contains(enemy.CastingSpell.Name));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017DEC File Offset: 0x00015FEC
		private static bool anyoneCastingFearSpellOnUnit(WoWObject unit, IEnumerable<WoWUnit> castingUnits)
		{
			return castingUnits.Any((WoWUnit enemy) => enemy.Target == unit.Guid && SpecialSpells.FearInducingWithCast.Contains(enemy.CastingSpell.Name));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017E18 File Offset: 0x00016018
		private static bool CanGiveFortitude(WoWUnit unit)
		{
			return !unit.CHaveBuff("Power Word: Fortitude") && !unit.CHaveBuff("Prayer of Fortitude") && !unit.CHaveBuff("Holy Word: Fortitude");
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017E45 File Offset: 0x00016045
		private static bool CanGiveSpirit(WoWUnit unit)
		{
			return !unit.CHaveBuff("Divine Spirit") && !unit.CHaveBuff("Prayer of Spirit");
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017E65 File Offset: 0x00016065
		private static bool CanGiveShadowProtection(WoWUnit unit)
		{
			return !unit.CHaveBuff("Shadow Protection") && !unit.CHaveBuff("Prayer of Shadow Protection");
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017E85 File Offset: 0x00016085
		private static bool CanShackleNew()
		{
			return Holy.EnemiesTargetingMe.Any((WoWUnit enemy) => enemy.GetDistance > enemy.CombatReach && enemy.HaveMyBuff(new string[]
			{
				"Shackle Undead"
			}));
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017EB0 File Offset: 0x000160B0
		private static bool ShouldCastDivineHymn()
		{
			return RotationCombatUtil.CCountAlivePartyMembers((WoWUnit partyMember) => partyMember.GetDistance < 48f && partyMember.HealthPercent < (double)BasePersistentSettings<PriestLevelSettings>.Current.HolyDivineHymnTresh) > 2;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00017EDC File Offset: 0x000160DC
		private static CancelableSpell FindCorrectSlowHealSpell()
		{
			bool flag = SpellManager.KnowSpell("Greater Heal");
			CancelableSpell result;
			if (flag)
			{
				result = new CancelableSpell("Greater Heal", (WoWUnit unit) => unit.HealthPercent > (double)(BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal + 15), false);
			}
			else
			{
				CancelableSpell cancelableSpell;
				if (!SpellManager.KnowSpell("Heal"))
				{
					cancelableSpell = new CancelableSpell("Lesser Heal", (WoWUnit unit) => unit.HealthPercent > (double)(BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal + 15), false);
				}
				else
				{
					cancelableSpell = new CancelableSpell("Heal", (WoWUnit unit) => unit.HealthPercent > (double)(BasePersistentSettings<PriestLevelSettings>.Current.HolyBigSingleTargetHeal + 15), false);
				}
				result = cancelableSpell;
			}
			return result;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00017F8C File Offset: 0x0001618C
		private static CancelableSpell FindCorrectFastHealSpell()
		{
			CancelableSpell result;
			if (!SpellManager.KnowSpell("Flash Heal"))
			{
				result = new CancelableSpell("Lesser Heal", (WoWUnit unit) => unit.HealthPercent > 90.0, false);
			}
			else
			{
				result = new CancelableSpell("Flash Heal", (WoWUnit unit) => unit.HealthPercent > 90.0, false);
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00017FFB File Offset: 0x000161FB
		private static RotationSpell FindCorrectRemoveDiseaseSpell()
		{
			return SpellManager.KnowSpell("Abolish Disease") ? new RotationSpell("Abolish Disease", false) : new RotationSpell("Cure Disease", false);
		}

		// Token: 0x04000301 RID: 769
		private const bool IsDebug = true;

		// Token: 0x04000302 RID: 770
		private static readonly LinkedList<WoWUnit> CastingOnMeOrGroup = new LinkedList<WoWUnit>();

		// Token: 0x04000303 RID: 771
		private static readonly LinkedList<WoWUnit> EnemiesTargetingMe = new LinkedList<WoWUnit>();

		// Token: 0x04000304 RID: 772
		private static CancelableSpell _slowHealSpell;

		// Token: 0x04000305 RID: 773
		private static CancelableSpell _fastHealSpell;

		// Token: 0x04000306 RID: 774
		private static RotationSpell _diseaseSpell;

		// Token: 0x04000307 RID: 775
		private static bool _castDivineHymn;

		// Token: 0x04000308 RID: 776
		private static bool _groupInCombat;

		// Token: 0x04000309 RID: 777
		private static bool _ignoreManaManagementOoc;

		// Token: 0x0400030A RID: 778
		private static bool _shouldCastOffTank;

		// Token: 0x0400030B RID: 779
		private static bool _isSpirit;

		// Token: 0x0400030C RID: 780
		private static WoWUnit _tank;
	}
}
