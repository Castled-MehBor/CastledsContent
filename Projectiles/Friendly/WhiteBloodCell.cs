using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace CastledsContent.Projectiles.Friendly
{
    public class WhiteBloodCell : ModProjectile
    {
        public Vector2 vel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Blood Cell");
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
            projectile.ai[0]++;
            bool shoot = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (projectile.Distance(Main.npc[i].Center) < 500f && Main.npc[i].active && !Main.npc[i].friendly)
                {
                    shoot = true;
                    break;
                }
            } 
            if (projectile.ai[0] > 20 && shoot)
            {
                Projectile.NewProjectile(projectile.Center, (Vector2.UnitX * 10).RotatedByRandom(Math.PI * 2), ModContent.ProjectileType<Projectiles.Antibodies>(), projectile.damage * (int)0.6, projectile.knockBack, projectile.owner);
                projectile.ai[0] = 0;
            }
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            DelegateMethods.v3_1 = Color.White.ToVector3();
			Utils.PlotTileLine(projectile.Center, projectile.Center + new Vector2(1, 1), 26, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }
        public override void Kill(int timeLeft)
        {
            int type = DustID.Blood;
            int amount = 16;
            for (int i = 0; i <= amount; i++)
            {
                double spread = (Math.PI * 2) / amount;
                Vector2 vel = new Vector2(5f, 0).RotatedBy(spread * i);
                int d = Dust.NewDust(projectile.Center, 4, 4, type, vel.X, vel.Y, 0, Color.White);
                Main.dust[d].noGravity = true;
            }
            Main.PlaySound(SoundID.NPCHit19, projectile.Center);
        }
    }
    }
}