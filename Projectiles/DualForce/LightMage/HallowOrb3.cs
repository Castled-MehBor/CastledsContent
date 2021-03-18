using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class HallowOrb3 : ModProjectile
    {
        bool warning = false;
        int rotational = Main.rand.Next(-45, 45);
        readonly double[] rotation = new double[] { 0, 45, 90, 135, 180, 225, 270, 315 };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 75;
            projectile.light = 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (!warning)
            {
                for (int a = 0; a < 8; a++)
                    Projectile.NewProjectile(projectile.Center, new Vector2(0, -12).RotatedBy(rotation[a] + rotational, default), ModContent.ProjectileType<WarningProj>(), 0, 0, projectile.owner, 0f, 0f);
                warning = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_WitherBeastDeath);
            for (int a = 0; a < 8; a++)
                Projectile.NewProjectile(projectile.Center, new Vector2(0, -12).RotatedBy(rotation[a] + rotational, default), ModContent.ProjectileType<LightSpark>(), 12, 3, projectile.owner, 0f, 0f);
        }
    }
}