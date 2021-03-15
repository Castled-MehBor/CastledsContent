using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using System.Collections.Generic;
using CastledsContent.Projectiles.Friendly.HarpyQueen;

namespace CastledsContent.Items.Weapons.Melee
{
	public class HarpyArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Appendage of Her Highness");
			Tooltip.SetDefault("'Despite all my rage'\nWield one of the arms of the Harpy Queen.\nHold the attack to increase power and precision");
		}
		public override void SetDefaults()
		{
			item.damage = 35;
			item.melee = true;
			item.width = 22;
			item.height = 44;
			item.crit = 5;
			item.useTime = 10;
			item.useAnimation = 10;
			item.reuseDelay = 5;
			item.useTurn = true;
			item.channel = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 6f;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.rare = ItemRarityID.Orange;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 0.1f;
		}
		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<HarpyArmProjectile>()] < 1;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HarpyArmProjectile>()] < 1)
			{
				player.direction = (player.Center.X < Main.mouseX + Main.screenPosition.X) ? 1 : (-1);
				int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<HarpyArmProjectile>(), damage, knockBack, player.whoAmI, 0f, 0f);
				//Main.projectile[num].rotation = (float)Math.Atan2((Main.mouseY + Main.screenPosition.Y - Main.projectile[num].Center.Y), (Main.mouseX + Main.screenPosition.X - Main.projectile[num].Center.X));
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