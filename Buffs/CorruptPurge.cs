using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class CorruptPurge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Purge");
            Description.SetDefault("Melee critical strike Chance and attack speed increased.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeCrit = 3 * Main.LocalPlayer.GetModPlayer<CastledPlayer>().purge;
            player.meleeSpeed = 1 * Main.LocalPlayer.GetModPlayer<CastledPlayer>().purge;
        }
    }
}