using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public sealed class PriestLevelSettings : BasePersistentSettings<PriestLevelSettings>
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00009E20 File Offset: 0x00008020
		public PriestLevelSettings()
		{
			this.ChooseTalent = "PriestShadow";
			this.UseWand = true;
			this.UseWandTresh = 20;
			this.UseAutoBuffInt = true;
			this.UseAutoBuff = false;
			this.ShadowForm = true;
			this.ShadowShadowfiend = 30;
			this.ShadowUseHeaInGrp = false;
			this.ShadowUseShieldTresh = 60;
			this.ShadowUseRenewTresh = 90;
			this.ShadowUseFlashTresh = 60;
			this.ShadowUseHealTresh = 40;
			this.ShadowUseMindflay = false;
			this.ShadowDPUse = true;
			this.ShadowDotOff = true;
			this.ShadowUseShieldParty = true;
			this.ShadowDispersion = 30;
			this.HolyGuardianSpiritTresh = 25;
			this.HolyBigSingleTargetHeal = 65;
			this.HolyBindingHealTresh = 85;
			this.HolyPrayerOfMendingTresh = 95;
			this.HolyPrayerOfHealingTresh = 80;
			this.HolyCircleOfHealingTresh = 90;
			this.HolyDivineHymnTresh = 45;
			this.HolyProtectAgainstFear = true;
			this.HolyUseMindSoothe = true;
			this.HolyMindSootheDistance = 6;
			this.HolyShackleUndead = false;
			this.HolyOffTankCastingMana = 40;
			this.HolyPreventiveHealMana = 70;
			this.HolyIgnoreManaManagementOOC = true;
			this.HolyCustomTank = "";
			this.HolyDeDeBuff = false;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009F55 File Offset: 0x00008155
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009F5D File Offset: 0x0000815D
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Use Wand?")]
		[Description("Use Wand in General?")]
		public bool UseWand { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009F66 File Offset: 0x00008166
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009F6E File Offset: 0x0000816E
		[Setting]
		[DefaultValue(20)]
		[Category("General")]
		[DisplayName("Use Wand Treshold?")]
		[Description("Life Treshold for Wandusage?")]
		[Percentage(true)]
		public int UseWandTresh { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009F77 File Offset: 0x00008177
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009F7F File Offset: 0x0000817F
		[Setting]
		[DefaultValue(false)]
		[Category("General")]
		[DisplayName("Use standart Buffing (Solo)?")]
		[Description("Use Standart AutoBuffing in General?")]
		public bool UseAutoBuff { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009F88 File Offset: 0x00008188
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00009F90 File Offset: 0x00008190
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Use intelligent Buffing (Group)?")]
		[Description("Use Intelligent Buffing in General?")]
		public bool UseAutoBuffInt { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009F99 File Offset: 0x00008199
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00009FA1 File Offset: 0x000081A1
		[Setting]
		[DefaultValue(true)]
		[Category("Shadow")]
		[DisplayName("ShadowForm?")]
		[Description("Use ShadowForm in General?")]
		public bool ShadowForm { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009FAA File Offset: 0x000081AA
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009FB2 File Offset: 0x000081B2
		[Setting]
		[DefaultValue(30)]
		[Category("Shadow")]
		[DisplayName("Shadowfiend")]
		[Description("% when use Shadowfiend in General?")]
		public int ShadowShadowfiend { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009FBB File Offset: 0x000081BB
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009FC3 File Offset: 0x000081C3
		[Setting]
		[DefaultValue(true)]
		[Category("Shadow")]
		[DisplayName("Use Dot on Offtargets?")]
		[Description("Use Dot on Adds?")]
		public bool ShadowDotOff { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009FCC File Offset: 0x000081CC
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009FD4 File Offset: 0x000081D4
		[Setting]
		[DefaultValue(true)]
		[Category("Shadow")]
		[DisplayName("Use Devouring Plague?")]
		[Description("Use DP up to level 80?")]
		public bool ShadowDPUse { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009FDD File Offset: 0x000081DD
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009FE5 File Offset: 0x000081E5
		[Setting]
		[DefaultValue(60)]
		[Category("Shadow")]
		[DisplayName("Use Shield Treshold?")]
		[Description("Own life for Shield Usage?")]
		[Percentage(true)]
		public int ShadowUseShieldTresh { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009FEE File Offset: 0x000081EE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009FF6 File Offset: 0x000081F6
		[Setting]
		[DefaultValue(90)]
		[Category("Shadow")]
		[DisplayName("Use Renew Treshold?")]
		[Description("Treshold for Renew Usage?")]
		[Percentage(true)]
		public int ShadowUseRenewTresh { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009FFF File Offset: 0x000081FF
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A007 File Offset: 0x00008207
		[Setting]
		[DefaultValue(40)]
		[Category("Shadow")]
		[DisplayName("Use Heal Treshold?")]
		[Description("Treshold for Heal Usage?")]
		[Percentage(true)]
		public int ShadowUseHealTresh { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000A010 File Offset: 0x00008210
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000A018 File Offset: 0x00008218
		[Setting]
		[DefaultValue(60)]
		[Category("Shadow")]
		[DisplayName("Use Flash Heal Treshold?")]
		[Description("Treshold for Flash Heal Usage?")]
		[Percentage(true)]
		public int ShadowUseFlashTresh { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000A021 File Offset: 0x00008221
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000A029 File Offset: 0x00008229
		[Setting]
		[DefaultValue(30)]
		[Category("Shadow")]
		[DisplayName("Dispersion")]
		[Description("Treshold for Dispersion Usage?")]
		[Percentage(true)]
		public int ShadowDispersion { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000A032 File Offset: 0x00008232
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000A03A File Offset: 0x0000823A
		[Setting]
		[DefaultValue(false)]
		[Category("Shadow")]
		[DisplayName("Use Mindflay?")]
		[Description("Use Mindflay in General?")]
		public bool ShadowUseMindflay { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A043 File Offset: 0x00008243
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000A04B File Offset: 0x0000824B
		[Setting]
		[DefaultValue(false)]
		[Category("Shadow")]
		[DisplayName("Use Heal in Group?")]
		[Description("Use Heal in Group?")]
		public bool ShadowUseHeaInGrp { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A054 File Offset: 0x00008254
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000A05C File Offset: 0x0000825C
		[Setting]
		[DefaultValue(true)]
		[Category("Shadow")]
		[DisplayName("PW Shield?")]
		[Description("Use Shield on Partymembers in Shadow Spec?")]
		public bool ShadowUseShieldParty { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000A065 File Offset: 0x00008265
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000A06D File Offset: 0x0000826D
		[Setting]
		[DefaultValue("")]
		[Category("Holy")]
		[DisplayName("Custom Tank")]
		[Description("If you want to override the tank. Leave empty if you don't know")]
		public string HolyCustomTank { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A076 File Offset: 0x00008276
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000A07E File Offset: 0x0000827E
		[Setting]
		[DefaultValue(65)]
		[Category("Holy")]
		[DisplayName("Slow Heal")]
		[Description("Treshhold to cast a big single target heal")]
		[Percentage(true)]
		public int HolyBigSingleTargetHeal { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A087 File Offset: 0x00008287
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000A08F File Offset: 0x0000828F
		[Setting]
		[DefaultValue(85)]
		[Category("Holy")]
		[DisplayName("Binding Heal")]
		[Description("Treshhold to cast binding heal on you and a friendly target")]
		[Percentage(true)]
		public int HolyBindingHealTresh { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A098 File Offset: 0x00008298
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000A0A0 File Offset: 0x000082A0
		[Setting]
		[DefaultValue(95)]
		[Category("Holy")]
		[DisplayName("Prayer of Mending")]
		[Description("Treshhold to cast Prayer of Mending on friendly targets")]
		[Percentage(true)]
		public int HolyPrayerOfMendingTresh { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A0A9 File Offset: 0x000082A9
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000A0B1 File Offset: 0x000082B1
		[Setting]
		[DefaultValue(90)]
		[Category("Holy")]
		[DisplayName("Circle of Healing")]
		[Description("Treshhold to cast Circle of Healing on friendly targets")]
		[Percentage(true)]
		public int HolyCircleOfHealingTresh { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A0BA File Offset: 0x000082BA
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A0C2 File Offset: 0x000082C2
		[Setting]
		[DefaultValue(80)]
		[Category("Holy")]
		[DisplayName("Prayer of Healing")]
		[Description("Treshhold to cast Prayer of Healing on friendly targets")]
		[Percentage(true)]
		public int HolyPrayerOfHealingTresh { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A0CB File Offset: 0x000082CB
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A0D3 File Offset: 0x000082D3
		[Setting]
		[DefaultValue(45)]
		[Category("Holy")]
		[DisplayName("Divine Hymn")]
		[Description("Treshhold to cast Divine Hymn if 3 party members fall below the mana treshhold")]
		[Percentage(true)]
		public int HolyDivineHymnTresh { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A0DC File Offset: 0x000082DC
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000A0E4 File Offset: 0x000082E4
		[Setting]
		[DefaultValue(25)]
		[Category("Holy")]
		[DisplayName("Guardian Spirit")]
		[Description("Treshhold to cast Guardian Spirit on tank or me")]
		[Percentage(true)]
		public int HolyGuardianSpiritTresh { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A0ED File Offset: 0x000082ED
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000A0F5 File Offset: 0x000082F5
		[Setting]
		[DefaultValue(true)]
		[Category("Holy")]
		[DisplayName("Fear Ward")]
		[Description("Cast Fear Ward if an enemy is casting an fear inducing spell on us")]
		public bool HolyProtectAgainstFear { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A0FE File Offset: 0x000082FE
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000A106 File Offset: 0x00008306
		[Setting]
		[DefaultValue(true)]
		[Category("Holy")]
		[DisplayName("Mind Soothe")]
		[Description("Uses Mind Soothe if you are too close to an enemy")]
		public bool HolyUseMindSoothe { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A10F File Offset: 0x0000830F
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000A117 File Offset: 0x00008317
		[Setting]
		[DefaultValue(6)]
		[Category("Holy")]
		[DisplayName("Mind Soothe dist.")]
		[Description("Distance to cast Mind Soothe before an enemy will attack you")]
		public int HolyMindSootheDistance { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A120 File Offset: 0x00008320
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000A128 File Offset: 0x00008328
		[Setting]
		[DefaultValue(false)]
		[Category("Holy")]
		[DisplayName("Shackle Undead")]
		[Description("Shackles Undead if they are out of range and targeting you")]
		public bool HolyShackleUndead { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A131 File Offset: 0x00008331
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000A139 File Offset: 0x00008339
		[Setting]
		[DefaultValue(40)]
		[Category("Holy")]
		[DisplayName("Cast Off Tank")]
		[Description("Minimum mana percentage to do off tank casts")]
		[Percentage(true)]
		public int HolyOffTankCastingMana { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A142 File Offset: 0x00008342
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000A14A File Offset: 0x0000834A
		[Setting]
		[DefaultValue(70)]
		[Category("Holy")]
		[DisplayName("Preventive Healing")]
		[Description("Minimum mana percentage to do preventive healing (100 will disable it)")]
		[Percentage(true)]
		public int HolyPreventiveHealMana { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A153 File Offset: 0x00008353
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000A15B File Offset: 0x0000835B
		[Setting]
		[DefaultValue(true)]
		[Category("Holy")]
		[DisplayName("OOC Refresh")]
		[Description("Will refresh your group OOC? Will burn up your mana. Expected to use drinks afterwards")]
		public bool HolyIgnoreManaManagementOOC { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A164 File Offset: 0x00008364
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A16C File Offset: 0x0000836C
		[Setting]
		[DefaultValue(false)]
		[Category("Holy")]
		[DisplayName("De-DeBuff")]
		[Description("Will remove harmful magic and diseases")]
		public bool HolyDeDeBuff { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A175 File Offset: 0x00008375
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000A17D File Offset: 0x0000837D
		[DropdownList(new string[]
		{
			"PriestShadow",
			"PriestDiscipline",
			"PriestHoly"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A186 File Offset: 0x00008386
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000A18E File Offset: 0x0000838E
		[DropdownList(new string[]
		{
			"Auto",
			"Shadow",
			"Discipline",
			"Holy"
		})]
		public override string ChooseRotation { get; set; }
	}
}
