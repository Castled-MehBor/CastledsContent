using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class HallowBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shard of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 30;
            projectile.light = 0.95f;
            projectile.hostile = true;
            projectile.timeLeft = 900;
            projectile.CloneDefaults(ProjectileID.BulletSnowman);
            aiType = ProjectileID.BulletSnowman;
        }
        public override void AI()
        {
            if (Main.rand.Next(6) == 0)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, default(Color), 1f);
                Dust val = Main.dust[num];
                val = Main.dust[num];
                Dust val2 = val;
                val2.velocity *= 0.2f;
                Main.dust[num].noGravity = true;
            }
        }
            public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 1.25f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -1.25f;
            }
            return false;
        }
    }
}