using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Vanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class HarpyLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harpy Leg Protectors");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.value = 6500;
			item.rare = ItemRarityID.Blue;
			item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			if (!male)
			{
				equipSlot = mod.GetEquipSlot("HarpyLeggings_FemaleLegs", (EquipType)2);
			}
		}
	}
}
