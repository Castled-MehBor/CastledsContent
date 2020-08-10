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

namespace CastledsContent.NPCs.Boss.CorruptBoss
{
    [AutoloadBossHead]
    public class CorruptionBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amalgamus, Core of the Corruption");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {

            aiType = 63;
            npc.CloneDefaults(NPCID.Flocko);
            npc.lifeMax = 15000;
            npc.damage = 85;
            npc.defense = 35;
            npc.knockBackResist = 0f;
            npc.width = 64;
            npc.height = 72;
            npc.value = Item.buyPrice(0, 0, 0, 1);
            npc.npcSlots = 40f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath13;
            npc.buffImmune[69] = true;
            music = MusicID.Boss2;
            bossBag = mod.ItemType("TreasureBag1");
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
                npc.lifeMax = (int)(20000.0 * num2);
            }
            else
            {
                npc.lifeMax = 20000;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }

        public override void NPCLoot()
        {
            CastledWorld.downedCorruptGuardians = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, 25);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CursedFlame, Main.rand.Next(28, 46));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofNight, Main.rand.Next(13, 28));
                int num = Main.rand.Next(4);
                if (num == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LunaBlaster"));
                }
                if (num == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CursedFlames);
                }
                if (num == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.ArmorBracing);
                }
                if (num == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CountercurseMantra);
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
                }
            }
        }

        public override bool PreAI()
        {
            if (Main.expertMode)
            {
                npc.velocity.X *= 3.85f;
                npc.velocity.Y *= 3.85f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * 5.5) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * 4.5) * -1;
                }
            }
            else
            {
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * 5.5) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * 4.5) * -1;
                }
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
                if ((double)npc.life < (double)npc.lifeMax)
                {
                    if (npc.ai[1] % 240 == 6)
                    {
                        npc.velocity.X *= 9f;
                        npc.velocity.Y *= 9f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                        {
                            float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                            npc.velocity.X = (float)(Math.Cos(rotation) * 46) * -12;
                            npc.velocity.Y = (float)(Math.Sin(rotation) * 46) * -12;
                        }

                        npc.ai[0] %= (float)Math.PI * 2f;
                        Vector2 offset = new Vector2((float)Math.Cos(npc.ai[0]), (float)Math.Sin(npc.ai[0]));
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                        npc.ai[2] = -300;
                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 30;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 7.5f);
                            Main.dust[dust].noGravity = true;
                        }
                        return;
                    }
                }
                npc.ai[2] += 0;
                if ((double)npc.life < (double)npc.lifeMax * 0.55)
                {
                    if (npc.ai[1] >= 4)
                    {
                        if (Main.expertMode)
                        {
                            float Speed = 18f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 65;
                            int type = 96;
                            Main.PlaySound(6, (int)npc.position.X, (int)npc.position.Y, 17);
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                            //dust
                            Color color = new Color();
                            Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                            int count = 30;
                            for (int i = 1; i <= count; i++)
                            {
                                int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 7.5f);
                                Main.dust[dust].noGravity = true;
                            }
                            return;
                        }
                        else
                        {
                             float Speed = 12f;
                             Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                             int damage = 65;
                             int type = 96;
                             Main.PlaySound(6, (int)npc.position.X, (int)npc.position.Y, 17);
                             float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                             int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                             npc.ai[1] = 0;
                            //dust
                            Color color = new Color();
                            Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                            int count = 30;
                            for (int i = 1; i <= count; i++)
                            {
                                int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 3.5f);
                                Main.dust[dust].noGravity = true;
                            }
                            return;
                        }
                    }
                }

                {

                    if (NPC.AnyNPCs(mod.NPCType("BreatherHead")))
                    {
                        npc.dontTakeDamage = true;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;

                    }
                }
            }
        }
    }
}