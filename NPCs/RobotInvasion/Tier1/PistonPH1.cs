using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class PistonPH1 : ModNPC
    {
        public int pistonUse = 5;
        public int pistonJumpTimer = 0;
        public bool hasBusted = false;
        public int pistonDirection;
        public int motionBlurCounter = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Automated Piston");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 150;
            npc.defense = 10;
            npc.damage = 10;
            npc.knockBackResist = 0f;
            npc.width = 28;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            pistonDirection = CastledWorld.leftOrRight;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < npc.lifeMax * 0.35 && hasBusted == false)
            {
                Main.PlaySound(SoundID.NPCHit53);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Cyan, 3f);
            }
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
                npc.lifeMax = (int)(200.0 * num2);
                npc.damage = (int)(npc.damage * 0.5f);
            }
            else
            {
                npc.lifeMax = 200;
                npc.damage = (int)(npc.damage * 0.5f);
            }
        }

        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
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
        public override void AI()
        {
            Timer++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || P.dead || !P.active)
            {
                npc.TargetClosest(false);
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -500f);
                    npc.active = false;
                }
            }
            npc.netUpdate = true;
            {
                {
                    Timer++;
                    if (pistonUse > 0)
                    {
                        npc.damage = 35;
                        if (Timer > 240)
                        {
                            pistonJumpTimer++;
                            if (pistonJumpTimer > 60 && pistonJumpTimer < 120)
                            {
                                npc.position.Y -= 20;
                                npc.position.X += (5 * pistonDirection);
                                motionBlurCounter++;
                                if (motionBlurCounter > 2)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("PistonBlur"), 0, 0f, 255, 0f, 0f);
                                }
                            }
                            else if (pistonJumpTimer > 120 && pistonJumpTimer < 180)
                            {
                                npc.position.Y += 2;
                                motionBlurCounter++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("PistonBlur"), 0, 0f, 255, 0f, 0f);
                                }
                            }
                            else if (pistonJumpTimer > 180)
                            {
                                Timer = 0;
                                pistonJumpTimer = 0;
                                pistonUse--;
                                npc.damage = 10;
                            }
                        }
                    }
                    if (pistonUse == 0 && hasBusted == false)
                    {
                        Main.PlaySound(SoundID.NPCHit53);
                        Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Cyan, 3f);
                        hasBusted = true;
                        npc.damage = 35;
                    }
                }
            }
        }
        private const int Frame_Jump1 = 0;
        private const int Frame_Jump2 = 1;
        private const int Frame_Jump3 = 2;
        private const int Frame_Jump4 = 3;
        private const int Frame_Jump5 = 4;
        private const int Frame_Jump6 = 5;
        private const int Frame_Jump7 = 6;
        private const int Frame_Jump8 = 7;
        public override void FindFrame(int frameHeight)
        {
            if (hasBusted == true)
            {
                npc.frame.Y = Frame_Jump8 * frameHeight;
            }
            if (pistonUse > 0)
            {
                if (Timer < 60)
                {
                    npc.frame.Y = Frame_Jump1 * frameHeight;
                }
                else if (Timer < 120)
                {
                    npc.frame.Y = Frame_Jump2 * frameHeight;
                }
                else if (Timer < 160)
                {
                    npc.frame.Y = Frame_Jump3 * frameHeight;
                }
                else if (Timer < 200)
                {
                    npc.frame.Y = Frame_Jump4 * frameHeight;
                }
                else if (Timer < 240)
                {
                    npc.frame.Y = Frame_Jump4 * frameHeight;
                }
                if (pistonJumpTimer > 60)
                {
                    npc.frame.Y = Frame_Jump4 * frameHeight;
                }
                else if (pistonJumpTimer > 60)
                {
                    npc.frame.Y = Frame_Jump4 * frameHeight;
                }
                //Falling
                if (pistonJumpTimer > 60 && pistonJumpTimer < 180)
                {
                    npc.frame.Y = Frame_Jump5 * frameHeight;
                }
                else if (pistonJumpTimer > 120)
                {
                    npc.frame.Y = Frame_Jump6 * frameHeight;
                }
                else if (pistonJumpTimer > 160)
                {
                    npc.frame.Y = Frame_Jump7 * frameHeight;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}