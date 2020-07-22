using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class CursedBullet : ModProjectile
    {
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
            projectile.timeLeft = 60;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(1))
            {
                target.AddBuff(BuffID.CursedInferno, 360, false);
            }
        }

    }
}