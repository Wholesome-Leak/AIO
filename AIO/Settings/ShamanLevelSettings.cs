using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class ShamanLevelSettings : BasePersistentSettings<ShamanLevelSettings>
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A2B1 File Offset: 0x000084B1
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A2B9 File Offset: 0x000084B9
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Heal OOC")]
		[Description("Use Healspells Out of Combat?")]
		public bool HealOOC { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A2C2 File Offset: 0x000084C2
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000A2CA File Offset: 0x000084CA
		[Setting]
		[DefaultValue(3)]
		[Category("Elemental")]
		[DisplayName("Chainlightning Count")]
		[Description("Enemy Count to use  Treshhold?")]
		public int ElementalChainlightningTresshold { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000A2D3 File Offset: 0x000084D3
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000A2DB File Offset: 0x000084DB
		[Setting]
		[DefaultValue(false)]
		[Category("Elemental")]
		[DisplayName("Cure Toxin")]
		[Description("Use on Groupmembers??")]
		public bool ElementalCureToxin { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A2E4 File Offset: 0x000084E4
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A2EC File Offset: 0x000084EC
		[Setting]
		[DefaultValue(true)]
		[Category("Elemental")]
		[DisplayName("Flame Shock")]
		[Description("Use Flame Shock in Rotation??")]
		public bool ElementalFlameShock { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A2F5 File Offset: 0x000084F5
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000A2FD File Offset: 0x000084FD
		[Setting]
		[DefaultValue(true)]
		[Category("Elemental")]
		[DisplayName("Earth Shock")]
		[Description("Use Earth Shock in Rotation?")]
		public bool ElementalEarthShock { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A306 File Offset: 0x00008506
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000A30E File Offset: 0x0000850E
		[Setting]
		[DefaultValue(5)]
		[Category("Enhancement")]
		[DisplayName("Fire  Nova")]
		[Description("Use Fire Nova?")]
		public int EnhFireNova { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000A317 File Offset: 0x00008517
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000A31F File Offset: 0x0000851F
		[Setting]
		[DefaultValue(true)]
		[Category("Enhancement")]
		[DisplayName("Ghostwolf")]
		[Description("Use Ghostwolfform?")]
		public bool Ghostwolf { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A328 File Offset: 0x00008528
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000A330 File Offset: 0x00008530
		[Setting]
		[DefaultValue(10)]
		[Category("Enhancement")]
		[DisplayName("Selfheal")]
		[Description("Set the Enemytreshold in % when to heal?")]
		[Percentage(true)]
		public int Enemylife { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A339 File Offset: 0x00008539
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000A341 File Offset: 0x00008541
		[Setting]
		[DefaultValue(false)]
		[Category("Enhancement")]
		[DisplayName("Lightning Bolt")]
		[Description("Use LNB for Pull?")]
		public bool LNB { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A34A File Offset: 0x0000854A
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000A352 File Offset: 0x00008552
		[Setting]
		[DefaultValue(false)]
		[Category("Enhancement")]
		[DisplayName("Cure Toxins")]
		[Description("Use Cure Toxins in Group?")]
		public bool EnhanceCureToxinGroup { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A35B File Offset: 0x0000855B
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000A363 File Offset: 0x00008563
		[Setting]
		[DefaultValue(99)]
		[Category("Restoration")]
		[DisplayName("Earthshield")]
		[Description("Set the Tank Treshhold for Earthshield?")]
		[Percentage(true)]
		public int RestorationEarthshieldTank { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A36C File Offset: 0x0000856C
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000A374 File Offset: 0x00008574
		[Setting]
		[DefaultValue(75)]
		[Category("Restoration")]
		[DisplayName("Riptide")]
		[Description("Set the Treshhold for Riptide usage?")]
		[Percentage(true)]
		public int RestorationRiptideGroup { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A37D File Offset: 0x0000857D
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A385 File Offset: 0x00008585
		[Setting]
		[DefaultValue(85)]
		[Category("Restoration")]
		[DisplayName("Chain Heal / Health")]
		[Description("Set the Player Treshhold for Chain Heal?")]
		[Percentage(true)]
		public int RestorationChainHealGroup { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A38E File Offset: 0x0000858E
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A396 File Offset: 0x00008596
		[Setting]
		[DefaultValue(2)]
		[Category("Restoration")]
		[DisplayName("Chain Heal / Player")]
		[Description("Set the PlayerCount Treshhold for Chain Heal (more then x  Player) ?")]
		[Percentage(false)]
		public int RestorationChainHealCountGroup { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A39F File Offset: 0x0000859F
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000A3A7 File Offset: 0x000085A7
		[Setting]
		[DefaultValue(70)]
		[Category("Restoration")]
		[DisplayName("Healing Wave")]
		[Description("Set the Player Treshhold for Healing Wave?")]
		[Percentage(true)]
		public int RestorationHealingWaveGroup { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A3B0 File Offset: 0x000085B0
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000A3B8 File Offset: 0x000085B8
		[Setting]
		[DefaultValue(85)]
		[Category("Restoration")]
		[DisplayName("Lesser Healing Wave")]
		[Description("Set the Player Treshhold for Lesser Healing Wave?")]
		[Percentage(true)]
		public int RestorationLesserHealingWaveGroup { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A3C1 File Offset: 0x000085C1
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A3C9 File Offset: 0x000085C9
		[Setting]
		[DefaultValue(true)]
		[Category("Totem")]
		[DisplayName("Totemic Recall")]
		[Description("Use Totemic Recall?")]
		public bool UseTotemicCall { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A3D2 File Offset: 0x000085D2
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A3DA File Offset: 0x000085DA
		[Setting]
		[DefaultValue(true)]
		[Category("Totem")]
		[DisplayName("Fire Nova")]
		[Description("Use Fire Nova?")]
		public bool UseFireNova { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A3E3 File Offset: 0x000085E3
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000A3EB File Offset: 0x000085EB
		[Setting]
		[DefaultValue(true)]
		[Category("Totem")]
		[DisplayName("Cleansing Totem")]
		[Description("Use Cleansing Totem?")]
		public bool UseCleansingTotem { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A3F4 File Offset: 0x000085F4
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000A3FC File Offset: 0x000085FC
		[Setting]
		[DefaultValue(true)]
		[Category("Totem")]
		[DisplayName("Grounding Totem")]
		[Description("Use Grounding Totem?")]
		public bool UseGroundingTotem { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A405 File Offset: 0x00008605
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A40D File Offset: 0x0000860D
		[DropdownList(new string[]
		{
			"ShamanEnhancement",
			"ShamanRestoration",
			"ShamanElemental"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A416 File Offset: 0x00008616
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000A41E File Offset: 0x0000861E
		[DropdownList(new string[]
		{
			"Auto",
			"Enhancement",
			"Restoration",
			"Elemental"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x06000222 RID: 546 RVA: 0x0000A428 File Offset: 0x00008628
		public ShamanLevelSettings()
		{
			this.ChooseTalent = "ShamanEnhancement";
			this.HealOOC = true;
			this.Ghostwolf = true;
			this.Enemylife = 10;
			this.LNB = false;
			this.EnhFireNova = 5;
			this.EnhanceCureToxinGroup = false;
			this.ElementalCureToxin = false;
			this.ElementalChainlightningTresshold = 3;
			this.ElementalEarthShock = true;
			this.ElementalFlameShock = true;
			this.RestorationEarthshieldTank = 99;
			this.RestorationChainHealGroup = 85;
			this.RestorationChainHealCountGroup = 2;
			this.RestorationHealingWaveGroup = 70;
			this.RestorationLesserHealingWaveGroup = 85;
			this.RestorationRiptideGroup = 75;
			this.UseTotemicCall = true;
			this.UseFireNova = true;
			this.UseCleansingTotem = true;
			this.UseGroundingTotem = true;
		}
	}
}
