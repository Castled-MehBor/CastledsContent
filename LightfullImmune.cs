using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using CastledsContent.Buffs;

namespace CastledsContent.Buffs
{
    public class LightfullImmune : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lightful Immunity");
            Description.SetDefault("Cooldown.");
            Main.debuff[Type] = true;
            longerExpertDebuff = false;  
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] > 2)
            {
                npc.buffImmune[ModContent.BuffType<Lightful>()] = true;
            }
            if (npc.buffTime[buffIndex] == 2) 
            {
                npc.buffImmune[ModContent.BuffType<Lightful>()] = false;
            }
        }
    }
}