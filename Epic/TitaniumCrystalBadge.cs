using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Epic
{
    public class TitaniumCrystalBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Badge of the Ascendence");
            Tooltip.SetDefault("'You are worthy.'"
            + "\nIncreases maximum minions by 2"
            + "\nIncreases all primary class damage by 15%"
            + "\nMana cost reduced by 35%"
            + "\nMelee attack speed increased by 35%"
            + "\nGrants immunity to Cursed Inferno and Ichor");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.width = 40;
            item.value = 80000;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.minionDamage += 0.15f;
            player.magicDamage += 0.15f;
            player.manaCost -= 0.35f;
            player.rangedDamage += 0.15f;
            player.meleeDamage += 0.15f;
            player.meleeSpeed += 0.35f;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
        }
    }
}