﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class FlaskIchor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottled Ichor");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 22;
            projectile.CloneDefaults(ProjectileID.DrManFlyFlask);
            aiType = ProjectileID.DrManFlyFlask;
            projectile.timeLeft = 1200;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 10f, -10f, ProjectileID.GoldenShowerHostile, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -10f, -10f, ProjectileID.GoldenShowerHostile, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 12f, ProjectileID.GoldenShowerHostile, (int)((double)projectile.damage * 0.8), 3f, projectile.owner, 0f, 0f);
        }
    }
}