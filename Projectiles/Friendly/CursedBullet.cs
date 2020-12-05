using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
    public class CursedBullet : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedBullet;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Bullet");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 2;
            projectile.CloneDefaults(ProjectileID.CursedBullet);
            aiType = ProjectileID.CursedBullet;
            projectile.timeLeft = 300;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(1))
            {
                target.AddBuff(BuffID.CursedInferno, 360, false);
            }
        }
        public override void AI()
        {
            if (projectile.timeLeft <= 51)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
    }
}
