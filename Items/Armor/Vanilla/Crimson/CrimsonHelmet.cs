using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Crimson
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonHelmet : ModItem
	{
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
			player.setBonus = "Greatly Increased life regen\nAttacking with a weapon up close will grant 'Crimtane Purge'.\nThis buff increases melee damage and defense, and can be increasd by attacking more.\nA maximum of 15 purge can be attained.";
			player.GetModPlayer<CastledPlayer>().CrimMelee = true;
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