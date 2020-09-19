using CastledsContent.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Placeable.MusicBox
{
	public class MusicBox2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Harpy Queen)");
			Tooltip.SetDefault("OST (Original Sound Track)"
			+ "\nComposed by: Akocis");
		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = TileType<MusicBoxFG>();
			item.width = 30;
			item.height = 20;
			item.rare = ItemRarityID.LightRed;
			item.value = 20000;
			item.accessory = true;
		}
	}
}