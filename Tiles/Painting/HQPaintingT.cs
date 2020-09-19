using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CastledsContent.Tiles.Painting
{
	public class HQPaintingT : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 6;
			TileObjectData.newTile.CoordinateHeights = (new int[4]
			{
			16,
			16,
			16,
			16
			});
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			ModTranslation val = CreateMapEntryName();
			val.SetDefault("Painting");
			this.AddMapEntry(new Color(90, 50, 30), val);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("HQPainting"), 1, false, 0, false, false);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 0;
		}
	}
}
