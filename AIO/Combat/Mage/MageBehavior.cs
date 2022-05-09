using System;
using System.Collections.Generic;
using System.ComponentModel;
using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Settings;
using robotManager.Helpful;

namespace AIO.Combat.Mage
{
	// Token: 0x020000D1 RID: 209
	internal class MageBehavior : BaseCombatClass
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00012652 File Offset: 0x00010852
		public override float Range
		{
			get
			{
				return 29f;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001E53C File Offset: 0x0001C73C
		internal MageBehavior()
		{
			BaseSettings settings = BasePersistentSettings<MageLevelSettings>.Current;
			Dictionary<string, BaseRotation> dictionary = new Dictionary<string, BaseRotation>();
			dictionary.Add("LowLevel", new LowLevel());
			dictionary.Add("Frost", new Frost());
			dictionary.Add("Arcane", new Arcane());
			dictionary.Add("Fire", new Fire());
			ICycleable[] array = new ICycleable[2];
			array[0] = new Buffs();
			array[1] = new ConditionalCycleable(() => BasePersistentSettings<MageLevelSettings>.Current.Backpaddle, new AutoBackpedal(() => Constants.Target.GetDistance <= (float)BasePersistentSettings<MageLevelSettings>.Current.BackpaddleRange && Constants.Target.HaveBuff("Frost Nova"), (float)BasePersistentSettings<MageLevelSettings>.Current.BackpaddleRange));
			base..ctor(settings, dictionary, array);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001E603 File Offset: 0x0001C803
		protected override void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			MageFoodManager.CheckIfEnoughFoodAndDrinks();
		}
	}
}
