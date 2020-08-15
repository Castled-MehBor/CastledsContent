using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Shadow
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowHeadgear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Headgear");
			Tooltip.SetDefault("8% increased magic critical strike chance"
				+ "\n7% increased movement speed"
				+ "\nIncreases maximum mana by 30");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.defense = 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return (body.type == ItemID.ShadowScalemail || body.type == ItemID.AncientShadowScalemail) && (legs.type == ItemID.ShadowGreaves || legs.type == ItemID.AncientShadowGreaves);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+15% increaesd movement speed\nYou will gain mana when hurt";
			player.GetModPlayer<CastledPlayer>().ShadowMage = true;
			player.moveSpeed += 0.15f;
			player.magicCuffs = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.07f;
			player.magicCrit += 8;
			player.statManaMax2 += 30;
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