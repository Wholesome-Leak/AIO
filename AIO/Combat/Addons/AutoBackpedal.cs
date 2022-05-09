using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using AIO.Combat.Common;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Addons
{
	// Token: 0x0200010A RID: 266
	internal class AutoBackpedal : ICycleable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x00026C0C File Offset: 0x00024E0C
		public AutoBackpedal(Func<bool> should, float range)
		{
			this.Should = should;
			this.Circle = (from x in Enumerable.Range(0, 8)
			select (double)x * 45.0 into angle
			select new Vector3
			{
				X = (float)((double)range * Math.Cos(angle * 0.017453292519943295)),
				Y = (float)((double)range * Math.Sin(angle * 0.017453292519943295)),
				Z = 0f
			}).ToList<Vector3>();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00026C87 File Offset: 0x00024E87
		public void Dispose()
		{
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00026C9B File Offset: 0x00024E9B
		public void Initialize()
		{
			FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00026CB0 File Offset: 0x00024EB0
		private bool MoveToClosest()
		{
			Vector3 vector = (from b in this.Circle
			select new Vector3
			{
				X = Constants.Target.Position.X + b.X,
				Y = Constants.Target.Position.Y + b.Y,
				Z = Constants.Target.Position.Z + b.Z
			} into x
			where !TraceLine.TraceLineGo(Constants.Target.Position, x, 337) && !TraceLine.TraceLineGo(x)
			orderby x.DistanceTo2D(Constants.Me.Position)
			select x).FirstOrDefault<Vector3>();
			bool flag = vector == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				MovementManager.MoveTo(vector);
				result = true;
			}
			return result;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00026D54 File Offset: 0x00024F54
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool flag = !this.Should() || !this.MoveTimer.IsReady;
			if (!flag)
			{
				bool flag2 = !Constants.Pet.IsAlive && Constants.Me.WowClass == 3;
				if (!flag2)
				{
					WoWClass wowClass = Constants.Me.WowClass;
					WoWClass woWClass = wowClass;
					if (woWClass != 3)
					{
						if (woWClass != 8)
						{
							switch (Others.Random(0, 2))
							{
							case 0:
								Logging.WriteFight("Default  Backup");
								Move.Backward(0, 1200);
								break;
							case 1:
								Logging.WriteFight("Default  Backup");
								Move.StrafeLeft(0, 1200);
								break;
							case 2:
								Logging.WriteFight("Default  Backup");
								Move.StrafeRight(0, 1200);
								break;
							}
							this.MoveTimer.Reset(1200.0);
						}
						else
						{
							bool flag3 = this.MoveToClosest();
							if (flag3)
							{
								Logging.WriteFight("Mage Backup");
								Thread.Sleep(2800);
								this.MoveTimer.Reset(2500.0);
							}
						}
					}
					else
					{
						bool isInGroup = Constants.Me.IsInGroup;
						if (isInGroup)
						{
							bool flag4 = this.MoveToClosest();
							if (flag4)
							{
								Logging.WriteFight("Hunter Backup");
								Thread.Sleep(2800);
								this.MoveTimer.Reset(2500.0);
							}
						}
						else
						{
							bool flag5 = !Constants.Me.IsInGroup;
							if (flag5)
							{
								switch (Others.Random(0, 2))
								{
								case 0:
									Logging.WriteFight("Default  Backup");
									Move.Backward(0, 1200);
									break;
								case 1:
									Logging.WriteFight("Default  Backup");
									Move.StrafeLeft(0, 1200);
									break;
								case 2:
									Logging.WriteFight("Default  Backup");
									Move.StrafeRight(0, 1200);
									break;
								}
								this.MoveTimer.Reset(1200.0);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400058B RID: 1419
		private readonly Func<bool> Should;

		// Token: 0x0400058C RID: 1420
		private readonly IEnumerable<Vector3> Circle;

		// Token: 0x0400058D RID: 1421
		private readonly Timer MoveTimer = new Timer();
	}
}
