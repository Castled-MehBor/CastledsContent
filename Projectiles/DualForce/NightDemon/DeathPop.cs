using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class DeathPop : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Impact");
        }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.aiStyle = -1;
            projectile.light = 1.5f;
            projectile.hostile = true;
            projectile.timeLeft = 40;
            projectile.alpha = 35;
            projectile.damage = 45;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter < 10)
            {
                projectile.frame++;
                projectile.light = 1f;
            }
            if (projectile.frameCounter < 20)
            {
                projectile.frame++;
                projectile.light = 0.75f;
            }
            if (projectile.frameCounter < 30)
            {
                projectile.frame++;
                projectile.light = 0.5f;
            }
            if (projectile.frameCounter < 40)
            {
                projectile.frame++;
                projectile.light = 0.25f;
            }
        }
    }
}