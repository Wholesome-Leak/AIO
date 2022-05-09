using System;
using System.ComponentModel;
using System.Diagnostics;
using AIO.Combat.Common;
using robotManager.Events;
using robotManager.FiniteStateMachine;

namespace AIO.Events
{
	// Token: 0x02000062 RID: 98
	internal class SyntheticEvents : ICycleable
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		private static void OnBeforeCheckIfNeedToRunState(Engine engine, State state, CancelEventArgs cancelable)
		{
			bool flag = ((engine != null) ? engine.States : null) == null;
			if (!flag)
			{
				for (int i = 0; i < engine.States.Count; i++)
				{
					State state2 = engine.States[i];
					bool flag2 = state2 == null;
					if (!flag2)
					{
						string displayName = state2.DisplayName;
						string a = displayName;
						if (a == "Idle")
						{
							FiniteStateMachineEvents.FSMEngineStateCancelableHandler onIdleStateAvailable = SyntheticEvents.OnIdleStateAvailable;
							if (onIdleStateAvailable != null)
							{
								onIdleStateAvailable.Invoke(engine, state, cancelable);
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EF68 File Offset: 0x0000D168
		private static void OnRunState(Engine engine, State state, CancelEventArgs cancelable)
		{
			string text = (state != null) ? state.DisplayName : null;
			string a = text;
			if (a == "InFight" || a == "Healtarget" || a == "dCombat")
			{
				FiniteStateMachineEvents.FSMEngineStateCancelableHandler onCombatStateRun = SyntheticEvents.OnCombatStateRun;
				if (onCombatStateRun != null)
				{
					onCombatStateRun.Invoke(engine, state, cancelable);
				}
			}
			bool flag = state != null && state.DisplayName.Contains("SmoothMove");
			if (flag)
			{
				FiniteStateMachineEvents.FSMEngineStateCancelableHandler onCombatStateRun2 = SyntheticEvents.OnCombatStateRun;
				if (onCombatStateRun2 != null)
				{
					onCombatStateRun2.Invoke(engine, state, cancelable);
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		public void Initialize()
		{
			FiniteStateMachineEvents.OnBeforeCheckIfNeedToRunState += new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(SyntheticEvents.OnBeforeCheckIfNeedToRunState);
			FiniteStateMachineEvents.OnRunState += new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(SyntheticEvents.OnRunState);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F01B File Offset: 0x0000D21B
		public void Dispose()
		{
			FiniteStateMachineEvents.OnBeforeCheckIfNeedToRunState -= new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(SyntheticEvents.OnBeforeCheckIfNeedToRunState);
			FiniteStateMachineEvents.OnRunState -= new FiniteStateMachineEvents.FSMEngineStateCancelableHandler(SyntheticEvents.OnRunState);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003D4 RID: 980 RVA: 0x0000F044 File Offset: 0x0000D244
		// (remove) Token: 0x060003D5 RID: 981 RVA: 0x0000F078 File Offset: 0x0000D278
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event FiniteStateMachineEvents.FSMEngineStateCancelableHandler OnCombatStateRun;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003D6 RID: 982 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		// (remove) Token: 0x060003D7 RID: 983 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event FiniteStateMachineEvents.FSMEngineStateCancelableHandler OnIdleStateAvailable;

		// Token: 0x040001DF RID: 479
		private const string DefaultCombatState = "InFight";

		// Token: 0x040001E0 RID: 480
		private const string DefaultIdleState = "Idle";

		// Token: 0x040001E1 RID: 481
		private const string DungeonCrawlerCombatState = "dCombat";

		// Token: 0x040001E2 RID: 482
		private const string HealBotCombatState = "Healtarget";
	}
}
