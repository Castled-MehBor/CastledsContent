using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Techno
{
    public class CodebreakerC : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Rift - Prismance");
            Tooltip.SetDefault("'You feel pride.'"
            + "\nFires slightly innacurate bolts of magic.");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.magic = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 100000;
            item.rare = ItemRarityID.LightRed;
            item.mana = 2;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = ProjectileID.DiamondBolt;
            item.shootSpeed = 15f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
            int num = Main.rand.Next(5);
            switch (Main.rand.Next(5))
            {
                case 0:
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.RubyBolt, damage, knockBack, player.whoAmI);
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.DiamondBolt, damage, knockBack, player.whoAmI);
                    break;
                case 1:
                    for (int i = 0; i < 2; i++)
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.SapphireBolt, damage, knockBack, player.whoAmI);
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.EmeraldBolt, damage, knockBack, player.whoAmI);
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.TopazBolt, damage, knockBack, player.whoAmI);
                    break;
                case 4:
                    for (int i = 0; i < 3; i++)
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.AmethystBolt, damage, knockBack, player.whoAmI);
                    break;
                default:
                    break;
            }

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<EmptyCodebreakerRune>(), 1);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.Topaz, 5);
            recipe.AddIngredient(ItemID.Sapphire, 5);
            recipe.AddIngredient(ItemID.Emerald, 5);
            recipe.AddIngredient(ItemID.Amethyst, 5);
            recipe.needWater = true;
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}