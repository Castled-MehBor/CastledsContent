// Tag #1: Skyware Boss
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class HarpyQueenDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Avian Prescence");
            Description.SetDefault("Her glare scares off even other harpies...");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}