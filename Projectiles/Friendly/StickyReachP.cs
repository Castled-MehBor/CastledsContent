using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
    public class StickyReachP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Grasp of the Slime King");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 40;
            projectile.friendly = true;
            projectile.penetrate = 999;
            projectile.ignoreWater = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(1))
            {
                target.AddBuff(BuffID.Slimed, 360, false);
                target.AddBuff(BuffID.Slow, 360, false);
            }
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 41, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

    }
}