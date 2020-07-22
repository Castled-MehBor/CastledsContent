using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class DeathLaserFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 60;
            projectile.CloneDefaults(ProjectileID.PurpleLaser);
            aiType = ProjectileID.PurpleLaser;
            projectile.timeLeft = 600;
            projectile.penetrate = 75;
            projectile.light = 0.8f;
        }
    }
}