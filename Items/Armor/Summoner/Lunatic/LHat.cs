using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Summoner.Lunatic
{
	[AutoloadEquip(EquipType.Head)]
	public class LHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Hat");
			Tooltip.SetDefault("4% increased Minion Damage");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<LShirt>() && legs.type == ItemType<LShoes>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 Defense\nHaving a minion summoned will greatly increase your movement speed";
			player.GetModPlayer<CastledPlayer>().lunatic = true;
			player.statDefense += 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.04f;
		}
	}
}