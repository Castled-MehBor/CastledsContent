using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class LGauntlet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fists of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 10;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public override void Kill(int timeLeft)
        {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8f, 0f, mod.ProjectileType("GauntletofLeft"), (int)((double)projectile.damage * 1), 3f, projectile.owner, 0f, 0f);
        }
    }
}