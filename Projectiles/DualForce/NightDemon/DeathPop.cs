using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class DeathPop : ModProjectile
    {
        int rotation = Main.rand.Next(-5, 5);
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
            projectile.scale = 1.5f;
            projectile.hostile = true;
            projectile.timeLeft = 40;
            projectile.alpha = 35;
            projectile.damage = 45;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] > 5)
            {
                projectile.rotation += rotation;
                projectile.scale -= 0.35f;
                projectile.light -= 0.25f;
                projectile.alpha += 15;
                projectile.ai[0] = 0;
            }
        }
    }
}