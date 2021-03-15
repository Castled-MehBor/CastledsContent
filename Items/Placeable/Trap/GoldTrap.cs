using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace CastledsContent.Items.Placeable.Trap
{
    public class GoldTrap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Trap");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 3;
            item.useTime = 2;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.consumable = false;
            item.createTile = ModContent.TileType<Tiles.Trap.GoldOreTrap>();
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