// Tag #1: Skyware Boss
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Placeable
{
	public class SkywareArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skyware Artifact");
			Tooltip.SetDefault("Found in Sky Islands and Sky Crates"
			+ "\nWhen placed in space, nearby players will fall quicker."
			+ "\nAfter Queen Bee has been defeated, you can use three feathers to summon an ancient being.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.maxStack = 3;
			item.rare = ItemRarityID.Orange;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = mod.TileType("SkywareArtifactT");
		}
	}
}