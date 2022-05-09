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
	// Token: 0x02000082 RID: 130
	internal class Buffs : BaseRotation
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00012DEC File Offset: 0x00010FEC
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012DF9 File Offset: 0x00010FF9
		internal Buffs(BaseCombatClass combatClass, Totems totems) : base(true, false, BasePersistentSettings<ShamanLevelSettings>.Current.UseSyntheticCombatEvents, false)
		{
			this.CombatClass = combatClass;
			this.Totems = totems;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00012E20 File Offset: 0x00011020
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Water Shield", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => this.Spec == "Restoration" || this.Spec == "Elemental" || Constants.Me.ManaPercentage <= 50U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.ShamanShield, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Lightning Shield", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage > 50U || !SpellManager.KnowSpell("Water Shield"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.ShamanShield, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Totemic Recall", false), 10f, (IRotationAction s, WoWUnit t) => Totems.ShouldRecall() && !Totems.HasTemporary(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Call of the Elements", false), 11f, (IRotationAction s, WoWUnit t) => !MovementManager.InMovement && this.Totems.MissingDefaults() && !Totems.HasTemporary(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Mana Tide Totem", false), 20f, (IRotationAction s, WoWUnit t) => Constants.Me.ManaPercentage <= 30U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Earth Elemental Totem", false), 30f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !Totems.HasAny(new string[]
				{
					"Stoneclaw Totem"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 20f) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Stoneclaw Totem", false), 31f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !Totems.HasAny(new string[]
				{
					"Earth Elemental Totem"
				}) && RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <= 20f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Magma Totem", false), 40f, (IRotationAction s, WoWUnit t) => this.Spec == "Enhancement" && Constants.Target.GetDistance <= 15f && !Totems.HasAny(new string[]
				{
					"Magma Totem"
				}) && Constants.Me.ManaPercentage > 40U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cleansing Totem", false), 50f, delegate(IRotationAction s, WoWUnit t)
				{
					if (BasePersistentSettings<ShamanLevelSettings>.Current.UseCleansingTotem)
					{
						if (RotationFramework.PartyMembers.Count((WoWPlayer o) => o.HasDebuffType(new string[]
						{
							"Poison",
							"Disease"
						})) > 0)
						{
							return !Totems.HasAny(new string[]
							{
								"Cleansing Totem"
							});
						}
					}
					return false;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Tremor Totem", false), 51f, (IRotationAction s, WoWUnit t) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.HasDebuffType(new string[]
				{
					"Fear",
					"Charm",
					"Sleep"
				})) > 0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Grounding Totem", false), 52f, delegate(IRotationAction s, WoWUnit t)
				{
					bool result;
					if (BasePersistentSettings<ShamanLevelSettings>.Current.UseGroundingTotem)
					{
						result = (RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance < 30f && o.IsCast) > 0);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0400026E RID: 622
		private readonly BaseCombatClass CombatClass;

		// Token: 0x0400026F RID: 623
		private readonly Totems Totems;
	}
}
