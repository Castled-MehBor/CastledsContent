using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class Robot : ModNPC
    {
        public bool hasLanded = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rubble Bot");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.AngryBones];
        }

        public override void SetDefaults()
        {
            aiType = NPCID.AngryBones;
            animationType = NPCID.AngryBones;
            npc.CloneDefaults(NPCID.AngryBones);
            npc.lifeMax = 150;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
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
            if (NPC.AnyNPCs(mod.NPCType("CleanupShip_PH")) && CastledWorld.numberOfEnemies > 0)
            {
                Main.PlaySound(SoundID.Grab, (int)npc.position.X, (int)npc.position.Y, 17);
                CastledWorld.numberOfEnemies--;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, -2f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
            }
            float Speed = 1f;
            Vector2 vector8 = npc.BottomRight;
            int damage = 6;
            int type = ProjectileID.SaucerScrap;
            float rotation = npc.rotation;
            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y - 15, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            npc.ai[1] = 0;
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
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
                npc.lifeMax = (int)(175.0 * num2);
                npc.damage = (int)(npc.damage * 0.3f);
            }
            else
            {
                npc.lifeMax = 175;
                npc.damage = (int)(npc.damage * 0.3f);
            }
        }
        public override void AI()
        {
            if (hasLanded == false)
            {
                aiType = -1;
                Timer++;
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                npc.position.Y += 4;
                if (Timer > 60)
                {
                    hasLanded = true;
                    aiType = NPCID.AngryBones;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}