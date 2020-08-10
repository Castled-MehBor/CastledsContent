using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class QueenBee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Queen Bee");
            Tooltip.SetDefault("'Contrary to some, this does NOT fire stingers.'"
            + "\nFires bees, bees and MORE bees!"
            + "\nand as usual, converts normal arrows into EVEN MORE BEES!!!");
        }

        public override void SetDefaults()
        {
            item.damage = 34;
            item.ranged = true;
            item.width = 26;
            item.height = 56;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = 4;
            item.UseSound = SoundID.Item17;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 32;
            item.useAmmo = AmmoID.Arrow;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.BeeArrow;
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Bee, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BeeArrow, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BeeArrow, damage, knockBack, player.whoAmI);
            return true;
        }
    }
}