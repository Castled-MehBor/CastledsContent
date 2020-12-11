using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class IronShieldBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Iron Shield's Insurgence");
            Description.SetDefault("The aegis' power is active");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 20;
            player.noKnockback = true;
        }
    }
}