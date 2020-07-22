using Microsoft.Xna.Framework;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class CrystalSpear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 100;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.velocity.X = 10f;
            projectile.velocity.Y = 10f;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.JavelinHostile);
            aiType = ProjectileID.JavelinHostile;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 58, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f);

            Timer++;
            if (Timer > 45)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 0f, mod.ProjectileType("LightSpark"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 0f, mod.ProjectileType("LightSpark"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                Timer = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
        }
    }
}