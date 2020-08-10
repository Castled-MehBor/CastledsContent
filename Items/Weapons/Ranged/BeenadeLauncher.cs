using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
	public class BeenadeLauncher : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Beenade Launcher"); 
			Tooltip.SetDefault("All the bees.");
		}

		public override void SetDefaults() 
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 80;
			item.height = 22;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 70000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item11;
			item.shoot = ProjectileID.Beenade;
            item.shootSpeed = 12f;
			item.autoReuse = true;
		}
	}
}