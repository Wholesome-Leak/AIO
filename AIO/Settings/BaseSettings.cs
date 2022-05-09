using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using AIO.Framework;
using MarsSettingsGUI;
using robotManager.Helpful;

namespace AIO.Settings
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public abstract class BaseSettings : Settings
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00009063 File Offset: 0x00007263
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000906B File Offset: 0x0000726B
		[Setting]
		[DefaultValue(true)]
		[Category("General")]
		[DisplayName("Frame Lock")]
		[Description("Use framelock for better logic performance, but lower FPS")]
		public bool FrameLock { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00009074 File Offset: 0x00007274
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000907C File Offset: 0x0000727C
		[Setting]
		[DefaultValue(50)]
		[Category("General")]
		[DisplayName("Scan Range")]
		[Description("Unit scan range for caching")]
		public int ScanRange { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00009085 File Offset: 0x00007285
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000908D File Offset: 0x0000728D
		[Setting]
		[DefaultValue(5)]
		[Category("General")]
		[DisplayName("Players LoS Credits")]
		[Description("Maximum number of players to compute LoS for, as part of the caching")]
		public int LoSCreditsPlayers { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00009096 File Offset: 0x00007296
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000909E File Offset: 0x0000729E
		[Setting]
		[DefaultValue(10)]
		[Category("General")]
		[DisplayName("NPCs LoS Credits")]
		[Description("Maximum number of NPCs to compute LoS for, as part of the caching")]
		public int LoSCreditsNPCs { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000090A7 File Offset: 0x000072A7
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000090AF File Offset: 0x000072AF
		[Setting]
		[DefaultValue(false)]
		[Category("General")]
		[DisplayName("Use synthetic combat events")]
		[Description("Use synthetic combat events based on state transitions. Do not enable this unless you know what it does.")]
		public bool UseSyntheticCombatEvents { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000090B8 File Offset: 0x000072B8
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000090C0 File Offset: 0x000072C0
		[Setting]
		[DefaultValue(false)]
		[Category("General")]
		[DisplayName("Straight Pipe")]
		[Description("You will most likely not want to enable this. Only makes sense when playing manually.")]
		public bool CompletelySynthetic { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000090C9 File Offset: 0x000072C9
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000090D1 File Offset: 0x000072D1
		[Setting]
		[DefaultValue(false)]
		[Category("General")]
		[DisplayName("Free Move")]
		[Description("Will allow you to move freely.")]
		public bool FreeMove { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000090DA File Offset: 0x000072DA
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000090E2 File Offset: 0x000072E2
		[Setting]
		[Category("Talents")]
		[DisplayName("Talents Codes")]
		[Description("Use a talent calculator to generate your own codes: https://talentcalculator.org/wotlk/. Do not modify if you are not sure.")]
		public List<string> TalentCodes { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000090EB File Offset: 0x000072EB
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000090F3 File Offset: 0x000072F3
		[Setting]
		[Category("Talents")]
		[DefaultValue(true)]
		[DisplayName("Use default talents")]
		[Description("If True, Make sure your talents match the default talents, or reset your talents.")]
		public bool UseDefaultTalents { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000090FC File Offset: 0x000072FC
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00009104 File Offset: 0x00007304
		[Setting]
		[Category("Talents")]
		[DefaultValue(true)]
		[DisplayName("Auto assign talents")]
		[Description("Will automatically assign your talent points.")]
		public bool AssignTalents { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A8 RID: 168
		// (set) Token: 0x060000A9 RID: 169
		[Setting]
		[DefaultValue("invalid")]
		[Category("Talents")]
		[DisplayName("Talent Tree")]
		[Description("Choose which Talent Tree you want to learn")]
		public abstract string ChooseTalent { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000AB RID: 171
		[Setting]
		[DefaultValue("Auto")]
		[Category("Talents")]
		[DisplayName("Rotation")]
		[Description("Choose which spell rotation you want to execute")]
		public abstract string ChooseRotation { get; set; }

		// Token: 0x060000AC RID: 172 RVA: 0x00009110 File Offset: 0x00007310
		protected BaseSettings()
		{
			this.FrameLock = true;
			this.ScanRange = 50;
			this.LoSCreditsPlayers = 5;
			this.LoSCreditsNPCs = 10;
			this.UseSyntheticCombatEvents = false;
			this.CompletelySynthetic = false;
			this.FreeMove = false;
			this.AssignTalents = true;
			this.TalentCodes = new List<string>();
			this.UseDefaultTalents = true;
			this.ChooseTalent = "invalid";
			this.ChooseRotation = "Auto";
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009194 File Offset: 0x00007394
		protected virtual void OnUpdate()
		{
			TalentsManager.Set(this.AssignTalents, this.UseDefaultTalents, this.TalentCodes.ToArray(), this.ChooseTalent);
			RotationFramework.Setup(this.FrameLock, this.LoSCreditsPlayers, this.LoSCreditsNPCs, this.ScanRange);
			RotationCombatUtil.freeMove = this.FreeMove;
			RotationFramework.UseSynthetic = this.UseSyntheticCombatEvents;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000091FC File Offset: 0x000073FC
		public void ShowConfiguration()
		{
			SettingsWindow settingsWindow = new SettingsWindow(this, Constants.Me.WowClass.ToString());
			settingsWindow.ShowDialog();
			this.OnUpdate();
		}
	}
}
