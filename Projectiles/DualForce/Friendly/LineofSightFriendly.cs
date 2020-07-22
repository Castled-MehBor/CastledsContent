using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class LineofSightFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Line of Sight");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.penetrate = 999;
            projectile.velocity.X = 8f;
            projectile.velocity.Y = 8f;
        }

        public override void AI()
        {

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DeathCrosshairFriendly"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                Gore.NewGore(projectile.position, projectile.velocity, GoreID.ChimneySmoke3, 0.8f);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 2f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * 2f;
            }
            return false;
        }
    }
}