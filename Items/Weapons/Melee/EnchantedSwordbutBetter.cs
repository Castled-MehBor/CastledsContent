using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Melee
{
    public class EnchantedSwordbutBetter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Sword");
            Tooltip.SetDefault("The blade of the legendary hero...");
        }
        public override void SetDefaults()
        {
            item.damage = 35;
            item.melee = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 18;
            item.value = 25000;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = 532;
            item.shootSpeed = 28f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.SwordBeam, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.EnchantedBeam, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.EnchantedBeam, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}