using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers;
using AIO.Helpers.Caching;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warrior
{
	// Token: 0x0200006B RID: 107
	internal class Protection : BaseRotation
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new DebugSpell("Pre-Calculations", 2.14748365E+09f, true), 0f, (IRotationAction action, WoWUnit unit) => this.DoPreCalculations(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(new RotationSpell("Last Stand", false), 1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CHealthPercent() < 15.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Shield Wall", false), 2.11f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CInCombat() && Constants.Me.CHealthPercent() < 65.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Shield Block", false), 2.15f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.CHealthPercent() > 70.0 || t.IsElite || t.IsBoss)
					{
						result = this.EnemiesAttackingGroup.ContainsAtLeast((WoWUnit o) => o.CGetDistance() <= 10f && o.CIsTargetingMe(), BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionShieldBlock);
					}
					else
					{
						result = false;
					}
					return result;
				}, (IRotationAction _) => Constants.Me.CHealthPercent() < 90.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Enraged Regeneration", false), 2.151f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CHealthPercent() <= (double)BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionEnragedRegeneration && Constants.Me.CHaveBuff("Bloodrage") && Constants.Me.CBuffTimeLeft("Bloodrage") <= 2000, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Intercept", false), 2.16f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionIntercept && t.CGetDistance() > 8f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, true));
				list.Add(new RotationStep(new RotationSpell("Shield Bash", false), 2.17f, (IRotationAction s, WoWUnit t) => t.CCanInterruptCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Spell Reflection", false), 2.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => this.EnemiesAttackingGroup.Any((WoWUnit unit) => unit.CIsTargetingMe() && unit.CIsCast()), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Challenging Shout", false), 3.1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction _)
				{
					bool result;
					if (Constants.Me.CIsInGroup())
					{
						result = this.EnemiesAttackingGroup.ContainsAtLeast((WoWUnit o) => o.CGetDistance() <= 10f && !o.CIsTargetingMe(), 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 3.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CHaveBuff("Glyph of Revenge") && !RotationCombatUtil.IsCurrentSpell("Heroic Strike"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Taunt", false), 4f, (IRotationAction s, WoWUnit t) => !t.CIsTargetingMe(), (IRotationAction _) => BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionTauntGroup && Constants.Me.CIsInGroup(), new Func<Func<WoWUnit, bool>, WoWUnit>(this.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Taunt", false), 4.1f, (IRotationAction s, WoWUnit t) => !t.CIsTargetingMe(), (IRotationAction _) => !Constants.Me.CIsInGroup(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mocking Blow", false), 4.15f, (IRotationAction s, WoWUnit t) => !t.CIsTargetingMe() && t.CGetDistance() <= 8f, new Func<Func<WoWUnit, bool>, WoWUnit>(this.FindEnemyAttackingGroup), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shield Slam", false), 5.1f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CHaveBuff("Sword and Board"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Berserker Rage", false), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Shockwave", false), 8f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => this.EnemiesAttackingGroup.ContainsAtLeast((WoWUnit o) => o.CGetDistance() < 10f, 2), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Thunder Clap", false), 10f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => this.EnemiesAttackingGroup.Any((WoWUnit unit) => unit.CGetDistance() < 8f && !unit.CHaveMyBuff("Thunder Clap")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Revenge", false), 10.2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Shield Slam", false), 10.3f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Devastate", false), 10.4f, (IRotationAction s, WoWUnit t) => Constants.Me.CRage() > 70U || t.CMyBuffStack("Sunder Armor") < 5, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Bloodrage", false), 11f, (IRotationAction s, WoWUnit t) => Constants.Me.CRage() < 70U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cleave", false), 13f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction _)
				{
					bool result;
					if ((ulong)Constants.Me.CRage() > (ulong)((long)BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionCleaveRageCount) && !Constants.Me.CHaveBuff("Glyph of Revenge"))
					{
						result = this.EnemiesAttackingGroup.ContainsAtLeast((WoWUnit o) => o.CGetDistance() < 10f, BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionCleaveCount);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Concussion Blow", false), 15f, (IRotationAction s, WoWUnit t) => t.CHealthPercent() > 40.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Demoralizing Shout", false), 19f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => this.EnemiesAttackingGroup.ContainsAtLeast((WoWUnit unit) => unit.CGetDistance() < 15f && unit.CHealthPercent() > 65.0 && !unit.CHaveBuff("Demoralizing Shout") && !unit.CHaveBuff("Demoralizing Roar"), BasePersistentSettings<WarriorLevelSettings>.Current.ProtectionDemoralizingCount), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, false, false));
				list.Add(new RotationStep(new RotationSpell("Rend", false), 20f, (IRotationAction s, WoWUnit t) => t.CHealthPercent() > 50.0 && !t.CHaveMyBuff("Rend") && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Victory Rush", false), 21f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => !Constants.Me.CHaveBuff("Defensive Stance") && (Constants.Me.CHaveBuff("Battle Stance") || Constants.Me.CHaveBuff("Berserker Stance")), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 22f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CRage() >= 40U && Constants.Me.Level < 40U && !RotationCombatUtil.IsCurrentSpell("Heroic Strike"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 23f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction _) => Constants.Me.CRage() >= 80U && Constants.Me.Level >= 40U && !RotationCombatUtil.IsCurrentSpell("Heroic Strike"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTargetFast), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001063C File Offset: 0x0000E83C
		private bool DoPreCalculations()
		{
			bool flag = this.LimitExecutionSpeed(100);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Cache.Reset();
				this.EnemiesAttackingGroup = (from unit in RotationFramework.Enemies
				where unit.CIsTargetingMeOrMyPetOrPartyMember()
				select unit).ToArray<WoWUnit>();
				result = false;
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001069C File Offset: 0x0000E89C
		private bool LimitExecutionSpeed(int delay)
		{
			bool flag = this.watch.ElapsedMilliseconds > (long)delay;
			bool result;
			if (flag)
			{
				this.watch.Restart();
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000106D3 File Offset: 0x0000E8D3
		public WoWUnit FindEnemyAttackingGroup(Func<WoWUnit, bool> predicate)
		{
			return this.EnemiesAttackingGroup.FirstOrDefault(predicate);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000106E1 File Offset: 0x0000E8E1
		public Protection() : base(true, false, false, false)
		{
		}

		// Token: 0x04000207 RID: 519
		private WoWUnit[] EnemiesAttackingGroup = new WoWUnit[0];

		// Token: 0x04000208 RID: 520
		private Stopwatch watch = Stopwatch.StartNew();
	}
}
