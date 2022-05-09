using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using robotManager.Helpful;
using wManager;
using wManager.Wow.Bot.States;
using wManager.Wow.Class;
using wManager.Wow.Helpers;

namespace AIO.Combat.Shaman
{
	// Token: 0x02000097 RID: 151
	internal class ShamanBehavior : BaseCombatClass
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00015053 File Offset: 0x00013253
		public override float Range
		{
			get
			{
				return this.CombatRange;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001505C File Offset: 0x0001325C
		internal ShamanBehavior()
		{
			BaseSettings settings = BasePersistentSettings<ShamanLevelSettings>.Current;
			Dictionary<string, BaseRotation> dictionary = new Dictionary<string, BaseRotation>();
			dictionary.Add("LowLevel", new LowLevel());
			dictionary.Add("Elemental", new Elemental());
			dictionary.Add("Restoration", new Restoration());
			dictionary.Add("Enhancement", new Enhancement());
			ICycleable[] array = new ICycleable[2];
			array[0] = new AutoPartyResurrect("Ancestral Spirit", false, true);
			array[1] = new ConditionalCycleable(() => BasePersistentSettings<ShamanLevelSettings>.Current.HealOOC, new HealOOC());
			base..ctor(settings, dictionary, array);
			Totems totems = new Totems(this);
			base.Addons.Add(totems);
			base.Addons.Add(new Buffs(this, totems));
			base.Addons.Add(new WeaponHelper(this));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001514C File Offset: 0x0001334C
		public override void Initialize()
		{
			base.Initialize();
			string specialisation = base.Specialisation;
			string a = specialisation;
			if (!(a == "Enhancement"))
			{
				this.CombatRange = 29f;
			}
			else
			{
				this.CombatRange = 5f;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015193 File Offset: 0x00013393
		protected override void OnMoveToPulse(Vector3 point, CancelEventArgs cancelable)
		{
			this.UseGhostWolf(point);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000151A0 File Offset: 0x000133A0
		protected override void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			Vector3 vector = points.LastOrDefault<Vector3>();
			bool flag = vector == null;
			if (!flag)
			{
				this.UseGhostWolf(vector);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000151CC File Offset: 0x000133CC
		private void UseGhostWolf(Vector3 point)
		{
			bool flag = point.DistanceTo(Constants.Me.Position) < wManagerSetting.CurrentSetting.MountDistance;
			if (!flag)
			{
				bool flag2 = string.IsNullOrWhiteSpace(wManagerSetting.CurrentSetting.GroundMountName) && !new Regeneration().NeedToRun && !Constants.Me.HaveMyBuff(new string[]
				{
					"Ghost Wolf"
				}) && BasePersistentSettings<ShamanLevelSettings>.Current.Ghostwolf && Constants.Me.IsAlive && this.GhostWolf.KnownSpell && !Constants.Me.InCombat;
				if (flag2)
				{
					this.GhostWolf.Launch();
					Usefuls.WaitIsCasting();
				}
			}
		}

		// Token: 0x040002C5 RID: 709
		private float CombatRange;

		// Token: 0x040002C6 RID: 710
		private readonly Spell GhostWolf = new Spell("Ghost Wolf");
	}
}
