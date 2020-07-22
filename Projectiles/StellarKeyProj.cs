using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class StellarKeyProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Key");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.CloneDefaults(ProjectileID.FallingStar);
            aiType = ProjectileID.FallingStar;
        }

    }
}