using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Helpers;

namespace AIO.Combat.Rogue
{
	// Token: 0x020000A1 RID: 161
	internal static class PoisonHelper
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00013C0A File Offset: 0x00011E0A
		private static bool hasMainHandEnchant
		{
			get
			{
				return Lua.LuaDoString<bool>("local hasMainHandEnchant, _, _, _, _, _, _, _, _ = GetWeaponEnchantInfo()\r\n            if (hasMainHandEnchant) then \r\n               return '1'\r\n            else\r\n               return '0'\r\n            end", "");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00013C1B File Offset: 0x00011E1B
		private static bool hasOffHandEnchant
		{
			get
			{
				return Lua.LuaDoString<bool>("local _, _, _, _, hasOffHandEnchant, _, _, _, _ = GetWeaponEnchantInfo()\r\n            if (hasOffHandEnchant) then \r\n               return '1'\r\n            else\r\n               return '0'\r\n            end", "");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00013C2C File Offset: 0x00011E2C
		private static bool hasoffHandWeapon
		{
			get
			{
				return Lua.LuaDoString<bool>("local hasWeapon = OffhandHasWeapon()\r\n            return hasWeapon", "");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016098 File Offset: 0x00014298
		private static IEnumerable<uint> MP
		{
			get
			{
				return from i in PoisonHelper.DeadlyPoisonDictionary
				where (long)i.Key <= (long)((ulong)Constants.Me.Level) && ItemsManager.HasItemById(i.Value)
				orderby i.Key descending
				select i.Value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00016118 File Offset: 0x00014318
		private static IEnumerable<uint> OP
		{
			get
			{
				return from i in PoisonHelper.InstantPoisonDictionary
				where (long)i.Key <= (long)((ulong)Constants.Me.Level) && ItemsManager.HasItemById(i.Value)
				orderby i.Key descending
				select i.Value;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016198 File Offset: 0x00014398
		public static void CheckPoison()
		{
			bool flag = !PoisonHelper.hasMainHandEnchant;
			if (flag)
			{
				bool flag2 = Constants.Me.Level >= 20U && Constants.Me.Level <= 29U;
				if (flag2)
				{
					bool flag3 = PoisonHelper.OP.Any<uint>();
					if (flag3)
					{
						uint num = PoisonHelper.OP.First<uint>();
						bool getMove = Constants.Me.GetMove;
						if (getMove)
						{
							MovementManager.StopMoveTo(true, 1000);
						}
						Logging.Write("Trying to apply" + num.ToString());
						ItemsManager.UseItem(num);
						Thread.Sleep(100 + Usefuls.Latency);
						Lua.RunMacroText("/use 16");
						Usefuls.WaitIsCasting();
					}
				}
				bool flag4 = Constants.Me.Level > 29U;
				if (flag4)
				{
					bool flag5 = PoisonHelper.MP.Any<uint>();
					if (flag5)
					{
						uint num2 = PoisonHelper.MP.First<uint>();
						bool getMove2 = Constants.Me.GetMove;
						if (getMove2)
						{
							MovementManager.StopMoveTo(true, 1000);
						}
						Logging.Write("Trying to apply" + num2.ToString());
						ItemsManager.UseItem(num2);
						Thread.Sleep(100 + Usefuls.Latency);
						Lua.RunMacroText("/use 16");
						Usefuls.WaitIsCasting();
					}
				}
			}
			bool flag6 = !PoisonHelper.hasOffHandEnchant && PoisonHelper.hasoffHandWeapon;
			if (flag6)
			{
				bool flag7 = PoisonHelper.OP.Any<uint>();
				if (flag7)
				{
					uint num3 = PoisonHelper.OP.First<uint>();
					bool getMove3 = Constants.Me.GetMove;
					if (getMove3)
					{
						MovementManager.StopMoveTo(true, 1000);
					}
					ItemsManager.UseItem(num3);
					Thread.Sleep(100 + Usefuls.Latency);
					Lua.RunMacroText("/use 17");
					Usefuls.WaitIsCasting();
				}
			}
		}

		// Token: 0x040002F1 RID: 753
		private static readonly Dictionary<int, uint> InstantPoisonDictionary = new Dictionary<int, uint>
		{
			{
				79,
				43231U
			},
			{
				73,
				43230U
			},
			{
				68,
				21927U
			},
			{
				60,
				8928U
			},
			{
				52,
				8927U
			},
			{
				44,
				8926U
			},
			{
				36,
				6950U
			},
			{
				28,
				6949U
			},
			{
				20,
				6947U
			}
		};

		// Token: 0x040002F2 RID: 754
		private static readonly Dictionary<int, uint> DeadlyPoisonDictionary = new Dictionary<int, uint>
		{
			{
				80,
				43233U
			},
			{
				76,
				43232U
			},
			{
				70,
				22054U
			},
			{
				62,
				22053U
			},
			{
				30,
				2892U
			},
			{
				60,
				20844U
			},
			{
				54,
				8985U
			},
			{
				46,
				8984U
			},
			{
				38,
				2893U
			}
		};
	}
}
