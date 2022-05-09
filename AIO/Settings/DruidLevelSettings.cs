using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class DruidLevelSettings : BasePersistentSettings<DruidLevelSettings>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00009237 File Offset: 0x00007437
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000923F File Offset: 0x0000743F
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Swimming")]
		[Description("Make use if Swimming Form while swimming??")]
		public bool Swimming { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00009248 File Offset: 0x00007448
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00009250 File Offset: 0x00007450
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Buffing IC")]
		[Description("Should the Bot Buff while InCombat?")]
		public bool BuffIC { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00009259 File Offset: 0x00007459
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00009261 File Offset: 0x00007461
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("HealOOC")]
		[Description("Use Heal Out of Combat?")]
		public bool HealOOC { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000926A File Offset: 0x0000746A
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00009272 File Offset: 0x00007472
		[Setting]
		[DefaultValue(2)]
		[Category("Feral")]
		[DisplayName("Bear")]
		[Description("Set the Amount of Enemies in Close Range to switch to Bear?")]
		public int FeralBearCount { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000927B File Offset: 0x0000747B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00009283 File Offset: 0x00007483
		[Setting]
		[DefaultValue(25)]
		[Category("General")]
		[DisplayName("Innervate")]
		[Description("Set the Mana Treshhold, when to use Innervate?")]
		public int Innervate { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000928C File Offset: 0x0000748C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00009294 File Offset: 0x00007494
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Prowl")]
		[Description("Use Prowl?")]
		public bool Prowl { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000929D File Offset: 0x0000749D
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000092A5 File Offset: 0x000074A5
		[Setting]
		[DefaultValue(false)]
		[Category("Feral")]
		[DisplayName("Force Faerie")]
		[Description("Use Faerie for pull?")]
		public bool ForceFaerie { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000092AE File Offset: 0x000074AE
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000092B6 File Offset: 0x000074B6
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Tigers Fury")]
		[Description("Use Tigers Fury on Cooldown?")]
		public bool TF { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000092BF File Offset: 0x000074BF
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000092C7 File Offset: 0x000074C7
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Ferocious Bite")]
		[Description("Use FB?")]
		public bool FeralFB { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000092D0 File Offset: 0x000074D0
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000092D8 File Offset: 0x000074D8
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Rip")]
		[Description("Use Rip?")]
		public bool FeralRip { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000092E1 File Offset: 0x000074E1
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000092E9 File Offset: 0x000074E9
		[Setting]
		[DefaultValue(30)]
		[Category("Feral")]
		[DisplayName("Rip Health")]
		[Description("Set the Health Treshhold until when Rip is used?!")]
		public int FeralRipHealth { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000092F2 File Offset: 0x000074F2
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000092FA File Offset: 0x000074FA
		[Setting]
		[DefaultValue(5)]
		[Category("Feral")]
		[DisplayName("Ferocious Bite/Rip")]
		[Description("Set the Combopoint, when to use FB/Rip?")]
		public int FBC { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00009303 File Offset: 0x00007503
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000930B File Offset: 0x0000750B
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Faerie Fire Feral")]
		[Description("Use FF in the Rotation?")]
		public bool FFF { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00009314 File Offset: 0x00007514
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000931C File Offset: 0x0000751C
		[Setting]
		[DefaultValue(40)]
		[Category("Feral")]
		[DisplayName("OOC Regrowth")]
		[Description("Set the HealthTreshhold for OOC Regrwoth Healing")]
		public int FeralRegrowth { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00009325 File Offset: 0x00007525
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000932D File Offset: 0x0000752D
		[Setting]
		[DefaultValue(85)]
		[Category("Feral")]
		[DisplayName("OOC Rejuvenation")]
		[Description("Set the HealthTreshhold for OOC Rejuvenation Healing")]
		public int FeralRejuvenation { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00009336 File Offset: 0x00007536
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000933E File Offset: 0x0000753E
		[Setting]
		[DefaultValue(false)]
		[Category("Feral")]
		[DisplayName("Decurse")]
		[Description("Decurse Important Spells as Feral in Combat?")]
		public bool FeralDecurse { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00009347 File Offset: 0x00007547
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000934F File Offset: 0x0000754F
		[Setting]
		[DefaultValue(true)]
		[Category("Balance")]
		[DisplayName("Use AOE in Instance")]
		[Description("Set this if you want to use AOE in Instance")]
		public bool UseAOE { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00009358 File Offset: 0x00007558
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00009360 File Offset: 0x00007560
		[Setting]
		[DefaultValue(true)]
		[Category("Balance")]
		[DisplayName("Use Starfall in Instance")]
		[Description("Set this if you want to use Starfall in Instance")]
		public bool UseStarfall { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00009369 File Offset: 0x00007569
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00009371 File Offset: 0x00007571
		[Setting]
		[DefaultValue(3)]
		[Category("Balance")]
		[DisplayName("AOE in Instance")]
		[Description("Number of Targets around the Tank to use AOE in Instance")]
		[Percentage(false)]
		public int AOEInstance { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000937A File Offset: 0x0000757A
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00009382 File Offset: 0x00007582
		[Setting]
		[DefaultValue(true)]
		[Category("Feral")]
		[DisplayName("Dash")]
		[Description("Use Dash while stealthed?")]
		public bool Dash { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000938B File Offset: 0x0000758B
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00009393 File Offset: 0x00007593
		[Setting]
		[DefaultValue(10)]
		[Category("Balance")]
		[DisplayName("Healing Touch Treshhold")]
		[Description("Set the Healing Treshhold for Healing Touch")]
		[Percentage(false)]
		public int BalanceHealingTouch { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000939C File Offset: 0x0000759C
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000093A4 File Offset: 0x000075A4
		[Setting]
		[DefaultValue(30)]
		[Category("Balance")]
		[DisplayName("Rejuvenatio Treshhold")]
		[Description("Set the Healing Treshhold for Rejuvenation")]
		[Percentage(false)]
		public int BalanceRejuvenation { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000093AD File Offset: 0x000075AD
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000093B5 File Offset: 0x000075B5
		[Setting]
		[DefaultValue(60)]
		[Category("Balance")]
		[DisplayName("Regrowth Treshhold")]
		[Description("Set the Healing Treshhold for Regrowth")]
		[Percentage(false)]
		public int BalanceRegrowth { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000093BE File Offset: 0x000075BE
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000093C6 File Offset: 0x000075C6
		[Setting]
		[DefaultValue(65)]
		[Category("Restoration")]
		[DisplayName("Regrowth")]
		[Description("Treshhold for Regrowth")]
		[Percentage(false)]
		public int RestorationRegrowth { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000093CF File Offset: 0x000075CF
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000093D7 File Offset: 0x000075D7
		[Setting]
		[DefaultValue(90)]
		[Category("Restoration")]
		[DisplayName("Rejuvenation")]
		[Description("Treshhold for Rejuvenation")]
		[Percentage(false)]
		public int RestorationRejuvenation { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000093E0 File Offset: 0x000075E0
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000093E8 File Offset: 0x000075E8
		[Setting]
		[DefaultValue(60)]
		[Category("Restoration")]
		[DisplayName("Swiftmend")]
		[Description("Treshhold for Swiftmend")]
		[Percentage(false)]
		public int RestorationSwiftmend { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000093F1 File Offset: 0x000075F1
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000093F9 File Offset: 0x000075F9
		[Setting]
		[DefaultValue(90)]
		[Category("Restoration")]
		[DisplayName("Wild Growth")]
		[Description("Treshhold for Wild Growth")]
		[Percentage(false)]
		public int RestorationWildGrowth { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00009402 File Offset: 0x00007602
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000940A File Offset: 0x0000760A
		[Setting]
		[DefaultValue(2)]
		[Category("Restoration")]
		[DisplayName("Wild Growth")]
		[Description("Treshhold for Wild Growth Player Count")]
		[Percentage(false)]
		public int RestorationWildGrowthCount { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00009413 File Offset: 0x00007613
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000941B File Offset: 0x0000761B
		[Setting]
		[DefaultValue(2)]
		[Category("Restoration")]
		[DisplayName("Healing Touch")]
		[Description("Treshhold for Healing Touch")]
		[Percentage(false)]
		public int RestorationHealingTouch { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00009424 File Offset: 0x00007624
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000942C File Offset: 0x0000762C
		[Setting]
		[DefaultValue(95)]
		[Category("Restoration")]
		[DisplayName("Lifebloom")]
		[Description("Treshhold for Lifebloom")]
		[Percentage(false)]
		public int RestorationLifebloom { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00009435 File Offset: 0x00007635
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000943D File Offset: 0x0000763D
		[Setting]
		[DefaultValue(3)]
		[Category("Restoration")]
		[DisplayName("Lifebloom")]
		[Description("Count for bloom Stacks on Tank")]
		[Percentage(false)]
		public int RestorationLifebloomCount { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00009446 File Offset: 0x00007646
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000944E File Offset: 0x0000764E
		[Setting]
		[DefaultValue(50)]
		[Category("Restoration")]
		[DisplayName("Nourish")]
		[Description("Treshhold for Nourish use")]
		[Percentage(false)]
		public int RestorationNourish { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00009457 File Offset: 0x00007657
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000945F File Offset: 0x0000765F
		[Setting]
		[DefaultValue(true)]
		[Category("Restoration")]
		[DisplayName("Rebirth")]
		[Description("AutoRebirth on dead Targets?")]
		public bool RestorationRebirthAuto { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00009468 File Offset: 0x00007668
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00009470 File Offset: 0x00007670
		[DropdownList(new string[]
		{
			"DruidFeral",
			"DruidBalance",
			"DruidRestoration"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00009479 File Offset: 0x00007679
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00009481 File Offset: 0x00007681
		[DropdownList(new string[]
		{
			"Auto",
			"FeralCombat",
			"Balance",
			"Restoration"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x060000F5 RID: 245 RVA: 0x0000948C File Offset: 0x0000768C
		public DruidLevelSettings()
		{
			this.ChooseTalent = "DruidFeral";
			this.Swimming = true;
			this.BuffIC = true;
			this.HealOOC = true;
			this.Prowl = true;
			this.ForceFaerie = false;
			this.TF = true;
			this.FBC = 5;
			this.FFF = true;
			this.Dash = true;
			this.FeralFB = true;
			this.FeralRip = true;
			this.FeralBearCount = 2;
			this.Innervate = 25;
			this.FeralRipHealth = 30;
			this.FeralRegrowth = 60;
			this.FeralRejuvenation = 30;
			this.FeralDecurse = false;
			this.UseAOE = true;
			this.UseStarfall = true;
			this.AOEInstance = 3;
			this.RestorationRebirthAuto = true;
			this.BalanceHealingTouch = 10;
			this.BalanceRegrowth = 60;
			this.BalanceRejuvenation = 30;
			this.RestorationSwiftmend = 60;
			this.RestorationRegrowth = 65;
			this.RestorationRejuvenation = 90;
			this.RestorationWildGrowth = 90;
			this.RestorationWildGrowthCount = 2;
			this.RestorationHealingTouch = 50;
			this.RestorationLifebloom = 95;
			this.RestorationLifebloomCount = 3;
			this.RestorationNourish = 50;
		}
	}
}
