using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class CleanupShip_PH : ModNPC
    {
        public bool swoopIn = false;
        public bool waveHasBegun = false;
        public bool dispatchWave = false;
        public bool awaitingWaveTerminate = false;
        public bool hasLeft = false;
        public bool nextWaveCountdown = false;
        public bool startWave = false;
        public bool eventComplete = false;
        public int wave = 1;
        public int waveDelay;
        public int deployDelay;
        public int deployAmount;
        public int waveDelay2;
        public int motionBlurCounter = 0;
        //public bool terminateAnimation = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Aircraft");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 50;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 210;
            npc.height = 96;
            npc.alpha = 100;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -60f);
                    npc.active = false;
                }
            }
            npc.netUpdate = true;
            {
                //af4bff or R175 G75 B255 is the purple text color.
                Timer++;
                if (swoopIn == false)
                {
                    if (Timer < 60)
                    {
                        npc.position.Y = Main.player[npc.target].position.Y - 900;
                    }
                    else if (Timer > 60)
                    {
                        npc.alpha = 0;
                        motionBlurCounter++;
                        if (motionBlurCounter > 4)
                        {
                            motionBlurCounter = 0;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                        }
                        npc.position.Y += 10;
                        if (Timer > 180)
                        {
                            swoopIn = true;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipPulse"), 0, 0f, 255, 0f, 0f);
                            Timer = 0;
                            CastledWorld.numberOfEnemies = 0;
                            CastledWorld.invasionPoints = 0;
                            CastledWorld.counterType = 1;
                        }
                    }
                }
                if (swoopIn == true)
                {
                    if (wave == 1)
                    {
                        waveDelay++;
                        if (waveDelay > 120 && waveDelay2 < 3 && waveHasBegun == false)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipPulse"), 0, 0f, 255, 0f, 0f);
                            waveDelay = 0;
                            waveDelay2++;
                            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/RobotInvasionTheme");
                        }
                        if (waveDelay2 > 2 && waveHasBegun == false)
                        {
                            Main.NewText("[c/af4bff:First Wave: 4 Simple Robots]");
                            Main.NewText("[c/af4bff:Total: 4 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                    }
                    if (startWave == true && waveHasBegun == false)
                    {
                        if (wave == 2)
                        {
                            Main.NewText("[c/af4bff:Wave 2: 4 Simple Robots, and 2 Evaders]");
                            Main.NewText("[c/af4bff:Total: 6 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                        if (wave == 3)
                        {
                            Main.NewText("[c/af4bff:Wave 3: 3 Simple Robots, 2 Evaders and 2 Automated Pistions]");
                            Main.NewText("[c/af4bff:Total: 7 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                        if (wave == 4)
                        {
                            Main.NewText("[c/af4bff:Wave 4: 4 Simple Robots, 2 Evaders and 4 Automated Pistions]");
                            Main.NewText("[c/af4bff:Total: 10 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                        if (wave == 5)
                        {
                            Main.NewText("[c/af4bff:Final Wave: BladeBot]");
                            Main.NewText("[c/af4bff:Total: 1 Miniboss]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                    }
                    if (awaitingWaveTerminate == true)
                    {
                        if (wave < 5)
                        {
                            if (CastledWorld.numberOfEnemies == 0)
                            {
                                Main.NewText("[c/af4bff:Wave Complete!]");
                                awaitingWaveTerminate = false;
                                CastledWorld.counterType = 2;
                                CastledWorld.waveDelayCountdown = 10;
                                nextWaveCountdown = true;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
                                Timer = 0;
                            }
                        }
                        else
                        {
                            if (wave == 5)
                            {
                                if (CastledWorld.numberOfEnemies == 0)
                                {
                                    eventComplete = true;
                                    awaitingWaveTerminate = false;
                                    nextWaveCountdown = true;
                                    Timer = 0;
                                }
                            }
                        }
                    }
                }
                if (dispatchWave == true)
                {
                    if (wave == 1)
                    {
                        deployDelay++;
                        if (deployDelay > 15 && deployAmount < 4)
                        {
                            CastledWorld.numberOfEnemies++;
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (deployAmount > 3)
                        {
                            deployDelay = 0;
                            deployAmount = 0;
                            dispatchWave = false;
                            awaitingWaveTerminate = true;
                        }
                    }
                    if (wave == 2)
                    {
                        deployDelay++;
                        if (deployDelay > 35 && deployAmount < 4)
                        {
                            CastledWorld.numberOfEnemies++;
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (CastledWorld.numberOfEnemies == 4)
                        {
                            CastledWorld.numberOfEnemies += 2;
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH1"));
                        }
                        if (CastledWorld.numberOfEnemies > 5)
                        {
                            deployDelay = 0;
                            deployAmount = 0;
                            dispatchWave = false;
                            awaitingWaveTerminate = true;
                        }
                    }
                    if (wave == 3)
                    {
                        deployDelay++;
                        if (deployDelay > 35 && deployAmount < 3)
                        {
                            CastledWorld.numberOfEnemies++;
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (CastledWorld.numberOfEnemies == 3)
                        {
                            if (deployDelay > 70)
                            {
                                CastledWorld.numberOfEnemies += 2;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH"));
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH1"));
                            }
                        }
                        if (CastledWorld.numberOfEnemies == 5)
                        {
                            if (deployDelay > 150)
                            {
                                CastledWorld.numberOfEnemies += 2;

                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH1"));
                            }
                        }
                        if (CastledWorld.numberOfEnemies > 5)
                        {
                            deployDelay = 0;
                            deployAmount = 0;
                            dispatchWave = false;
                            awaitingWaveTerminate = true;
                        }
                    }
                    if (wave == 4)
                    {
                        deployDelay++;
                        if (deployDelay > 180 && deployAmount < 6)
                        {
                            CastledWorld.numberOfEnemies += 6;

                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH"));
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("RobotPH1"));
                            deployDelay = 0;
                            deployAmount += 6;
                        }
                        if (CastledWorld.numberOfEnemies == 6)
                        {
                            if (deployDelay > 120)
                            {
                                CastledWorld.numberOfEnemies += 2;

                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH2"));
                            }
                        }
                        if (CastledWorld.numberOfEnemies == 8)
                        {
                            if (deployDelay > 360)
                            {
                                CastledWorld.numberOfEnemies += 2;

                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("PistonPH2"));
                            }
                        }
                        if (CastledWorld.numberOfEnemies > 8)
                        {
                            deployDelay = 0;
                            deployAmount = 0;
                            dispatchWave = false;
                            awaitingWaveTerminate = true;
                        }
                    }
                    if (wave == 5)
                    {
                        deployDelay++;
                        if (deployDelay == 120 && deployAmount < 10)
                        {
                            Main.PlaySound(SoundID.DD2_KoboldIgnite);
                        }
                        if (deployDelay > 240 && deployAmount < 10)
                        {
                            CastledWorld.numberOfEnemies += 10;
                            Main.PlaySound(SoundID.Item61);
                            NPC.NewNPC((int)npc.position.X + 105, (int)npc.position.Y + 70, mod.NPCType("BladeBot"));
                            deployDelay = 0;
                            deployAmount += 10;
                        }
                        if (CastledWorld.numberOfEnemies > 9)
                        {
                            deployDelay = 0;
                            deployAmount = 0;
                            dispatchWave = false;
                            awaitingWaveTerminate = true;
                        }
                    }
                }
                if (nextWaveCountdown == true)
                {
                    if (eventComplete == false)
                    {
                        if (Timer > 120)
                        {
                            Timer = 0;
                            CastledWorld.waveDelayCountdown--;
                        }
                        if (CastledWorld.waveDelayCountdown == 0)
                        {
                            CastledWorld.counterType = 1;
                            startWave = true;
                            waveHasBegun = false;
                            nextWaveCountdown = false;
                            wave++;
                            Timer = 0;
                        }
                    }
                    if (eventComplete == true)
                    {
                        if (Timer < 60)
                        {
                            motionBlurCounter++;
                            if (motionBlurCounter > 4)
                            {
                                music = -1;
                                motionBlurCounter = 0;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                            }
                            npc.position.Y += 2;
                        }
                        if (Timer > 60)
                        {
                            motionBlurCounter++;
                            if (motionBlurCounter > 4)
                            {
                                motionBlurCounter = 0;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                            }
                            npc.position.Y -= 12;
                        }
                        if (Timer > 150 && hasLeft == false)
                        {
                            Main.NewText("[c/66ff66:The robots have been defeated!]");
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Identifier1"), 0, 0f, 255, 0f, 0f);
                            hasLeft = true;
                            npc.life = 0;
                        }
                    }
                }
            }
        //private const int Frame_Ship1 = 0;
        /*
        private const int Frame_Ship2 = 1;
        private const int Frame_Ship3 = 2;
        private const int Frame_Ship4 = 3;
        public override void FindFrame(int frameHeight)
        {
            if (waveHasBegun == true)
            {
                if (terminateAnimation == false)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 60)
                    {
                        npc.frame.Y = Frame_Ship1 * frameHeight;
                    }
                    if (npc.frameCounter < 61)
                    {
                        npc.frame.Y = Frame_Ship2 * frameHeight;
                    }
                    else if (npc.frameCounter < 70)
                    {
                        npc.frame.Y = Frame_Ship3 * frameHeight;
                    }
                    else if (npc.frameCounter < 150)
                    {
                        npc.frame.Y = Frame_Ship4 * frameHeight;
                    }
                    else if (npc.frameCounter < 156)
                    {
                        npc.frame.Y = Frame_Ship3 * frameHeight;
                    }
                    else if (npc.frameCounter < 164)
                    {
                        npc.frame.Y = Frame_Ship2 * frameHeight;
                    }
                    else if (npc.frameCounter < 170)
                    {
                        npc.frame.Y = Frame_Ship1 * frameHeight;
                    }
                    if (npc.frameCounter > 170)
                    {
                        terminateAnimation = true;
                    }
                    else
                    {
                        terminateAnimation = true;
                    }
                }
            }*/
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}