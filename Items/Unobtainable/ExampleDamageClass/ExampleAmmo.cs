using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable.ExampleDamageClass
{
    public class ExampleAmmo : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.DD2EnergyCrystal;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Mana");
            Tooltip.SetDefault("Test Description");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.rare = 8;
            item.ammo = item.type;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DD2EnergyCrystal);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}