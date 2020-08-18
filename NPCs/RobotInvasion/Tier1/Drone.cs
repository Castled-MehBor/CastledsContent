using Microsoft.Xna.Framework;
using System;
using System.Runtime.Remoting.Contexts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class Drone : ModNPC
    {
        public int aiState = 1;
        public int SOC = 0;
        public int flyDist = 0;
        public int xDist = 0;
        public int direction = 0;
        public int stageOfAI = 1;
        public bool dAI = false;
        public bool contribute = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rebus Drone");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {

            aiType = 0;
            npc.lifeMax = 100;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 56;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.damage = 10;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
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
        public override void NPCLoot()
        {
            if (CastledWorld.globalWave < 7)
            {
                if (NPC.AnyNPCs(mod.NPCType("CleanupShip_PH")) && CastledWorld.numberOfEnemies < 21)
                {
                    Main.PlaySound(SoundID.Grab, (int)npc.position.X, (int)npc.position.Y, 17);
                    CastledWorld.numberOfEnemies--;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, -2f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
                }
            }
        }
        public override void AI()
        {
            Timer++;
            npc.spriteDirection = 1;

            #region
            //determine randomized height and x movement
            if (Timer < 600)
            {
                aiState = 1;
            }
            else if (Timer < 900)
            {
                aiState = 2;
            }
            else if (Timer < 1200)
            {
                aiState = 3;
            }
            //determine left or right
            if (aiState == 1)
            {
                if (dAI == false)
                {
                    dAI = true;
                    int num = Main.rand.Next(2);
                    if (num == 0)
                    {
                        direction = 1;
                    }
                    if (num == 1)
                    {
                        direction = 2;
                    }
                }
                if (direction == 1)
                {
                    xDist = 2;
                }
                else if (direction == 2)
                {
                    xDist = -2;
                }
                //calculate random flyDist value
                if (flyDist == 0 && stageOfAI == 1)
                {
                    stageOfAI = 0;
                    flyDist = Main.rand.Next(6, 24);
                }
                //go up by however many flyDist was amounted
                if (dAI == true)
                {
                    if (stageOfAI == 0)
                    {
                        if (flyDist > 0)
                        {
                            flyDist--;
                            npc.position.Y -= 8;
                        }
                        if (flyDist == 0 || flyDist < 0)
                        {
                            npc.position.X += xDist;
                        }
                    }
                }
            }
            #endregion
            #region
            //home in above player
            if (aiState == 2)
            {
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y - 60f), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * 5.5) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * 4.5) * -1;
                }
            }
            #endregion
            #region
            //swoop down
            if (aiState == 3)
            {
                SOC++;
                if (SOC < 80 && SOC > 40)
                {
                    npc.position.Y += 6;
                    npc.damage = 55;
                }
                else if (SOC > 120)
                {
                    Timer = 0;
                    dAI = false;
                    flyDist = 0;
                    SOC = 0;
                    stageOfAI = 1;
                    npc.damage = 10;
                }
            }
            #endregion
            if (CastledWorld.eventSpawnMethod && contribute == false)
            {
                CastledWorld.droneCount--;
                contribute = true;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (CastledWorld.eventSpawnMethod && CastledWorld.droneCount > 0)
            {
                return 200f;
            }
            else
            {
                return 0f;
            }
        }
        private const int F0 = 0;
        private const int F1 = 1;
        private const int F2 = 2;
        private const int F3 = 3;
        private const int F4 = 4;
        private const int F5 = 5;
        private const int F6 = 6;
        private const int F7 = 7;
        public override void FindFrame(int frameHeight)
        {
            #region
            npc.frameCounter++;
            //if aiState is less than 3, perform standard animation
            if (aiState < 3)
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = F0 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = F1 * frameHeight;
                }
                else if (npc.frameCounter < 15)
                {
                    npc.frame.Y = F2 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = F3 * frameHeight;
                }
                else if (npc.frameCounter < 25)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            #endregion
            #region
            //clasp mandibles
            if (aiState == 3)
            {
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else if (npc.frameCounter < 65)
                {
                    npc.frame.Y = F5 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = F6 * frameHeight;
                }
                else if (npc.frameCounter < 75)
                {
                    npc.frame.Y = F7 * frameHeight;
                }
                else if (npc.frameCounter < 120)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            #endregion
        }
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
 * using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class Drone : ModNPC
    {
        public int aiState = 1;
        public int SOC = 0;
        public int flyDist = 0;
        public int xDist = 0;
        public int direction = 0;
        public int stageOfAI = 1;
        public bool dAI = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rebus Drone");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {

            aiType = 0;
            npc.lifeMax = 100;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 56;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.damage = 10;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
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
            Timer++;
            npc.spriteDirection = 1;

            #region
            //determine randomized height and x movement
            if (Timer < 600)
            {
                aiState = 1;
            }
            else if (Timer < 900)
            {
                aiState = 2;
            }
            else if (Timer < 1200)
            {
                aiState = 3;
            }
            //determine left or right
            if (aiState == 1)
            {
                if (dAI == false)
                {
                    dAI = true;
                    int num = Main.rand.Next(2);
                    if (num == 0)
                    {
                        direction = 1;
                    }
                    if (num == 1)
                    {
                        direction = 2;
                    }
                }
                if (direction == 1)
                {
                    xDist = 2;
                }
                else if (direction == 2)
                {
                    xDist = -2;
                }
                //calculate random flyDist value
                if (flyDist == 0 && stageOfAI == 1)
                {
                    stageOfAI = 0;
                    flyDist = Main.rand.Next(6, 24);
                }
                //go up by however many flyDist was amounted
                if (dAI == true)
                {
                    if (stageOfAI == 0)
                    {
                        if (flyDist > 0)
                        {
                            flyDist--;
                            npc.position.Y -= 8;
                        }
                        if (flyDist == 0 || flyDist < 0)
                        {
                            npc.position.X += xDist;
                        }
                    }
                }
            }
            #endregion
            #region
            //home in above player
            if (aiState == 2)
            {
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y - 60f), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * 5.5) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * 4.5) * -1;
                }
            }
            #endregion
            #region
            //swoop down
            if (aiState == 3)
            {
                SOC++;
                if (SOC < 80 && SOC > 40)
                {
                    npc.position.Y += 6;
                    npc.damage = 55;
                }
                else if (SOC > 120)
                {
                    Timer = 0;
                    dAI = false;
                    flyDist = 0;
                    SOC = 0;
                    stageOfAI = 1;
                    npc.damage = 10;
                }
            }
            #endregion
        }
        private const int F0 = 0;
        private const int F1 = 1;
        private const int F2 = 2;
        private const int F3 = 3;
        private const int F4 = 4;
        private const int F5 = 5;
        private const int F6 = 6;
        private const int F7 = 7;
        public override void FindFrame(int frameHeight)
        {
            #region
            npc.frameCounter++;
            //if aiState is less than 3, perform standard animation
            if (aiState < 3)
            {
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = F0 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = F1 * frameHeight;
                }
                else if (npc.frameCounter < 15)
                {
                    npc.frame.Y = F2 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = F3 * frameHeight;
                }
                else if (npc.frameCounter < 25)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            #endregion
            #region
            //clasp mandibles
            if (aiState == 3)
            {
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else if (npc.frameCounter < 65)
                {
                    npc.frame.Y = F5 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = F6 * frameHeight;
                }
                else if (npc.frameCounter < 75)
                {
                    npc.frame.Y = F7 * frameHeight;
                }
                else if (npc.frameCounter < 120)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            #endregion
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
}*/
#endregion