using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories
{
    public class SpeedItem1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Capsule of Tomorrow");
            Tooltip.SetDefault("'Soar into next week'"
            + "\nRun acceleration increased by 240%"
            + "\nDefense increased by 15"
            + "\nLife regen is increased by 6"
            + "\nGrants immunity to knockback & fall damage"
            + "\nIncreases maximum life by 80"
            + "\nGrants immunity to many debuffs");
        }

        public override void SetDefaults()
        {
            item.width = 74;
            item.width = 40;
            item.value = 150000;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.runAcceleration += 2.4f;
            player.statDefense += 15;
            player.lifeRegen += 6;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.statLifeMax2 += 80;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Burning] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TitaniumCrystalBadge>(), 3);
            recipe.AddIngredient(ItemType<CrimtaneScrap>(), 3);
            recipe.AddIngredient(ItemID.LifeFruit, 5);
            recipe.AddIngredient(ItemID.EyeoftheGolem);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 250);
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}