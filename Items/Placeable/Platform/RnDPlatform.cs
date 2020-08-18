using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Placeable.Platform
{
    public class RnDPlatform : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Iron Shelf");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 0;
            item.consumable = true;
            item.createTile = TileType<Tiles.DnRPlatform>();
        }
    }
}