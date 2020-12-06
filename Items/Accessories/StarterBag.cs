using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace CastledsContent.Items.Accessories
{
    public class StarterBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Starter Bag");
            Tooltip.SetDefault("'The ultimate beginner bag'"
            + "\n2% increase to all damage types"
            + "\n5% increased movement and max running speed"
            + "\n5% increase to all knockback"
            + "\nIncreased rocket boot flight time");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = 15000;
            item.rare = ItemRarityID.Blue;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.02f;
            player.moveSpeed += 0.05f;
            player.maxRunSpeed += 0.05f;
            player.GetWeaponKnockback(item, 1.05f);
            player.rocketTimeMax += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CastledsContent:OneTierBar", 2);
            recipe.AddRecipeGroup("CastledsContent:TwoTierBar", 3);
            recipe.AddRecipeGroup("CastledsContent:ThreeTierBar", 4);
            recipe.AddRecipeGroup("CastledsContent:FourTierBar", 5);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
