using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
    public class FireSpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Spark");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 1200;
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
                projectile.velocity.X -= 0.35f;
                projectile.velocity.Y -= 0.35f;
            }
            if (num == 1)
            {
                projectile.velocity.X += 0.35f;
                projectile.velocity.Y += 0.35f;
            }
            return true;
        }
        public override void AI()
        {
            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.SolarFlare, 0f, 0f, 100, Color.Yellow, 1f);
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 1200);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 240);
        }
    }
}