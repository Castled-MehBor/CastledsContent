using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace CastledsContent.Tiles.Trap
{
    public class GoldOreTrap : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            dustType = DustID.t_Golden;
            soundType = SoundID.Tink;
            soundStyle = 2;
            minPick = 0;
            AddMapEntry(new Color(165, 165, 165));
            drop = ModContent.ItemType<Items.Placeable.Trap.GoldTrap>();
        }
        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }
        public override void RandomUpdate(int i, int j)
        {
            Main.PlaySound(SoundID.DD2_KoboldIgnite, i, j);
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
            {
                Main.NewText("bruj");
				Main.PlaySound(SoundID.Item15, i, j);
                Projectile.NewProjectileDirect(new Vector2 (i, j), new Vector2 (0, 0), ProjectileID.Explosives, 250, 1f, Main.myPlayer);
			}
        }
    }
}