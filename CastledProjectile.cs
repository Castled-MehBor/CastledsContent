using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent;
using static Terraria.ModLoader.ModContent;


namespace CastledsContent
{
    public class CastledProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        //Lightful
        public bool isLightful;

        public override void SetDefaults(Projectile projectile)
        {
            isLightful = false;
        }

    }
}
