using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class KingSlimeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Morphed");
            Description.SetDefault("You are now King Slime!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegenTime = 0;
            player.statDefense = 10;
            player.statLifeMax2 = 2000;
            player.jumpSpeedBoost = 15f;
            player.moveSpeed = 0.2f;
            player.minionDamage = 0;
            player.magicDamage = 0;
            player.rangedDamage = 0;
            player.meleeDamage = 0;
            player.aggro = 100;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Venom] = true;
        }
    }
}