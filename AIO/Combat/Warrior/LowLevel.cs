using System;
using System.Collections.Generic;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Warrior
{
	// Token: 0x02000069 RID: 105
	internal class LowLevel : BaseRotation
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		protected override List<RotationStep> Rotation
		{
			get
			{
				List<RotationStep> list = new List<RotationStep>();
				list.Add(new RotationStep(new RotationSpell("Intercept", false), 2f, (IRotationAction s, WoWUnit t) => t.GetDistance > 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Charge", false), 3f, (IRotationAction s, WoWUnit t) => t.GetDistance > 7f, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Rend", false), 4f, (IRotationAction s, WoWUnit t) => !t.HaveMyBuff(new string[]
				{
					"Rend"
				}) && !t.IsCreatureType("Elemental"), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Victory Rush", false), 5f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Thunder Clap", false), 6f, (IRotationAction s, WoWUnit t) => RotationFramework.Enemies.Count((WoWUnit o) => o.GetDistance <= 10f) >= 2, new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				list.Add(new RotationStep(new RotationSpell("Heroic Strike", false), 7f, new Func<IRotationAction, WoWUnit, bool>(RotationCombatUtil.Always), new Func<Func<WoWUnit, bool>, WoWUnit>(RotationCombatUtil.BotTarget), null, false, true, false));
				return list;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F279 File Offset: 0x0000D479
		public LowLevel() : base(true, false, false, false)
		{
		}
	}
}
