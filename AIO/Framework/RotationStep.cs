using System;
using System.Diagnostics;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x02000039 RID: 57
	public class RotationStep : IComparable<RotationStep>
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000C340 File Offset: 0x0000A540
		public RotationStep(IRotationAction action, float priority, Func<IRotationAction, WoWUnit, bool> targetPredicate, Func<IRotationAction, bool> constantPredicate, Func<Func<WoWUnit, bool>, WoWUnit> targetFinder, Exclusive exclusive = null, bool forceCast = false, bool checkRange = true, bool checkLoS = false)
		{
			this.Action = action;
			this.Priority = priority;
			this.TargetPredicate = targetPredicate;
			this.ConstantPredicate = constantPredicate;
			this.TargetFinder = targetFinder;
			this.Exclusive = exclusive;
			this.ForceCast = forceCast;
			this.CheckRange = checkRange;
			this.CheckLoS = checkLoS;
			this.Name = action.GetType().FullName;
			RotationSpell rotationSpell = this.Action as RotationSpell;
			bool flag = rotationSpell != null;
			if (flag)
			{
				this.Name = rotationSpell.Name;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public RotationStep(IRotationAction action, float priority, Func<IRotationAction, WoWUnit, bool> targetPredicate, Func<Func<WoWUnit, bool>, WoWUnit> targetFinder, Exclusive exclusive = null, bool forceCast = false, bool checkRange = true, bool checkLoS = false) : this(action, priority, targetPredicate, (IRotationAction _) => true, targetFinder, exclusive, forceCast, checkRange, checkLoS)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C428 File Offset: 0x0000A628
		public int CompareTo(RotationStep other)
		{
			return this.Priority.CompareTo(other.Priority);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C44C File Offset: 0x0000A64C
		private Func<WoWUnit, bool> CreatePredicate(Exclusives exclusives)
		{
			return delegate(WoWUnit target)
			{
				bool flag = this.CheckRange && target.GetDistance > this.Action.MaxRange;
				bool result;
				if (flag)
				{
					RotationLogger.Trace(this.Name + " is out of range on " + target.Name);
					result = false;
				}
				else
				{
					bool flag2 = exclusives.Contains(target, this.Exclusive);
					if (flag2)
					{
						RotationLogger.Trace(this.Name + " has already had its token consumed on " + target.Name);
						result = false;
					}
					else
					{
						Stopwatch stopwatch = Stopwatch.StartNew();
						bool flag3 = this.TargetPredicate(this.Action, target);
						bool flag4 = flag3 && (!this.CheckLoS || target.IsLocalPlayer || !TraceLine.TraceLineGo(ObjectManager.Me.PositionWithoutType, target.PositionWithoutType, 16));
						stopwatch.Stop();
						RotationLogger.Trace(string.Format("{0} target predicated evaluated to {1} on {2} in {3} ms", new object[]
						{
							this.Name,
							flag4,
							target.Name,
							stopwatch.ElapsedMilliseconds
						}));
						bool flag5 = !flag4;
						if (flag5)
						{
							result = false;
						}
						else
						{
							stopwatch.Restart();
							ValueTuple<bool, bool> valueTuple = this.Action.Should(target);
							bool item = valueTuple.Item1;
							bool item2 = valueTuple.Item2;
							stopwatch.Stop();
							RotationLogger.Trace(string.Format("{0} action should predicate evaluated to ({1}, {2}) on {3} in {4} ms", new object[]
							{
								this.Name,
								item,
								item2,
								target.Name,
								stopwatch.ElapsedMilliseconds
							}));
							bool flag6 = item2;
							if (flag6)
							{
								exclusives.Add(target, this.Exclusive);
							}
							result = item;
						}
					}
				}
				return result;
			};
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C47C File Offset: 0x0000A67C
		public bool Execute(bool globalActive, Exclusives exclusives)
		{
			bool flag;
			if (RotationCombatUtil.freeMove)
			{
				RotationSpell rotationSpell = this.Action as RotationSpell;
				if (rotationSpell != null && (double)rotationSpell.CastTime > 0.0)
				{
					flag = Constants.Me.GetMove;
					goto IL_36;
				}
			}
			flag = false;
			IL_36:
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = (globalActive && !this.Action.IgnoresGlobal) || (!this.ForceCast && Constants.Me.IsCast);
				if (flag3)
				{
					RotationLogger.Trace(this.Name + " false because of GCD or IsCast.");
					result = false;
				}
				else
				{
					bool flag4 = !this.ConstantPredicate(this.Action);
					if (flag4)
					{
						RotationLogger.Trace(this.Name + " false because of constant predicate.");
						result = false;
					}
					else
					{
						Func<WoWUnit, bool> arg = this.CreatePredicate(exclusives);
						Stopwatch stopwatch = Stopwatch.StartNew();
						WoWUnit woWUnit = this.TargetFinder(arg);
						stopwatch.Stop();
						RotationLogger.Trace(string.Format("({0}) targetFinder: {1} {2} ms", this.Name, (woWUnit != null) ? woWUnit.Name : null, stopwatch.ElapsedMilliseconds));
						bool flag5 = woWUnit == null;
						if (flag5)
						{
							result = false;
						}
						else
						{
							stopwatch.Restart();
							bool flag6 = this.Action.Execute(woWUnit, this.ForceCast);
							stopwatch.Stop();
							RotationLogger.Trace(string.Format("({0}) execute {1}: {2} ms", this.Name, flag6, stopwatch.ElapsedMilliseconds));
							bool flag7 = !flag6;
							if (flag7)
							{
								exclusives.Remove(woWUnit, this.Exclusive);
							}
							result = flag6;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000C61F File Offset: 0x0000A81F
		public override string ToString()
		{
			return string.Format("[{0}] {1}", this.Priority, this.Name);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000C63C File Offset: 0x0000A83C
		public Exclusive Exclusive { get; }

		// Token: 0x04000165 RID: 357
		private readonly float Priority;

		// Token: 0x04000166 RID: 358
		private readonly IRotationAction Action;

		// Token: 0x04000167 RID: 359
		private readonly Func<IRotationAction, WoWUnit, bool> TargetPredicate;

		// Token: 0x04000168 RID: 360
		private readonly Func<IRotationAction, bool> ConstantPredicate;

		// Token: 0x04000169 RID: 361
		private readonly Func<Func<WoWUnit, bool>, WoWUnit> TargetFinder;

		// Token: 0x0400016A RID: 362
		private readonly bool ForceCast = false;

		// Token: 0x0400016B RID: 363
		private readonly bool CheckRange = true;

		// Token: 0x0400016C RID: 364
		private readonly bool CheckLoS = false;

		// Token: 0x0400016D RID: 365
		private readonly string Name;
	}
}
