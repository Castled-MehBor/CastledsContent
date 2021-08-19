using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
    public class SpikeBall : ModProjectile
    {
        public int bounce;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lead Spike Ball");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.aiStyle = 14;
            projectile.friendly = true;
            projectile.penetrate = 10;
            projectile.timeLeft = 1800;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.SpikyBall;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.75f;
            }
            Main.PlaySound(SoundID.NPCHit42, projectile.position);
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i));
                Dust dust = Dust.NewDustPerfect(position, DustID.Lead);
            }
            bounce++;
            return false;
        }
        public override void AI()
        {
            if (bounce > 5)
            {
                projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i));
                Dust dust = Dust.NewDustPerfect(position, DustID.Lead);
            }
            Main.PlaySound(SoundID.Tink, projectile.position);
            Main.PlaySound(SoundID.NPCHit42, projectile.position);
        }
    }
}