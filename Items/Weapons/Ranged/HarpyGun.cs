using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using System.Collections.Generic;
using CastledsContent.Projectiles.Friendly.HarpyQueen;

namespace CastledsContent.Items.Weapons.Ranged
{
	public class HarpyGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinion Talon");
			Tooltip.SetDefault("'The things I would give to see a harpy hold a gun...'\n'Heck, I would pay in platinum coins to see a harpy hold anything!'\nFires balls of feathers that explode into three homing feathers");
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 44;
			item.height = 32;
			item.crit = 5;
			item.useTime = 10;
			item.useAnimation = 10;
			item.reuseDelay = 5;
			item.channel = true;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 3f;
			item.value = Item.sellPrice(0, 7, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 0.1f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HarpyGunProjectile>()] < 1)
			{
				player.direction = (player.Center.X < Main.mouseX + Main.screenPosition.X) ? 1 : (-1);
				int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<HarpyGunProjectile>(), damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num].rotation = (float)Math.Atan2((Main.mouseY + Main.screenPosition.Y - Main.projectile[num].Center.Y), (Main.mouseX + Main.screenPosition.X - Main.projectile[num].Center.X));
			}
			return false;
		}
		#region Harpy Queen Hook
		public override void ModifyTooltips(List<TooltipLine> list)
		{

			foreach (TooltipLine item in list)
			{
				if (item.mod == "Terraria" && item.Name == "ItemName")
				{
					item.overrideColor = new Color(150, 0, 100);
				}
			}
		}
		#endregion
	}
}
