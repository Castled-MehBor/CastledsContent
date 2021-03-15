using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Crimson
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonHelmet : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Helmet");
			Tooltip.SetDefault("8% increased melee damage"
				+ "\n7% increased melee speed"
				+ "\n'Crimson Helmet, but awesome!'");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.CrimsonScalemail && legs.type == ItemID.CrimsonGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Greatly Increased life regen\nYour melee damage scales with life lost, and your swing speed scales with life regeneration";
			//player.GetModPlayer<CastledPlayer>().CrimMelee = true;
			setBonus.BuffPlayer(player, "CrimsonMelee");
			player.crimsonRegen = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.08f;
			player.meleeSpeed += 0.07f;
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 15);
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimsonHelmet);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(ItemID.CrimsonHelmet);
			recipe.AddRecipe();
		}
	}
}