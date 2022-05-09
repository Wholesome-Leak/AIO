using System;
using System.Collections.Generic;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000035 RID: 53
	public class CancelableSpell : RotationSpell
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public CancelableSpell(string name, Func<WoWUnit, bool> cancelPred, bool ignoresGlobal = false) : base(name, ignoresGlobal)
		{
			this.CancelPred = cancelPred;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BF30 File Offset: 0x0000A130
		public CancelableSpell(Spell spell, Func<WoWUnit, bool> cancelPred, bool ignoresGlobal = false) : base(spell, ignoresGlobal)
		{
			this.CancelPred = cancelPred;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BF44 File Offset: 0x0000A144
		public static bool Check()
		{
			bool result = false;
			object @lock = CancelableSpell.Lock;
			lock (@lock)
			{
				bool flag2 = CancelableSpell._current != null;
				if (flag2)
				{
					result = CancelableSpell._current.CheckCancel();
				}
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		private bool CheckCancel()
		{
			bool result = false;
			object @lock = CancelableSpell.Lock;
			lock (@lock)
			{
				bool flag2 = CancelableSpell._current == this && this.CancelPred(this.Target);
				if (flag2)
				{
					CancelableSpell._current = null;
					result = true;
					RotationCombatUtil.StopCasting();
					Logging.WriteFight("Canceled " + base.Name);
				}
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C034 File Offset: 0x0000A234
		public static void CastStopHandler(string id, List<string> args)
		{
			object @lock = CancelableSpell.Lock;
			lock (@lock)
			{
				bool flag2 = CancelableSpell._current != null && (id == "UNIT_SPELLCAST_FAILED" || id == "UNIT_SPELLCAST_FAILED_QUIET" || id == "UNIT_SPELLCAST_INTERRUPTED" || id == "UNIT_SPELLCAST_STOP" || id == "UNIT_SPELLCAST_SUCCEEDED") && args.Count >= 1 && args[0].Equals("player");
				if (flag2)
				{
					CancelableSpell._current = null;
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public override bool Execute(WoWUnit target, bool force = false)
		{
			this.Target = target;
			bool flag = RotationCombatUtil.CastSpell(this, target, force);
			bool flag2 = flag;
			if (flag2)
			{
				object @lock = CancelableSpell.Lock;
				lock (@lock)
				{
					CancelableSpell._current = this;
				}
			}
			return flag;
		}

		// Token: 0x0400015C RID: 348
		private static readonly object Lock = new object();

		// Token: 0x0400015D RID: 349
		private static CancelableSpell _current;

		// Token: 0x0400015E RID: 350
		private readonly Func<WoWUnit, bool> CancelPred;

		// Token: 0x0400015F RID: 351
		private WoWUnit Target;
	}
}
