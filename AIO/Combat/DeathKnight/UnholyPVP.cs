using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Helpers;
using AIO.Helpers.Caching;
using AIO.Lists;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x02000101 RID: 257
	internal class UnholyPVP : BaseRotation
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00025BC4 File Offset: 0x00023DC4
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new DebugSpell("Pre-Calculations", 2.14748365E+09f, false), 0f, (IRotationAction action, WoWUnit me) => UnholyPVP.DoPreCalculations(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mind Freeze", false), 1f, (IRotationAction s, WoWUnit t) => t.IsCast, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Strangulate", false), 1.1f, (IRotationAction s, WoWUnit t) => t.IsCast && t.IsTargetingMe && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Grip", false), 1.2f, (IRotationAction s, WoWUnit t) => t.IsCast && t.IsTargetingMe && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lichborne", false), 2f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction action) => UnholyPVP.anyoneCastingFearSpellOnMe(UnholyPVP.CastingOnMeOrGroup), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, true, false, false));
				list.Add(new RotationStep(new RotationSpell("Anti-Magic Shell", false), 2.5f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsCast && o.IsTargetingMe) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Anti-Magic Zone", false), 2.6f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.IsCast && o.IsTargetingMe) >= 1 && !RotationFramework.SpellReady(48707U), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icebound Fortitude", false), 2.7f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 70.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Raise Dead", false), 3f, (IRotationAction s, WoWUnit t) => !Constants.Pet.IsAlive && Constants.Me.RunicPower > 80U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chains of Ice", false), 3.1f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Plague Strike", false), 3.2f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Blood Plague"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Strike", false), 3.3f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Scourge Strike", false), 3.4f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent >= 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Empower Rune Weapon", false), 3.5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Summon Gargoyle", false), 4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pestilence", false), 5f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HaveMyBuff(new string[]
					{
						"Blood Plague",
						"Frost Fever"
					}))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f && !o.HaveMyBuff(new string[]
						{
							"Blood Plague",
							"Frost Fever"
						})) >= 2);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Strike", false), 5.1f, (IRotationAction s, WoWUnit t) => t.HaveMyBuff(new string[]
				{
					"Blood Plague",
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Coil", false), 6f, (IRotationAction s, WoWUnit t) => Constants.Me.RunicPower > 80U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00026109 File Offset: 0x00024309
		private static bool anyoneCastingFearSpellOnMe(IEnumerable<WoWUnit> castingUnits)
		{
			return castingUnits.Any((WoWUnit enemy) => enemy.IsTargetingMe && SpecialSpells.FearInducingWithCast.Contains(enemy.CastingSpell.Name));
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00026130 File Offset: 0x00024330
		private static bool DoPreCalculations()
		{
			UnholyPVP.Reset();
			for (int i = 0; i < RotationFramework.Enemies.Length; i++)
			{
				WoWUnit woWUnit = RotationFramework.Enemies[i];
				bool isTargetingMe = woWUnit.IsTargetingMe;
				if (isTargetingMe)
				{
					UnholyPVP.EnemiesTargetingMe.AddLast(woWUnit);
				}
				bool flag = woWUnit.IsCast && woWUnit.IsTargetingMe;
				if (flag)
				{
					UnholyPVP.CastingOnMeOrGroup.AddLast(woWUnit);
				}
			}
			return false;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000261A4 File Offset: 0x000243A4
		private static void Reset()
		{
			Cache.Reset();
			UnholyPVP.CastingOnMeOrGroup.Clear();
			UnholyPVP.EnemiesTargetingMe.Clear();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0000F279 File Offset: 0x0000D479
		public UnholyPVP() : base(true, false, false, false)
		{
		}

		// Token: 0x0400055D RID: 1373
		private static readonly LinkedList<WoWUnit> CastingOnMeOrGroup = new LinkedList<WoWUnit>();

		// Token: 0x0400055E RID: 1374
		private static readonly LinkedList<WoWUnit> EnemiesTargetingMe = new LinkedList<WoWUnit>();
	}
}
