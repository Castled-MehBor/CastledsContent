using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class HallowOrb3Friendly : ModProjectile
    {
        readonly double[] rotation = new double[] { 0, 90, 180, 270 };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 100;
            projectile.timeLeft = 25;
            projectile.light = 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public override void Kill(int timeLeft)
        {
            int rotational = Main.rand.Next(-90, 90);
            Main.PlaySound(SoundID.DD2_WitherBeastDeath, projectile.position);
            for (int a = 0; a < 4; a++)
                Projectile.NewProjectile(projectile.Center, new Vector2(0, -9).RotatedBy(rotation[a] + rotational, default), ModContent.ProjectileType<LightSparkFriendly>(), (int)(projectile.damage * 1.5), 1.5f, projectile.owner, 0f, 0f);
        }
    }
}