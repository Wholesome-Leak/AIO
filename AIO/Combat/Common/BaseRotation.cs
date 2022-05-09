using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Events;
using AIO.Framework;
using robotManager.Events;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Common
{
	// Token: 0x02000104 RID: 260
	internal abstract class BaseRotation : ICycleable
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600089B RID: 2203
		protected abstract List<RotationStep> Rotation { get; }

		// Token: 0x0600089C RID: 2204 RVA: 0x00026634 File Offset: 0x00024834
		protected BaseRotation(bool runInCombat = true, bool runOutsideCombat = false, bool useCombatSynthetics = false, bool completelySynthetic = false)
		{
			this.RunInCombat = runInCombat;
			this.RunOutsideCombat = runOutsideCombat;
			this.UseCombatSynthetics = useCombatSynthetics;
			this.CompletelySynthetic = completelySynthetic;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002669C File Offset: 0x0002489C
		public virtual void Initialize()
		{
			this._rotation = this.Rotation;
			this._rotation.Sort();
			bool completelySynthetic = this.CompletelySynthetic;
			if (completelySynthetic)
			{
				RotationFramework.CacheDirectTransmission = true;
				RotationFramework.OnCacheUpdated += this.TickRotation;
			}
			else
			{
				bool runInCombat = this.RunInCombat;
				if (runInCombat)
				{
					bool useCombatSynthetics = this.UseCombatSynthetics;
					if (useCombatSynthetics)
					{
						SyntheticEvents.OnCombatStateRun += new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnCombatStateRun);
					}
					else
					{
						FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
					}
				}
				bool runOutsideCombat = this.RunOutsideCombat;
				if (runOutsideCombat)
				{
					SyntheticEvents.OnIdleStateAvailable += new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnIdleStateAvailable);
				}
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00026748 File Offset: 0x00024948
		public virtual void Dispose()
		{
			bool completelySynthetic = this.CompletelySynthetic;
			if (completelySynthetic)
			{
				RotationFramework.OnCacheUpdated -= this.TickRotation;
				RotationFramework.CacheDirectTransmission = false;
			}
			else
			{
				bool runInCombat = this.RunInCombat;
				if (runInCombat)
				{
					bool useCombatSynthetics = this.UseCombatSynthetics;
					if (useCombatSynthetics)
					{
						SyntheticEvents.OnCombatStateRun -= new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnCombatStateRun);
					}
					else
					{
						FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
					}
				}
				bool runOutsideCombat = this.RunOutsideCombat;
				if (runOutsideCombat)
				{
					SyntheticEvents.OnIdleStateAvailable -= new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(this.OnIdleStateAvailable);
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000267D9 File Offset: 0x000249D9
		private string RotationName
		{
			get
			{
				return base.GetType().FullName;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000267E6 File Offset: 0x000249E6
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			RotationFramework.RunRotation(this.RotationName, this._rotation, false);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000267E6 File Offset: 0x000249E6
		private void OnCombatStateRun(Engine engine, State state, CancelEventArgs cancelable)
		{
			RotationFramework.RunRotation(this.RotationName, this._rotation, false);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000267FC File Offset: 0x000249FC
		private void OnIdleStateAvailable(Engine engine, State state, CancelEventArgs cancelable)
		{
			bool flag = Constants.Me.InCombatFlagOnly || !this.IdleTimer.IsReady;
			if (!flag)
			{
				try
				{
					RotationFramework.RunRotation(this.RotationName, this._rotation, false);
				}
				finally
				{
					this.IdleTimer.Reset();
				}
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00026868 File Offset: 0x00024A68
		private void TickRotation(object sender, EventArgs e)
		{
			bool productIsStartedNotInPause = Conditions.ProductIsStartedNotInPause;
			if (productIsStartedNotInPause)
			{
				RotationFramework.RunRotation(this.RotationName, this._rotation, true);
			}
		}

		// Token: 0x04000579 RID: 1401
		private List<RotationStep> _rotation;

		// Token: 0x0400057A RID: 1402
		protected readonly bool RunInCombat = true;

		// Token: 0x0400057B RID: 1403
		protected readonly bool RunOutsideCombat = false;

		// Token: 0x0400057C RID: 1404
		protected readonly bool UseCombatSynthetics = false;

		// Token: 0x0400057D RID: 1405
		protected readonly bool CompletelySynthetic = false;

		// Token: 0x0400057E RID: 1406
		private readonly Timer IdleTimer = new Timer(new TimeSpan(0, 0, 0, 500));
	}
}
