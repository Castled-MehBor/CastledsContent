﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace CastledsContent.Items.Armor.Summoner.HavocWasp
{
	[AutoloadEquip(EquipType.Body)]
	public class HWBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havoc Wasp's Torso");
			Tooltip.SetDefault("+1 Max Minions"
				+ "\n18% increased minion damage'");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.defense = 8;
		}
		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
		{
			foreach (TooltipLine item2 in tooltips)
			{
				if (item2.mod == "Terraria" && item2.Name == "ItemName")
				{
					item2.overrideColor = new Microsoft.Xna.Framework.Color(Main.DiscoR + 35, 35, 60);
				}
			}
			int num = -1;
			int num2 = 0;
			while (num2 < tooltips.Count)
			{
				if (!tooltips[num2].Name.Equals("ItemName"))
				{
					num2++;
					continue;
				}
				num = num2;
				break;
			}
			tooltips.Insert(num + 1, new TooltipLine(mod, "UnfinishedTooltip", "[c/ff0000:Unfinished]"));
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.18f;
			player.maxMinions++;
		}
	}
}