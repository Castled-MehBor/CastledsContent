using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class FlaskIchorFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottled Ichor");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 22;
            projectile.CloneDefaults(ProjectileID.ToxicFlask);
            aiType = ProjectileID.ToxicFlask;
            projectile.timeLeft = 1200;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 10f, -10f, ProjectileID.GoldenShowerFriendly, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -10f, -10f, ProjectileID.GoldenShowerFriendly, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 12f, ProjectileID.GoldenShowerFriendly, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
        }
    }
}