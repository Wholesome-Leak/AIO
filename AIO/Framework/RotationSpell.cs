using System;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x0200003C RID: 60
	public class RotationSpell : IRotationAction
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000C85F File Offset: 0x0000AA5F
		public RotationSpell(string name, bool ignoresGlobal = false)
		{
			this.Spell = new Spell(name);
			this.IgnoresGlobal = ignoresGlobal;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public RotationSpell(Spell spell, bool ignoresGlobal = false)
		{
			this.Spell = spell;
			this.IgnoresGlobal = ignoresGlobal;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C894 File Offset: 0x0000AA94
		public string Name
		{
			get
			{
				return this.Spell.Name;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C8A1 File Offset: 0x0000AAA1
		public bool IsSpellUsable
		{
			get
			{
				return this.Spell.IsSpellUsable;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C8AE File Offset: 0x0000AAAE
		public bool KnownSpell
		{
			get
			{
				return this.Spell.KnownSpell;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C8BB File Offset: 0x0000AABB
		public float CastTime
		{
			get
			{
				return this.Spell.CastTime;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public float MaxRange
		{
			get
			{
				return this.Spell.MaxRange;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C8D5 File Offset: 0x0000AAD5
		public virtual bool Execute(WoWUnit target, bool force = false)
		{
			return RotationCombatUtil.CastSpell(this, target, force);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C8DF File Offset: 0x0000AADF
		public virtual ValueTuple<bool, bool> Should(WoWUnit target)
		{
			return new ValueTuple<bool, bool>(true, true);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public bool IgnoresGlobal { get; }

		// Token: 0x06000302 RID: 770 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		public static int GetSpellCost(string spellName)
		{
			return Lua.LuaDoString<int>("local name, rank, icon, cost, isFunnel, powerType, castTime, minRange, maxRange = GetSpellInfo('" + spellName + "'); return cost", "");
		}

		// Token: 0x04000173 RID: 371
		public readonly Spell Spell;
	}
}
