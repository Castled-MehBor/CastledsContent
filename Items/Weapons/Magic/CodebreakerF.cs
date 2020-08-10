using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Magic
{
	public class CodebreakerF : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Reality Rift - Pyromance"); 
			Tooltip.SetDefault("'All you can see is orange for miles.'"
			+ "\nFires varying arrays of fire projectiles.");
		}

		public override void SetDefaults() 
		{     
            item.damage = 25;                        
            item.magic = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 5;
            item.value = 10000;
            item.rare = (-11);
            item.mana = 6;
            item.UseSound = SoundID.Item8;           
            item.autoReuse = true;
            item.shoot = ProjectileID.Flamelash;
            item.shootSpeed = 12f;  
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flames, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
			}
			if (num == 1)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flames, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BallofFire, damage, knockBack, player.whoAmI);
			}
			if (num == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BallofFire, damage, knockBack, player.whoAmI);
			}
			if (num == 3)
			{
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flamelash, damage, knockBack, player.whoAmI);
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BallofFire, damage, knockBack, player.whoAmI);
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BallofFire, damage, knockBack, player.whoAmI);
				}
				if (num == 4)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flames, damage, knockBack, player.whoAmI);
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flames, damage, knockBack, player.whoAmI);
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Flames, damage, knockBack, player.whoAmI);
				}
			}
			return false;
		}
	}
}