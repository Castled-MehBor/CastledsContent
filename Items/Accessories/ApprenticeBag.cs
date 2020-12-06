using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories
{
    public class ApprenticeBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Apprentice Bag");
            Tooltip.SetDefault("'The only apprentice bag'"
            + "\n5% increase to all damage types"
            + "\n8% increased movement and max running speed"
            + "\n10% increase to all knockback"
            + "\nIncreases life regen by 2"
            + "\nIncreased rocket boot flight time");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = 15000;
            item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.05f;
            player.moveSpeed += 0.08f;
            player.maxRunSpeed += 0.08f;
            player.GetWeaponKnockback(item, 1.1f);
            player.rocketTimeMax += 20;
            player.lifeRegen += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("CastledsContent:EvilBar", 10);
            recipe.AddRecipeGroup("CastledsContent:EvilDrop", 5);
            recipe.AddIngredient(ItemType<StarterBag>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
