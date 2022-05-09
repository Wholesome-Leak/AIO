using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class PaladinLevelSettings : BasePersistentSettings<PaladinLevelSettings>
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009A6D File Offset: 0x00007C6D
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00009A75 File Offset: 0x00007C75
		[Setting]
		[DefaultValue(50)]
		[Category("General")]
		[DisplayName("Divine Plea")]
		[Description("Set when to use Divine Plea ")]
		public int GeneralDivinePlea { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009A7E File Offset: 0x00007C7E
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00009A86 File Offset: 0x00007C86
		[Setting]
		[DefaultValue(2)]
		[Category("General")]
		[DisplayName("Consecration")]
		[Description("Set the Enemycount >= for Consecration on all Specs ")]
		public int GeneralConsecration { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009A8F File Offset: 0x00007C8F
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00009A97 File Offset: 0x00007C97
		[Setting]
		[DefaultValue(false)]
		[Category("General")]
		[DisplayName("Crusader")]
		[Description("switch Crusader Aura")]
		public bool Crusader { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009AA0 File Offset: 0x00007CA0
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00009AA8 File Offset: 0x00007CA8
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Auto Buffing")]
		[Description("use Autobuffing?")]
		public bool Buffing { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009AB1 File Offset: 0x00007CB1
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00009AB9 File Offset: 0x00007CB9
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Heal OOC")]
		[Description("Use Healspells Out of Combat?")]
		public bool HealOOC { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009AC2 File Offset: 0x00007CC2
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00009ACA File Offset: 0x00007CCA
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Divine Plea OOC")]
		[Description("Use DP out of Combat?")]
		public bool DivinePleaOOC { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009AD3 File Offset: 0x00007CD3
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00009ADB File Offset: 0x00007CDB
		[Setting]
		[DefaultValue(false)]
		[Category("Retribution")]
		[DisplayName("In Combat Heal")]
		[Description("Activate this to let Retribution Paladin Heal himself in Combat")]
		public bool RetributionHealInCombat { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00009AE4 File Offset: 0x00007CE4
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00009AEC File Offset: 0x00007CEC
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Hammer of Justice")]
		[Description("Hammer of Justice when more then 1 Target")]
		public bool RetributionHammerofJustice { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009AF5 File Offset: 0x00007CF5
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00009AFD File Offset: 0x00007CFD
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Hand of Reckoning")]
		[Description("Use Hand of Reckoning in Rotation?")]
		public bool RetributionHOR { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009B06 File Offset: 0x00007D06
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00009B0E File Offset: 0x00007D0E
		[Setting]
		[DefaultValue(50)]
		[Category("Retribution")]
		[DisplayName("Holy Light")]
		[Description("Set your Treshhold when to use Holy Light")]
		[Percentage(true)]
		public int RetributionHL { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00009B17 File Offset: 0x00007D17
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00009B1F File Offset: 0x00007D1F
		[Setting]
		[DefaultValue(30)]
		[Category("Retribution")]
		[DisplayName("Flash of Light")]
		[Description("Set your Treshhold when to use Flash of Light")]
		[Percentage(true)]
		public int RetributionFL { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009B28 File Offset: 0x00007D28
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00009B30 File Offset: 0x00007D30
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Purify")]
		[Description("Allow Purify on yourself")]
		public bool RetributionPurify { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00009B39 File Offset: 0x00007D39
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00009B41 File Offset: 0x00007D41
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Sacred Shield")]
		[Description("Allow the Use of Sacredshield")]
		public bool RetributionSShield { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00009B4A File Offset: 0x00007D4A
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00009B52 File Offset: 0x00007D52
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Lay on Hands")]
		[Description("Allow the Use of Lay on Hands")]
		public bool RetributionLayOnHands { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00009B5B File Offset: 0x00007D5B
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00009B63 File Offset: 0x00007D63
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Divine Protection")]
		[Description("Allow the Use of Divine Protection")]
		public bool RetributionDivProtection { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00009B6C File Offset: 0x00007D6C
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00009B74 File Offset: 0x00007D74
		[Setting]
		[DefaultValue(false)]
		[Category("Retribution")]
		[DisplayName("Judgement Spam")]
		[Description("Use Judgement just when the debuff runs out? (false will spam Judgement)")]
		public bool RetributionJudgementofWisdomSpam { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00009B7D File Offset: 0x00007D7D
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00009B85 File Offset: 0x00007D85
		[Setting]
		[DefaultValue(true)]
		[Category("Retribution")]
		[DisplayName("Avenging Wrath")]
		[Description("Use Avenging Wrath?")]
		public bool AvengingWrathRetribution { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00009B8E File Offset: 0x00007D8E
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00009B96 File Offset: 0x00007D96
		[Setting]
		[DefaultValue(false)]
		[Category("Aura")]
		[DisplayName("Combat Aura")]
		[Description("Set Combat Aura")]
		[DropdownList(new string[]
		{
			"Devotion Aura",
			"Retribution Aura",
			"Concentration Aura"
		})]
		public string Aura { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009B9F File Offset: 0x00007D9F
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00009BA7 File Offset: 0x00007DA7
		[Setting]
		[DefaultValue(false)]
		[Category("Retribution")]
		[DisplayName("Seal of Command or other")]
		[Description("Set the Seal you want to used by the FC")]
		[DropdownList(new string[]
		{
			"Seal of Command",
			"Seal of Righteousness",
			"Seal of Justice"
		})]
		public string Sealret { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00009BB0 File Offset: 0x00007DB0
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00009BB8 File Offset: 0x00007DB8
		[Setting]
		[DefaultValue(false)]
		[Category("Protection")]
		[DisplayName("Seal of Command or other")]
		[Description("Set the Seal you want to used by the FC")]
		[DropdownList(new string[]
		{
			"Seal of Command",
			"Seal of Righteousness",
			"Seal of Justice",
			"Seal of Light",
			"Seal of Wisdom"
		})]
		public string Sealprot { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00009BC1 File Offset: 0x00007DC1
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00009BC9 File Offset: 0x00007DC9
		[Setting]
		[DefaultValue(95)]
		[Category("Protection")]
		[DisplayName("Seal of Light")]
		[Description("Set your Treshhold when to use Seal of Light")]
		[Percentage(true)]
		public int ProtectionSoL { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009BD2 File Offset: 0x00007DD2
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00009BDA File Offset: 0x00007DDA
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Avenging Wrath")]
		[Description("Use Avenging Wrath?")]
		public bool AvengingWrathProtection { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009BE3 File Offset: 0x00007DE3
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00009BEB File Offset: 0x00007DEB
		[Setting]
		[DefaultValue(40)]
		[Category("Protection")]
		[DisplayName("Seal of Wisdom")]
		[Description("Set your Treshhold when to use Seal of Wisdom")]
		[Percentage(true)]
		public int ProtectionSoW { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00009BF4 File Offset: 0x00007DF4
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00009BFC File Offset: 0x00007DFC
		[Setting]
		[DefaultValue(5)]
		[Category("Protection")]
		[DisplayName("Lay on Hands")]
		[Description("Set your Treshhold for LoH on Paladin")]
		[Percentage(true)]
		public int ProtectionLoH { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00009C05 File Offset: 0x00007E05
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00009C0D File Offset: 0x00007E0D
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Hand of Reckoning")]
		[Description("Use HoR in Dungeons? Autotaunt.")]
		public bool ProtectionHoR { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00009C16 File Offset: 0x00007E16
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00009C1E File Offset: 0x00007E1E
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Righteous Defense")]
		[Description("Use Righteous Defense in Dungeons on mobs? Autotaunt, Server dependent.")]
		public bool RightDefense { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00009C27 File Offset: 0x00007E27
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00009C2F File Offset: 0x00007E2F
		[Setting]
		[DefaultValue(true)]
		[Category("Protection")]
		[DisplayName("Hand of Protection")]
		[Description("Use HoP in Dungeons? ")]
		public bool ProtectionHoP { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00009C38 File Offset: 0x00007E38
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009C40 File Offset: 0x00007E40
		[Setting]
		[DefaultValue(60)]
		[Category("Holy")]
		[DisplayName("Holy Shock")]
		[Description("Set your Treshhold when to use Holy Shock")]
		[Percentage(true)]
		public int HolyHS { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009C49 File Offset: 0x00007E49
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009C51 File Offset: 0x00007E51
		[Setting]
		[DefaultValue(75)]
		[Category("Holy")]
		[DisplayName("Holy Light")]
		[Description("Set your Treshhold when to use Holy Light")]
		[Percentage(true)]
		public int HolyHL { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009C5A File Offset: 0x00007E5A
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009C62 File Offset: 0x00007E62
		[Setting]
		[DefaultValue(95)]
		[Category("Holy")]
		[DisplayName("Flash of Light")]
		[Description("Set your Treshhold when to use Flash of Light")]
		[Percentage(true)]
		public int HolyFL { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009C6B File Offset: 0x00007E6B
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009C73 File Offset: 0x00007E73
		[Setting]
		[DefaultValue("")]
		[Category("Holy")]
		[DisplayName("Custom Tank")]
		[Description("If you want to override the tank. Leave empty if you don't know")]
		public string HolyCustomTank { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009C7C File Offset: 0x00007E7C
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009C84 File Offset: 0x00007E84
		[Setting]
		[DefaultValue(true)]
		[Category("Holy")]
		[DisplayName("Purify")]
		[Description("Allow Purify on yourself")]
		public bool HolyPurify { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009C8D File Offset: 0x00007E8D
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00009C95 File Offset: 0x00007E95
		[Setting]
		[DefaultValue(false)]
		[Category("Holy")]
		[DisplayName("LoH")]
		[Description("Allow Lay on Hands on Tank with hp < 15%")]
		public bool HolyLoH { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00009C9E File Offset: 0x00007E9E
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00009CA6 File Offset: 0x00007EA6
		[Setting]
		[DefaultValue(15)]
		[Category("Holy")]
		[DisplayName("LoH Value")]
		[Description("Set your Treshhold when to LoH on Tank")]
		[Percentage(true)]
		public int HolyLoHTresh { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009CAF File Offset: 0x00007EAF
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009CB7 File Offset: 0x00007EB7
		[DropdownList(new string[]
		{
			"PaladinRetribution",
			"PaladinHoly",
			"PaladinProtection"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009CC0 File Offset: 0x00007EC0
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00009CC8 File Offset: 0x00007EC8
		[DropdownList(new string[]
		{
			"Auto",
			"Retribution",
			"Holy",
			"Protection"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x0600019B RID: 411 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public PaladinLevelSettings()
		{
			this.ChooseTalent = "PaladinRetribution";
			this.Crusader = false;
			this.HealOOC = true;
			this.DivinePleaOOC = true;
			this.GeneralDivinePlea = 50;
			this.GeneralConsecration = 2;
			this.Buffing = true;
			this.HolyHS = 60;
			this.HolyHL = 75;
			this.HolyFL = 95;
			this.HolyLoH = false;
			this.HolyLoHTresh = 15;
			this.HolyCustomTank = "";
			this.HolyPurify = true;
			this.RetributionHammerofJustice = true;
			this.RetributionHOR = true;
			this.RetributionPurify = true;
			this.RetributionSShield = true;
			this.RetributionLayOnHands = true;
			this.RetributionDivProtection = true;
			this.RetributionJudgementofWisdomSpam = false;
			this.RetributionHealInCombat = false;
			this.RightDefense = true;
			this.ProtectionHoP = true;
			this.RetributionHL = 50;
			this.RetributionFL = 30;
			this.ProtectionSoL = 95;
			this.ProtectionSoW = 40;
			this.ProtectionHoR = true;
			this.ProtectionLoH = 5;
			this.AvengingWrathProtection = true;
			this.AvengingWrathRetribution = true;
			this.Aura = "Devotion Aura";
			this.Sealret = "Seal of Righteousness";
			this.Sealprot = "Seal of Light";
		}
	}
}
