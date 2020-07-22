using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class AdrenalineBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hyped up");
            Description.SetDefault("Adrenaline is coursing through your body.");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.runAcceleration = 35f;
            player.immune = true;
            player.jumpSpeedBoost = 15f;
        }
    }
}