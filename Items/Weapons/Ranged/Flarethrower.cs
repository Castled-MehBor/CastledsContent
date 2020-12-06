using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class Flarethrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Flarethrower");
            Tooltip.SetDefault("50% chance to not consume gel"
            + "\nSometimes creates wavvy sparks that will inflict Daybroken");
        }

        public override void SetDefaults()
        {

            item.damage = 85;
            item.ranged = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 125000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item74.WithVolume(0.25f);
            item.shoot = ProjectileID.Flames;
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Gel;
            item.autoReuse = true;

        }
        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(2) == 0)
            {
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.Next(2) == 0)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<Projectiles.Friendly.FireSpark>(), damage * 3, knockBack * 3, player.whoAmI);
                Main.PlaySound(SoundID.DD2_FlameburstTowerShot.WithVolume(0.45f));
            }
            return true;
        }
    }
}