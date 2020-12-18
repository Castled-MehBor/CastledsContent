using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.Projectiles.Friendly
{
    public class RedBloodCell : ModProjectile
    {
        public Vector2 vel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Blood Cell");
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.timeLeft = 600;
            projectile.friendly = true;
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                vel = projectile.velocity;
                projectile.ai[1]++;
            }
            projectile.velocity *= 0.97f;
            if (projectile.velocity.X < vel.X / 10 && projectile.velocity.Y < vel.Y / 10 && projectile.velocity.X > vel.X / 10 && projectile.velocity.Y > vel.Y / 10)
                projectile.velocity = Vector2.Zero;
        }
    }
}