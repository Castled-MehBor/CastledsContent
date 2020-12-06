using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace CastledsContent.Projectiles.Friendly
{
    public class Sharkphyte : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharphyte Tooth");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - 1.57f;

            Player player = Main.player[projectile.owner];
            float num4 = projectile.Center.X;
            float num5 = projectile.Center.Y;
            float num6 = 500f;
            bool flag = false;
            int num11;
            for (int num7 = 0; num7 < 200; num7 = num11 + 1)
            {
                if (Main.npc[num7].CanBeChasedBy((object)projectile, false) && projectile.Distance(Main.npc[num7].Center) < num6 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num7].Center, 1, 1))
                {
                    float num8 = Main.npc[num7].position.X + (float)(Main.npc[num7].width / 2);
                    float num9 = Main.npc[num7].position.Y + (float)(Main.npc[num7].height / 2);
                    float num10 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num8) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num9);
                    if (num10 < num6)
                    {
                        num6 = num10;
                        num4 = num8;
                        num5 = num9;
                        flag = true;
                    }
                }
                num11 = num7;
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 2f)
            {
                if (flag)
                {
                    float num16 = 4f;
                    Vector2 val2 = default(Vector2);
                    float num17 = num4 - val2.X;
                    float num18 = num5 - val2.Y;
                    float num19 = (float)Math.Sqrt((double)(num17 * num17 + num18 * num18));
                    num19 = num16 / num19;
                    num17 *= num19;
                    num18 *= num19;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num17) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num18) / 21f;
                }
                projectile.ai[1] = 0f;
            }
            /*
                Player player = Main.player[projectile.owner];
                for (int k = 0; k < 200; k++)
                {
                    NPC target = Main.npc[k].active;
                    float homeInX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                    float homeInY = target.position.Y - projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(homeInX * homeInX + homeInY * homeInY));
                    distance = 3f / distance;
                    homeInX *= distance * 1.5f;
                    homeInY *= distance * 1.5f;
                    if (Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        projectile.velocity.X = homeInX;
                        projectile.velocity.Y = homeInY;
                    }
                }
            }*/
        }
    }
}