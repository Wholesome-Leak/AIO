using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using AIO;
using AIO.Combat.Common;
using AIO.Framework;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x02000007 RID: 7
internal class RacialManager : ICycleable
{
	// Token: 0x06000020 RID: 32 RVA: 0x000025FC File Offset: 0x000007FC
	private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
	{
		bool flag = !RacialManager.Enabled;
		if (!flag)
		{
			bool flag2 = this.BloodFury.KnownSpell && this.BloodFury.IsSpellUsable && Constants.Me.InCombat;
			if (flag2)
			{
				bool flag3 = RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.GetDistance <= 7f) >= 2 || BossList.isboss;
				if (flag3)
				{
					this.RacialBloodFury();
				}
			}
			bool flag4 = this.Berserking.KnownSpell && this.Berserking.IsSpellUsable && Constants.Me.InCombat;
			if (flag4)
			{
				int range = (Constants.Me.WowClass == 1 || Constants.Me.WowClass == 4 || Constants.Me.WowClass == 6) ? 7 : 30;
				bool flag5 = RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.GetDistance <= (float)range) >= 2 || BossList.isboss;
				if (flag5)
				{
					this.RacialBerserking();
				}
			}
			bool knownSpell = this.WilloftheForsaken.KnownSpell;
			if (knownSpell)
			{
				this.RacialWillForsaken();
			}
			bool knownSpell2 = this.Everyman.KnownSpell;
			if (knownSpell2)
			{
				this.RacialEveryManforHimself();
			}
			bool flag6 = this.WarStomp.KnownSpell && this.WarStomp.IsSpellUsable && Constants.Me.InCombat && !Constants.Me.HaveBuff("Cat Form") && !Constants.Me.HaveBuff("Bear Form") && !Constants.Me.HaveBuff("Dire Bear Form");
			if (flag6)
			{
				bool flag7 = RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.GetDistance <= 7f) >= 2;
				if (flag7)
				{
					this.RacialWarStomp();
				}
			}
			bool knownSpell3 = this.EscapeArtist.KnownSpell;
			if (knownSpell3)
			{
				this.RacialEscapeArtist();
			}
			bool knownSpell4 = this.StoneForm.KnownSpell;
			if (knownSpell4)
			{
				this.RacialStoneform();
			}
			bool flag8 = this.ArcaneTorrent.KnownSpell && Constants.Target.GetDistance <= 8f && Constants.Target.IsCast;
			if (flag8)
			{
				this.RacialArcaneTorrent();
			}
			bool flag9 = this.GiftoftheNaruu.KnownSpell && this.GiftoftheNaruu.IsSpellUsable && Constants.Me.InCombat;
			if (flag9)
			{
				bool flag10 = RotationFramework.Enemies.Count((WoWUnit o) => o.IsTargetingMeOrMyPetOrPartyMember && o.GetDistance <= 7f) >= 2;
				if (flag10)
				{
					this.RacialGiftofNaru();
				}
			}
			bool flag11 = this.Shadowmeld.KnownSpell && Constants.Me.HealthPercent < 5.0;
			if (flag11)
			{
				this.RacialShadowmeld();
			}
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002908 File Offset: 0x00000B08
	private void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
	{
		bool flag = !RacialManager.Enabled;
		if (!flag)
		{
			bool flag2 = !Constants.Me.InCombat && Constants.Me.HealthPercent < 50.0 && Constants.Me.IsAlive;
			if (flag2)
			{
				this.RacialCannibalize();
			}
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002960 File Offset: 0x00000B60
	private void RacialCannibalize()
	{
		bool flag = (from u in RotationFramework.AllUnits
		where u.GetDistance <= 8f && u.IsDead && (u.CreatureTypeTarget == "Humanoid" || u.CreatureTypeTarget == "Undead")
		select u).Count<WoWUnit>() > 0;
		if (flag)
		{
			bool flag2 = !Constants.Me.HaveBuff("Drink") && !Constants.Me.HaveBuff("Food") && Constants.Me.IsAlive;
			if (flag2)
			{
				bool flag3 = this.Cannibalize.KnownSpell && this.Cannibalize.IsSpellUsable;
				if (flag3)
				{
					this.Cannibalize.Launch();
					Usefuls.WaitIsCasting();
				}
			}
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002A10 File Offset: 0x00000C10
	private void RacialShadowmeld()
	{
		bool flag = Constants.Me.InCombat && this.Shadowmeld.KnownSpell && this.Shadowmeld.IsSpellUsable;
		if (flag)
		{
			this.Shadowmeld.Launch();
			Thread.Sleep(8000);
			Usefuls.WaitIsCasting();
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002A68 File Offset: 0x00000C68
	private void RacialWillForsaken()
	{
		bool flag = Constants.Me.InCombat && (Constants.Me.HaveBuff("Fear") || Constants.Me.HaveBuff("Charm") || Constants.Me.HaveBuff("Sleep"));
		if (flag)
		{
			bool flag2 = this.WilloftheForsaken.KnownSpell && this.WilloftheForsaken.IsSpellUsable;
			if (flag2)
			{
				this.WilloftheForsaken.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002AF4 File Offset: 0x00000CF4
	private void RacialGiftofNaru()
	{
		bool flag = Constants.Me.InCombat && this.GiftoftheNaruu.KnownSpell && this.GiftoftheNaruu.IsSpellUsable;
		if (flag)
		{
			this.GiftoftheNaruu.Launch();
			Usefuls.WaitIsCasting();
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002B44 File Offset: 0x00000D44
	private void RacialEveryManforHimself()
	{
		bool flag = Constants.Me.InCombat && (Constants.Me.HaveBuff("Fear") || Constants.Me.HaveBuff("Charm") || Constants.Me.HaveBuff("Sleep"));
		if (flag)
		{
			bool flag2 = this.Everyman.KnownSpell && this.Everyman.IsSpellUsable;
			if (flag2)
			{
				this.Everyman.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002BD0 File Offset: 0x00000DD0
	private void RacialStoneform()
	{
		bool flag = Constants.Me.InCombat && (Extension.HasPoisonDebuff() || Extension.HasDiseaseDebuff());
		if (flag)
		{
			bool flag2 = this.StoneForm.KnownSpell && this.StoneForm.IsSpellUsable;
			if (flag2)
			{
				this.StoneForm.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002C38 File Offset: 0x00000E38
	private void RacialEscapeArtist()
	{
		bool flag = (Constants.Me.InCombat && Constants.Me.Rooted) || Constants.Me.HaveBuff("Frostnova");
		if (flag)
		{
			bool flag2 = this.EscapeArtist.KnownSpell && this.EscapeArtist.IsSpellUsable;
			if (flag2)
			{
				this.EscapeArtist.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002CAC File Offset: 0x00000EAC
	private void RacialWarStomp()
	{
		bool flag = Constants.Me.InCombat && Constants.Me.IsAlive && !Constants.Me.HaveBuff("Bear Form") && !Constants.Me.HaveBuff("Cat Form") && !Constants.Me.HaveBuff("Dire Bear Form");
		if (flag)
		{
			bool flag2 = this.WarStomp.KnownSpell && this.WarStomp.IsSpellUsable;
			if (flag2)
			{
				this.WarStomp.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002D44 File Offset: 0x00000F44
	private void RacialArcaneTorrent()
	{
		bool flag = Constants.Me.InCombat && Constants.Me.IsAlive;
		if (flag)
		{
			bool flag2 = this.ArcaneTorrent.KnownSpell && this.ArcaneTorrent.IsSpellUsable;
			if (flag2)
			{
				this.ArcaneTorrent.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002DA4 File Offset: 0x00000FA4
	private void RacialBloodFury()
	{
		bool flag = Constants.Me.InCombat && Constants.Me.IsAlive;
		if (flag)
		{
			bool flag2 = this.BloodFury.KnownSpell && this.BloodFury.IsSpellUsable;
			if (flag2)
			{
				this.BloodFury.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002E04 File Offset: 0x00001004
	private void RacialBerserking()
	{
		bool flag = Constants.Me.InCombat && Constants.Me.IsAlive;
		if (flag)
		{
			bool flag2 = this.Berserking.KnownSpell && this.Berserking.IsSpellUsable;
			if (flag2)
			{
				this.Berserking.Launch();
				Usefuls.WaitIsCasting();
			}
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002E64 File Offset: 0x00001064
	public void Initialize()
	{
		FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
		MovementEvents.OnMovementPulse += new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002E8B File Offset: 0x0000108B
	public void Dispose()
	{
		FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
		MovementEvents.OnMovementPulse -= new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
	}

	// Token: 0x04000007 RID: 7
	private readonly Spell Cannibalize = new Spell("Cannibalize");

	// Token: 0x04000008 RID: 8
	private readonly Spell ArcaneTorrent = new Spell("Arcane Torrent");

	// Token: 0x04000009 RID: 9
	private readonly Spell WarStomp = new Spell("War Stomp");

	// Token: 0x0400000A RID: 10
	private readonly Spell BloodFury = new Spell("Blood Fury");

	// Token: 0x0400000B RID: 11
	private readonly Spell Berserking = new Spell("Berserking");

	// Token: 0x0400000C RID: 12
	private readonly Spell WilloftheForsaken = new Spell("Will of the Forsaken");

	// Token: 0x0400000D RID: 13
	private readonly Spell Everyman = new Spell("Every Man for Himself");

	// Token: 0x0400000E RID: 14
	private readonly Spell StoneForm = new Spell("Stoneform");

	// Token: 0x0400000F RID: 15
	private readonly Spell EscapeArtist = new Spell("Escape Artist");

	// Token: 0x04000010 RID: 16
	private readonly Spell GiftoftheNaruu = new Spell("Gift of the Naaru");

	// Token: 0x04000011 RID: 17
	private readonly Spell Shadowmeld = new Spell("Shadowmeld");

	// Token: 0x04000012 RID: 18
	public static bool Enabled = true;
}
