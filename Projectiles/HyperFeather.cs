using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
    public class HyperFeather : ModProjectile
    {
        public override string Texture => "CastledsContent/Projectiles/Friendly/HarpyQueen/GlazedFeatherSmall";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Small Glazed Feather");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 20;
            projectile.CloneDefaults(ProjectileID.HarpyFeather);
            aiType = ProjectileID.HarpyFeather;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * 1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * 1f;
            }
            return false;
        }
    }
}