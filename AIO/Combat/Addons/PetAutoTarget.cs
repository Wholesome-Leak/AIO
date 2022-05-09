using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AIO.Combat.Common;
using AIO.Framework;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Addons
{
	// Token: 0x02000111 RID: 273
	internal class PetAutoTarget : ICycleable
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x000277DB File Offset: 0x000259DB
		public PetAutoTarget(string taunt)
		{
			this.Taunt = taunt;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000277EC File Offset: 0x000259EC
		public void Initialize()
		{
			FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00027800 File Offset: 0x00025A00
		public void Dispose()
		{
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00027814 File Offset: 0x00025A14
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			bool flag = !Constants.Pet.IsAlive || !Constants.Pet.IsValid;
			if (!flag)
			{
				bool isInGroup = Constants.Me.IsInGroup;
				if (!isInGroup)
				{
					IOrderedEnumerable<WoWUnit> source = from uu in RotationFramework.Enemies
					orderby uu.HealthPercent
					select uu;
					List<WoWUnit> list = (from u in source
					where u.IsTargetingMe
					select u).ToList<WoWUnit>();
					List<WoWUnit> list2 = (from u in source
					where u.IsTargetingMyPet
					select u).ToList<WoWUnit>();
					List<WoWUnit> source2 = list;
					bool flag2 = list.Count == 0;
					if (flag2)
					{
						source2 = list2;
					}
					WoWUnit woWUnit = source2.FirstOrDefault<WoWUnit>();
					bool flag3 = woWUnit == null;
					if (!flag3)
					{
						bool flag4 = !woWUnit.IsMyPetTarget;
						if (flag4)
						{
							Constants.Me.FocusGuid = woWUnit.Guid;
							Lua.RunMacroText("/petattack [@focus]");
							Lua.LuaDoString("ClearFocus();", false);
							Logging.WriteFight(string.Format("Changing pet target to {0} [{1}]", woWUnit.Name, woWUnit.Guid));
						}
						bool isInGroup2 = Constants.Me.IsInGroup;
						if (!isInGroup2)
						{
							PetManager.PetSpellCast(this.Taunt);
						}
					}
				}
			}
		}

		// Token: 0x0400059F RID: 1439
		private readonly string Taunt;
	}
}
