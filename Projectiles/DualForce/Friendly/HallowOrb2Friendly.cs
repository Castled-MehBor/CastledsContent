﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class HallowOrb2Friendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 100;
            projectile.timeLeft = 60;
            projectile.alpha = 10;
            projectile.light = 0.5f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override void AI()
        {
            Timer++;
            Color color = new Color();
            Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)(projectile.position.Y + ((projectile.height - projectile.width) / 2)), projectile.width, projectile.width);
            int count = (int)(Timer / 5);
            for (int i = 1; i <= count; i++)
            {
                int dust = Dust.NewDust(projectile.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item8, projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("HallowOrb3Friendly"), (int)((double)projectile.damage * 1), 3f, projectile.owner, 0f, 0f);
        }
    }
}