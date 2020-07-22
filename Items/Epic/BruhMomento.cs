using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Epic
{
    public class BruhMomento : ModItem
    {
        public bool isMelee = false;
        public bool isRanged = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fleshy Tiger-Claw");
            Tooltip.SetDefault("Aim projectiles down to use original ability (rapid melee attack)"
                + "\nCan be crafted back.");
        }

        public override void SetDefaults()
        {

                item.damage = 95;
                item.melee = true;
                item.width = 28;
                item.height = 22;
                item.useTime = 1;
                item.useAnimation = 1;
                item.useStyle = 1;
                item.knockBack = 1;
                item.value = 75000;
                item.rare = 6;
                item.UseSound = SoundID.Item1;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("SlashRed");
                item.shootSpeed = 5f;
            
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.immune[255] = 0;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.SetResult(ItemID.FetidBaghnakhs);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.SetResult(mod.ItemType("EpicQuartz"));
            recipe.AddRecipe();
        }
    }
}