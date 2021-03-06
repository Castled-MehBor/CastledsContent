﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class CrystalSpearFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 100;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.penetrate = 8;
            projectile.timeLeft = 500;
            projectile.velocity.X = 10f;
            projectile.velocity.Y = 10f;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.JavelinFriendly);
            aiType = ProjectileID.JavelinFriendly;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 58, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f);

            Timer++;
            if (Timer > 45)
            {
                int num = Main.rand.Next(2);
                if (num == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 0f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 0f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                    Timer = 0;
                }
                if (num == 1)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 3f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -3f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 0.5), 3f, projectile.owner, 0f, 0f);
                    Timer = 0;
                }
            }
        }
        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = oldVelocity.X * -1f;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = oldVelocity.Y * -1f;
            return false;
        }
        public override void Kill(int timeLeft) { Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f); }
    }
}