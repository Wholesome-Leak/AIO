using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000D3 RID: 211
	internal class Buffs : BaseRotation
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00011754 File Offset: 0x0000F954
		internal Buffs() : base(true, true, false, false)
		{
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001E650 File Offset: 0x0001C850
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Aspect of the Viper", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted && (ulong)t.ManaPercentage < (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofViper), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.HunterAspect, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Aspect of the Pack", false, 0, 0), 2f, delegate(IRotationAction s, WoWUnit t)
				{
					if (!ObjectManager.Me.IsMounted && BasePersistentSettings<HunterLevelSettings>.Current.UseAspecofthePack && !ObjectManager.Me.InCombat && ObjectManager.Me.IsInGroup)
					{
						if (RotationFramework.Enemies.Count((WoWUnit u) => u.IsTargetingMeOrMyPetOrPartyMember) <= 0 && (ulong)t.ManaPercentage < (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofHawks))
						{
							return (ulong)t.ManaPercentage > (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofViper);
						}
					}
					return false;
				}, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.HunterAspect, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Aspect of the Dragonhawk", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted && (ulong)t.ManaPercentage > (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofHawks), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.HunterAspect, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Aspect of the Hawk", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted && (ulong)t.ManaPercentage > (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofHawks), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.HunterAspect, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Aspect of the Monkey", false, 0, 0), 5f, (IRotationAction s, WoWUnit t) => !ObjectManager.Me.IsMounted && (ulong)t.ManaPercentage > (ulong)((long)BasePersistentSettings<HunterLevelSettings>.Current.AspecofHawks), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), Exclusive.HunterAspect, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Trueshot Aura", false, 0, 0), 6f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Mend Pet", false, 0, 0), 7f, (IRotationAction s, WoWUnit t) => BasePersistentSettings<HunterLevelSettings>.Current.Checkpet && t.IsAlive && t.HealthPercent <= (double)BasePersistentSettings<HunterLevelSettings>.Current.PetHealth, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPet), null, false, true, false));
				return list;
			}
		}
	}
}
