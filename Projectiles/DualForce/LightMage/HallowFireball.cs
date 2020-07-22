using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class HallowFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.DD2BetsyFireball);
            aiType = ProjectileID.DD2BetsyFireball;
        }

        public override void AI()
        {
            if (Main.rand.Next(9) == 0)
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