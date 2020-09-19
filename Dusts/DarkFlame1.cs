using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Dusts
{
    public class DarkFlame1 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }
        public override bool Update(Dust dust)
        {
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.color.R / 0f * 0f, dust.color.G / 0f * 0f, dust.color.B / 0f * 0f);
            return true;
        }
    }
}