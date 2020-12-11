using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
    public class RainCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monsoon Projectile");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.RainCloudRaining);
            aiType = ProjectileID.RainCloudRaining;
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(Main.rand.Next(5) - 2.5f, Main.rand.Next(5) - 2.5f), ProjectileID.RainCloudRaining, projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}