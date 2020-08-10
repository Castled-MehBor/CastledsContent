using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Material
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

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.Topaz, 5);
            recipe.AddIngredient(ItemID.Sapphire, 5);
            recipe.AddIngredient(ItemID.Emerald, 5);
            recipe.AddIngredient(ItemID.Amethyst, 5);
            recipe.needWater = true;
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemType<Weapons.Magic.CodebreakerC>());
            recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddIngredient(ItemID.FlowerofFire);
			recipe.AddIngredient(ItemID.Fireblossom, 5);
			recipe.needLava = true;
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemType<Weapons.Magic.CodebreakerF>());
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddIngredient(ItemID.Flamelash);
			recipe.AddIngredient(ItemID.Fireblossom, 5);
			recipe.needLava = true;
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemType<Weapons.Magic.CodebreakerF>());
			recipe.AddRecipe();
		}
	}
}
