using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class HallowPellet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pellet of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 16;
            projectile.light = 0.45f;
            projectile.hostile = true;
            projectile.timeLeft = 900;
            projectile.tileCollide = true;
            projectile.CloneDefaults(ProjectileID.BulletSnowman);
            aiType = ProjectileID.BulletSnowman;
        }
        public override void AI()
        {
            if (Main.rand.Next(6) == 0)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, default(Color), 1f);
                Dust val = Main.dust[num];
                val = Main.dust[num];
                Dust val2 = val;
                val2.velocity *= 0.2f;
                Main.dust[num].noGravity = true;
            }
        }
    }
}