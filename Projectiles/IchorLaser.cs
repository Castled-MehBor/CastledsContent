using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class IchorLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 60;
            projectile.magic = true;
            projectile.CloneDefaults(ProjectileID.DeathLaser);
            aiType = ProjectileID.DeathLaser;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(1))
            {
                target.AddBuff(BuffID.Ichor, 600, false);
            }
        }

        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 55, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }

    }
}