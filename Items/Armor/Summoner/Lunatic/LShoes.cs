using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Summoner.Lunatic
{
	[AutoloadEquip(EquipType.Legs)]
	public class LShoes : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Shoes");
			Tooltip.SetDefault("4% increased Minion Damage");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.04f;
		}
	}
}