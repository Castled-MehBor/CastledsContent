using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Techno
{
	public class EmptyCodebreakerRune : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Unactivated Reality Rift"); 
			Tooltip.SetDefault("'Doesn't seem to be anything here...'"
			+ "\nCombining with either gems or fire will activate the rift.");
		}

		public override void SetDefaults() 
		{
            item.width = 34;
            item.height = 34;
            item.maxStack = 99;
            item.value = 35000;
            item.rare = ItemRarityID.LightRed;
        }
         
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 75);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddIngredient(ItemID.Timer1Second, 1);
			recipe.AddIngredient(ItemType<UsedGadget>(), 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
