using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Armor.Summoner.HavocWasp
{
	[AutoloadEquip(EquipType.Body)]
	public class HWBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havoc Wasp's Torso");
			Tooltip.SetDefault("+1 Max Minions"
				+ "\n18% increased minion damage'");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.18f;
			player.maxMinions++;
		}
	}
}