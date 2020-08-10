using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Items.Weapons.Ranged;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Material
{
	public class UsedHeroMagazine : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Used Hero Magazine"); 
			Tooltip.SetDefault("It's not broken, just used... And also very dirty.");
		}

		public override void SetDefaults() 
		{
            item.width = 18;
            item.height = 40;
            item.maxStack = 999;
            item.value = 2000000;
            item.rare = 8;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<QuickDraw>(), 1);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<TrueQuickDraw>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<NightsBump>(), 1);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<TrueNightsBump>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TrueNightsBump>(), 1);
            recipe.AddIngredient(ItemType<TrueQuickDraw>(), 1);
            recipe.AddIngredient(ItemID.RifleScope, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<TL27>());
            recipe.AddRecipe();
        }
    }
}
