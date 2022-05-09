using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AIO.Combat.Common;
using AIO.Events;
using AIO.Framework;
using robotManager.Events;
using robotManager.FiniteStateMachine;
using wManager.Events;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Addons
{
	// Token: 0x02000108 RID: 264
	internal class AutoPartyResurrect : ICycleable
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00026945 File Offset: 0x00024B45
		public AutoPartyResurrect(string ressurection, bool combat = false, bool enabled = true)
		{
			this.Ressurection = new Spell(ressurection);
			this.Combat = combat;
			this.Enabled = enabled;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00026969 File Offset: 0x00024B69
		public void Dispose()
		{
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
			SyntheticEvents.OnIdleStateAvailable -= new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnIdleStateAvailable);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00026990 File Offset: 0x00024B90
		public void Initialize()
		{
			FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
			SyntheticEvents.OnIdleStateAvailable += new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnIdleStateAvailable);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000269B7 File Offset: 0x00024BB7
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			this.Run();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000269B7 File Offset: 0x00024BB7
		private void OnIdleStateAvailable(Engine engine, State state, CancelEventArgs cancelable)
		{
			this.Run();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000269C0 File Offset: 0x00024BC0
		private void Run()
		{
			bool flag = Constants.Me.InCombatFlagOnly != this.Combat;
			if (!flag)
			{
				bool flag2 = !this.Enabled;
				if (!flag2)
				{
					bool flag3 = !Constants.Me.IsInGroup;
					if (!flag3)
					{
						bool flag4 = !this.Ressurection.KnownSpell;
						if (!flag4)
						{
							AutoPartyResurrect._blacklist = (from entry in AutoPartyResurrect._blacklist
							where entry.Value > DateTime.Now
							select entry).ToDictionary((KeyValuePair<ulong, DateTime> pair) => pair.Key, (KeyValuePair<ulong, DateTime> pair) => pair.Value);
							foreach (WoWPlayer woWPlayer in from o in RotationFramework.PartyMembers
							where !o.IsAlive && !AutoPartyResurrect._blacklist.ContainsKey(o.Guid)
							select o)
							{
								bool flag5 = !this.Ressurection.IsSpellUsable;
								if (flag5)
								{
									break;
								}
								bool flag6 = TraceLine.TraceLineGo(woWPlayer.Position) || woWPlayer.GetDistance > this.Ressurection.MaxRange;
								if (flag6)
								{
									MovementManager.MoveTo(woWPlayer.Position);
									break;
								}
								this.Ressurection.Launch();
								Interact.InteractGameObject(woWPlayer.GetBaseAddress, false, false);
								Usefuls.WaitIsCasting();
								AutoPartyResurrect._blacklist.Add(woWPlayer.Guid, DateTime.Now.AddMinutes(1.0));
							}
						}
					}
				}
			}
		}

		// Token: 0x04000582 RID: 1410
		private readonly Spell Ressurection;

		// Token: 0x04000583 RID: 1411
		private readonly bool Combat;

		// Token: 0x04000584 RID: 1412
		private readonly bool Enabled;

		// Token: 0x04000585 RID: 1413
		private static Dictionary<ulong, DateTime> _blacklist = new Dictionary<ulong, DateTime>();
	}
}
