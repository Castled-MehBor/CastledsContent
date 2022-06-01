using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Dusts
{
    public class TarrockDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
        }
    }
    public class IonyxDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
        }
    }
    public class SludgeDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
        }
    }
}