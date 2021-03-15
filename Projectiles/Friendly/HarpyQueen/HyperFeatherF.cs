using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.HarpyQueen
{
    public class HyperFeatherF : ModProjectile
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
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
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