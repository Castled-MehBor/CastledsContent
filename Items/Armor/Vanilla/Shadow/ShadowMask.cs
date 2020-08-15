using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Shadow
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowMask : ModItem
	{
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

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return (body.type == ItemID.ShadowScalemail || body.type == ItemID.AncientShadowScalemail) && (legs.type == ItemID.ShadowGreaves || legs.type == ItemID.AncientShadowGreaves);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+15% increaesd movement speed\nAttacking with a weapon up close will grant 'Corrupt Purge'.\nThis buff increases melee crit chance and melee attack speed, and can be increasd by attacking more.\nA maximum of 15 purge can be attained.";
			player.GetModPlayer<CastledPlayer>().ShadowMelee = true;
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