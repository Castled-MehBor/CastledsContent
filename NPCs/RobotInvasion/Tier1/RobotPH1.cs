using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class RobotPH1 : ModNPC
    {
        public int laserDelay = 0;
        public bool hasEvaded = false;
        public bool isAttacking = false;
        public int runTimer = 0;
        public int motionBlurCounter = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evader");
            Main.npcFrameCount[npc.type] = 11;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 80;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.damage = 35;
            npc.height = 50;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.netAlways = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (numPlayers > 1)
            {
                double num = 0.35;
                double num2 = 1.0 + num;
                for (int i = 2; i < numPlayers; i++)
                {
                    num += (1.0 - num) / 3.0;
                    num2 += num;
                }
                npc.lifeMax = (int)(100.0 * num2);
                npc.damage = (int)(npc.damage * 0.5f);
            }
            else
            {
                npc.lifeMax = 100;
                npc.damage = (int)(npc.damage * 0.5f);
            }
        }
        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType("CleanupShip_PH")) && CastledWorld.numberOfEnemies > 0)
            {
                Main.PlaySound(SoundID.Grab, (int)npc.position.X, (int)npc.position.Y, 17);
                CastledWorld.numberOfEnemies--;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, -2f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
            }
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < npc.lifeMax * 0.35)
            {
                Main.PlaySound(SoundID.NPCHit53);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Cyan, 3f);
            }
        }
        public override void AI()
        {
            {
                npc.ai[0]++;
                Player P = Main.player[npc.target];
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    npc.TargetClosest(false);
                    P = Main.player[npc.target];
                    if (!P.active || P.dead)
                    {
                        npc.velocity = new Vector2(0f, -500f);
                        npc.active = false;
                    }
                }
                npc.netUpdate = true;
                {
                    Timer++;
                    if (Timer > 120 && hasEvaded == false)
                    {
                        npc.position.X -= 7;
                        npc.noGravity = true;
                        npc.noTileCollide = true;
                        motionBlurCounter++;
                        if (motionBlurCounter > 4)
                        {
                            motionBlurCounter = 0;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("EvaderRightBlur"), 0, 0f, 255, 0f, 0f);
                        }
                    }
                    if (Timer > 360)
                    {
                        hasEvaded = true;
                        npc.noGravity = false;
                        npc.noTileCollide = false;
                    }
                    if (Timer > 800 && Timer < 850)
                    {
                        npc.position.Y -= 20;
                    }
                    if (Timer > 850)
                    {
                        npc.velocity.Y += 1;
                        laserDelay++;
                        if (laserDelay > 10)
                        {
                            Main.PlaySound(SoundID.Item12);
                            float Speed = 3f;
                            Vector2 vector8 = npc.BottomRight;
                            int damage = 10;
                            int type = mod.ProjectileType("EvaderLaser");
                            float rotation = npc.rotation + 135;
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y + 18, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                            laserDelay = 0;
                        }
                    }
                    if (Timer > 950)
                    {
                        Timer = 0;
                    }
                }
            }
        }
        private const int Frame_Fall = 0;
        private const int Frame_Run1 = 1;
        private const int Frame_Run2 = 2;
        private const int Frame_Run3 = 3;
        private const int Frame_Run4 = 4;
        private const int Frame_Run5 = 5;
        private const int Frame_Run6 = 6;
        private const int Frame_Pew1 = 7;
        private const int Frame_Pew2 = 8;
        private const int Frame_Pew3 = 9;
        private const int Frame_Pew4 = 10;
        public override void FindFrame(int frameHeight)
        {
            if (Timer < 120 && hasEvaded == false)
            {
                npc.frame.Y = Frame_Fall * frameHeight;
            }
            else if (Timer < 360 && hasEvaded == false)
            {
                runTimer++;
                if (runTimer < 10)
                {
                    npc.frame.Y = Frame_Run1 * frameHeight;
                }
                else if (runTimer < 20)
                {
                    npc.frame.Y = Frame_Run2 * frameHeight;
                }
                else if (runTimer < 30)
                {
                    npc.frame.Y = Frame_Run3 * frameHeight;
                }
                else if (runTimer < 40)
                {
                    npc.frame.Y = Frame_Run4 * frameHeight;
                }
                else if (runTimer < 50)
                {
                    npc.frame.Y = Frame_Run5 * frameHeight;
                }
                else if (runTimer < 60)
                {
                    npc.frame.Y = Frame_Run6 * frameHeight;
                }
                else
                {
                    runTimer = 0;
                }
            }
            else if (Timer < 850)
            {
                npc.frame.Y = Frame_Pew1 * frameHeight;
            }
            else if (Timer < 950)
            {
                if (laserDelay < 5)
                {
                    npc.frame.Y = Frame_Pew2 * frameHeight;
                }
                else if (laserDelay < 10)
                {
                    npc.frame.Y = Frame_Pew3 * frameHeight;
                }
                else if (laserDelay < 15)
                {
                    npc.frame.Y = Frame_Pew4 * frameHeight;
                }
            }
            else
            {
                npc.frame.Y = Frame_Pew1 * frameHeight;
                Timer = 0;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}