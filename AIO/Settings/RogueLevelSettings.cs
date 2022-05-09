using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class RogueLevelSettings : BasePersistentSettings<RogueLevelSettings>
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A197 File Offset: 0x00008397
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000A19F File Offset: 0x0000839F
		[Setting]
		[DefaultValue(true)]
		[Category("Fighting")]
		[DisplayName("Ranged Pull")]
		[Description("Should we use ranged pull when we have ranged weapon?")]
		public bool PullRanged { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A1A8 File Offset: 0x000083A8
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000A1B0 File Offset: 0x000083B0
		[Setting]
		[DefaultValue(false)]
		[Category("Fighting")]
		[DisplayName("Stealth")]
		[Description("Use Stealth?")]
		public bool Stealth { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A1B9 File Offset: 0x000083B9
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000A1C1 File Offset: 0x000083C1
		[Setting]
		[DefaultValue(false)]
		[Category("Fighting")]
		[DisplayName("Distracting  (not functional atm")]
		[Description("Use distracting while stealthed?")]
		public bool Distract { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A1CA File Offset: 0x000083CA
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000A1D2 File Offset: 0x000083D2
		[Setting]
		[DefaultValue(2)]
		[Category("Fighting")]
		[DisplayName("Evasion")]
		[Description("Enemycount for using Evasion?")]
		public int Evasion { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A1DB File Offset: 0x000083DB
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000A1E3 File Offset: 0x000083E3
		[Setting]
		[DefaultValue(2)]
		[Category("Fighting")]
		[DisplayName("Blade Flurry")]
		[Description("Enemycount for using BladeFlurry?")]
		public int BladeFLurry { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A1EC File Offset: 0x000083EC
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000A1F4 File Offset: 0x000083F4
		[Setting]
		[DefaultValue(2)]
		[Category("Fighting")]
		[DisplayName("Killing Spree")]
		[Description("Enemycount for using Killing Spree?")]
		public int KillingSpree { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A1FD File Offset: 0x000083FD
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000A205 File Offset: 0x00008405
		[Setting]
		[DefaultValue(3)]
		[Category("Fighting")]
		[DisplayName("Adrenaline Rush")]
		[Description("Enemycount for using Adrenaline Rush?")]
		public int AdrenalineRush { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A20E File Offset: 0x0000840E
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000A216 File Offset: 0x00008416
		[Setting]
		[DefaultValue(3)]
		[Category("Fighting")]
		[DisplayName("Eviscarate")]
		[Description("Combopoints for using Eviscarate?")]
		public int Eviscarate { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A21F File Offset: 0x0000841F
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000A227 File Offset: 0x00008427
		[DropdownList(new string[]
		{
			"RogueCombat",
			"RogueAssassination",
			"RogueSubletly"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A230 File Offset: 0x00008430
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000A238 File Offset: 0x00008438
		[DropdownList(new string[]
		{
			"Auto",
			"Combat",
			"Assassination",
			"Sublety"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A244 File Offset: 0x00008444
		public RogueLevelSettings()
		{
			this.ChooseTalent = "RogueCombat";
			this.Evasion = 2;
			this.BladeFLurry = 2;
			this.KillingSpree = 2;
			this.AdrenalineRush = 3;
			this.Eviscarate = 3;
			this.ChooseTalent = "RogueCombat";
			this.Stealth = false;
			this.Distract = false;
			this.PullRanged = true;
		}
	}
}
