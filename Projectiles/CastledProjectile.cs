using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CastledsContent.Items.Weapons.Melee;
using Microsoft.Xna.Framework.Graphics;

namespace CastledsContent.Projectiles
{
	public class CastledProjectile : GlobalProjectile
	{
        private int timer;
        private int rand;
        public override bool InstancePerEntity
        {
            get { return true; }
        }
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.WoodenArrowFriendly)
            {
                Player player = Main.player[projectile.owner];

                if (player.GetModPlayer<CastledPlayer>().aimBot)
                {
                    rand = Main.rand.Next(60, 140);
                }
            }
        }
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.WoodenArrowFriendly)
            {
                Player player = Main.player[projectile.owner];

                if (timer >= rand)
                {
                    if (player.GetModPlayer<CastledPlayer>().aimBot && !player.GetModPlayer<CastledPlayer>().restrictAimbot)
                    {
                        #region Vanilla Chlorophyte Bullet AI
                        for (int i = 0; i < 200; i++)
                        {
                            NPC target = Main.npc[i];
                            if (!target.friendly)
                            {
                                float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                                float shootToY = target.position.Y - projectile.Center.Y + (target.height / 2);
                                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                                if ((distance < Main.screenWidth / 2 || distance < Main.screenHeight / 2) && !target.friendly && target.active)
                                {
                                    distance = 3f / distance;

                                    shootToX *= distance * 5;
                                    shootToY *= distance * 5;
                                }
                            }
                        }

                        float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
                        float num133 = projectile.localAI[0];
                        if (num133 == 0f)
                        {
                            projectile.localAI[0] = num132;
                            num133 = num132;
                        }
                        float num134 = projectile.position.X;
                        float num135 = projectile.position.Y;
                        float num136 = 300f;
                        bool flag3 = false;
                        int num137 = 0;
                        if (projectile.ai[1] == 0f)
                        {
                            for (int num138 = 0; num138 < 200; num138++)
                            {
                                if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
                                {
                                    float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
                                    float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                                    float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
                                    if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                                    {
                                        num136 = num141;
                                        num134 = num139;
                                        num135 = num140;
                                        flag3 = true;
                                        num137 = num138;
                                    }
                                }
                            }
                            if (flag3)
                            {
                                projectile.ai[1] = (float)(num137 + 1);
                            }
                            flag3 = false;
                        }
                        if (projectile.ai[1] > 0f)
                        {
                            int num142 = (int)(projectile.ai[1] - 1f);
                            if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                            {
                                float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                                float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                                if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 10000f)
                                {
                                    flag3 = true;
                                    num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                                    num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                                }
                            }
                            else
                            {
                                projectile.ai[1] = 0f;
                            }
                        }
                        if (!projectile.friendly)
                        {
                            flag3 = false;
                        }
                        if (flag3)
                        {
                            float num145 = num133;
                            Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                            float num146 = num134 - vector10.X;
                            float num147 = num135 - vector10.Y;
                            float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                            num148 = num145 / num148;
                            num146 *= num148;
                            num147 *= num148;
                            int num149 = 8;
                            projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                            projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
                        }
                        #endregion
                    }
                }
                if (timer > 238)
                {
                    timer = 0;
                }
                timer++;
            }
        }
    }
}
