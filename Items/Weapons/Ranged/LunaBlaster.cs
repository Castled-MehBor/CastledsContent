using System;
using CastledsContent.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class LunaBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunatic's Blaster");
            Tooltip.SetDefault("'Why is it made out of worm teeth?'");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 48;
            item.height = 36;
            item.useTime = 85;
            item.useAnimation = 85;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 12500;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ProjectileType<CursedBullet>();
            item.shootSpeed = 2f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-9, -4);

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
          int numberProjectiles = 60 + Main.rand.Next(15);
         for (int i = 0; i < numberProjectiles; i++)
         {
              Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(75));
             Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
         }
         return false;
        }
    }
}