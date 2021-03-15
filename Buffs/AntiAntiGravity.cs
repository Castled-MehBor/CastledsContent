// Tag #1: Skyware Boss
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class AntiAntiGravity : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Astral Defiance");
            Description.SetDefault("You are heavier and enveloped by oxygen.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxFallSpeed = 40f;
        }
    }
}