using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Epic
{
    public class BayonettaKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Witch Killer");
            Tooltip.SetDefault("'Did somebody say... Witch Hunt?'"
            + "\nFires rounds of bullets, crystal bullets and onyx blasts");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.ranged = true;
            item.width = 64;
            item.height = 30;
		 	item.useAnimation = 30;
			item.useTime = 10;
			item.reuseDelay = 33;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 75000;
            item.rare = 8;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 2);
        }


        public override bool ConsumeAmmo(Player player)
		{
			return !(player.itemAnimation < item.useAnimation - 2);
		}


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.CrystalBullet, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.CrystalBullet, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.CrystalBullet, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BlackBolt, damage, knockBack, player.whoAmI);
            return true;
		}
    }
}
