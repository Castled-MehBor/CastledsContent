using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using CastledsContent.Buffs;

namespace CastledsContent.Buffs
{
    public class Lightful : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lightful");
            Description.SetDefault("The eye deems you unworthy.");
            Main.debuff[Type] = true;
            longerExpertDebuff = false;  
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CastledNPC>().lightfull = true;
            for (int i = 0; i < 2; i++)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(npc.Center - new Vector2(npc.width / 2, npc.height / 2), npc.width, npc.height, 43, 0f, 0f, 100, new Color(255, 255, 255), 1.2f)];
                dust.noGravity = true;
            }
            if (npc.buffTime[buffIndex] == 2) 
            {
                npc.AddBuff(ModContent.BuffType<LightfullImmune>(), 600);
            }
        }
    }
}