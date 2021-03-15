using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Summoner.Lunatic
{
	[AutoloadEquip(EquipType.Legs)]
	public class LShoes : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Shoes");
			Tooltip.SetDefault("4% increased Minion Damage");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.defense = 1;
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
			player.minionDamage += 0.04f;
		}
	}
}