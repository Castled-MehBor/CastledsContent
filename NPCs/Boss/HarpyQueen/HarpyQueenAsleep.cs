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
using System.Threading;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.Boss.HarpyQueen
{
    public class HarpyQueenAsleep : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Queen");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 350;
            npc.damage = 0;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.width = 150;
            npc.height = 126;
            npc.npcSlots = 1f;
            npc.rarity = 3;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.DD2_EtherianPortalOpen;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Midas] = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Venom] = true;
            music = MusicID.Space;
        }
        public override void AI()
        {
            npc.spriteDirection = 2;
        }
        public override bool CheckDead()
        {
            npc.Transform(NPCType<HarpyQueen>());
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneSkyHeight && NPC.downedQueenBee && !NPC.AnyNPCs(NPCType<HarpyQueenAsleep>()) && !CastledWorld.downedHarpyQueen)
            {
                return SpawnCondition.Sky.Chance * 0.025f;
            }
            return 0f;
        }
    }
}