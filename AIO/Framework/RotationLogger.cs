using System;
using robotManager.Helpful;

namespace AIO.Framework
{
	// Token: 0x0200003D RID: 61
	public static class RotationLogger
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public static void Trace(string log)
		{
			bool flag = RotationLogger.Level >= RotationLogger.LogLevel.TRACE;
			if (flag)
			{
				Logging.WriteDebug("[RTF]: " + log);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public static void Fight(string log)
		{
			Logging.WriteFight("[RTF] " + log);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C960 File Offset: 0x0000AB60
		public static void LightDebug(string log)
		{
			bool flag = RotationLogger.Level >= RotationLogger.LogLevel.DEBUG_LIGHT;
			if (flag)
			{
				Logging.WriteFight("[RTF] " + log);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C990 File Offset: 0x0000AB90
		public static void Debug(string log)
		{
			bool flag = RotationLogger.Level >= RotationLogger.LogLevel.DEBUG;
			if (flag)
			{
				Logging.WriteFight("[RTF] " + log);
			}
		}

		// Token: 0x04000175 RID: 373
		public static RotationLogger.LogLevel Level;

		// Token: 0x0200003E RID: 62
		public enum LogLevel
		{
			// Token: 0x04000177 RID: 375
			INFO,
			// Token: 0x04000178 RID: 376
			DEBUG_LIGHT,
			// Token: 0x04000179 RID: 377
			DEBUG = 3,
			// Token: 0x0400017A RID: 378
			TRACE
		}
	}
}
