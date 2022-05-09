using System;
using System.Collections.Generic;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Rogue
{
	// Token: 0x0200009F RID: 159
	internal class LowLevel : BaseRotation
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00015F64 File Offset: 0x00014164
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Eviscerate", false), 2f, (IRotationAction s, WoWUnit t) => Constants.Me.ComboPoint >= BasePersistentSettings<RogueLevelSettings>.Current.Eviscarate, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Evasion", false), 3f, (IRotationAction s, WoWUnit t) => Constants.Me.HealthPercent < 30.0, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.FindMe), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Sinister Strike", false), 4f, (IRotationAction s, WoWUnit t) => true, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
