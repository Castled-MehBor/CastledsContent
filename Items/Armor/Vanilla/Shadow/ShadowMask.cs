﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Shadow
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowMask : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Mask");
			Tooltip.SetDefault("10% increased melee critical strike chance"
				+ "\n7% increased melee speed");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
			item.defense = 9;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => (body.type == ItemID.ShadowScalemail || body.type == ItemID.AncientShadowScalemail) && (legs.type == ItemID.ShadowGreaves || legs.type == ItemID.AncientShadowGreaves);

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increased movement speed by 15%\nYour melee damage scales with horizontal velocity, and your swing speed scales with vertical velocity";
			//player.GetModPlayer<CastledPlayer>().ShadowMelee = true;
			setBonus.BuffPlayer(player, "ShadowMelee");
			player.moveSpeed += 0.15f;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeSpeed += 0.07f;
			player.meleeCrit += 10;
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 15);
			recipe.AddIngredient(ItemID.ShadowScale, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}