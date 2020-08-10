using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Boss.CrimsonBoss
{
    [AutoloadBossHead]
    public class SkeletonGuard3 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Algurah, the Forsaken Spirit");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {

            aiType = 86;
            npc.CloneDefaults(NPCID.AncientCultistSquidhead);
            npc.lifeMax = 2345;
            npc.damage = 1;
            npc.defense = 35;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 66;
            npc.value = Item.buyPrice(0, 0, 0, 1);
            npc.npcSlots = 40f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[69] = true;
            music = MusicID.Boss2;
            bossBag = mod.ItemType("TreasureBag2");
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
                npc.lifeMax = (int)(3000 * num2);
            }
            else
            {
                npc.lifeMax = 3000;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }
        public override void NPCLoot()
        {
            if (Main.expertMode && NPC.AnyNPCs(mod.NPCType("FleshGuard1")))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin);
            }
            else
            {
                if (Main.expertMode && NPC.AnyNPCs(mod.NPCType("FleshGuard2")))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin);
                }
                else
                {
                    if (Main.expertMode)
                    {
                        CastledWorld.downedCrimsonPrisoners = true;
                        npc.DropBossBags();
                    }

                    else
                    {
                        if (!Main.expertMode && !NPC.AnyNPCs(mod.NPCType("FleshGuard1")))
                        {
                            CastledWorld.downedCrimsonPrisoners = true;
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, 35);
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Ichor, Main.rand.Next(28, 46));
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofNight, Main.rand.Next(13, 28));
                            int num = Main.rand.Next(4);
                            if (num == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RapidBlaster"));
                            }
                            if (num == 1)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldenShower);
                            }
                            if (num == 2)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.ThePlan);
                            }
                            if (num == 3)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MedicatedBandage);
                            }
                            if (Main.rand.Next(9) == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnchantedSwordbutBetter"));
                            }
                            if (Main.rand.Next(9) == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueenBee"));
                            }
                            if (Main.rand.Next(9) == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LaserTron"));
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EpicQuartz"));
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                                {
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BruhMomento"));
                                }
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                                {
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BayonettaKiller"));
                                }
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist)
                                {
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShinyStaff"));
                                }
                                else
                                {
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, 5);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool PreAI()
        {
            npc.velocity.X *= 0.98f;
            npc.velocity.Y *= 0.98f;
            Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            {
                float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                npc.velocity.X = (float)(Math.Cos(rotation) * 3.5) * -1;
                npc.velocity.Y = (float)(Math.Sin(rotation) * 1.5) * -1;
            }
            return true;
        }

        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            {
                npc.ai[1]++;
                npc.ai[1] += 0;
                if (npc.ai[1] >= 25)
                {
                    if (Main.expertMode)
                    {
                    float Speed = 9f;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 45;
                    int type = mod.ProjectileType("IchorFire");
                    Main.PlaySound(6, (int)npc.position.X, (int)npc.position.Y, 17);
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    npc.ai[1] = 0;
                    }
                    else
                    {
                        float Speed = 5f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        int damage = 45;
                        int type = mod.ProjectileType("IchorFire");
                        Main.PlaySound(6, (int)npc.position.X, (int)npc.position.Y, 17);
                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                        npc.ai[1] = 0;
                    }
                }
            }
        }
    }
}