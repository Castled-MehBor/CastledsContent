﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.LightMage
{
    public class TargetProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Target");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.timeLeft = 60;
            projectile.velocity.X = 10f;
            projectile.velocity.Y = 10f;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, default(Color), 1f);
            Dust val = Main.dust[num];
            val = Main.dust[num];
            Dust val2 = val;
            val2.velocity *= 0.2f;
            Main.dust[num].noGravity = true;
        }
    }
}