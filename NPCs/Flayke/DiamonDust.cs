
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Flayke
{
    public class DiamonDust : ModDust
    {
        float[] vals = new float[2];
        public override void SetDefaults()
        {
            updateType = -1;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.alpha = 0;
        }
        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, new Vector3(0, (Main.DiscoG + 125) * 0.00125f, (Main.DiscoB + 125) * 0.00125f));
            vals[0] += 0.001f;
            dust.rotation = (float)dust.customData - vals[0];
            dust.alpha += 3;
            if (dust.alpha > 254)
                dust.active = false;
            return false;
        }
    }
}