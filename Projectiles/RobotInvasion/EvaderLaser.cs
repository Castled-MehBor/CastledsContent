using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion
{
    public class EvaderLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamma Ray Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 30;
            projectile.light = 1.5f;
            projectile.hostile = true;
            projectile.timeLeft = 1200;
            projectile.alpha = 85;
            projectile.CloneDefaults(ProjectileID.PinkLaser);
            aiType = ProjectileID.PinkLaser;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * 1f;
            }
            return false;
        }
    }
}