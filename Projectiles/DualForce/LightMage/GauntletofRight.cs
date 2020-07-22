using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class GauntletofRight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gauntlet of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 90;
            projectile.height = 90;
            projectile.light = 0.15f;
            projectile.hostile = true;
            projectile.timeLeft = 600;
            projectile.alpha = 25;
            projectile.scale = 0.15f;
            projectile.CloneDefaults(ProjectileID.BulletSnowman);
            aiType = ProjectileID.BulletSnowman;
        }
        public override void AI()
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, default(Color), 1f);
            projectile.scale += 0.005f;
            projectile.light = projectile.timeLeft / 60;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -2f;
            }
            return false;
        }
    }
}