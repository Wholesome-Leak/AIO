using System;
using System.ComponentModel;
using System.Configuration;
using MarsSettingsGUI;

namespace AIO.Settings
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class DeathKnightLevelSettings : BasePersistentSettings<DeathKnightLevelSettings>
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000095C3 File Offset: 0x000077C3
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000095CB File Offset: 0x000077CB
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Raise Dead")]
		[Description("Use Raise Dead asap?")]
		public bool RaiseDead { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000095D4 File Offset: 0x000077D4
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000095DC File Offset: 0x000077DC
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Dark Command")]
		[Description("Use Dark Command in Group?")]
		public bool DarkCommand { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000095E5 File Offset: 0x000077E5
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000095ED File Offset: 0x000077ED
		[Setting]
		[DefaultValue(true)]
		[Category("Fight")]
		[DisplayName("Deathgrip")]
		[Description("use Deathgrip for in Group?")]
		public bool DeathGrip { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000095F6 File Offset: 0x000077F6
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000095FE File Offset: 0x000077FE
		[Setting]
		[DefaultValue(false)]
		[Category("Fight")]
		[DisplayName("Choose Presence")]
		[Description("Set the Presence you want the FC to fight in")]
		[DropdownList(new string[]
		{
			"BloodPresence",
			"FrostPresence",
			"UnholyPresence"
		})]
		public string Presence { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00009607 File Offset: 0x00007807
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000960F File Offset: 0x0000780F
		[Setting]
		[DefaultValue(1)]
		[Category("Fight")]
		[DisplayName("Bloodstrike")]
		[Description("Set Enemy Count Equal X enemy to use Bloodstrike")]
		[Percentage(false)]
		public int BloodStrike { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00009618 File Offset: 0x00007818
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00009620 File Offset: 0x00007820
		[Setting]
		[DefaultValue(2)]
		[Category("Fight")]
		[DisplayName("Hearthstrike")]
		[Description("Set Enemy Count Equal X enemy to use Hearthstrike")]
		[Percentage(false)]
		public int HearthStrike { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00009629 File Offset: 0x00007829
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00009631 File Offset: 0x00007831
		[Setting]
		[DefaultValue(2)]
		[Category("Fight")]
		[DisplayName("BloodBoil")]
		[Description("Set Enemy Count larger X enemy to use Bloodboil")]
		[Percentage(false)]
		public int BloodBoil { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000963A File Offset: 0x0000783A
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00009642 File Offset: 0x00007842
		[Setting]
		[DefaultValue(3)]
		[Category("Fight")]
		[DisplayName("Death and Decay")]
		[Description("Set Enemy Count larger X enemy to use DnD")]
		[Percentage(false)]
		public int DnD { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000964B File Offset: 0x0000784B
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00009653 File Offset: 0x00007853
		[DropdownList(new string[]
		{
			"DeathKnightBlood",
			"DeathKnightFrost",
			"DeathKnightUnholy"
		})]
		public override string ChooseTalent { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000965C File Offset: 0x0000785C
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00009664 File Offset: 0x00007864
		[DropdownList(new string[]
		{
			"Auto",
			"Blood",
			"Frost",
			"Unholy",
			"UnholyPVP"
		})]
		public override string ChooseRotation { get; set; }

		// Token: 0x0600010A RID: 266 RVA: 0x00009670 File Offset: 0x00007870
		public DeathKnightLevelSettings()
		{
			this.RaiseDead = true;
			this.ChooseTalent = "DeathKnightBlood";
			this.DarkCommand = true;
			this.DeathGrip = true;
			this.Presence = "BloodPresence";
			this.BloodStrike = 1;
			this.HearthStrike = 2;
			this.BloodBoil = 2;
			this.DnD = 3;
		}
	}
}
