using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.FX
{
    public class CoolFX1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warning");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.timeLeft = 30;
            projectile.velocity.X = 8f;
            projectile.velocity.Y = 8f;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }
        public override bool PreAI()
        {
            int num = Main.rand.Next(2);
            if (num == 0)
            {
                projectile.velocity.X -= 0.5f;
                projectile.velocity.Y -= 0.5f;
            }
            if (num == 1)
            {
                projectile.velocity.X += 0.5f;
                projectile.velocity.Y += 0.5f;
            }
            return true;
        }
        public override void AI()
        {
            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, Color.Yellow, 1f);
            Dust val = Main.dust[num];
            val = Main.dust[num];
            Dust val2 = val;
            val2.velocity *= 0.2f;
            Main.dust[num].noGravity = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * 1f;
            }
            return false;
        }
    }
}