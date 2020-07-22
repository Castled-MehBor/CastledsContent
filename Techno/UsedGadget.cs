using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Techno
{
	public class UsedGadget : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Home-made Motherboard"); 
			Tooltip.SetDefault("'The wonders of Terrarian brains!");
		}

		public override void SetDefaults() 
		{
            item.width = 22;
            item.height = 24;
            item.maxStack = 99;
            item.value = 15000;
            item.rare = 4;
        }
         
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddIngredient(ItemID.Actuator, 10);
			recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
