using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Crimson
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Hat");
			Tooltip.SetDefault("8% increased magic damage"
				+ "\n2% increased magic critical strike chance"
				+ "\nIncreases maximum mana by 30");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
			item.defense = 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.CrimsonScalemail && legs.type == ItemID.CrimsonGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Greatly Increased life regen\nYou will gain mana when hurt";
			player.GetModPlayer<CastledPlayer>().CrimMage = true;
			player.crimsonRegen = true;
			player.magicCuffs = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.08f;
			player.magicCrit += 2;
			player.statManaMax2 += 30;
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