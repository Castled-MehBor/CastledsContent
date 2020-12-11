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
            Main.projFrames[projectile.type] = 6;
            DisplayName.SetDefault("Monsoon Projectile");
        }

        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.RainCloudRaining);
            aiType = ProjectileID.RainCloudRaining;
        }
    }
}