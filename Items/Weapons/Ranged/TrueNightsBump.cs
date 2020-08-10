using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class TrueNightsBump : ModItem
    {
        public bool isMelee = false;
        public bool isRanged = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Nights Bump");
            Tooltip.SetDefault("[c/16e3c9:The said bump became a bang...]"
                + "\nCan damage enemies with melee."
                + "\nConverst regular bullets into a mix of cursed and ichor bullets.");
        }

        public override void SetDefaults()
        {

                item.damage = 35;
                item.ranged = true;
                item.width = 52;
                item.height = 28;
                item.useTime = 7;
                item.useAnimation = 7;
                item.useStyle = 5;
                item.noMelee = true;
                item.knockBack = 4;
                item.value = 200000;
                item.noMelee = false;
                item.rare = 8;
                item.UseSound = SoundID.Item11;
                item.shoot = ProjectileID.CursedBullet;
                item.shootSpeed = 18f;
                item.useAmmo = AmmoID.Bullet;
                item.autoReuse = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (isRanged == true)
            {
                if (type == ProjectileID.Bullet)
                {
                    type = ProjectileID.CursedBullet;
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.IchorBullet, damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<NightsBump>(), 1);
            recipe.AddIngredient(ItemType<UsedHeroMagazine>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}