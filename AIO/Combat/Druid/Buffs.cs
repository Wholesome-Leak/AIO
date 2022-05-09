using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
	// Token: 0x020000E6 RID: 230
	internal class Buffs : BaseRotation
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x0002160C File Offset: 0x0001F80C
		private bool UseTranquility(IRotationAction s, WoWUnit t)
		{
			List<WoWPlayer> source = (from o in RotationFramework.PartyMembers
			where o.IsAlive && o.GetDistance <= 40f
			select o).ToList<WoWPlayer>();
			int num = source.Count((WoWPlayer o) => o.HealthPercent <= 40.0);
			int num2 = source.Count((WoWPlayer o) => o.HealthPercent <= 55.0);
			int num3 = source.Count((WoWPlayer o) => o.HealthPercent <= 65.0);
			if (Constants.Me.IsInGroup)
			{
				if (RotationFramework.PartyMembers.Count((WoWPlayer u) => u.IsAlive && u.IsTargetingMeOrMyPetOrPartyMember) >= 1)
				{
					return num >= 2 || num2 >= 3 || num3 >= 4;
				}
			}
			return false;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002170E File Offset: 0x0001F90E
		internal Buffs() : base(BasePersistentSettings<DruidLevelSettings>.Current.BuffIC, true, false, false)
		{
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00021728 File Offset: 0x0001F928
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationBuff("Mark of the Wild", false, 0, 0), 1f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsMounted && !t.HaveBuff("Gift of the Wild"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindPartyMember), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Mark of the Wild", false, 0, 0), 2f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsMounted && !t.HaveBuff("Gift of the Wild"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Thorns", false, 0, 0), 3f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsMounted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindTank), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Thorns", false, 0, 0), 4f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsInGroup && !Constants.Me.IsMounted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tree of Life", false, 0, 0), 5f, (IRotationAction s, WoWUnit t) => !Constants.Me.IsMounted, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationBuff("Tranquility", false, 0, 0), 7f, new Func<IRotationAction, WoWUnit, bool>(this.UseTranquility), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				return list;
			}
		}
	}
}
