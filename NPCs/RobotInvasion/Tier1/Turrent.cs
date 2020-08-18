using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class Turrent : ModNPC
    {
        public bool isRecharging = false;
        public int decideDirection;
        public int projectile = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rebus Turrent");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 125;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 30;
            npc.damage = 15;
            npc.height = 30;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.netAlways = true;
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
            if (npc.life < npc.lifeMax * 0.1)
            {
                Main.PlaySound(SoundID.NPCHit53);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Cyan, 3f);
            }
        }
        public override void AI()
        {
            #region
            //decide what direction it faces and fires at
            npc.spriteDirection = decideDirection;
            if (decideDirection == 0)
            {
                decideDirection = Main.rand.Next(0, 3);
            }
            //delay between recharging and firing
            if (isRecharging == true)
            {
                Timer++;
                if (Timer > 120)
                {
                    Timer = 0;
                    isRecharging = false;
                }
            }
            #endregion
            #region
            //fire in the correct direction
            if (isRecharging == false)
            {
                switch (decideDirection)
                {
                    case 1:
                        {
                            if (npc.frameCounter == 60)
                            {
                                Main.PlaySound(SoundID.Item61);
                                Vector2 vector8 = npc.BottomRight;
                                int damage = 10;
                                int type = mod.ProjectileType("TurrentBlast");
                                float rotation = npc.spriteDirection;
                                int num54 = Projectile.NewProjectile(vector8.X - 8, vector8.Y - 15, 14f, 0f, type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (npc.frameCounter == 60)
                            {
                                Main.PlaySound(SoundID.Item61);
                                Vector2 vector8 = npc.BottomRight;
                                int damage = 10;
                                int type = mod.ProjectileType("TurrentBlast");
                                float rotation = npc.spriteDirection;
                                int num54 = Projectile.NewProjectile(vector8.X - 8, vector8.Y - 15, -14f, 0f, type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            break;
                        }
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
        private const int F8 = 8;
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            #region
            //fire animation
            if (!isRecharging)
            {
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = F2 * frameHeight;
                }
                else if (npc.frameCounter < 65)
                {
                    npc.frame.Y = F3 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = F4 * frameHeight;
                }
                else if (npc.frameCounter < 75)
                {
                    npc.frame.Y = F5 * frameHeight;
                }
                else if (npc.frameCounter < 80)
                {
                    npc.frame.Y = F6 * frameHeight;
                }
                else if (npc.frameCounter < 85)
                {
                    npc.frame.Y = F7 * frameHeight;
                }
                else if (npc.frameCounter < 90)
                {
                    npc.frame.Y = F8 * frameHeight;
                }
                else
                {
                    isRecharging = true;
                    npc.frameCounter = 0;
                }
            }
            //recharging animation
            if (isRecharging)
            {
                if (npc.frameCounter < 40)
                {
                    npc.frame.Y = F0 * frameHeight;
                }
                else if (npc.frameCounter < 80)
                {
                    npc.frame.Y = F1 * frameHeight;
                }
                else if (npc.frameCounter < 120)
                {
                    npc.frame.Y = F2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isRecharging = false;
                    Main.PlaySound(SoundID.MenuTick);
                }
            }
            #endregion
        }
    }
}