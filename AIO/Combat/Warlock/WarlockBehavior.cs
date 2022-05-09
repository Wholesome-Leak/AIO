using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warlock
{
	// Token: 0x02000080 RID: 128
	internal class WarlockBehavior : BaseCombatClass
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00012652 File Offset: 0x00010852
		public override float Range
		{
			get
			{
				return 29f;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001265C File Offset: 0x0001085C
		internal WarlockBehavior() : base(BasePersistentSettings<WarlockLevelSettings>.Current, new Dictionary<string, BaseRotation>
		{
			{
				"LowLevel",
				new LowLevel()
			},
			{
				"Affliction",
				new Affliction()
			},
			{
				"Destruction",
				new Destruction()
			},
			{
				"Demonology",
				new Demonology()
			}
		}, new ICycleable[]
		{
			new Buffs(),
			new PetAutoTarget("Torment")
		})
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001272A File Offset: 0x0001092A
		protected override void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
			this.RefreshPet();
			Lua.LuaDoString("PetDefensiveMode();", false);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00012740 File Offset: 0x00010940
		protected override void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool flag = Constants.Me.HealthPercent < 20.0 && Constants.Me.IsAlive;
			if (flag)
			{
				Consumables.UseHealthstone();
			}
			bool flag2 = !Fight.InFight || BasePersistentSettings<WarlockLevelSettings>.Current.PetInfight;
			if (flag2)
			{
				this.RefreshPet();
			}
			bool flag3 = RotationFramework.Enemies.Count((WoWUnit o) => o.Position.DistanceTo(Constants.Pet.Position) <= 8f) > 1 && PetManager.CurrentWarlockPet == "Voidwalker";
			if (flag3)
			{
				PetManager.PetSpellCast("Suffering");
			}
			WoWPlayer woWPlayer = (from u in RotationFramework.PartyMembers
			where u.IsAlive && u.HaveImportantMagic()
			select u).FirstOrDefault<WoWPlayer>();
			bool flag4 = PetManager.CurrentWarlockPet == "Felhunter" && woWPlayer != null;
			if (flag4)
			{
				Constants.Me.FocusGuid = woWPlayer.Guid;
				bool flag5 = Constants.Pet.Position.DistanceTo(Constants.Target.Position) <= 30f;
				if (flag5)
				{
					PetManager.PetSpellCastFocus("Devour Magic");
					Thread.Sleep(50);
					Lua.LuaDoString("ClearFocus();", false);
				}
			}
			WoWUnit woWUnit = (from u in RotationFramework.Enemies
			where u.IsCast && u.IsTargetingMeOrMyPetOrPartyMember
			select u).FirstOrDefault<WoWUnit>();
			bool flag6 = PetManager.CurrentWarlockPet == "Felhunter" && woWUnit != null;
			if (flag6)
			{
				bool flag7 = Constants.Pet.Target != woWUnit.Guid;
				if (flag7)
				{
					string str = "Found Target to Interrupt";
					WoWUnit woWUnit2 = woWUnit;
					Logging.Write(str + ((woWUnit2 != null) ? woWUnit2.ToString() : null));
					Constants.Me.FocusGuid = woWUnit.Guid;
					Logging.Write("Attacking Target with Pet to Interrupt");
					Lua.RunMacroText("/petattack [@focus]");
					Lua.LuaDoString("ClearFocus();", false);
				}
				bool flag8 = Constants.Pet.Target == woWUnit.Guid;
				if (flag8)
				{
					bool flag9 = Constants.Pet.Position.DistanceTo(woWUnit.Position) <= 30f;
					if (flag9)
					{
						PetManager.PetSpellCast("Spell Lock");
					}
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000129A4 File Offset: 0x00010BA4
		protected override void OnFightEnd(ulong guid)
		{
			bool flag = Constants.Pet.IsAlive && Constants.Pet.HealthPercent < 80.0 && !Constants.Pet.HaveBuff("Consume Shadows") && PetManager.CurrentWarlockPet == "Voidwalker";
			if (flag)
			{
				PetManager.PetSpellCast("Consume Shadows");
			}
			bool flag2 = !Constants.Me.IsInGroup && !Constants.Pet.InCombat;
			if (flag2)
			{
				Lua.LuaDoString("PetDefensiveMode();", false);
			}
			bool flag3;
			if (Constants.Me.IsInGroup)
			{
				if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe && o.IsTargetingPartyMember) <= 0)
				{
					flag3 = !Constants.Pet.InCombat;
					goto IL_CC;
				}
			}
			flag3 = false;
			IL_CC:
			bool flag4 = flag3;
			if (flag4)
			{
				Lua.LuaDoString("PetDefensiveMode();", false);
			}
			bool flag5 = ItemsHelper.CountItemStacks("Soul Shard") >= 5;
			if (flag5)
			{
				ItemsHelper.DeleteItems(6265U, 5);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00012AB1 File Offset: 0x00010CB1
		protected override void OnMovementCalculation(Vector3 from, Vector3 to, string continentnamempq, CancelEventArgs cancelable)
		{
			this.RefreshPet();
			SpellstoneHelper.Refresh();
			this.HealthstoneRefresh();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00012AC8 File Offset: 0x00010CC8
		protected override void OnObjectManagerPulse()
		{
			this.RefreshPet();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012AD2 File Offset: 0x00010CD2
		protected override void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			SpellstoneHelper.Refresh();
			this.HealthstoneRefresh();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012AE4 File Offset: 0x00010CE4
		private void HealthstoneRefresh()
		{
			bool flag = !Consumables.HaveHealthstone();
			if (flag)
			{
				bool flag2 = this.CreateHealthStone.KnownSpell && this.CreateHealthStone.IsSpellUsable;
				if (flag2)
				{
					this.CreateHealthStone.Launch();
					Usefuls.WaitIsCasting();
				}
			}
			bool flag3 = ItemsHelper.CountItemStacks("Soul Shard") >= 5;
			if (flag3)
			{
				ItemsHelper.DeleteItems(6265U, 5);
			}
			bool flag4 = !Fight.InFight || BasePersistentSettings<WarlockLevelSettings>.Current.PetInfight;
			if (flag4)
			{
				this.RefreshPet();
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00012B78 File Offset: 0x00010D78
		private void RefreshPet()
		{
			bool flag = (!Constants.Pet.IsAlive || (Constants.Pet.IsAlive && PetManager.CurrentWarlockPet != BasePersistentSettings<WarlockLevelSettings>.Current.Pet)) && !Constants.Me.IsMounted;
			if (flag)
			{
				bool flag2 = BasePersistentSettings<WarlockLevelSettings>.Current.Pet == "Felhunter" && this.SummonFelhunter.KnownSpell && this.SummonFelhunter.IsSpellUsable;
				if (flag2)
				{
					this.SummonFelhunter.Launch();
					Usefuls.WaitIsCasting();
					return;
				}
				bool flag3 = BasePersistentSettings<WarlockLevelSettings>.Current.Pet == "Voidwalker" && this.SummonVoidWalker.KnownSpell && this.SummonVoidWalker.IsSpellUsable;
				if (flag3)
				{
					this.SummonVoidWalker.Launch();
					Usefuls.WaitIsCasting();
					return;
				}
				bool flag4 = BasePersistentSettings<WarlockLevelSettings>.Current.Pet == "Felguard" && this.SummonFelguard.KnownSpell && this.SummonFelguard.IsSpellUsable;
				if (flag4)
				{
					this.SummonFelguard.Launch();
					Usefuls.WaitIsCasting();
					return;
				}
				bool flag5 = PetManager.CurrentWarlockPet != "Imp" && this.SummonImp.KnownSpell && this.SummonImp.IsSpellUsable;
				if (flag5)
				{
					this.SummonImp.Launch();
					Usefuls.WaitIsCasting();
				}
			}
			bool flag6 = Constants.Pet.IsAlive && PetManager.CurrentWarlockPet == "Imp";
			if (flag6)
			{
				PetManager.TogglePetSpellAuto("Blood Pact", true);
				Thread.Sleep(50);
				PetManager.TogglePetSpellAuto("Firebolt", true);
			}
			bool flag7 = Constants.Pet.IsAlive && PetManager.CurrentWarlockPet == "Felhunter";
			if (flag7)
			{
				PetManager.TogglePetSpellAuto("Fel Intelligence", true);
				Thread.Sleep(50);
				PetManager.TogglePetSpellAuto("Shadow Bite", true);
			}
		}

		// Token: 0x04000264 RID: 612
		private readonly Spell CreateHealthStone = new Spell("Create Healthstone");

		// Token: 0x04000265 RID: 613
		private readonly Spell SummonImp = new Spell("Summon Imp");

		// Token: 0x04000266 RID: 614
		private readonly Spell SummonVoidWalker = new Spell("Summon Voidwalker");

		// Token: 0x04000267 RID: 615
		private readonly Spell SummonFelguard = new Spell("Summon Felguard");

		// Token: 0x04000268 RID: 616
		private readonly Spell SummonFelhunter = new Spell("Summon Felhunter");
	}
}
