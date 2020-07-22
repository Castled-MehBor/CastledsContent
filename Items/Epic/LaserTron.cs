using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Epic
{
    public class LaserTron : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LaserTron v.1.0575");
            Tooltip.SetDefault("Fires a spread of green, blue and death lasers."
            + "\nDeath lasers pierce many enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.magic = true;
            item.mana = 8;
            item.width = 46;
            item.height = 24;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 50000;
            item.rare = 4;
            item.UseSound = SoundID.Item10;
            item.autoReuse = true;
            item.shoot = 88;
            item.shootSpeed = 22f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(35);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DeathLaserFriendly"), damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GreenLaser, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GreenLaser, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GreenLaser, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
