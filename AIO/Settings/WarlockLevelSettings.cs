using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class WarlockLevelSettings : BasePersistentSettings<WarlockLevelSettings>
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A4EF File Offset: 0x000086EF
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000A4F7 File Offset: 0x000086F7
		[Setting]
		[DefaultValue(true)]
		[Category("Pet")]
		[DisplayName("Cast Pet Infight?")]
		[Description("Checks if Pet is dead and Cast Infight?")]
		public bool PetInfight { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A500 File Offset: 0x00008700
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000A508 File Offset: 0x00008708
		[Setting]
		[DefaultValue(false)]
		[Category("Pet")]
		[DisplayName("Pets for Warlock")]
		[Description("Set your Pet")]
		[DropdownList(new string[]
		{
			"Felguard",
			"Voidwalker",
			"Imp",
			"Felhunter"
		})]
		public string Pet { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A511 File Offset: 0x00008711
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000A519 File Offset: 0x00008719
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Buffing")]
		[Description("True/False for Buffing?")]
		public bool Buffing { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A522 File Offset: 0x00008722
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000A52A File Offset: 0x0000872A
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Soulshards")]
		[Description("Automanage your Soulshards?")]
		public bool Soulshards { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A533 File Offset: 0x00008733
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000A53B File Offset: 0x0000873B
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Healthstone")]
		[Description("Use Healthstone / Cast Healthstone?")]
		public bool Healthstone { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A544 File Offset: 0x00008744
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000A54C File Offset: 0x0000874C
		[Setting]
		[DefaultValue(20)]
		[Category("Fight")]
		[DisplayName("Lifetap")]
		[Description("Tells on which Mana % to use Lifetap")]
		[Percentage(true)]
		public int Lifetap { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A555 File Offset: 0x00008755
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000A55D File Offset: 0x0000875D
		[Setting]
		[DefaultValue(40)]
		[Category("Fight")]
		[DisplayName("Drain Life")]
		[Description("Tells on which Health % to use Drain Life")]
		[Percentage(true)]
		public int Drainlife { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A566 File Offset: 0x00008766
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000A56E File Offset: 0x0000876E
		[Setting]
		[DefaultValue(20)]
		[Category("Fight")]
		[DisplayName("Use Wand Treshold?")]
		[Description("Enemy Life Treshold for Wandusage?")]
		[Percentage(true)]
		public int UseWandTresh { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A577 File Offset: 0x00008777
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000A57F File Offset: 0x0000877F
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Use Wand")]
		[Description("Use Wand?")]
		public bool UseWand { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A588 File Offset: 0x00008788
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000A590 File Offset: 0x00008790
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Shadowbolt")]
		[Description("should Shadowbolt ignore Wand Treshhold?")]
		public bool ShadowboltWand { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A599 File Offset: 0x00008799
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000A5A1 File Offset: 0x000087A1
		[Setting]
		[DefaultValue(30)]
		[Category("Fight")]
		[DisplayName("Health Funnel Pet")]
		[Description("Tells on which PetHealth % to use Health Funnel")]
		[Percentage(true)]
		public int HealthfunnelPet { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A5AA File Offset: 0x000087AA
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000A5B2 File Offset: 0x000087B2
		[Setting]
		[DefaultValue(50)]
		[Category("Fight")]
		[DisplayName("Health Funnel Player")]
		[Description("Tells until which PlayerHealth % to use Health Funnel")]
		[Percentage(true)]
		public int HealthfunnelMe { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A5BB File Offset: 0x000087BB
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000A5C3 File Offset: 0x000087C3
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Use AOE in Instance")]
		[Description("Set this if you want to use AOE in Instance")]
		public bool UseAOE { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A5CC File Offset: 0x000087CC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000A5D4 File Offset: 0x000087D4
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Use AOE Outside Instance")]
		[Description("Set this if you want to use AOE in Outside Instance")]
		public bool UseAOEOutside { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A5DD File Offset: 0x000087DD
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000A5E5 File Offset: 0x000087E5
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Use Seed of Corruption")]
		[Description("Make use of SoC while in Group?")]
		public bool UseSeedGroup { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A5EE File Offset: 0x000087EE
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000A5F6 File Offset: 0x000087F6
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Use Corruption on Multidot")]
		[Description("Make use of Corruption while in Group on multiple Enemies?")]
		public bool UseCorruptionGroup { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A5FF File Offset: 0x000087FF
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A607 File Offset: 0x00008807
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("AOE in Instance")]
		[Description("Number of Targets around the Tank to use AOE in Instance")]
		[Percentage(false)]
		public int AOEInstance { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A610 File Offset: 0x00008810
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000A618 File Offset: 0x00008818
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("AOE Outside Instance")]
		[Description("Number of Targets to use AOE in Outside Instance")]
		[Percentage(false)]
		public int AOEOutsideInstance { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A621 File Offset: 0x00008821
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000A629 File Offset: 0x00008829
		[DropdownList(new string[]
		{
			"WarlockDestruction",
			"WarlockAffliction",
			"WarlockDemonology"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A632 File Offset: 0x00008832
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000A63A File Offset: 0x0000883A
		[DropdownList(new string[]
		{
			"Auto",
			"Destruction",
			"Affliction",
			"Demonology"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x0600024B RID: 587 RVA: 0x0000A644 File Offset: 0x00008844
		public WarlockLevelSettings()
		{
			this.ChooseTalent = "WarlockAffliction";
			this.Healthstone = true;
			this.Pet = "Voidwalker";
			this.PetInfight = true;
			this.UseSeedGroup = true;
			this.UseCorruptionGroup = true;
			this.Lifetap = 20;
			this.Drainlife = 40;
			this.HealthfunnelPet = 30;
			this.HealthfunnelMe = 50;
			this.UseAOE = false;
			this.UseAOEOutside = true;
			this.AOEInstance = 3;
			this.AOEOutsideInstance = 3;
			this.UseWandTresh = 20;
			this.UseWand = true;
			this.Buffing = true;
			this.Soulshards = true;
			this.ShadowboltWand = true;
		}
	}
}
