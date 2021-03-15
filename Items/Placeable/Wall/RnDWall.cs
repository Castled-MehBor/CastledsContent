using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace CastledsContent.Items.Placeable.Wall
{
	public class RnDWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusted Iron Wall");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = ModContent.WallType<Walls.DnRWall>();
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
	}
}