using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class HunterLevelSettings : BasePersistentSettings<HunterLevelSettings>
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000096D5 File Offset: 0x000078D5
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000096DD File Offset: 0x000078DD
		[Setting]
		[DefaultValue(29)]
		[Category("General")]
		[DisplayName("Range")]
		[Description("Set your Range for your FC")]
		public int RangeSet { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000096E6 File Offset: 0x000078E6
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000096EE File Offset: 0x000078EE
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Feign Death")]
		[Description("Should use Feign Death?")]
		public bool FD { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000096F7 File Offset: 0x000078F7
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000096FF File Offset: 0x000078FF
		[Setting]
		[DefaultValue(false)]
		[Category("Pet")]
		[DisplayName("Use Macro for handle Pet?")]
		[Description("This can be used when Actions are blocked by Server")]
		public bool UseMacro { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00009708 File Offset: 0x00007908
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00009710 File Offset: 0x00007910
		[Setting]
		[DefaultValue(20)]
		[Category("Fight")]
		[DisplayName("Aspect of the Viper")]
		[Description("Set the your  Mana  Treshold when to use AotV")]
		[Percentage(true)]
		public int AspecofViper { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00009719 File Offset: 0x00007919
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00009721 File Offset: 0x00007921
		[Setting]
		[DefaultValue(60)]
		[Category("Fight")]
		[DisplayName("Aspect of the Hawk/DragonHawk")]
		[Description("Set the your  Mana  Treshold when to use Hawk/Dragonhawk")]
		[Percentage(true)]
		public int AspecofHawks { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000972A File Offset: 0x0000792A
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00009732 File Offset: 0x00007932
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Disengage")]
		[Description("Use  Disengage?")]
		public bool Dis { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000973B File Offset: 0x0000793B
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00009743 File Offset: 0x00007943
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Multishot")]
		[Description("Use  Multishot?")]
		public bool MultiS { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000974C File Offset: 0x0000794C
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00009754 File Offset: 0x00007954
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("Multishot")]
		[Description("Use  Multishot?")]
		public int MultiSCount { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000975D File Offset: 0x0000795D
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00009765 File Offset: 0x00007965
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Backpaddle")]
		[Description("Auto Backpaddle?")]
		public bool Backpaddle { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000976E File Offset: 0x0000796E
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00009776 File Offset: 0x00007976
		[Setting]
		[DefaultValue(5)]
		[Category("Fight")]
		[DisplayName("BackpaddleRange")]
		[Description("Set your Range for your FC Backpaddle")]
		public int BackpaddleRange { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000977F File Offset: 0x0000797F
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00009787 File Offset: 0x00007987
		[Setting]
		[DefaultValue(true)]
		[Category("Pet")]
		[DisplayName("Pet Feeding")]
		[Description("Want the Pet get Autofeeded?")]
		public bool Petfeed { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009790 File Offset: 0x00007990
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00009798 File Offset: 0x00007998
		[Setting]
		[DefaultValue(true)]
		[Category("Pet")]
		[DisplayName("Pet Health OOC")]
		[Description("Should Check Pet Health before attack?")]
		public bool Checkpet { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000097A1 File Offset: 0x000079A1
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000097A9 File Offset: 0x000079A9
		[Setting]
		[DefaultValue(80)]
		[Category("Pet")]
		[DisplayName("Pet Health OOC")]
		[Description("Set Treshhold for Petattack?")]
		[Percentage(true)]
		public int PetHealth { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000097B2 File Offset: 0x000079B2
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000097BA File Offset: 0x000079BA
		[Setting]
		[DefaultValue(true)]
		[Category("BeastMastery")]
		[DisplayName("Misdirection")]
		[Description("Use Misdirection Solo on Pet/Group on Tank?")]
		public bool BeastMasteryMisdirection { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000097C3 File Offset: 0x000079C3
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000097CB File Offset: 0x000079CB
		[Setting]
		[DefaultValue(true)]
		[Category("Marksman")]
		[DisplayName("Aimed Shot")]
		[Description("Use Aimed Shot in Rota?")]
		public bool MarksmanAimedShot { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000097D4 File Offset: 0x000079D4
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000097DC File Offset: 0x000079DC
		[Setting]
		[DefaultValue(true)]
		[Category("Marksman")]
		[DisplayName("Arcane Shot")]
		[Description("Use Arcane Shot in Rota?")]
		public bool MarksmanArcaneShot { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000097E5 File Offset: 0x000079E5
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000097ED File Offset: 0x000079ED
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Use AOE in Instance")]
		[Description("Set this if you want to use AOE in Instance")]
		public bool UseAOE { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000097F6 File Offset: 0x000079F6
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000097FE File Offset: 0x000079FE
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Use Aspec of Pack")]
		[Description("Set this if you want to use in Instance")]
		public bool UseAspecofthePack { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009807 File Offset: 0x00007A07
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000980F File Offset: 0x00007A0F
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("AOE in Instance")]
		[Description("Number of Targets around the Tank to use AOE in Instance")]
		public int AOEInstance { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00009818 File Offset: 0x00007A18
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00009820 File Offset: 0x00007A20
		[DropdownList(new string[]
		{
			"HunterBeastMastery",
			"HunterSurvival",
			"HunterMarksmanship"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00009829 File Offset: 0x00007A29
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00009831 File Offset: 0x00007A31
		[DropdownList(new string[]
		{
			"Auto",
			"BeastMastery",
			"Survival",
			"Marksmanship"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x06000135 RID: 309 RVA: 0x0000983C File Offset: 0x00007A3C
		public HunterLevelSettings()
		{
			this.ChooseTalent = "HunterBeastMastery";
			this.UseAspecofthePack = false;
			this.RangeSet = 29;
			this.UseMacro = false;
			this.PetHealth = 80;
			this.AspecofViper = 20;
			this.AspecofHawks = 60;
			this.MultiS = false;
			this.MultiSCount = 3;
			this.Backpaddle = true;
			this.BackpaddleRange = 5;
			this.Checkpet = true;
			this.Petfeed = true;
			this.MarksmanAimedShot = true;
			this.MarksmanArcaneShot = true;
			this.BeastMasteryMisdirection = true;
			this.Dis = false;
			this.FD = true;
			this.UseAOE = true;
			this.AOEInstance = 3;
		}
	}
}
