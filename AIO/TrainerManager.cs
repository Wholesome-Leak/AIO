using System;
using System.Collections.Generic;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;

// Token: 0x0200000A RID: 10
public static class TrainerManager
{
	// Token: 0x06000039 RID: 57 RVA: 0x00003008 File Offset: 0x00001208
	public static void TrainerCheck()
	{
		try
		{
			foreach (Npc npc in TrainerManager.Trainers)
			{
				bool flag = !NpcDB.ListNpc.Contains(npc);
				if (flag)
				{
					NpcDB.AddNpc(npc, true, false);
					string str = "Added: ";
					Npc npc2 = npc;
					Main.Log(str + ((npc2 != null) ? npc2.ToString() : null));
					Thread.Sleep(50);
				}
			}
		}
		catch
		{
			Main.Log("something gone wrong");
		}
	}

	// Token: 0x04000019 RID: 25
	private static Npc DruidTrainer = new Npc
	{
		Name = "Turak Runetotem",
		Entry = 3033,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(-1039.41, -281.56, 159.0305, "None"),
		CanFlyTo = false,
		Type = 11
	};

	// Token: 0x0400001A RID: 26
	private static Npc ShamanTrainer = new Npc
	{
		Name = "Sian'tsu",
		Entry = 3403,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1932.88, -4211.3, 42.32275, "None"),
		CanFlyTo = false,
		Type = 18
	};

	// Token: 0x0400001B RID: 27
	private static Npc PaladinTrainer = new Npc
	{
		Name = "Master Pyreanor",
		Entry = 23128,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1939.69, -4133.33, 41.14468, "None"),
		CanFlyTo = false,
		Type = 14
	};

	// Token: 0x0400001C RID: 28
	private static Npc PriestTrainer = new Npc
	{
		Name = "Ur'kyo",
		Entry = 6018,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1452.43, -4179.82, 44.27711, "None"),
		CanFlyTo = false,
		Type = 16
	};

	// Token: 0x0400001D RID: 29
	private static Npc MageTrainer = new Npc
	{
		Name = "Enyo",
		Entry = 5883,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1472.48, -4224.6, 43.18612, "None"),
		CanFlyTo = false,
		Type = 19
	};

	// Token: 0x0400001E RID: 30
	private static Npc WarlockTrainer = new Npc
	{
		Name = "Grol'dar",
		Entry = 3324,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1844.21, -4353.61, -14.6542, "None"),
		CanFlyTo = false,
		Type = 20
	};

	// Token: 0x0400001F RID: 31
	private static Npc RogueTrainer = new Npc
	{
		Name = "Shenthul",
		Entry = 3401,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1771.21, -4284.42, 7.980936, "None"),
		CanFlyTo = false,
		Type = 12
	};

	// Token: 0x04000020 RID: 32
	private static Npc WarriorTrainer = new Npc
	{
		Name = "Sorek",
		Entry = 3354,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(1970.95, -4808.17, 56.99199, "None"),
		CanFlyTo = false,
		Type = 13
	};

	// Token: 0x04000021 RID: 33
	private static Npc HunterTrainer = new Npc
	{
		Name = "Xor'juul",
		Entry = 3406,
		Faction = 1,
		ContinentId = 1,
		Position = new Vector3(2084.96, -4623.77, 58.82039, "None"),
		CanFlyTo = false,
		Type = 15
	};

	// Token: 0x04000022 RID: 34
	public static List<Npc> Trainers = new List<Npc>
	{
		TrainerManager.DruidTrainer,
		TrainerManager.ShamanTrainer,
		TrainerManager.PaladinTrainer,
		TrainerManager.PriestTrainer,
		TrainerManager.MageTrainer,
		TrainerManager.RogueTrainer,
		TrainerManager.WarlockTrainer,
		TrainerManager.WarriorTrainer,
		TrainerManager.HunterTrainer
	};
}
