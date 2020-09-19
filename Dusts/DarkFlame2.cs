using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Dusts
{
	public class DarkFlame2 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 1.3f;
		}

		public override bool MidUpdate(Dust dust)
		{
			float num = dust.scale * 1.25f;
			if (num > 1f)
			{
				num = 1f;
			}
			Lighting.AddLight(dust.position, 0.15f * num, 0.15f * num, 0.15f * num);
			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color((float)(int)lightColor.R * 0.4f, (float)(int)lightColor.G * 0.4f, (float)(int)lightColor.B * 0.4f, 0f);
		}
	}
}
