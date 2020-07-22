using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class EaterofWorldsBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Morphed");
            Description.SetDefault("You are now the Eater of Worlds!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance = -1;
            player.lifeRegenTime = 0;
            player.statLifeMax2 = 7485;
            player.moveSpeed = 1.5f;
            player.minionDamage = 0;
            player.magicDamage = 0;
            player.rangedDamage = 0;
            player.meleeDamage = 0;
            player.aggro = 100;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Venom] = true;
        }
    }
}