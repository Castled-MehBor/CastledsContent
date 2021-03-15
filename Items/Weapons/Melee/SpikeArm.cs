using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using System.Collections.Generic;
using CastledsContent.Projectiles.Friendly;

namespace CastledsContent.Items.Weapons.Melee
{
	public class SpikeArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spike Exo-arm");
			Tooltip.SetDefault("'It forms lead from air... it's magic!'\nWeild an Exo-Arm\nOn contact with an enemy, it will become primed\nWhen primed, it will launch a spike ball when released");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 46;
			item.height = 46;
			item.crit = 5;
			item.useTime = 10;
			item.useAnimation = 10;
			item.reuseDelay = 5;
			item.channel = true;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 6f;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.rare = ItemRarityID.Blue;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 0.1f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SpikeArmPro2>()] < 1)
            {
				player.direction = (player.Center.X < Main.mouseX + Main.screenPosition.X) ? 1 : (-1);
				int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<SpikeArmPro2>(), damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num].rotation = (float)Math.Atan2((Main.mouseY + Main.screenPosition.Y - Main.projectile[num].Center.Y), (Main.mouseX + Main.screenPosition.X - Main.projectile[num].Center.X));
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
			recipe.AddIngredient(ModContent.ItemType<Accessories.RobotInvasion.RobotPlate>(), 10);
			recipe.AddIngredient(ItemID.Obsidian, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		#region Robot Invasion Hook
		public override void ModifyTooltips(List<TooltipLine> list)
		{

			foreach (TooltipLine item in list)
			{
				if (item.mod == "Terraria" && item.Name == "ItemName")
				{
					item.overrideColor = new Color(215, 135, 95);
				}
			}
			int num = -1;
			int num2 = 0;
			while (num2 < list.Count)
			{
				if (!list[num2].Name.Equals("ItemName"))
				{
					num2++;
					continue;
				}
				num = num2;
				break;
			}
		}
		#endregion
	}
}
