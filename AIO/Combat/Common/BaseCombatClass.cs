using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Settings;
using robotManager.Events;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Events;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Common
{
	// Token: 0x02000103 RID: 259
	internal abstract class BaseCombatClass : ICycleable
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600088A RID: 2186
		public abstract float Range { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0002634A File Offset: 0x0002454A
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00026352 File Offset: 0x00024552
		private BaseRotation FightRotation { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0002635B File Offset: 0x0002455B
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00026363 File Offset: 0x00024563
		protected List<ICycleable> Addons { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0002636C File Offset: 0x0002456C
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00026374 File Offset: 0x00024574
		public string Specialisation { get; private set; }

		// Token: 0x06000891 RID: 2193 RVA: 0x0002637D File Offset: 0x0002457D
		internal BaseCombatClass(BaseSettings settings, Dictionary<string, BaseRotation> specialisations, params ICycleable[] addons)
		{
			this.Settings = settings;
			this.Specialisations = specialisations;
			this.Addons = new List<ICycleable>(addons);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000263A4 File Offset: 0x000245A4
		public virtual void Initialize()
		{
			this.Specialisation = ((Constants.Me.Level < 10U) ? "LowLevel" : ((this.Settings.ChooseRotation == "Auto") ? Extension.GetSpec() : this.Settings.ChooseRotation));
			BaseRotation baseRotation;
			this.FightRotation = (this.Specialisations.TryGetValue(this.Specialisation, out baseRotation) ? baseRotation : null);
			bool flag = this.FightRotation == null;
			if (flag)
			{
				Logging.WriteError("Unrecognized specialisation " + this.Specialisation, true);
				Products.ProductStop();
			}
			else
			{
				Logging.Write("Running " + this.Specialisation + " specialisation");
				foreach (ICycleable cycleable in this.Addons)
				{
					cycleable.Initialize();
				}
				this.FightRotation.Initialize();
				FightEvents.OnFightStart += new FightEvents.FightTargetHandler(this.OnFightStart);
				FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
				FightEvents.OnFightEnd += new FightEvents.FightTargetGuidHandler(this.OnFightEnd);
				MovementEvents.OnMovementPulse += new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
				MovementEvents.OnMoveToPulse += new MovementEvents.MovementVector3Handler(this.OnMoveToPulse);
				OthersEvents.OnPathFinderFindPath += new OthersEvents.VectorVectorCancelableHandler(this.OnMovementCalculation);
				ObjectManagerEvents.OnObjectManagerPulsed += new SimpleHandler(this.OnObjectManagerPulse);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00026540 File Offset: 0x00024740
		public virtual void Dispose()
		{
			foreach (ICycleable cycleable in this.Addons)
			{
				cycleable.Dispose();
			}
			BaseRotation fightRotation = this.FightRotation;
			if (fightRotation != null)
			{
				fightRotation.Dispose();
			}
			FightEvents.OnFightStart -= new FightEvents.FightTargetHandler(this.OnFightStart);
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
			FightEvents.OnFightEnd -= new FightEvents.FightTargetGuidHandler(this.OnFightEnd);
			MovementEvents.OnMovementPulse -= new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
			MovementEvents.OnMoveToPulse -= new MovementEvents.MovementVector3Handler(this.OnMoveToPulse);
			OthersEvents.OnPathFinderFindPath -= new OthersEvents.VectorVectorCancelableHandler(this.OnMovementCalculation);
			ObjectManagerEvents.OnObjectManagerPulsed -= new SimpleHandler(this.OnObjectManagerPulse);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnFightEnd(ulong guid)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnMoveToPulse(Vector3 point, CancelEventArgs cancelable)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnMovementCalculation(Vector3 from, Vector3 to, string continentnamempq, CancelEventArgs cancelable)
		{
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00026630 File Offset: 0x00024830
		protected virtual void OnObjectManagerPulse()
		{
		}

		// Token: 0x04000574 RID: 1396
		private readonly BaseSettings Settings;

		// Token: 0x04000575 RID: 1397
		private readonly Dictionary<string, BaseRotation> Specialisations;
	}
}
