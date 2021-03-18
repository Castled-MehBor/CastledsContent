// Tag #1: Skyware Boss
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class HarpyQueenDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bird's Eye");
            Description.SetDefault("Don't look down.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}