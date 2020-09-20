using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class RangeBuffC : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corrupt Energy");
            Description.SetDefault("Movement speed and ranged critical strike chance is increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed = 0.15f;
            player.rangedCrit = 15;
        }
    }
}