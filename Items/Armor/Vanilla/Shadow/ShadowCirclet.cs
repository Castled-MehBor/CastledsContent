using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Vanilla.Shadow
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowCirclet : ModItem
	{
		TempSetBonus setBonus = new TempSetBonus();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Crown");
			Tooltip.SetDefault("10% increased minion damage"
				+ "\nIncreases your max number of minions by 1");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 14;
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return (body.type == ItemID.ShadowScalemail || body.type == ItemID.AncientShadowScalemail) && (legs.type == ItemID.ShadowGreaves || legs.type == ItemID.AncientShadowGreaves);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increased movement speed by 25%\nYour minion damage scales with horizontal velocity, and your damage reduction scales with vertical velocity";
			//player.GetModPlayer<CastledPlayer>().ShadowSummon = true;
			setBonus.BuffPlayer(player, "ShadowSummon");
			player.moveSpeed += 0.25f;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.12f;
			player.maxMinions++;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
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