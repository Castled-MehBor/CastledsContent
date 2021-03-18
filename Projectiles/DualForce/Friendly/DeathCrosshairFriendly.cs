using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class DeathCrosshairFriendly : ModProjectile
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
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.timeLeft = 180;
            projectile.alpha = 35;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter < 45)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 3;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(39) == 0)
                Main.PlaySound(SoundID.Item14.WithVolume(0.75f), projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DeathPopFriendly"), projectile.damage = 50, 1.5f, projectile.owner, 0f, 0f);
        }
    }
}