using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class TackleP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Fury of Cthulhu's Eye");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.timeLeft = 65;
            projectile.friendly = true;
            projectile.penetrate = 15;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 235, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

    }
}