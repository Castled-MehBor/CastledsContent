using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace CastledsContent.Items.Accessories
{
	public class SamuraiInstincts : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Samurai's Instincts");
            Tooltip.SetDefault("'You have made an enemy out of something you have never met.'"
            + "\n40% increased melee speed"
            + "\n40% decreased melee damage");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = 45000;
            item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeSpeed += 0.4f;
            player.meleeDamage -= 0.4f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<JuggernautEmblem>());
            recipe.needWater = true;
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.needWater = true;
            recipe.SetResult(ModContent.ItemType<JuggernautEmblem>());
            recipe.AddRecipe();
        }
    }
    public class JuggernautEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Juggernaut Emblem");
            Tooltip.SetDefault("'You have made enemies with balloons covered in hardened clay'"
            + "\n40% increased melee damage"
            + "\n40% decreased melee speed");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.accessory = true;
            item.value = 45000;
            item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeSpeed -= 0.4f;
            player.meleeDamage += 0.4f;
        }
    }
}
