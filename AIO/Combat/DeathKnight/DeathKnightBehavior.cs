using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Combat.Common;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.DeathKnight
{
	// Token: 0x020000FC RID: 252
	internal class DeathKnightBehavior : BaseCombatClass
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001B5E4 File Offset: 0x000197E4
		public override float Range
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00024E0C File Offset: 0x0002300C
		internal DeathKnightBehavior() : base(BasePersistentSettings<DeathKnightLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"Blood",
				new Blood()
			},
			{
				"Unholy",
				new Unholy()
			},
			{
				"Frost",
				new Frost()
			},
			{
				"UnholyPVP",
				new UnholyPVP()
			}
		}, new ICycleable[]
		{
			new Buffs()
		})
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00024E90 File Offset: 0x00023090
		protected override void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool flag = !Constants.Pet.IsAlive;
			if (flag)
			{
				bool flag2 = this.RaiseDead.IsSpellUsable && this.RaiseDead.KnownSpell && !Constants.Me.IsMounted && BasePersistentSettings<DeathKnightLevelSettings>.Current.RaiseDead;
				if (flag2)
				{
					this.RaiseDead.Launch();
					Usefuls.WaitIsCasting();
				}
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00024EFC File Offset: 0x000230FC
		protected override void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool isAlive = Constants.Pet.IsAlive;
			if (isAlive)
			{
				bool flag = Constants.Pet.Target != Constants.Me.Target;
				if (flag)
				{
					Lua.RunMacroText("/petattack");
					Logging.WriteFight(string.Format("Changing pet target to {0} [{1}]", Constants.Target.Name, Constants.Target.Guid));
				}
				bool flag2 = Constants.Pet.Target == Constants.Me.Target;
				if (flag2)
				{
					bool flag3 = Constants.Target.IsCast && Constants.Pet.Position.DistanceTo(Constants.Target.Position) <= 6f;
					if (flag3)
					{
						PetManager.PetSpellCast("Gnaw");
					}
					bool flag4 = Constants.Pet.Position.DistanceTo(Constants.Target.Position) >= 7f;
					if (flag4)
					{
						PetManager.PetSpellCast("Leap");
					}
				}
			}
		}

		// Token: 0x04000535 RID: 1333
		private readonly Spell RaiseDead = new Spell("Raise Dead");
	}
}
