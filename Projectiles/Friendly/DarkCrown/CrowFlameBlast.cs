using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.DarkCrown
{
    public class CrowFlameBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Solution");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
        }
        public override void AI()
        {

            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("DarkFlame2"), 0f, 0f, 100, default(Color), 1f);
            Dust val = Main.dust[num];
            val = Main.dust[num];
            Dust val2 = val;
            val2.velocity *= 0.2f;
            Main.dust[num].noGravity = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 1.1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * 1.1f;
            }
            return false;
        }
    }
}