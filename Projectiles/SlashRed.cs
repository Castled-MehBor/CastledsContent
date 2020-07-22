using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class SlashRed : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Slash");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 24;
            projectile.CloneDefaults(ProjectileID.DD2SquireSonicBoom);
            aiType = ProjectileID.DD2SquireSonicBoom;
            projectile.timeLeft = 45;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[255] = 0;
        }
    }
}