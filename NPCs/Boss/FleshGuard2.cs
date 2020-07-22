﻿using Microsoft.Xna.Framework;
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

namespace CastledsContent.NPCs.Boss
{
    [AutoloadBossHead]
    public class FleshGuard2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichorated Immolator of the Wall");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {

            aiType = 14;
            npc.CloneDefaults(NPCID.CaveBat);
            npc.lifeMax = 8750;
            npc.damage = 85;
            npc.defense = 25;
            npc.knockBackResist = 0f;
            npc.width = 28;
            npc.height = 60;
            npc.value = Item.buyPrice(0, 0, 0, 1);
            npc.npcSlots = 40f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath13;
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
                npc.lifeMax = (int)(8750.0 * num2);
            }
            else
            {
                npc.lifeMax = 8750;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }
        public override void NPCLoot()
        {
            if (Main.expertMode && NPC.AnyNPCs(mod.NPCType("SkeletonGuard1")))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin);
            }
            else
            {
                if (Main.expertMode && NPC.AnyNPCs(mod.NPCType("SkeletonGuard2")))
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin);
                }
                else
                {
                    if (Main.expertMode && NPC.AnyNPCs(mod.NPCType("SkeletonGuard3")))
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
                npc.velocity.X = (float)(Math.Cos(rotation) * 6.5) * -1;
                npc.velocity.Y = (float)(Math.Sin(rotation) * 3.5) * -1;
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

                if (npc.ai[1] >= 65)
                {

                    float Speed = 5f;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 60;
                    int type = mod.ProjectileType("IchorLaser");
                    Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 17);
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    npc.ai[1] = 0;

                    npc.ai[0] %= (float)Math.PI * 2f;
                    Vector2 offset = new Vector2((float)Math.Cos(npc.ai[0]), (float)Math.Sin(npc.ai[0]));
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    npc.ai[2] = -300;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 12;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 5, 0, 0, 100, color, 1.5f);
                        Main.dust[dust].noGravity = false;
                    }
                    return;
                }
            }
        }
    }
}