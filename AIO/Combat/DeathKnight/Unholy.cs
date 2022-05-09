using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x020000FF RID: 255
	internal class Unholy : BaseRotation
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00025618 File Offset: 0x00023818
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Raise Dead", false), 2f, (IRotationAction s, WoWUnit t) => !Constants.Pet.IsAlive && Constants.Me.RunicPower > 80U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mind Freeze", false), 3.1f, (IRotationAction s, WoWUnit t) => t.IsCasting(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Strangulate", false), 4f, (IRotationAction s, WoWUnit t) => t.IsCasting() && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindEnemyCasting), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death and Decay", false), 5f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 15f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.DnD, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Icy Touch", false), 6f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Plague Strike", false), 7f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Blood Plague"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Pestilence", false), 8f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (t.HaveMyBuff(new string[]
					{
						"Blood Plague",
						"Frost Fever"
					}))
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 15f && !o.HaveMyBuff(new string[]
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
				list.Add(new RotationStep(new RotationSpell("Blood Strike", false), 9f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) == BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heart Strike", false), 10f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= BasePersistentSettings<DeathKnightLevelSettings>.Current.HearthStrike, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Boil", false), 11f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) > BasePersistentSettings<DeathKnightLevelSettings>.Current.BloodBoil, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Strike", false), 12f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 50.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Scourge Strike", false), 13f, (IRotationAction s, WoWUnit t) => t.HaveMyBuff(new string[]
				{
					"Blood Plague",
					"Frost Fever"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Death Coil", false), 14f, (IRotationAction s, WoWUnit t) => Constants.Me.RunicPower > 80U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Blood Strike", false), 15f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0000F279 File Offset: 0x0000D479
		public Unholy() : base(true, false, false, false)
		{
		}
	}
}
