using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class CrimPurge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Purge");
            Description.SetDefault("Melee damage and defense increased.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage = 0.05f * Main.LocalPlayer.GetModPlayer<CastledPlayer>().purge;
            player.statDefense = 1 * Main.LocalPlayer.GetModPlayer<CastledPlayer>().purge;
        }
    }
}