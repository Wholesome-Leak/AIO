using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class MageLevelSettings : BasePersistentSettings<MageLevelSettings>
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000098F9 File Offset: 0x00007AF9
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00009901 File Offset: 0x00007B01
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Backpaddle")]
		[Description("Auto Backpaddle?")]
		public bool Backpaddle { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000990A File Offset: 0x00007B0A
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00009912 File Offset: 0x00007B12
		[Setting]
		[DefaultValue(20)]
		[Category("Fight")]
		[DisplayName("BackpaddleRange")]
		[Description("Set your Range for your FC Backpaddle")]
		public int BackpaddleRange { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000991B File Offset: 0x00007B1B
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00009923 File Offset: 0x00007B23
		[Setting]
		[DefaultValue(10)]
		[Category("Fight")]
		[DisplayName("Manastone")]
		[Description("Treshhold for Manastone")]
		public int Manastone { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000992C File Offset: 0x00007B2C
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00009934 File Offset: 0x00007B34
		[Setting]
		[DefaultValue(10)]
		[Category("Fight")]
		[DisplayName("Fire Blast")]
		[Description("Treshhold for Enemy Health <= to use Fire Blast")]
		[Percentage(true)]
		public int FrostFireBlast { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000993D File Offset: 0x00007B3D
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00009945 File Offset: 0x00007B45
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Sheep")]
		[Description("Uses Sheep if 2 Targets attacking")]
		public bool Sheep { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000994E File Offset: 0x00007B4E
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00009956 File Offset: 0x00007B56
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Use AOE in Instance")]
		[Description("Set this if you want to use AOE in Instance")]
		public bool UseAOE { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000995F File Offset: 0x00007B5F
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00009967 File Offset: 0x00007B67
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("AOE in Instance")]
		[Description("Number of Targets around the Tank to use AOE in Instance")]
		[Percentage(false)]
		public int AOEInstance { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00009970 File Offset: 0x00007B70
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00009978 File Offset: 0x00007B78
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Use Wand?")]
		[Description("Use Wand in General?")]
		public bool UseWand { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00009981 File Offset: 0x00007B81
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00009989 File Offset: 0x00007B89
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Use Blink?")]
		[Description("Use Blink while Backpaddle?")]
		public bool Blink { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00009992 File Offset: 0x00007B92
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000999A File Offset: 0x00007B9A
		[Setting]
		[DefaultValue(true)]
		[Category("Fire")]
		[DisplayName("Use Flamestrike?")]
		[Description("Use Flamestrike without Firestarter Buff?")]
		public bool FlamestrikeWithoutFire { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000099A3 File Offset: 0x00007BA3
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000099AB File Offset: 0x00007BAB
		[Setting]
		[DefaultValue(3)]
		[Category("Fire")]
		[DisplayName("Flamestrike EnemyCount")]
		[Description("Number of Targets around the Tank to use FS in Instance")]
		[Percentage(false)]
		public int FlamestrikeWithoutCountFire { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000099B4 File Offset: 0x00007BB4
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000099BC File Offset: 0x00007BBC
		[Setting]
		[DefaultValue(20)]
		[Category("General")]
		[DisplayName("Use Wand Treshold?")]
		[Description("Enemy Life Treshold for Wandusage?")]
		[Percentage(true)]
		public int UseWandTresh { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000099C5 File Offset: 0x00007BC5
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000099CD File Offset: 0x00007BCD
		[DropdownList(new string[]
		{
			"MageFrost",
			"MageFire",
			"MageArcane"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000099D6 File Offset: 0x00007BD6
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000099DE File Offset: 0x00007BDE
		[DropdownList(new string[]
		{
			"Auto",
			"Frost",
			"Fire",
			"Arcane"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x06000152 RID: 338 RVA: 0x000099E8 File Offset: 0x00007BE8
		public MageLevelSettings()
		{
			this.ChooseTalent = "MageFrost";
			this.FrostFireBlast = 10;
			this.Sheep = false;
			this.Manastone = 10;
			this.UseAOE = false;
			this.AOEInstance = 3;
			this.Blink = true;
			this.FlamestrikeWithoutFire = true;
			this.FlamestrikeWithoutCountFire = 3;
			this.UseWand = true;
			this.UseWandTresh = 20;
			this.Backpaddle = true;
			this.BackpaddleRange = 20;
		}
	}
}
