using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Magic
{
    public class Monsoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monsoon");
            Tooltip.SetDefault("'Feel the wrath of the ocean!'");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.magic = true;
            item.channel = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 35;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 10000;
            item.rare = 4;
            item.mana = 14;
            item.autoReuse = false;
            item.UseSound = SoundID.Item21;
            item.shoot = ProjectileType<Projectiles.Friendly.MonsoonP>();
            item.shootSpeed = 36f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}