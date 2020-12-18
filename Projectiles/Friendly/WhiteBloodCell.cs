using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace CastledsContent.Projectiles.Friendly
{
    public class WhiteBloodCell : ModProjectile
    {
        private Vector2 vel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Blood Cell");
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
            projectile.ai[0]++;
            if (projectile.ai[0] > 18)
            {
                Projectile.NewProjectile(projectile.Center, (Vector2.UnitX * 10).RotatedByRandom(Math.PI * 2), ModContent.ProjectileType<Projectiles.Friendly.Antibodies>(), projectile.damage, projectile.knockBack, projectile.owner);
                projectile.ai[0] = 0;
            }
        }
    }
}