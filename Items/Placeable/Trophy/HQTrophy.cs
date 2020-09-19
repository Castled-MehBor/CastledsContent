using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Placeable.Trophy
{
    public class HQTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Queen Trophy");

        }
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 50000;
            item.createTile = mod.TileType("HQTrophyT");
            item.placeStyle = 0;
            item.rare = ItemRarityID.Blue;
        }
    }
}
