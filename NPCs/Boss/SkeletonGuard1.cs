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
    public class SkeletonGuard1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alginth, the Chained");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {

            aiType = 14;
            npc.CloneDefaults(NPCID.CaveBat);
            npc.lifeMax = 12500;
            npc.damage = 1;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.width = 46;
            npc.height = 82;
            npc.npcSlots = 40f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath13;
            npc.buffImmune[69] = true;
            music = MusicID.Boss4;
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
                npc.lifeMax = (int)(15000.0 * num2);
            }
            else
            {
                npc.lifeMax = 15000;
            }
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

                if (npc.ai[1] >= 70)
                {

                    float Speed = 2f;
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 25;
                    int type = mod.ProjectileType("BoneTalon");
                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    npc.ai[1] = 0;
                }
                npc.ai[2] += 0;
                if ((double)npc.life < (double)npc.lifeMax * 0.75)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SkeletonGuard1_G"), 1.6f);
                    Main.PlaySound(21, (int)npc.position.X, (int)npc.position.Y, 10, 1f, 0f);
                    npc.Transform(mod.NPCType("SkeletonGuard2"));
                }
            }
        }
    }
}