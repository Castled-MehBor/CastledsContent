using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Weapons
{
    public class NightsBump : ModItem
    {
        public bool isMelee = false;
        public bool isRanged = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nights Bump");
            Tooltip.SetDefault("[c/66ff66:The thing that goes bump in the night...]"
            + "\nCan damage enemies with melee.");
        }

        public override void SetDefaults()
        {

                item.damage = 14;
                item.ranged = true;
                item.width = 52;
                item.height = 28;
                item.useTime = 8;
                item.useAnimation = 8;
                item.useStyle = 5;
                item.noMelee = true;
                item.knockBack = 1;
                item.value = 85000;
                item.noMelee = false;
                item.rare = 4;
                item.UseSound = SoundID.Item11;
                item.shoot = ProjectileID.Bullet;
                item.shootSpeed = 18f;
                item.useAmmo = AmmoID.Bullet;
                item.autoReuse = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddIngredient(ItemID.WaterCandle);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddIngredient(ItemID.WaterCandle);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}