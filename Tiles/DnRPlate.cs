using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Tiles
{
    public class DnRPlate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            dustType = 8;
            soundType = SoundID.Tink;
            soundStyle = 2;
            minPick = 25;
            AddMapEntry(new Color(48, 32, 28));
            drop = ItemType<Items.Placeable.Block.RnDPlate>();
        }

        public override bool CanExplode(int i, int j)
        {
            return true;
        }
    }
}