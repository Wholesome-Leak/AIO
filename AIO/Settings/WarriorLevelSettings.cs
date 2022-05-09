using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class WarriorLevelSettings : BasePersistentSettings<WarriorLevelSettings>
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A6FE File Offset: 0x000088FE
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A706 File Offset: 0x00008906
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Hamstring")]
		[Description("Use Hamstring in your Rotation?")]
		public bool Hamstring { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A70F File Offset: 0x0000890F
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A717 File Offset: 0x00008917
		[Setting]
		[DefaultValue(true)]
		[Category("Fury")]
		[DisplayName("Charge")]
		[Description("Should we use Charge?")]
		public bool FuryCharge { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A720 File Offset: 0x00008920
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A728 File Offset: 0x00008928
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Intercept")]
		[Description("Should we use Intercept?")]
		public bool ProtectionIntercept { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A731 File Offset: 0x00008931
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000A739 File Offset: 0x00008939
		[Setting]
		[DefaultValue(65)]
		[Category("Protection")]
		[DisplayName("Enraged Regeneration")]
		[Description("Treshhold for Warrior HP, when to use ER")]
		public int ProtectionEnragedRegeneration { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A742 File Offset: 0x00008942
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000A74A File Offset: 0x0000894A
		[Setting]
		[DefaultValue(2)]
		[Category("Protection")]
		[DisplayName("Shield Block")]
		[Description("Enemycount to use Block Wall")]
		public int ProtectionShieldBlock { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A753 File Offset: 0x00008953
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000A75B File Offset: 0x0000895B
		[Setting]
		[DefaultValue(3)]
		[Category("Protection")]
		[DisplayName("Shield Wall")]
		[Description("Enemycount to use Shield Wall")]
		public int ProtectionShieldWall { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A764 File Offset: 0x00008964
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000A76C File Offset: 0x0000896C
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Taunt")]
		[Description("Should we use Taunt in  Group?")]
		public bool ProtectionTauntGroup { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A775 File Offset: 0x00008975
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000A77D File Offset: 0x0000897D
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Ranged Pull")]
		[Description("Should we use ranged pull when we have ranged weapon?")]
		public bool PullRanged { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A786 File Offset: 0x00008986
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000A78E File Offset: 0x0000898E
		[Setting]
		[DefaultValue(3)]
		[Category("Protection")]
		[DisplayName("Cleave Count")]
		[Description("Enemycount to use Cleave")]
		public int ProtectionCleaveCount { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A797 File Offset: 0x00008997
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A79F File Offset: 0x0000899F
		[Setting]
		[DefaultValue(30)]
		[Category("Protection")]
		[DisplayName("Cleave Count")]
		[Description("Ragecount to use Cleave")]
		public int ProtectionCleaveRageCount { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A7A8 File Offset: 0x000089A8
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000A7B0 File Offset: 0x000089B0
		[Setting]
		[DefaultValue(1)]
		[Category("Protection")]
		[DisplayName("Demoralizing Shout")]
		[Description("Enemycount for Shout?")]
		public int ProtectionDemoralizingCount { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A7B9 File Offset: 0x000089B9
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A7C1 File Offset: 0x000089C1
		[Setting]
		[DefaultValue(2)]
		[Category("Protection")]
		[DisplayName("Shockwave")]
		[Description("Enemycount for Shout?")]
		public int ProtectionShockwaveCount { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A7CA File Offset: 0x000089CA
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A7D2 File Offset: 0x000089D2
		[DropdownList(new string[]
		{
			"WarriorProtection",
			"WarriorArms",
			"WarriorFury"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A7DB File Offset: 0x000089DB
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000A7E3 File Offset: 0x000089E3
		[DropdownList(new string[]
		{
			"Auto",
			"Protection",
			"Arms",
			"Fury"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x06000268 RID: 616 RVA: 0x0000A7EC File Offset: 0x000089EC
		public WarriorLevelSettings()
		{
			this.ChooseTalent = "WarriorFury";
			this.PullRanged = true;
			this.Hamstring = true;
			this.FuryCharge = true;
			this.ProtectionIntercept = true;
			this.ProtectionShieldBlock = 2;
			this.ProtectionShieldWall = 3;
			this.ProtectionCleaveCount = 3;
			this.ProtectionCleaveRageCount = 30;
			this.ProtectionDemoralizingCount = 1;
			this.ProtectionTauntGroup = true;
			this.ProtectionShockwaveCount = 2;
			this.ProtectionEnragedRegeneration = 65;
		}
	}
}
