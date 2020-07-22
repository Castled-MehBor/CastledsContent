using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class LightBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.CloneDefaults(ProjectileID.VortexBeaterRocket);
            aiType = ProjectileID.VortexBeaterRocket;
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 15, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

    }
}