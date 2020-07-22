using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class BoneTalon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Talon");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.CloneDefaults(ProjectileID.JavelinHostile);
            aiType = ProjectileID.JavelinHostile;
        }

    }
}