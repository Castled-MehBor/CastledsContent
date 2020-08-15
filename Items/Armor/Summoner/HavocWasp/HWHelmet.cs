using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace CastledsContent.Items.Armor.Summoner.HavocWasp
{
	[AutoloadEquip(EquipType.Head)]
	public class HWHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havoc Wasp's Head");
			Tooltip.SetDefault("+1 Max Minion"
				+ "\n12% increased Minion Damage"
				+ "\n20% increased use speed");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Orange;
			item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<HWBody>() && legs.type == ItemType<HWLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "All of your minions will either ignite or poison hit enemies\nA dead wasp will protect you";
			player.GetModPlayer<CastledPlayer>().havocWasp = true;
			player.statDefense += 5;
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
			player.armorEffectDrawShadowSubtle = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.12f;
			player.maxMinions++;
			player.pickSpeed += 0.2f;
			Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ProjectileID.Hornet, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}