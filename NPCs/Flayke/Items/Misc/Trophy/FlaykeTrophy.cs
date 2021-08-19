using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;

namespace CastledsContent.NPCs.Flayke.Items.Misc.Trophy
{
	public class FlaykeTrophy : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flayke Trophy");
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
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<FlaykeTrophyT>();
			item.placeStyle = 0;
		}
	}
    public class FlaykeTrophyT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            dustType = 7;
            AddMapEntry(new Microsoft.Xna.Framework.Color(120, 85, 60));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (frameX == 0)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, ModContent.ItemType<FlaykeTrophy>());
            }
        }
    }
}