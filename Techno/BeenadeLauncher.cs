using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Techno
{
	public class BeenadeLauncher : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Beenade Launcher"); 
			Tooltip.SetDefault("All the bees.");
		}

		public override void SetDefaults() 
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 80;
			item.height = 22;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 70000;
			item.rare = 3;
			item.UseSound = SoundID.Item11;
			item.shoot = 183;
            item.shootSpeed = 12f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeGun, 1);
			recipe.AddIngredient(ItemID.Beenade, 600);
			recipe.AddIngredient(ItemID.BeesKnees, 1);
			recipe.AddIngredient(mod.ItemType("UsedGadget"), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}