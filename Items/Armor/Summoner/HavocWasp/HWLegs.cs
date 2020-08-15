using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Summoner.HavocWasp
{
	[AutoloadEquip(EquipType.Legs)]
	public class HWLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havoc Wasp's Legs");
			Tooltip.SetDefault("+1 Max Minion"
				+ "\n12% increased Minion Damage"
				+ "\n10% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.rare = ItemRarityID.Orange;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.12f;
			player.maxMinions++;
			player.moveSpeed += 0.1f;
		}
	}
}