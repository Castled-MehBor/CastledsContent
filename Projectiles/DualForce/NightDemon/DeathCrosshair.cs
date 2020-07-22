using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class DeathCrosshair : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Crosshair");
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.light = 0.75f;
            projectile.hostile = true;
            projectile.timeLeft = 180;
            projectile.alpha = 35;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.damage = 25;
            projectile.extraUpdates = 1;
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter < 45)
            {
                projectile.frame++;
            }
            if (projectile.frameCounter < 90)
            {
                projectile.frame++;
            }
            if (projectile.frameCounter < 135)
            {
                projectile.frame++;
            }
            if (projectile.frameCounter < 180)
            {
                projectile.frame++;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(39) == 0)
            {
                Main.PlaySound(SoundID.Item14);
            }
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DeathPop"), projectile.damage = 20, 1.5f, projectile.owner, 0f, 0f);
        }
    }
}