using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Placeable.Block
{
    public class RnDPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Iron Plate");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 3;
            item.useTime = 2;
            item.useStyle = 1;
            item.rare = 0;
            item.consumable = true;
            item.createTile = TileType<Tiles.DnRPlate>();
        }
    }
}