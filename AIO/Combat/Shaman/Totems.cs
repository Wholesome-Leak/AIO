using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AIO.Combat.Common;
using AIO.Framework;
using robotManager.Helpful;
using wManager.Events;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Shaman
{
	// Token: 0x02000099 RID: 153
	internal class Totems : ICycleable
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001529C File Offset: 0x0001349C
		private static IEnumerable<WoWUnit> Pets
		{
			get
			{
				return from o in RotationFramework.AllUnits
				where o.IsMyPet
				select o;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000152C7 File Offset: 0x000134C7
		private static IEnumerable<WoWUnit> NearbyPets
		{
			get
			{
				return from o in Totems.Pets
				where o.GetDistance < 30f
				select o;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000152F2 File Offset: 0x000134F2
		private static IEnumerable<WoWUnit> DistantPets
		{
			get
			{
				return from o in Totems.Pets
				where o.GetDistance >= 30f
				select o;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001531D File Offset: 0x0001351D
		public static bool HasAny(params string[] totems)
		{
			return totems.Any((string t) => Totems.NearbyPets.Any((WoWUnit o) => o.Name.Contains(t)));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015344 File Offset: 0x00013544
		public static bool HasAll(params string[] totems)
		{
			return totems.All((string t) => t == null || Totems.NearbyPets.Any((WoWUnit o) => o.Name.Contains(t)));
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001536B File Offset: 0x0001356B
		public static bool ShouldRecall()
		{
			return Totems.DistantPets.Any<WoWUnit>();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015377 File Offset: 0x00013577
		public static bool HasTemporary()
		{
			return Totems.HasAny(new string[]
			{
				"Mana Tide Totem",
				"Earth Elemental Totem",
				"Tremor Totem",
				"Grounding Totem"
			});
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000153A4 File Offset: 0x000135A4
		private string Spec
		{
			get
			{
				return this.CombatClass.Specialisation;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000153B4 File Offset: 0x000135B4
		internal Totems(BaseCombatClass combatClass)
		{
			this.CombatClass = combatClass;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001546F File Offset: 0x0001366F
		public void Initialize()
		{
			MovementEvents.OnMovementPulse += new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00015483 File Offset: 0x00013683
		public void Dispose()
		{
			MovementEvents.OnMovementPulse -= new MovementEvents.MovementCancelableHandler(this.OnMovementPulse);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015497 File Offset: 0x00013697
		private void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
		{
			this.SetCall();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000154A0 File Offset: 0x000136A0
		private void SetTotems(Spell earthTotem, Spell fireTotem, Spell airTotem, Spell waterTotem)
		{
			Lua.LuaDoString(string.Format("SetMultiCastSpell(133, {0})", (fireTotem != null) ? fireTotem.Id : 0U), false);
			Lua.LuaDoString(string.Format("SetMultiCastSpell(134, {0})", (earthTotem != null) ? earthTotem.Id : 0U), false);
			Lua.LuaDoString(string.Format("SetMultiCastSpell(135, {0})", (waterTotem != null) ? waterTotem.Id : 0U), false);
			Lua.LuaDoString(string.Format("SetMultiCastSpell(136, {0})", (airTotem != null) ? airTotem.Id : 0U), false);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00015538 File Offset: 0x00013738
		[TupleElementNames(new string[]
		{
			"Earth",
			"Fire",
			"Air",
			"Water"
		})]
		private ValueTuple<Spell, Spell, Spell, Spell> DefaultTotems
		{
			[return: TupleElementNames(new string[]
			{
				"Earth",
				"Fire",
				"Air",
				"Water"
			})]
			get
			{
				Spell item = null;
				Spell item2 = null;
				Spell item3 = null;
				Spell item4 = null;
				string spec = this.Spec;
				string a = spec;
				if (!(a == "Enhancement"))
				{
					if (a == "Restoration" || a == "Elemental")
					{
						bool knownSpell = this.StoneskinTotem.KnownSpell;
						if (knownSpell)
						{
							item = this.StoneskinTotem;
						}
						else
						{
							bool knownSpell2 = this.StrengthOfEarthTotem.KnownSpell;
							if (knownSpell2)
							{
								item = this.StrengthOfEarthTotem;
							}
						}
						bool knownSpell3 = this.TotemOfWrath.KnownSpell;
						if (knownSpell3)
						{
							item2 = this.TotemOfWrath;
						}
						else
						{
							bool knownSpell4 = this.FlametongueTotem.KnownSpell;
							if (knownSpell4)
							{
								item2 = this.FlametongueTotem;
							}
						}
						bool knownSpell5 = this.WrathOfAirTotem.KnownSpell;
						if (knownSpell5)
						{
							item3 = this.WrathOfAirTotem;
						}
						else
						{
							bool knownSpell6 = this.WindfuryTotem.KnownSpell;
							if (knownSpell6)
							{
								item3 = this.WindfuryTotem;
							}
						}
						bool knownSpell7 = this.ManaSpringTotem.KnownSpell;
						if (knownSpell7)
						{
							item4 = this.ManaSpringTotem;
						}
						else
						{
							bool knownSpell8 = this.HealingStreamTotem.KnownSpell;
							if (knownSpell8)
							{
								item4 = this.HealingStreamTotem;
							}
						}
					}
				}
				else
				{
					bool knownSpell9 = this.StrengthOfEarthTotem.KnownSpell;
					if (knownSpell9)
					{
						item = this.StrengthOfEarthTotem;
					}
					else
					{
						bool knownSpell10 = this.StoneskinTotem.KnownSpell;
						if (knownSpell10)
						{
							item = this.StoneskinTotem;
						}
					}
					bool knownSpell11 = this.MagmaTotem.KnownSpell;
					if (knownSpell11)
					{
						item2 = this.MagmaTotem;
					}
					else
					{
						bool knownSpell12 = this.SearingTotem.KnownSpell;
						if (knownSpell12)
						{
							item2 = this.SearingTotem;
						}
						else
						{
							bool knownSpell13 = this.FlametongueTotem.KnownSpell;
							if (knownSpell13)
							{
								item2 = this.FlametongueTotem;
							}
						}
					}
					bool knownSpell14 = this.WindfuryTotem.KnownSpell;
					if (knownSpell14)
					{
						item3 = this.WindfuryTotem;
					}
					else
					{
						bool knownSpell15 = this.WrathOfAirTotem.KnownSpell;
						if (knownSpell15)
						{
							item3 = this.WrathOfAirTotem;
						}
					}
					bool knownSpell16 = this.HealingStreamTotem.KnownSpell;
					if (knownSpell16)
					{
						item4 = this.HealingStreamTotem;
					}
					else
					{
						bool knownSpell17 = this.ManaSpringTotem.KnownSpell;
						if (knownSpell17)
						{
							item4 = this.ManaSpringTotem;
						}
					}
				}
				return new ValueTuple<Spell, Spell, Spell, Spell>(item, item2, item3, item4);
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015774 File Offset: 0x00013974
		private void SetCall()
		{
			ValueTuple<Spell, Spell, Spell, Spell> defaultTotems = this.DefaultTotems;
			Spell item = defaultTotems.Item1;
			Spell item2 = defaultTotems.Item2;
			Spell item3 = defaultTotems.Item3;
			Spell item4 = defaultTotems.Item4;
			this.SetTotems(item, item2, item3, item4);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000157B0 File Offset: 0x000139B0
		public bool MissingDefaults()
		{
			ValueTuple<Spell, Spell, Spell, Spell> defaultTotems = this.DefaultTotems;
			Spell item = defaultTotems.Item1;
			Spell item2 = defaultTotems.Item2;
			Spell item3 = defaultTotems.Item3;
			Spell item4 = defaultTotems.Item4;
			return !Totems.HasAll(new string[]
			{
				(item != null) ? item.Name : null,
				(item2 != null) ? item2.Name : null,
				(item3 != null) ? item3.Name : null,
				(item4 != null) ? item4.Name : null
			});
		}

		// Token: 0x040002C9 RID: 713
		private readonly BaseCombatClass CombatClass;

		// Token: 0x040002CA RID: 714
		private readonly Spell StoneskinTotem = new Spell("Stoneskin Totem");

		// Token: 0x040002CB RID: 715
		private readonly Spell StrengthOfEarthTotem = new Spell("Strength of Earth Totem");

		// Token: 0x040002CC RID: 716
		private readonly Spell MagmaTotem = new Spell("Magma Totem");

		// Token: 0x040002CD RID: 717
		private readonly Spell SearingTotem = new Spell("Searing Totem");

		// Token: 0x040002CE RID: 718
		private readonly Spell FlametongueTotem = new Spell("Flametongue Totem");

		// Token: 0x040002CF RID: 719
		private readonly Spell TotemOfWrath = new Spell("Totem of Wrath");

		// Token: 0x040002D0 RID: 720
		private readonly Spell WrathOfAirTotem = new Spell("Wrath of Air Totem");

		// Token: 0x040002D1 RID: 721
		private readonly Spell WindfuryTotem = new Spell("Windfury Totem");

		// Token: 0x040002D2 RID: 722
		private readonly Spell ManaSpringTotem = new Spell("Mana Spring Totem");

		// Token: 0x040002D3 RID: 723
		private readonly Spell HealingStreamTotem = new Spell("Healing Stream Totem");
	}
}
