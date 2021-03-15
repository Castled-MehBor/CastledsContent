using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Shadow
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowHelmet : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Helmet");
			Tooltip.SetDefault("12% increased ranged critical strike chance"
				+ "\n7% increased movement speed"
				 +"\n'Shadow Helmet, but awesome.'");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.rare = ItemRarityID.Blue;
			item.defense = 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return (body.type == ItemID.ShadowScalemail || body.type == ItemID.AncientShadowScalemail) && (legs.type == ItemID.ShadowGreaves || legs.type == ItemID.AncientShadowGreaves);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increased movement speed by 15%\nYour ranged damage scales with horizontal velocity\nReduced ammo consumption above a specific vertical velocity";
			//player.GetModPlayer<CastledPlayer>().ShadowRange = true;
			setBonus.BuffPlayer(player, "ShadowRanged");
			player.moveSpeed += 0.15f;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.07f;
			player.rangedCrit += 12;
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

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShadowHelmet);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(ItemID.ShadowHelmet);
			recipe.AddRecipe();
		}
	}
}