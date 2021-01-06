using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.Projectiles.Friendly
{
    public class RedBloodCell : ModProjectile
    {
        public Vector2 vel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Blood Cell");
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.alpha = 50;
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                vel = projectile.velocity;
                projectile.ai[1]++;
            }
            projectile.velocity *= 0.97f;
            if (projectile.velocity.X < vel.X / 10 && projectile.velocity.Y < vel.Y / 10 && projectile.velocity.X > vel.X / 10 && projectile.velocity.Y > vel.Y / 10)
                projectile.velocity = Vector2.Zero;
            DelegateMethods.v3_1 = Color.Red.ToVector3();
            Utils.PlotTileLine(projectile.Center, projectile.Center  + new Vector2(1, 1), projectile.Size.Length() * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }
        public override void Kill(int timeLeft)
        {
            int type = DustID.SomethingRed;
            int amount = 16;
            for (int i = 0; i <= amount; i++)
            {
                double spread = (MathHelper.Pi * 2) / amount;
                Vector2 vel = new Vector2(5f, 0).RotatedBy(spread * i);
                int d = Dust.NewDust(projectile.Center, 4, 4, type, vel.X, vel.Y, 0, Color.Red);
                Main.dust[d].noGravity = true;
            }
            Main.PlaySound(SoundID.NPCHit19, projectile.Center);
        }
    }
}