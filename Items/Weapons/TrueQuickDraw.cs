using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.Weapons
{
    public class TrueQuickDraw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Quick Draw");
            Tooltip.SetDefault("[c/ff8d00:The noon sun brightens as you hold it.]"
                            + "\nConverts regular bullets into high-velocity bullets.");
        }

        public override void SetDefaults()
        {
            item.damage = 135;
            item.ranged = true;
            item.width = 44;
            item.height = 26;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.crit = 25;
            item.noMelee = true;
            item.knockBack = 20;
            item.value = 180000;
            item.rare = 8;
            item.UseSound = SoundID.Item41;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 45f;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -4);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("QuickDraw"), 1);
            recipe.AddIngredient(mod.ItemType("UsedHeroMagazine"), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}