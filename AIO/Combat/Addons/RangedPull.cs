using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Addons
{
	// Token: 0x0200010D RID: 269
	internal class RangedPull : ICycleable
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0002709C File Offset: 0x0002529C
		public RangedPull(string pull, Func<float, float> rangeSwap)
		{
			this.Pull = new RotationSpell(pull, false);
			this.RangeSwap = rangeSwap;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000270C5 File Offset: 0x000252C5
		public void Dispose()
		{
			FightEvents.OnFightStart -= new FightEvents.FightTargetHandler(this.OnFightStart);
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000270EC File Offset: 0x000252EC
		public void Initialize()
		{
			FightEvents.OnFightStart += new FightEvents.FightTargetHandler(this.OnFightStart);
			FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00027113 File Offset: 0x00025313
		private void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
			this.Run();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00027113 File Offset: 0x00025313
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			this.Run();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002711C File Offset: 0x0002531C
		private void Run()
		{
			float num = Constants.Me.Position.DistanceTo(Constants.Target.Position);
			bool flag = this.OldRange != null;
			if (flag)
			{
				float num2 = num;
				float? oldRange = this.OldRange;
				bool flag2 = (num2 <= oldRange.GetValueOrDefault() & oldRange != null) || Constants.Target.IsCast || this.Timeout.IsReady;
				if (flag2)
				{
					this.RangeSwap(this.OldRange.Value);
					this.OldRange = null;
				}
			}
			bool flag3 = !Extension.HaveRangedWeaponEquipped;
			if (!flag3)
			{
				bool inCombatFlagOnly = Constants.Me.InCombatFlagOnly;
				if (!inCombatFlagOnly)
				{
					bool flag4 = !this.Pull.KnownSpell;
					if (!flag4)
					{
						bool flag5 = num <= 29f && num >= 10f && RangedPull.HasNearbyEnemies(Constants.Target, 20f);
						if (flag5)
						{
							bool flag6 = this.OldRange == null;
							if (flag6)
							{
								this.OldRange = new float?(this.RangeSwap(29f));
								this.Timeout.Reset(7000.0);
							}
							RotationCombatUtil.CastSpell(this.Pull, Constants.Target, true);
							Usefuls.WaitIsCasting();
						}
					}
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00027284 File Offset: 0x00025484
		private static bool HasNearbyEnemies(WoWUnit target, float distance)
		{
			IEnumerable<WoWUnit> enumerable = from u in RotationFramework.Enemies
			where !u.IsTapDenied && !u.IsTaggedByOther && !u.PlayerControlled && u.IsAttackable && u.Guid != target.Guid
			select u;
			WoWUnit woWUnit = null;
			float num = float.PositiveInfinity;
			foreach (WoWUnit woWUnit2 in enumerable)
			{
				float num2 = woWUnit2.Position.DistanceTo(target.Position);
				bool flag = num2 < num;
				if (flag)
				{
					woWUnit = woWUnit2;
					num = num2;
				}
			}
			return woWUnit != null && num < distance;
		}

		// Token: 0x04000594 RID: 1428
		private readonly RotationSpell Pull;

		// Token: 0x04000595 RID: 1429
		private readonly Func<float, float> RangeSwap;

		// Token: 0x04000596 RID: 1430
		private readonly Timer Timeout = new Timer();

		// Token: 0x04000597 RID: 1431
		private float? OldRange;
	}
}
