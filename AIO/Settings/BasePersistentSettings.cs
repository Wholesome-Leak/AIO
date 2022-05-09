using System;
using System.IO;
using robotManager.Helpful;
using wManager.Wow.Helpers;

namespace AIO.Settings
{
	// Token: 0x0200001B RID: 27
	public abstract class BasePersistentSettings<T> : BaseSettings where T : BasePersistentSettings<T>, new()
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00008FB8 File Offset: 0x000071B8
		private static string FileName
		{
			get
			{
				return Settings.AdviserFilePathAndName(typeof(T).Name, Constants.Me.Name + "." + Usefuls.RealmName);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00008FE7 File Offset: 0x000071E7
		protected override void OnUpdate()
		{
			base.OnUpdate();
			base.Save(BasePersistentSettings<T>.FileName);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00009000 File Offset: 0x00007200
		public static T Current
		{
			get
			{
				bool flag = BasePersistentSettings<T>._current == null;
				if (flag)
				{
					string fileName = BasePersistentSettings<T>.FileName;
					BasePersistentSettings<T>._current = (File.Exists(fileName) ? Settings.Load<T>(fileName) : Activator.CreateInstance<T>());
					BasePersistentSettings<T>._current.OnUpdate();
				}
				return BasePersistentSettings<T>._current;
			}
		}

		// Token: 0x04000040 RID: 64
		private static T _current;
	}
}
