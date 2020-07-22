using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Placeable.MusicBox
{
	public class MusicBox1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Trivial Equality)");
			Tooltip.SetDefault("OST (Original Sound Track)"
			+ "\nComposed by: Castled (Mod Creator)");
		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("MusicBoxEquality");
			item.width = 32;
			item.height = 22;
			item.rare = ItemRarityID.LightRed;
			item.value = 100000;
			item.accessory = true;
		}
	}
}