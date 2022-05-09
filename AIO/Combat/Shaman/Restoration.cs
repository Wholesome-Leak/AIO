using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x02000095 RID: 149
	internal class Restoration : BaseRotation
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00014732 File Offset: 0x00012932
		private static WoWUnit _tank
		{
			get
			{
				return RotationCombatUtil.FindTank((WoWUnit unit) => true);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014758 File Offset: 0x00012958
		public Restoration() : base(true, false, BasePersistentSettings<ShamanLevelSettings>.Current.UseSyntheticCombatEvents, false)
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00014770 File Offset: 0x00012970
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Attack", false), 1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Nature's Swiftness", false, 0, 0), 4f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction s) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.IsAlive && o.HealthPercent <= 50.0 && o.GetDistance <= 40f) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 5f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= 50.0, (IRotationAction s) => Constants.Me.HaveBuff("Nature's Swiftness"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Earth Shield", false, 0, 0), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tidal Force", false, 0, 0), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction s) => RotationFramework.PartyMembers.Count((WoWPlayer o) => o.IsAlive && o.HealthPercent <= 80.0 && o.GetDistance <= 40f) >= 2 || BossList.isboss, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tidal Force", false, 0, 0), 8f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), (IRotationAction s) => RotationFramework.AllUnits.Count(delegate(WoWUnit o)
				{
					if (o.IsAlive)
					{
						ulong target = o.Target;
						WoWUnit tank = Restoration._tank;
						ulong? num = (tank != null) ? new ulong?(tank.Guid) : null;
						if ((target == num.GetValueOrDefault() & num != null) && BossList.BossListInt.Contains(o.Entry))
						{
							return o.GetDistance <= 40f;
						}
					}
					return false;
				}) >= 1, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cleanse Spirit", false), 9f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && t.HasDebuffType(new string[]
				{
					"Disease",
					"Poison",
					"Curse"
				}), (IRotationAction s) => Constants.Me.ManaPercentage > 25U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cleanse Spirit", false), 9.1f, (IRotationAction s, WoWUnit t) => Constants.Me.IsInGroup && (t.HaveImportantCurse() || t.HaveImportantDisease() || t.HaveImportantPoison()), (IRotationAction s) => Constants.Me.ManaPercentage > 25U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Riptide", false), 11f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<ShamanLevelSettings>.Current.RestorationRiptideGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Chain Heal", false), 12f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), delegate(IRotationAction s)
				{
					bool result;
					if (RotationFramework.PartyMembers.Count((WoWPlayer o) => o.IsAlive && o.HealthPercent <= (double)BasePersistentSettings<ShamanLevelSettings>.Current.RestorationChainHealGroup && o.GetDistance <= 40f) >= BasePersistentSettings<ShamanLevelSettings>.Current.RestorationChainHealCountGroup)
					{
						WoWUnit tank = Restoration._tank;
						result = (tank != null && tank.HealthPercent >= 50.0);
					}
					else
					{
						result = false;
					}
					return result;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lesser Healing Wave", false), 13f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<ShamanLevelSettings>.Current.RestorationLesserHealingWaveGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Healing Wave", false), 14f, (IRotationAction s, WoWUnit t) => t.HealthPercent <= (double)BasePersistentSettings<ShamanLevelSettings>.Current.RestorationHealingWaveGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cure Toxins", false), 14.1f, (IRotationAction s, WoWUnit t) => t.HaveImportantPoison() || t.HaveImportantDisease(), (IRotationAction s) => Constants.Me.ManaPercentage > 25U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Cure Toxins", false), 15f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && t.HasDebuffType(new string[]
				{
					"Disease",
					"Poison"
				}), (IRotationAction s) => Constants.Me.ManaPercentage > 25U, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Earth Shock", false), 19f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !t.HaveMyBuff(new string[]
				{
					"Earth Shock"
				}), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Lightning Bolt", false), 20f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}
	}
}
