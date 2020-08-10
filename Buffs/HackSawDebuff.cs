using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class HackSawDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Impaled");
            Description.SetDefault("This decision was all up to you.");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff(ModContent.BuffType<EaterofWorldsBuff>()))
            {
                player.lifeRegen -= 1875;
            }
        }
    }
}