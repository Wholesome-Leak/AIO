using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000D5 RID: 213
	internal class HunterBehavior : BaseCombatClass
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public override float Range
		{
			get
			{
				return (float)BasePersistentSettings<HunterLevelSettings>.Current.RangeSet;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		internal HunterBehavior()
		{
			BaseSettings settings = BasePersistentSettings<HunterLevelSettings>.Current;
			Dictionary<string, BaseRotation> dictionary = new Dictionary<string, BaseRotation>();
			dictionary.Add("LowLevel", new LowLevel());
			dictionary.Add("BeastMastery", new BeastMastery());
			dictionary.Add("Marksmanship", new Marksmanship());
			dictionary.Add("Survival", new Survival());
			ICycleable[] array = new ICycleable[3];
			array[0] = new Buffs();
			array[1] = new PetAutoTarget("Growl");
			array[2] = new ConditionalCycleable(() => BasePersistentSettings<HunterLevelSettings>.Current.Backpaddle, new AutoBackpedal(() => Constants.Target.GetDistance <= (float)BasePersistentSettings<HunterLevelSettings>.Current.BackpaddleRange && (Constants.Target.IsTargetingMyPet || Constants.Target.IsTargetingPartyMember), (float)BasePersistentSettings<HunterLevelSettings>.Current.BackpaddleRange));
			base..ctor(settings, dictionary, array);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		protected override void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool useMacro = BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
			if (useMacro)
			{
				Lua.RunMacroText("/petdefensive");
			}
			bool flag = !BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
			if (flag)
			{
				Lua.LuaDoString("PetDefensiveMode();", false);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		protected override void OnFightEnd(ulong guid)
		{
			bool flag = !Constants.Me.IsInGroup && !Constants.Pet.InCombat;
			if (flag)
			{
				bool useMacro = BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
				if (useMacro)
				{
					Lua.RunMacroText("/petdefensive");
				}
				bool flag2 = !BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
				if (flag2)
				{
					Lua.LuaDoString("PetDefensiveMode();", false);
				}
			}
			bool flag3;
			if (Constants.Me.IsInGroup)
			{
				if (RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMe || o.IsTargetingPartyMember) <= 0)
				{
					flag3 = !Constants.Pet.InCombat;
					goto IL_A5;
				}
			}
			flag3 = false;
			IL_A5:
			bool flag4 = flag3;
			if (flag4)
			{
				bool useMacro2 = BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
				if (useMacro2)
				{
					Lua.RunMacroText("/petdefensive");
				}
				bool flag5 = !BasePersistentSettings<HunterLevelSettings>.Current.UseMacro;
				if (flag5)
				{
					Lua.LuaDoString("PetDefensiveMode();", false);
				}
			}
			bool petfeed = BasePersistentSettings<HunterLevelSettings>.Current.Petfeed;
			if (petfeed)
			{
				this.FeedPet();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		protected override void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool flag = PetManager.GetPetSpellReady("Bite") && PetManager.GetPetSpellCooldown("Bite") <= 0 && ObjectManager.Pet.Focus >= 40U && ObjectManager.Pet.Position.DistanceTo(ObjectManager.Target.Position) <= 6f;
			if (flag)
			{
				PetManager.PetSpellCast("Bite");
			}
			this.RefreshPet();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001EC80 File Offset: 0x0001CE80
		protected override void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			this.RefreshPet();
			bool petfeed = BasePersistentSettings<HunterLevelSettings>.Current.Petfeed;
			if (petfeed)
			{
				this.FeedPet();
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		private void RefreshPet()
		{
			bool flag = this.RevivePet.IsSpellUsable && this.RevivePet.KnownSpell && Constants.Pet.IsDead && !Constants.Me.IsMounted;
			if (flag)
			{
				this.RevivePet.Launch();
				Usefuls.WaitIsCasting();
			}
			bool flag2 = this.CallPet.IsSpellUsable && this.CallPet.KnownSpell && !Constants.Pet.IsValid && !Constants.Me.IsMounted;
			if (flag2)
			{
				this.CallPet.Launch();
				Usefuls.WaitIsCasting();
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001ED58 File Offset: 0x0001CF58
		private void FeedPet()
		{
			bool flag = Constants.Pet.IsAlive && this.PetFeedTimer.IsReady && PetHelper.Happiness < 3;
			if (flag)
			{
				PetHelper.Feed();
			}
			this.PetFeedTimer.Reset(5000.0);
		}

		// Token: 0x0400044C RID: 1100
		private readonly Timer PetFeedTimer = new Timer();

		// Token: 0x0400044D RID: 1101
		private readonly Spell RevivePet = new Spell("Revive Pet");

		// Token: 0x0400044E RID: 1102
		private readonly Spell CallPet = new Spell("Call Pet");
	}
}
