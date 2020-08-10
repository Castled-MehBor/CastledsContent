using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons
{
    public class TL27 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TL27");
            Tooltip.SetDefault("[c/00fff9:Even the Doomslayer wished he had this...]"
            + "\nRight Click to zoom out"
            + "\nFires an onslaught of homing and high-speed destruction.");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 62;
            item.height = 24;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 15;
            item.value = 400000;
            item.rare = 9;
            item.UseSound = SoundID.Item11;
            item.shoot = ProjectileID.BulletHighVelocity;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 90f;
            item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3 + Main.rand.Next(3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BulletHighVelocity, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            player.scope = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TrueNightsBump>(), 1);
            recipe.AddIngredient(ItemType<TrueQuickDraw>(), 1);
            recipe.AddIngredient(ItemID.RifleScope, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}