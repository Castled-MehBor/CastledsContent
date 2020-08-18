using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    [AutoloadBossHead]
    public class CleanupShip_PH : ModNPC
    {
        //Substandard
        public int radar = 0;
        public bool hasLanded = false;
        public bool setup = false;
        //Here we go
        public bool startWave = false;
        public int wave = 1;
        public bool hasExclaimed = false;
        public bool waveFinished = false;
        public bool hasLeft = false;
        public bool dispatchWave = false;
        public bool waveCurrent = false;
        public bool capsules = false;
        public int capsuleChance = 999;
        public int manualSpawn = 0;
        public int distance = 75;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Aircraft");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 50;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 98;
            npc.height = 120;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/MarchoftheDroids");
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
                npc.spriteDirection = 1;
                CastledWorld.globalWave = wave;
                #region
                //Initial animation
                if (setup == false)
                {
                    Timer++;
                }
                if (hasLanded == false)
                {
                    if (Timer < 60)
                    {
                        npc.position.Y = Main.player[npc.target].position.Y - 900;
                    }
                    else if (Timer > 0 && Timer < 120)
                    {
                        npc.position.Y += 20;
                    }
                    else if (Timer > 120 && Timer < 150)
                    {
                        npc.position.Y += 20;
                    }
                    else if (Timer > 150 && Timer < 180)
                    {
                        npc.position.Y -= 20;
                    }
                    else
                    {
                        hasLanded = true;
                        Timer = 0;
                    }
                }
                if (hasLanded == true && setup == false)
                {
                    if (Timer == 60 && Timer < 300)
                    {
                        CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Establishing connection to [REDACTED]...", false, false);
                    }
                    else if (Timer == 300 && Timer < 900)
                    {
                        CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Sending NET packet...", false, false);
                    }
                    else if (Timer == 900 && Timer < 1200)
                    {
                        setup = true;
                        startWave = true;
                        CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Relay Startup Initiating...", false, false);
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("RobotRadar"), 0, 0f, 255, 0f, 0f);
                        Timer = 0;
                    }
                }
                #endregion
                if (setup)
                {
                    if (startWave)
                    {
                        switch (wave)
                        {
                            #region
                            //Wave 1
                            case 1:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Wave 1: Rubble Bots]");
                                    CastledWorld.numberOfEnemies = 5;
                                    CastledWorld.eventSpawnMethod = true;
                                    CastledWorld.rubCount = 10;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                            #region
                                //Wave 2
                            case 2:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Wave 2: Rubble Bots and Evaders]");
                                    CastledWorld.numberOfEnemies = 8;
                                    CastledWorld.rubCount = 12;
                                    CastledWorld.eventSpawnMethod = true;
                                    manualSpawn = 4;
                                    dispatchWave = true;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                            #region
                            //Wave 3
                            case 3:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Wave 3: Rubble Bots, Evaders and Automated Pistons]");
                                    CastledWorld.numberOfEnemies = 12;
                                    CastledWorld.rubCount = 16;
                                    CastledWorld.eventSpawnMethod = true;
                                    manualSpawn = 8;
                                    dispatchWave = true;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                            #region
                            //Wave 4
                            case 4:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Wave 4: Rubble Bots, Evaders, Automated Pistons and Drones]");
                                    CastledWorld.numberOfEnemies = 15;
                                    CastledWorld.rubCount = 20;
                                    CastledWorld.droneCount = 4;
                                    CastledWorld.eventSpawnMethod = true;
                                    capsules = true;
                                    capsuleChance = 999;
                                    manualSpawn = 6;
                                    dispatchWave = true;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                            #region
                            //Wave 5
                            case 5:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Wave 5: Rubble Bots, Evaders, Automated Pistons and Drones]");
                                    CastledWorld.numberOfEnemies = 19;
                                    CastledWorld.rubCount = 20;
                                    CastledWorld.droneCount = 8;
                                    CastledWorld.eventSpawnMethod = true;
                                    manualSpawn = 10;
                                    dispatchWave = true;
                                    hasExclaimed = true;
                                    capsuleChance = 799;
                                }
                                break;
                            #endregion
                            #region
                            //Wave 6
                            case 6:
                                if (!hasExclaimed)
                                {
                                    distance = 75;
                                    Main.NewText("[c/af4bff:Wave 6: Rubble Bots, Evaders, Automated Pistons, Drones and Turrents]");
                                    CastledWorld.numberOfEnemies = 18;
                                    CastledWorld.rubCount = 24;
                                    CastledWorld.droneCount = 10;
                                    CastledWorld.eventSpawnMethod = true;
                                    capsuleChance = 599;
                                    manualSpawn = 12;
                                    dispatchWave = true;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                            #region
                            //Wave 7
                            case 7:
                                if (!hasExclaimed)
                                {
                                    Main.NewText("[c/af4bff:Final Wave: Doomsday Drone]");
                                    NPC.NewNPC((int)npc.position.X - distance, (int)npc.position.Y - 450, mod.NPCType("BladeBot"));
                                    CastledWorld.numberOfEnemies = 1;
                                    capsules = false;
                                    hasExclaimed = true;
                                }
                                break;
                            #endregion
                        }
                    }
                    if (dispatchWave)
                    {
                        #region
                        switch (wave)
                        {
                            case 2:
                                Timer++;
                                if (Timer > 60 && manualSpawn == 4)
                                {
                                    NPC.NewNPC((int)npc.position.X + 150, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 150, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                if (Timer > 60 && manualSpawn == 2)
                                {
                                    NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 75, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                break;

                            case 3:
                                Timer++;
                                if (Timer > 60 && manualSpawn == 8)
                                {
                                    NPC.NewNPC((int)npc.position.X + 150, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 150, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    CastledWorld.leftOrRight = 1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    CastledWorld.leftOrRight = -1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    Timer = 0;
                                    manualSpawn -= 4;
                                }
                                if (Timer > 60 && manualSpawn == 4)
                                {
                                    NPC.NewNPC((int)npc.position.X + 250, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 250, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    CastledWorld.leftOrRight = 1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    CastledWorld.leftOrRight = -1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    Timer = 0;
                                    manualSpawn -= 4;
                                }
                                break;
                            case 4:
                                Timer++;
                                if (Timer > 60 && manualSpawn == 6)
                                {
                                    NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 75, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                if (Timer > 60 && manualSpawn == 4)
                                {
                                    NPC.NewNPC((int)npc.position.X + 200, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - 200, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    CastledWorld.leftOrRight = 1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    CastledWorld.leftOrRight = -1;
                                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    Timer = 0;
                                    manualSpawn -= 4;
                                }
                                break;
                            case 5:
                                Timer++;
                                if (Timer > 60 && manualSpawn > 3)
                                {
                                    NPC.NewNPC((int)npc.position.X + distance, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - distance, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    distance += 50;
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                if (Timer > 60 && manualSpawn == 2)
                                {
                                    CastledWorld.leftOrRight = 1;
                                    NPC.NewNPC((int)npc.position.X + distance, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    CastledWorld.leftOrRight = -1;
                                    NPC.NewNPC((int)npc.position.X - distance, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                break;
                            case 6:
                                Timer++;
                                if (Timer > 60 && manualSpawn > 6)
                                {
                                    NPC.NewNPC((int)npc.position.X + distance, (int)npc.position.Y, mod.NPCType("RobotPH"));
                                    NPC.NewNPC((int)npc.position.X - distance, (int)npc.position.Y, mod.NPCType("RobotPH1"));
                                    distance += 45;
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                if (Timer > 60 && manualSpawn == 2)
                                {
                                    distance -= 45;
                                    CastledWorld.leftOrRight = 1;
                                    NPC.NewNPC((int)npc.position.X + distance, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    CastledWorld.leftOrRight = -1;
                                    NPC.NewNPC((int)npc.position.X - distance, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                    Timer = 0;
                                    manualSpawn -= 2;
                                }
                                break;
                        }
                        #endregion
                    }
                    #region
                    //capsule manager
                    if (capsules)
                    {
                        if (Main.rand.Next(capsuleChance) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCap1"), 0, 0f, 255, 0f, 0f);
                        }
                        if (wave > 4)
                        {
                            if (Main.rand.Next(capsuleChance) == 0)
                            {
                                Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCap2"), 0, 0f, 255, 0f, 0f);
                            }
                        }
                        if (wave > 5)
                        {
                            if (Main.rand.Next(capsuleChance) == 0)
                            {
                                Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCapT"), 0, 0f, 255, 0f, 0f);
                            }
                        }
                    }
                    if (CastledWorld.doomsdayCapsule)
                    {
                        if (Main.rand.Next(CastledWorld.doomsdayCapsuleIntensity) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCap1"), 0, 0f, 255, 0f, 0f);
                        }
                        if (Main.rand.Next(CastledWorld.doomsdayCapsuleIntensity) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCap2"), 0, 0f, 255, 0f, 0f);
                        }
                        if (Main.rand.Next(CastledWorld.doomsdayCapsuleIntensity) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-345, 345), npc.Center.Y - 900f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("DeathCapT"), 0, 0f, 255, 0f, 0f);
                        }
                    }
                    #endregion
                    if (CastledWorld.numberOfEnemies <= 0 && startWave && !waveCurrent)
                    {
                        startWave = false;
                        waveFinished = true;
                        waveCurrent = true;
                        CastledWorld.eventSpawnMethod = false;
                        dispatchWave = false;
                        CastledWorld.waveDelayCountdown = 10;
                        Main.NewText("[c/af4bff:Wave Complete!]");
                    }

                    if (waveFinished)
                    {
                        if (wave < 8)
                        {
                            Timer++;
                            if (Timer > 120)
                            {
                                Timer = 0;
                                CastledWorld.waveDelayCountdown--;
                            }
                            if (CastledWorld.waveDelayCountdown == 0)
                            {
                                wave++;
                                waveCurrent = false;
                                waveFinished = false;
                                startWave = true;
                                hasExclaimed = false;
                            }
                        }
                        else
                        {
                            if (hasLeft == false)
                            {
                                hasLeft = true;
                                Main.NewText("[c/66ff66:The robots have been defeated!]");
                                Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Identifier1"), 0, 0f, 255, 0f, 0f);
                                hasLeft = true;
                                npc.life = 0;
                            }
                        }
                    }
                }
            }
        }
        #region
        //Standard Frames
        private const int Frame_1 = 0;
        private const int Frame_2 = 1;
        private const int Frame_3 = 2;
        private const int Frame_4 = 3;
        private const int Frame_5 = 4;
        private const int Frame_6 = 5;
        private const int Frame_7 = 6;
        private const int Frame_8 = 7;
        //Transformation Frames
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 4)
            {
                npc.frame.Y = Frame_1 * frameHeight;
            }
            else if (npc.frameCounter < 8)
            {
                npc.frame.Y = Frame_2 * frameHeight;
            }
            else if (npc.frameCounter < 12)
            {
                npc.frame.Y = Frame_3 * frameHeight;
            }
            else if (npc.frameCounter < 16)
            {
                npc.frame.Y = Frame_4 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = Frame_5 * frameHeight;
            }
            else if (npc.frameCounter < 24)
            {
                npc.frame.Y = Frame_6 * frameHeight;
            }
            else if (npc.frameCounter < 28)
            {
                npc.frame.Y = Frame_7 * frameHeight;
            }
            else if (npc.frameCounter < 32)
            {
                npc.frame.Y = Frame_8 * frameHeight;
            }
            else
            {
                if (setup == true)
                {
                    radar++;
                    if (radar > 6)
                    {
                        radar = 0;
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("RobotRadar"), 0, 0f, 255, 0f, 0f);
                    }
                    npc.frameCounter = 0;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
        #endregion

        public override bool CheckActive()
        {
            return false;
        }
    }
}
#region
//
/*
 * Old Code:
 * 
 * using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    [AutoloadBossHead]
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Aircraft");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 50;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 100;
            npc.height = 92;
            npc.alpha = 100;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.scale = 1.5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/RobotInvasionTheme");
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
                            Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                        }
                        npc.position.Y += 10;
                        if (Timer > 180)
                        {
                            swoopIn = true;
                            Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipPulse"), 0, 0f, 255, 0f, 0f);
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
                            Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipPulse"), 0, 0f, 255, 0f, 0f);
                            waveDelay = 0;
                            waveDelay2++;
                        }
                        if (waveDelay2 > 2 && waveHasBegun == false)
                        {
                            Main.NewText("[c/af4bff:First Wave: 4 Rubble Bots]");
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
                            Main.NewText("[c/af4bff:Wave 2: 4 Rubble Bots, and 2 Evaders]");
                            Main.NewText("[c/af4bff:Total: 6 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                        if (wave == 3)
                        {
                            Main.NewText("[c/af4bff:Wave 3: 3 Rubble Bots, 2 Evaders and 2 Automated Pistions]");
                            Main.NewText("[c/af4bff:Total: 7 enemies]");
                            Timer = 0;
                            waveHasBegun = true;
                            dispatchWave = true;
                        }
                        if (wave == 4)
                        {
                            Main.NewText("[c/af4bff:Wave 4: 4 Rubble Bots, 2 Evaders and 4 Automated Pistions]");
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
                            if (CastledWorld.numberOfEnemies <= 0)
                            {
                                Main.NewText("[c/af4bff:Wave Complete!]");
                                awaitingWaveTerminate = false;
                                CastledWorld.counterType = 2;
                                CastledWorld.waveDelayCountdown = 10;
                                nextWaveCountdown = true;
                                Projectile.NewProjectile(npc.Center.X - 5, npc.Center.Y - 58, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
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
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (deployAmount == 4)
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
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (deployAmount == 4)
                        {
                            CastledWorld.numberOfEnemies += 2;
                            Main.PlaySound(SoundID.Item61);
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH1"));
                            deployAmount += 2;
                        }
                        if (deployAmount == 6)
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
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            deployDelay = 0;
                            deployAmount++;
                        }
                        if (deployAmount == 3)
                        {
                            if (deployDelay > 70)
                            {
                                Main.PlaySound(SoundID.Item61);
                                CastledWorld.numberOfEnemies += 2;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH"));
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH1"));

                                deployAmount += 2;
                            }
                        }
                        if (deployAmount == 5)
                        {
                            if (deployDelay > 150)
                            {
                                CastledWorld.numberOfEnemies += 2;
                                Main.PlaySound(SoundID.Item61);
                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));

                                deployAmount += 2;
                            }
                        }
                        if (deployAmount == 7)
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
                            Main.PlaySound(SoundID.Item61);
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("Robot"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH"));
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("RobotPH1"));
                            deployDelay = 0;
                            deployAmount += 6;
                        }
                        if (deployAmount == 6)
                        {
                            if (deployDelay > 120)
                            {
                                CastledWorld.numberOfEnemies += 2;
                                Main.PlaySound(SoundID.Item61);
                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));

                                deployAmount += 2;
                            }
                        }
                        if (deployAmount == 8)
                        {
                            if (deployDelay > 360)
                            {
                                CastledWorld.numberOfEnemies += 2;
                                Main.PlaySound(SoundID.Item61);
                                CastledWorld.leftOrRight = 1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));
                                CastledWorld.leftOrRight = -1;
                                NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("PistonPH1"));

                                deployAmount += 2;
                            }
                        }
                        if (deployAmount > 8)
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
                            NPC.NewNPC((int)npc.position.X + 75, (int)npc.position.Y + 90, mod.NPCType("BladeBot"));
                            deployDelay = 0;
                            deployAmount += 10;
                        }
                        if (deployAmount == 10)
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
                                motionBlurCounter = 0;
                                Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                            }
                            npc.position.Y += 2;
                        }
                        if (Timer > 60)
                        {
                            motionBlurCounter++;
                            if (motionBlurCounter > 4)
                            {
                                motionBlurCounter = 0;
                                Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("CleanupShipBlur"), 0, 0f, 255, 0f, 0f);
                            }
                            npc.position.Y -= 12;
                        }
                        if (Timer > 150 && hasLeft == false)
                        {
                            Main.NewText("[c/66ff66:The robots have been defeated!]");
                            Projectile.NewProjectile(npc.Center.X + 25, npc.Center.Y + 35f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Identifier1"), 0, 0f, 255, 0f, 0f);
                            hasLeft = true;
                            npc.life = 0;
                        }
                    }
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
}*/
#endregion