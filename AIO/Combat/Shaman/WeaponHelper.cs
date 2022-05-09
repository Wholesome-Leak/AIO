using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Combat.Common;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x0200008C RID: 140
	internal class WeaponHelper : ICycleable
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00013B4A File Offset: 0x00011D4A
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013B58 File Offset: 0x00011D58
		internal WeaponHelper(BaseCombatClass combatClass)
		{
			this.CombatClass = combatClass;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013BB3 File Offset: 0x00011DB3
		public void Initialize()
		{
			FightEvents.OnFightLoop += new FightEvents.FightTargetHandler(this.OnFightLoop);
			MovementEvents.OnMovementPulse += new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013BDA File Offset: 0x00011DDA
		public void Dispose()
		{
			FightEvents.OnFightLoop -= new FightEvents.FightTargetHandler(this.OnFightLoop);
			MovementEvents.OnMovementPulse -= new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013C01 File Offset: 0x00011E01
		private void OnFightLoop(WoWUnit unit, CancelEventArgs cancelable)
		{
			this.Enchant();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013C01 File Offset: 0x00011E01
		private void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			this.Enchant();
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00013C0A File Offset: 0x00011E0A
		private bool HasMainHandEnchant
		{
			get
			{
				return Lua.LuaDoString<bool>("local hasMainHandEnchant, _, _, _, _, _, _, _, _ = GetWeaponEnchantInfo()\r\n            if (hasMainHandEnchant) then \r\n               return '1'\r\n            else\r\n               return '0'\r\n            end", "");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00013C1B File Offset: 0x00011E1B
		private bool HasOffHandEnchant
		{
			get
			{
				return Lua.LuaDoString<bool>("local _, _, _, _, hasOffHandEnchant, _, _, _, _ = GetWeaponEnchantInfo()\r\n            if (hasOffHandEnchant) then \r\n               return '1'\r\n            else\r\n               return '0'\r\n            end", "");
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00013C2C File Offset: 0x00011E2C
		private bool HasOffHandWeapon
		{
			get
			{
				return Lua.LuaDoString<bool>("local hasWeapon = OffhandHasWeapon()\r\n            return hasWeapon", "");
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013C40 File Offset: 0x00011E40
		private void Enchant()
		{
			string spec = this.Spec;
			string a = spec;
			if (!(a == "Enhancement"))
			{
				if (!(a == "Restoration"))
				{
					if (!(a == "Elemental"))
					{
						if (a == "LowLevel")
						{
							bool flag = !this.HasMainHandEnchant;
							if (flag)
							{
								this.RockbiterWeapon.Launch();
							}
							bool flag2 = this.HasOffHandWeapon && !this.HasOffHandEnchant;
							if (flag2)
							{
								this.RockbiterWeapon.Launch();
							}
						}
					}
					else
					{
						bool flag3 = !this.HasMainHandEnchant;
						if (flag3)
						{
							this.FlametongueWeapon.Launch();
						}
					}
				}
				else
				{
					bool flag4 = !this.HasMainHandEnchant;
					if (flag4)
					{
						bool knownSpell = this.EarthlivingWeapon.KnownSpell;
						if (knownSpell)
						{
							this.EarthlivingWeapon.Launch();
						}
						else
						{
							this.FlametongueWeapon.Launch();
						}
					}
				}
			}
			else
			{
				bool flag5 = !this.HasMainHandEnchant;
				if (flag5)
				{
					bool knownSpell2 = this.WindfuryWeapon.KnownSpell;
					if (knownSpell2)
					{
						this.WindfuryWeapon.Launch();
					}
					else
					{
						this.RockbiterWeapon.Launch();
					}
				}
				bool flag6 = this.HasOffHandWeapon && !this.HasOffHandEnchant;
				if (flag6)
				{
					bool knownSpell3 = this.FlametongueWeapon.KnownSpell;
					if (knownSpell3)
					{
						this.FlametongueWeapon.Launch();
					}
					else
					{
						this.RockbiterWeapon.Launch();
					}
				}
			}
		}

		// Token: 0x0400028F RID: 655
		private readonly BaseCombatClass CombatClass;

		// Token: 0x04000290 RID: 656
		private readonly Spell RockbiterWeapon = new Spell("Rockbiter Weapon");

		// Token: 0x04000291 RID: 657
		private readonly Spell FlametongueWeapon = new Spell("Flametongue Weapon");

		// Token: 0x04000292 RID: 658
		private readonly Spell EarthlivingWeapon = new Spell("Earthliving Weapon");

		// Token: 0x04000293 RID: 659
		private readonly Spell WindfuryWeapon = new Spell("Windfury Weapon");
	}
}
