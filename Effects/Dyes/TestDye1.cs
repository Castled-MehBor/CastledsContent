using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Effects.Dyes
{
	public class TestDye1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Equinox Dye");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 24;
			item.maxStack = 99;
			item.rare = ItemRarityID.Orange;
			item.value = 15000;
			item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
		}
	}
}