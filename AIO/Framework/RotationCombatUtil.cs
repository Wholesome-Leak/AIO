using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AIO.Helpers.Caching;
using AIO.Lists;
using robotManager.Helpful;
using wManager;
using wManager.Wow;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Framework
{
	// Token: 0x0200004B RID: 75
	public static class RotationCombatUtil
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public static WoWUnit GetBestAoETarget(Func<WoWUnit, bool> predicate, float range, float size, IEnumerable<WoWUnit> units, int minimum = 0)
		{
			WoWUnit[] unitArray = (units as WoWUnit[]) ?? units.ToArray<WoWUnit>();
			return (from target in unitArray
			where target.GetDistance < range
			select target into unit
			select new KeyValuePair<WoWUnit, int>(unit, unitArray.Count((WoWUnit otherUnit) => unit.Position.DistanceTo(otherUnit.Position) < size && predicate(otherUnit))) into pair
			orderby pair.Value descending
			select pair).FirstOrDefault((KeyValuePair<WoWUnit, int> pair) => pair.Value >= minimum).Key;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public static WoWUnit CGetHighestHpPartyMemberTarget(Func<WoWUnit, bool> predicate)
		{
			WoWPlayer woWPlayer = (from partyMember in RotationFramework.PartyMembers.Where(delegate(WoWPlayer partyMember)
			{
				if (partyMember.CInCombat())
				{
					WoWUnit targetObject = partyMember.TargetObject;
					ulong? num = (targetObject != null) ? new ulong?(targetObject.Target) : null;
					ulong guid = partyMember.Guid;
					if (num.GetValueOrDefault() == guid & num != null)
					{
						return predicate(partyMember.TargetObject);
					}
				}
				return false;
			})
			orderby partyMember.Health descending
			select partyMember).FirstOrDefault<WoWPlayer>();
			return (woWPlayer != null) ? woWPlayer.TargetObject : null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000DD25 File Offset: 0x0000BF25
		public static bool IsCurrentSpell(this Spell spell)
		{
			return RotationCombatUtil.IsCurrentSpell(spell.Name);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DD32 File Offset: 0x0000BF32
		public static bool IsCurrentSpell(string spellName)
		{
			return Lua.LuaDoString<bool>("if IsCurrentSpell('" + spellName + "') == 1 then return true else return false end", "");
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public static int CCountAlivePartyMembers(Func<WoWUnit, bool> predicate)
		{
			int num = 0;
			for (int i = 0; i < RotationFramework.PartyMembers.Length; i++)
			{
				WoWPlayer woWPlayer = RotationFramework.PartyMembers[i];
				bool flag = woWPlayer.CIsAlive() && predicate(woWPlayer);
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public static bool CHurtPartyMembersAtLeast(Func<WoWPlayer, bool> predicate, byte amount)
		{
			int num = 0;
			byte b = 0;
			while ((int)b < RotationFramework.PartyMembers.Length)
			{
				WoWPlayer woWPlayer = RotationFramework.PartyMembers[(int)b];
				bool flag = woWPlayer.CHealthPercent() > 99.0;
				if (flag)
				{
					break;
				}
				bool flag2 = woWPlayer.CIsAlive() && predicate(woWPlayer);
				if (flag2)
				{
					num++;
				}
				bool flag3 = num >= (int)amount;
				if (flag3)
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000DE24 File Offset: 0x0000C024
		public static ushort CCountHurtPartyMembers(Func<WoWPlayer, bool> predicate)
		{
			ushort num = 0;
			ushort num2 = 0;
			while ((int)num2 < RotationFramework.PartyMembers.Length)
			{
				WoWPlayer woWPlayer = RotationFramework.PartyMembers[(int)num2];
				bool flag = woWPlayer.CHealthPercent() > 99.0;
				if (flag)
				{
					break;
				}
				bool flag2 = woWPlayer.CIsAlive() && predicate(woWPlayer);
				if (flag2)
				{
					num += 1;
				}
				num2 += 1;
			}
			return num;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DE94 File Offset: 0x0000C094
		public static bool CAnyPartyMemberAlive()
		{
			for (int i = 0; i < RotationFramework.PartyMembers.Length; i++)
			{
				bool flag = RotationFramework.PartyMembers[i].CIsAlive();
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public static int CountAliveGroupMembers(Func<WoWUnit, bool> predicate)
		{
			int num = 0;
			List<WoWPlayer> party = Party.GetParty();
			for (int i = 0; i < party.Count; i++)
			{
				WoWPlayer woWPlayer = party[i];
				bool flag = woWPlayer.IsAlive && predicate(woWPlayer);
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000DF30 File Offset: 0x0000C130
		public static int CCountHurtGroupMembers(Func<WoWUnit, bool> predicate)
		{
			int num = 0;
			List<WoWPlayer> party = Party.GetParty();
			for (int i = 0; i < party.Count; i++)
			{
				WoWPlayer woWPlayer = party[i];
				bool flag = woWPlayer.CHealthPercent() > 99.0;
				if (flag)
				{
					break;
				}
				bool flag2 = woWPlayer.CIsAlive() && predicate(woWPlayer);
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public static int CountEnemies(Func<WoWUnit, bool> predicate)
		{
			return RotationFramework.Enemies.Count((WoWUnit unit) => unit.IsAlive && unit.IsEnemy() && predicate(unit));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public static WoWUnit FindFriendlyPlayer(Func<WoWUnit, bool> predicate)
		{
			return (from u in RotationFramework.PlayerUnits
			where u.Reaction == 4 && u.IsAlive && predicate(u)
			orderby u.HealthPercent
			select u).FirstOrDefault<WoWPlayer>();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000E030 File Offset: 0x0000C230
		public static WoWUnit FindExplicitPartyMember(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindPartyMember((WoWUnit u) => !u.IsLocalPlayer && predicate(u));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E05C File Offset: 0x0000C25C
		public static WoWUnit CFindExplicitHurtPartyMember(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.CFindHurtPartyMember((WoWUnit partyMember) => !partyMember.IsLocalPlayer && predicate(partyMember));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E088 File Offset: 0x0000C288
		public static WoWUnit CFindHurtPartyMember(Func<WoWUnit, bool> predicate)
		{
			ushort num = 0;
			while ((int)num < RotationFramework.PartyMembers.Length)
			{
				WoWPlayer woWPlayer = RotationFramework.PartyMembers[(int)num];
				bool flag = woWPlayer.CHealthPercent() > 99.0;
				if (flag)
				{
					break;
				}
				bool flag2 = predicate(woWPlayer);
				if (flag2)
				{
					return woWPlayer;
				}
				num += 1;
			}
			return null;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		public static WoWUnit FindPartyMember(Func<WoWUnit, bool> predicate)
		{
			return RotationFramework.PartyMembers.FirstOrDefault((WoWPlayer partyMember) => partyMember.IsAlive && predicate(partyMember));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000E118 File Offset: 0x0000C318
		public static WoWUnit CFindPartyMember(Func<WoWUnit, bool> predicate)
		{
			for (int i = 0; i < RotationFramework.PartyMembers.Length; i++)
			{
				WoWPlayer woWPlayer = RotationFramework.PartyMembers[i];
				bool flag = woWPlayer.CIsAlive() && predicate(woWPlayer);
				if (flag)
				{
					return woWPlayer;
				}
			}
			return null;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000E168 File Offset: 0x0000C368
		public static WoWUnit FindTank(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindPartyMember((WoWUnit u) => u.Name == RotationFramework.TankName && predicate(u));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000E194 File Offset: 0x0000C394
		public static WoWUnit CFindTank(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.CFindPartyMember((WoWUnit u) => u.Name == RotationFramework.TankName && predicate(u));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public static WoWUnit FindHeal(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindPartyMember((WoWUnit u) => u.Name == RotationFramework.HealName && predicate(u));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000E1EB File Offset: 0x0000C3EB
		public static WoWUnit FindEnemy(Func<WoWUnit, bool> predicate)
		{
			return RotationFramework.Enemies.FirstOrDefault(predicate);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static WoWUnit FindEnemyPlayer(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemy(RotationFramework.PlayerUnits, predicate);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000E205 File Offset: 0x0000C405
		public static WoWUnit FindEnemyCasting(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCasting(RotationFramework.Enemies, predicate);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000E212 File Offset: 0x0000C412
		public static WoWUnit FindPlayerCasting(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCasting(RotationFramework.PlayerUnits, predicate);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000E21F File Offset: 0x0000C41F
		public static WoWUnit FindEnemyCastingOnMe(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCastingOnMe(RotationFramework.Enemies, predicate);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E22C File Offset: 0x0000C42C
		public static WoWUnit FindEnemyCastingOnGroup(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCastingOnGroup(RotationFramework.Enemies, predicate);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E239 File Offset: 0x0000C439
		public static WoWUnit FindPlayerCastingOnMe(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCastingOnMe(RotationFramework.PlayerUnits, predicate);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E246 File Offset: 0x0000C446
		public static WoWUnit FindEnemyTargetingMe(Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyTargetingMe(RotationFramework.Enemies, predicate);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E254 File Offset: 0x0000C454
		public static WoWUnit CFindInRange(this WoWUnit[] units, Func<WoWUnit, bool> predicate, float distance, int count = 0)
		{
			int num = (count == 0) ? units.Length : Math.Min(count, units.Length);
			int i = 0;
			while (i < num)
			{
				WoWUnit woWUnit = units[i];
				bool flag = woWUnit.CGetDistance() > distance;
				WoWUnit result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = predicate(woWUnit);
					if (!flag2)
					{
						i++;
						continue;
					}
					result = woWUnit;
				}
				return result;
			}
			return null;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public static WoWUnit FindEnemyAttackingGroup(Func<WoWUnit, bool> predicate)
		{
			return RotationFramework.Enemies.FirstOrDefault((WoWUnit u) => u.IsAttackable && !u.IsTargetingMe && u.IsTargetingPartyMember && predicate(u));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public static WoWUnit FindEnemyAttackingGroupAndMe(Func<WoWUnit, bool> predicate)
		{
			return RotationFramework.Enemies.FirstOrDefault((WoWUnit u) => u.IsAttackable && u.IsTargetingMeOrMyPetOrPartyMember && predicate(u));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E318 File Offset: 0x0000C518
		private static WoWUnit FindEnemyCasting(IEnumerable<WoWUnit> units, Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemy(units, (WoWUnit u) => predicate(u) && u.IsCast);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000E344 File Offset: 0x0000C544
		private static WoWUnit FindEnemyCastingOnMe(IEnumerable<WoWUnit> units, Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCasting(units, (WoWUnit u) => predicate(u) && u.IsTargetingMe);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E370 File Offset: 0x0000C570
		private static WoWUnit FindEnemyCastingOnGroup(IEnumerable<WoWUnit> units, Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemyCasting(units, (WoWUnit u) => u.IsTargetingPartyMember && predicate(u));
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E39C File Offset: 0x0000C59C
		private static WoWUnit FindEnemyTargetingMe(IEnumerable<WoWUnit> units, Func<WoWUnit, bool> predicate)
		{
			return RotationCombatUtil.FindEnemy(units, (WoWUnit u) => predicate(u) && u.IsTargetingMe);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		private static WoWUnit FindEnemy(IEnumerable<WoWUnit> units, Func<WoWUnit, bool> predicate)
		{
			return (from u in units
			where u.IsAlive && u.IsEnemy() && predicate(u)
			orderby u.GetDistance
			select u).FirstOrDefault<WoWUnit>();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000E41D File Offset: 0x0000C61D
		public static WoWUnit BotTarget(Func<WoWUnit, bool> predicate)
		{
			WoWUnit target = Constants.Target;
			return (!TraceLine.TraceLineGo((target != null) ? target.Position : null) && predicate(Constants.Target)) ? Constants.Target : null;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E44C File Offset: 0x0000C64C
		public static WoWUnit BotTargetFast(Func<WoWUnit, bool> predicate)
		{
			return predicate(Constants.Target) ? Constants.Target : null;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E464 File Offset: 0x0000C664
		public static WoWUnit PetTarget(Func<WoWUnit, bool> predicate)
		{
			WoWUnit pet = Constants.Pet;
			Vector3 vector;
			if (pet == null)
			{
				vector = null;
			}
			else
			{
				WoWUnit targetObject = pet.TargetObject;
				vector = ((targetObject != null) ? targetObject.Position : null);
			}
			if (!TraceLine.TraceLineGo(vector))
			{
				WoWUnit pet2 = Constants.Pet;
				if (predicate((pet2 != null) ? pet2.TargetObject : null))
				{
					WoWUnit pet3 = Constants.Pet;
					return (pet3 != null) ? pet3.TargetObject : null;
				}
			}
			return null;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E4C2 File Offset: 0x0000C6C2
		public static WoWUnit FindPet(Func<WoWUnit, bool> predicate)
		{
			WoWUnit pet = Constants.Pet;
			return (!TraceLine.TraceLineGo((pet != null) ? pet.Position : null) && predicate(Constants.Pet)) ? Constants.Pet : null;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public static WoWUnit FindMe(Func<WoWUnit, bool> predicate)
		{
			return predicate(Constants.Me) ? Constants.Me : null;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C650 File Offset: 0x0000A850
		public static bool Always(IRotationAction action, WoWUnit target)
		{
			return true;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E508 File Offset: 0x0000C708
		public static int EnemyAttackingCount()
		{
			return RotationFramework.Enemies.Count((WoWUnit u) => u.IsTargetingMeOrMyPetOrPartyMember && u.IsAttackable);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E534 File Offset: 0x0000C734
		public static int EnemyAttackingCountCluster(int Range)
		{
			return RotationFramework.Enemies.Count((WoWUnit u) => u.IsTargetingMeOrMyPetOrPartyMember && u.IsAttackable && u.Position.DistanceTo(Constants.Target.Position) <= (float)Range);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000E564 File Offset: 0x0000C764
		public static uint GetThreatStatus(this WoWUnit unit)
		{
			return Lua.LuaDoString<uint>("return UnitThreatSituation(\"" + unit.Name + "\");", "");
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000E588 File Offset: 0x0000C788
		public static int GetThreatStatusMemory(this WoWUnit unit)
		{
			uint getBaseAddress = unit.GetBaseAddress;
			try
			{
				bool flag = !Conditions.InGameAndConnected;
				if (flag)
				{
					return -1;
				}
				bool flag2 = getBaseAddress > 0U;
				if (flag2)
				{
					uint num = Memory.WowMemory.AllocData.Get(4);
					bool flag3 = num <= 0U;
					if (flag3)
					{
						return -1;
					}
					string[] array = new string[]
					{
						Memory.WowMemory.CallWrapperCode(Memory.WowMemory.Memory.RebaseAddress(14576U), Array.Empty<object>()),
						"test eax, eax",
						"je @out",
						"mov ecx, " + (getBaseAddress + 48U).ToString(),
						Memory.WowMemory.CallWrapperCodeRebaseEsp(Memory.WowMemory.Memory.RebaseAddress(1221024U), 4, new object[]
						{
							"ecx"
						}),
						"movzx ecx, al",
						"sub ecx, 1",
						"mov [" + num.ToString() + "], ecx",
						"@out:",
						Memory.WowMemory.RetnToHookCode
					};
					Memory.WowMemory.Memory.WriteInt32(num, 0);
					Memory.WowMemory.InjectAndExecute(array, false, 0, true);
					int val = Memory.WowMemory.Memory.ReadInt32(num);
					Memory.WowMemory.AllocData.Free(num);
					return Math.Max(0, val);
				}
			}
			catch
			{
				Logging.WriteError("Failed to get ThreatStatus from memory.", true);
			}
			return -1;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E734 File Offset: 0x0000C934
		public static int GetAggroDifferenceFor(WoWUnit unit)
		{
			uint threatStatus = unit.GetThreatStatus();
			uint num = (from p in RotationFramework.PartyMembers
			let tVal = p.GetThreatStatus()
			orderby tVal descending
			select tVal).FirstOrDefault<uint>();
			return (int)(threatStatus - num);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E7C9 File Offset: 0x0000C9C9
		public static bool IsAutoRepeating(string name)
		{
			return Lua.LuaDoString<bool>("return IsAutoRepeatSpell(\"" + name + "\")", "");
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000E7E5 File Offset: 0x0000C9E5
		public static bool IsAutoAttacking()
		{
			return Lua.LuaDoString<bool>("return IsCurrentSpell('Attack') == 1 or IsCurrentSpell('Attack') == true", "");
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public static bool CastSpell(RotationSpell spell, WoWUnit unit, bool force)
		{
			bool flag = unit == null || !spell.KnownSpell || !spell.IsSpellUsable || !unit.IsValid || unit.IsDead || (wManagerSetting.CurrentSetting.IgnoreFightGoundMount && Constants.Me.IsMounted && !LaggyTransports.Entries.Contains(ObjectManager.GetObjectByGuid(Constants.Me.TransportGuid).Entry));
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (double)spell.CastTime > 0.0 && Constants.Me.GetMove;
				if (flag2)
				{
					bool flag3 = RotationCombatUtil.freeMove;
					if (flag3)
					{
						return false;
					}
					MovementManager.StopMoveTo(false, (int)(spell.CastTime * 1000f));
				}
				bool flag4 = !force && Constants.Me.IsCast;
				if (flag4)
				{
					result = false;
				}
				else
				{
					if (force)
					{
						RotationCombatUtil.StopCasting();
					}
					RotationLogger.Fight(string.Format("Casting {0} on {1} [{2}]", spell.Name, unit.Name, unit.Guid));
					SpellManager.CastSpellByNameOn(spell.Name, RotationCombatUtil.GetLuaId(unit));
					bool flag5 = RotationCombatUtil.AreaSpells.Contains(spell.Name);
					if (flag5)
					{
						ClickOnTerrain.Pulse(unit.Position);
					}
					RotationCombatUtil.TimeSinceLastCast.Restart();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E94E File Offset: 0x0000CB4E
		public static void StopCasting()
		{
			Lua.LuaDoString("SpellStopCasting();", false);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000E960 File Offset: 0x0000CB60
		public static string GetLuaId(WoWUnit unit)
		{
			bool flag = unit.Guid == Constants.Me.Guid;
			string result;
			if (flag)
			{
				result = "player";
			}
			else
			{
				bool flag2 = unit.Guid == Constants.Target.Guid;
				if (flag2)
				{
					result = "target";
				}
				else
				{
					Constants.Me.FocusGuid = unit.Guid;
					result = "focus";
				}
			}
			return result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public static T ExecuteActionOnUnit<T>(WoWUnit unit, Func<string, T> action)
		{
			return RotationCombatUtil.ExecuteActionOnTarget<T>(unit.Guid, action);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public static T ExecuteActionOnTarget<T>(ulong target, Func<string, T> action)
		{
			bool flag = target == Constants.Me.Guid;
			T result;
			if (flag)
			{
				result = action("player");
			}
			else
			{
				bool flag2 = target == Constants.Target.Guid;
				if (flag2)
				{
					result = action("target");
				}
				else
				{
					RotationCombatUtil.SetMouseoverGuid(target);
					result = action("mouseover");
				}
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000EA38 File Offset: 0x0000CC38
		public static T ExecuteActionOnFocus<T>(ulong target, Func<string, T> action)
		{
			RotationCombatUtil.SetFocusGuid(target);
			return action("focus");
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public static void SetFocusGuid(ulong guid)
		{
			Constants.Me.FocusGuid = guid;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000EA6B File Offset: 0x0000CC6B
		private static void SetMouseoverGuid(ulong guid)
		{
			Constants.Me.MouseOverGuid = guid;
		}

		// Token: 0x040001B5 RID: 437
		private static readonly List<string> AreaSpells = new List<string>
		{
			"Death and Decay",
			"Mass Dispel",
			"Blizzard",
			"Rain of Fire",
			"Freeze",
			"Volley",
			"Flare",
			"Hurricane",
			"Flamestrike",
			"Distract",
			"Force of Nature"
		};

		// Token: 0x040001B6 RID: 438
		public static bool freeMove = false;

		// Token: 0x040001B7 RID: 439
		public static readonly Stopwatch TimeSinceLastCast = Stopwatch.StartNew();
	}
}
