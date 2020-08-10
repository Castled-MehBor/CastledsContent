using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Material
{
	public class UsedHeroMagazine : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Used Hero Magazine"); 
			Tooltip.SetDefault("It's not broken, just used... And also very dirty.");
		}

		public override void SetDefaults() 
		{
            item.width = 18;
            item.height = 40;
            item.maxStack = 999;
            item.value = 2000000;
            item.rare = 8;
		}
	}
}
