using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Crimson
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonHeadgear : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Headgear");
			Tooltip.SetDefault("10% increased ranged damage"
				+"\n3% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.CrimsonScalemail && legs.type == ItemID.CrimsonGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Greatly Increased life regen\nYour ranged damage scales with life lost\nReduced ammo consumption if you have enough life regeneration";
			//player.GetModPlayer<CastledPlayer>().CrimRange = true;
			setBonus.BuffPlayer(player, "CrimsonRanged");
			player.crimsonRegen = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.1f;
			player.rangedCrit += 4;
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
		}
	}
}