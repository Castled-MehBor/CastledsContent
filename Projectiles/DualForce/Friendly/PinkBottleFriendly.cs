using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class PinkBottleFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Flask");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 18;
            projectile.CloneDefaults(ProjectileID.ToxicFlask);
            aiType = ProjectileID.ToxicFlask;
            projectile.timeLeft = 1200;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, -3f, mod.ProjectileType("PinkEssenceFriendly"), (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, -3f, mod.ProjectileType("PinkEssenceFriendly"), (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -4f, mod.ProjectileType("PinkEssenceFriendly"), (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
        }
    }
}