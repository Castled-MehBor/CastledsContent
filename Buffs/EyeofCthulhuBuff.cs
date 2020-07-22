using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class EyeofCthulhuBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Morphed");
            Description.SetDefault("You are now the Eye of Cthulhu!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegenTime = 0;
            player.statDefense = 12;
            player.statLifeMax2 = 2800;
            player.moveSpeed = 0.2f;
            player.minionDamage = 0;
            player.magicDamage = 0;
            player.rangedDamage = 0;
            player.meleeDamage = 0;
            player.aggro = 100;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.buffImmune[BuffID.Confused] = true;
            player.dash = 1;
            player.moveSpeed = 0.5f;
        }
    }
}