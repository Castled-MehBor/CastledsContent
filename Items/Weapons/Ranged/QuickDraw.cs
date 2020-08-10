using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class QuickDraw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Quick Draw");
            Tooltip.SetDefault("[c/e3da10:This was the pistol of a legendary cowboy.]"
            + "\nUpgrade to the Handgun/Phoenix Blaster");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.ranged = true;
            item.width = 38;
            item.crit = 15;
            item.height = 26;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 12;
            item.value = 11000;
            item.rare = 6;
            item.UseSound = SoundID.Item41;
            item.shoot = 242;
            item.shootSpeed = 45f;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -4);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddIngredient(ItemID.SoulofSight, 25);
            recipe.AddIngredient(ItemID.Handgun, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddIngredient(ItemID.SoulofSight, 25);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}