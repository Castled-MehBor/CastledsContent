using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;
namespace CastledsContent.Projectiles.Friendly
{
    public class MonsoonP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monsoon Projectile");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.MagicMissile);
            aiType = ProjectileID.NebulaBlaze2;
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            Projectile.NewProjectile(new Vector2(projectile.position.X + Main.rand.Next(40) - 20f, projectile.position.Y + Main.rand.NextFloat(40) - 20f), Vector2.Zero, ProjectileType<RainCloud>(), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}