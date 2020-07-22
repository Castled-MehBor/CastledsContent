using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Epic
{
    public class CrimtaneScrap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimtane Cuffs");
            Tooltip.SetDefault("'Perfect for escaping the law.'"
            + "\nRun acceleration increased by 55%"
            + "\nDefense increased by 8"
            + "\nLife regen is increased by 3"
            + "\nGrants immunity to knockback & fall damage"
            + "\nIncreases maximum life by 50"
            + "\nGrants immunity to Cursed Inferno and Ichor");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.width = 30;
            item.value = 80000;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.runAcceleration += 0.55f;
            player.statDefense += 8;
            player.lifeRegen += 3;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.statLifeMax2 += 50;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
        }
    }
}