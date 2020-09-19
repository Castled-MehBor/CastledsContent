// Tag #1: Skyware Boss
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Placeable.Painting
{
	public class HQPainting : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The False Angel");
			Tooltip.SetDefault("'M. Bar'");
		}

		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 34;
			item.maxStack = 99;
			item.rare = ItemRarityID.White;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.value = 5000;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = mod.TileType("HQPaintingT");
		}
	}
}