using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace CastledsContent.Projectiles.Friendly
{
    public class RayGun : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hyper Ray");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 300;
            projectile.penetrate = 6;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.damage = (int)((double)projectile.damage * 0.8);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X += projectile.velocity.X;
                projectile.velocity.X = 0f - projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y += projectile.velocity.Y;
                projectile.velocity.Y = 0f - projectile.velocity.Y;
            }
            return false;
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)

           if (projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = projectile.position;
                    projectilePosition -= projectile.velocity * ((float)i * 0.25f);
                    projectile.alpha = 255;
					int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.CrystalPulse, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = projectilePosition;
					Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[dust].velocity *= 0.2f;
                }
            }
        }
    }
}
