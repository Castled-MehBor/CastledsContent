using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Armor.Summoner.Lunatic
{
	[AutoloadEquip(EquipType.Body)]
	public class LShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Shirt");
			Tooltip.SetDefault("+1 Max Minions"
				+ "\n5% increased minion damage'");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.rare = ItemRarityID.White;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.05f;
			player.maxMinions++;
		}
	}
}