using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using System.Collections.Generic;

namespace CastledsContent.Items.Armor.Summoner.Lunatic
{
	[AutoloadEquip(EquipType.Head)]
	public class LHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Hat");
			Tooltip.SetDefault("4% increased Minion Damage");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<LShirt>() && legs.type == ItemType<LShoes>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 Defense\nHaving a minion summoned will greatly increase your movement speed";
			player.GetModPlayer<CastledPlayer>().lunatic = true;
			player.statDefense += 1;
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
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
			player.minionDamage += 0.04f;
		}
	}
}