using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AIO;
using AIO.Combat.Common;
using AIO.Combat.DeathKnight;
using AIO.Combat.Druid;
using AIO.Combat.Hunter;
using AIO.Combat.Mage;
using AIO.Combat.Paladin;
using AIO.Combat.Priest;
using AIO.Combat.Rogue;
using AIO.Combat.Shaman;
using AIO.Combat.Warlock;
using AIO.Combat.Warrior;
using AIO.Events;
using AIO.Framework;
using AIO.Helpers;
using AIO.Lists;
using AIO.Settings;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

// Token: 0x02000017 RID: 23
public class Main : ICustomClass
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000073 RID: 115 RVA: 0x000087D3 File Offset: 0x000069D3
	public float Range
	{
		get
		{
			BaseCombatClass combatClass = this.CombatClass;
			return (combatClass != null) ? combatClass.Range : 5f;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000074 RID: 116 RVA: 0x000087EC File Offset: 0x000069EC
	private static BaseSettings CombatSettings
	{
		get
		{
			switch (Constants.Me.WowClass)
			{
			case 1:
				return BasePersistentSettings<WarriorLevelSettings>.Current;
			case 2:
				return BasePersistentSettings<PaladinLevelSettings>.Current;
			case 3:
				return BasePersistentSettings<HunterLevelSettings>.Current;
			case 4:
				return BasePersistentSettings<RogueLevelSettings>.Current;
			case 5:
				return BasePersistentSettings<PriestLevelSettings>.Current;
			case 6:
				return BasePersistentSettings<DeathKnightLevelSettings>.Current;
			case 7:
				return BasePersistentSettings<ShamanLevelSettings>.Current;
			case 8:
				return BasePersistentSettings<MageLevelSettings>.Current;
			case 9:
				return BasePersistentSettings<WarlockLevelSettings>.Current;
			case 11:
				return BasePersistentSettings<DruidLevelSettings>.Current;
			}
			Main.LogError("Class not supported.");
			return null;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000075 RID: 117 RVA: 0x000088A0 File Offset: 0x00006AA0
	private static BaseCombatClass LazyCombatClass
	{
		get
		{
			switch (Constants.Me.WowClass)
			{
			case 1:
				return new WarriorBehavior();
			case 2:
				return new PaladinBehavior();
			case 3:
				return new HunterBehavior();
			case 4:
				return new RogueBehavior();
			case 5:
				return new PriestBehavior();
			case 6:
				return new DeathKnightBehavior();
			case 7:
				return new ShamanBehavior();
			case 8:
				return new MageBehavior();
			case 9:
				return new WarlockBehavior();
			case 11:
				return new DruidBehavior();
			}
			Main.LogError("Class not supported.");
			Products.ProductStop();
			return null;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00008958 File Offset: 0x00006B58
	public Main()
	{
		this.Components = new List<ICycleable>
		{
			new SyntheticEvents(),
			new RotationFramework(),
			new RacialManager(),
			new TalentsManager(),
			new DeferredCycleable(() => this.CombatClass)
		};
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000089C0 File Offset: 0x00006BC0
	private void ForceStepBackward(string name, List<string> ps)
	{
		bool flag = name != "UI_ERROR_MESSAGE";
		if (!flag)
		{
			string text = ps[0];
			string a = text;
			if (a == "Target needs to be in front of you." || a == "Target too close")
			{
				Main.LogFight("Attempting to move backward due to wrong facet.");
				Move.Backward(0, 300);
			}
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00008A24 File Offset: 0x00006C24
	private void ForceBindItem(string name, List<string> ps)
	{
		if (name == "AUTOEQUIP_BIND_CONFIRM" || name == "EQUIP_BIND_CONFIRM" || name == "LOOT_BIND_CONFIRM" || name == "USE_BIND_CONFIRM")
		{
			Usefuls.SelectGossipOption(1);
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00008A78 File Offset: 0x00006C78
	private static void FixRelativePositionLag()
	{
		foreach (WoWUnit woWUnit in ObjectManager.GetObjectWoWUnit())
		{
			ulong transportGuid = woWUnit.TransportGuid;
			bool flag = transportGuid == 0UL;
			if (!flag)
			{
				WoWObject objectByGuid = ObjectManager.GetObjectByGuid(transportGuid);
				bool flag2 = LaggyTransports.Entries.Contains(objectByGuid.Entry);
				if (flag2)
				{
					bool flag3 = !WoWUnit.ForceRelativePosition;
					if (flag3)
					{
						Logging.WriteDebug(string.Concat(new string[]
						{
							"Forcing relative positions because ",
							woWUnit.Name,
							" is on ",
							objectByGuid.Name,
							"."
						}));
						WoWUnit.ForceRelativePosition = true;
					}
					return;
				}
			}
		}
		bool forceRelativePosition = WoWUnit.ForceRelativePosition;
		if (forceRelativePosition)
		{
			Logging.WriteDebug("Not forcing relative positions anymore.");
			WoWUnit.ForceRelativePosition = false;
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00008B74 File Offset: 0x00006D74
	public void Initialize()
	{
		Update.CheckUpdate();
		Main.Log("Started 3.1.40 of FightClass");
		Main.Log("Started 3.1.40 Discovering class and finding rotation...");
		bool flag = Others.ParseInt(Information.Version.Replace(".", "").Substring(0, 3)) == 172;
		if (flag)
		{
			Main.Log("AIO couldn't load (v " + Information.Version + ")");
		}
		else
		{
			EventsLuaWithArgs.OnEventsLuaStringWithArgs += new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.ForceBindItem);
			EventsLuaWithArgs.OnEventsLuaStringWithArgs += new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.ForceStepBackward);
			EventsLuaWithArgs.OnEventsLuaStringWithArgs += new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(CancelableSpell.CastStopHandler);
			EventsLuaWithArgs.OnEventsLuaStringWithArgs += new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(CombatLogger.ParseCombatLog);
			wManagerSetting.CurrentSetting.UseLuaToMove = true;
			this.TokenRelativePositionFix = new CancellationTokenSource();
			Task.Factory.StartNew(delegate()
			{
				while (!this.TokenRelativePositionFix.IsCancellationRequested)
				{
					Main.FixRelativePositionLag();
					Thread.Sleep(5000);
				}
			}, this.TokenRelativePositionFix.Token);
			this.TokenKeyboardHook = new CancellationTokenSource();
			Task.Factory.StartNew(delegate()
			{
				while (!this.TokenKeyboardHook.IsCancellationRequested)
				{
					Hotkeys.CheckKeyPress();
					Thread.Sleep(1000);
				}
			}, this.TokenKeyboardHook.Token);
			BaseSettings combatSettings = Main.CombatSettings;
			this.CombatClass = Main.LazyCombatClass;
			this.Components.ForEach(delegate(ICycleable c)
			{
				if (c != null)
				{
					c.Initialize();
				}
			});
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00008CD4 File Offset: 0x00006ED4
	public void Dispose()
	{
		EventsLuaWithArgs.OnEventsLuaStringWithArgs -= new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.ForceBindItem);
		EventsLuaWithArgs.OnEventsLuaStringWithArgs -= new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(this.ForceStepBackward);
		EventsLuaWithArgs.OnEventsLuaStringWithArgs -= new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(CancelableSpell.CastStopHandler);
		EventsLuaWithArgs.OnEventsLuaStringWithArgs -= new EventsLuaWithArgs.EventsLuaStringWithArgsHandler(CombatLogger.ParseCombatLog);
		CancellationTokenSource tokenRelativePositionFix = this.TokenRelativePositionFix;
		if (tokenRelativePositionFix != null)
		{
			tokenRelativePositionFix.Cancel();
		}
		CancellationTokenSource tokenKeyboardHook = this.TokenKeyboardHook;
		if (tokenKeyboardHook != null)
		{
			tokenKeyboardHook.Cancel();
		}
		Hotkeys.DisableRangeCircles();
		this.Components.ForEach(delegate(ICycleable c)
		{
			if (c != null)
			{
				c.Dispose();
			}
		});
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00008D7F File Offset: 0x00006F7F
	public void ShowConfiguration()
	{
		BaseSettings combatSettings = Main.CombatSettings;
		if (combatSettings != null)
		{
			combatSettings.ShowConfiguration();
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00008D94 File Offset: 0x00006F94
	private static string wowClass
	{
		get
		{
			return Constants.Me.WowClass.ToString();
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600007E RID: 126 RVA: 0x00008DB9 File Offset: 0x00006FB9
	private static bool _debug
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00008DBC File Offset: 0x00006FBC
	public static void LogFight(string message)
	{
		Logging.Write("[WOTLK - " + Main.wowClass + "]: " + message, 16, Color.ForestGreen);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00008DE1 File Offset: 0x00006FE1
	public static void LogError(string message)
	{
		Logging.Write("[WOTLK - " + Main.wowClass + "]: " + message, 4, Color.DarkRed);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00008E05 File Offset: 0x00007005
	public static void Log(string message)
	{
		Logging.Write("[WOTLK - " + Main.wowClass + "]: " + message);
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00008E24 File Offset: 0x00007024
	public static void LogDebug(string message)
	{
		bool debug = Main._debug;
		if (debug)
		{
			Logging.WriteDebug("[WOTLK - " + Main.wowClass + "]: " + message);
		}
	}

	// Token: 0x04000038 RID: 56
	public const string Version = "3.1.40";

	// Token: 0x04000039 RID: 57
	private CancellationTokenSource TokenRelativePositionFix;

	// Token: 0x0400003A RID: 58
	private CancellationTokenSource TokenKeyboardHook;

	// Token: 0x0400003B RID: 59
	private BaseCombatClass CombatClass;

	// Token: 0x0400003C RID: 60
	private readonly List<ICycleable> Components;
}
