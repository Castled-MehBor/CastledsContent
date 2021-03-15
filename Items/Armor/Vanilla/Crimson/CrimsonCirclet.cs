using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Crimson
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonCirclet : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Circlet");
			Tooltip.SetDefault("10% increased minion damage"
				+ "\nIncreases your max number of minions by 1");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 14;
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.CrimsonScalemail && legs.type == ItemID.CrimsonGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Greatly Increased life regen\nYour minion damage scales with life lost, and your damage reduction scales with life regeneration";
			//player.GetModPlayer<CastledPlayer>().CrimSummon = true;
			setBonus.BuffPlayer(player, "CrimsonSummon");
			player.crimsonRegen = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.12f;
			player.maxMinions += 1;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
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