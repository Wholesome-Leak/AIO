using System;
using System.Collections.Generic;
using robotManager.Helpful;
using wManager.Wow.Helpers;

namespace AIO.Combat.Hunter
{
	// Token: 0x020000DF RID: 223
	internal static class PetHelper
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000202F5 File Offset: 0x0001E4F5
		public static string FoodType
		{
			get
			{
				return Lua.LuaDoString<string>("return GetPetFoodTypes();", "");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00020306 File Offset: 0x0001E506
		public static int Happiness
		{
			get
			{
				return Lua.LuaDoString<int>("happiness, damagePercentage, loyaltyRate = GetPetHappiness() return happiness", "");
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00020318 File Offset: 0x0001E518
		public static void Feed()
		{
			string foodType = PetHelper.FoodType;
			foreach (KeyValuePair<string, List<string>> keyValuePair in PetHelper.Buffet)
			{
				bool flag = !foodType.Contains(keyValuePair.Key);
				if (!flag)
				{
					foreach (string text in keyValuePair.Value)
					{
						bool flag2 = ItemsManager.GetItemCountByNameLUA(text) == 0;
						if (!flag2)
						{
							Lua.LuaDoString("CastSpellByName('Feed Pet')", false);
							Lua.LuaDoString("UseItemByName('" + text + "')", false);
							Logging.WriteFight("[RTF] Feeding hungry Pet");
							return;
						}
					}
				}
			}
		}

		// Token: 0x04000489 RID: 1161
		private static readonly Dictionary<string, List<string>> Buffet = new Dictionary<string, List<string>>
		{
			{
				"Meat",
				new List<string>
				{
					"Tough Jerky",
					"Haunch of Meat",
					"Mutton Chop",
					"Wild Hog Shank",
					"Cured Ham Steak",
					"Roasted Quail",
					"Smoked Talbuk Venison",
					"Clefthoof Ribs",
					"Salted Venison",
					"Mead Basted Caribou",
					"Mystery Meat",
					"Red Wolf Meat"
				}
			},
			{
				"Fungus",
				new List<string>
				{
					"Raw Black Truffle"
				}
			},
			{
				"Fish",
				new List<string>
				{
					"Slitherskin Mackerel",
					"Longjaw Mud Snapper",
					"Bristle Whisker Catfish",
					"Rockscale Cod",
					"Striped Yellowtail",
					"Spinefin Halibut",
					"Sunspring Carp",
					"Zangar Trout",
					"Fillet of Icefin",
					"Poached Emperor Salmon"
				}
			},
			{
				"Fruit",
				new List<string>
				{
					"Shiny Red Apple",
					"Tel'Abim Banana",
					"Snapvine Watermelon",
					"Goldenbark Apple",
					"Heaven Peach",
					"Moon Harvest Pumpkin",
					"Deep Fried Plantains",
					"Skethyl Berries",
					"Telaari Grapes",
					"Tundra Berries",
					"Savory Snowplum"
				}
			},
			{
				"Bread",
				new List<string>
				{
					"Tough Hunk of Bread",
					"Freshly Baked Bread",
					"Moist Cornbread",
					"Mulgore Spice Bread",
					"Soft Banana Bread",
					"Homemade Cherry Pie",
					"Mag'har Grainbread",
					"Crusty Flatbread"
				}
			}
		};
	}
}
