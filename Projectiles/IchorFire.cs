using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class IchorFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Fire");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.magic = true;
            projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
            aiType = ProjectileID.CursedFlameHostile;
            projectile.timeLeft = 420;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(1))
            {
                target.AddBuff(BuffID.Ichor, 360, false);
                target.AddBuff(BuffID.CursedInferno, 60, false);
            }
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 55, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

    }
}