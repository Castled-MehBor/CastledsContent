using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace CastledsContent.Projectiles.Friendly
{
    public class FeatherBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feather Clump");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.timeLeft = 240;
            projectile.ignoreWater = false;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override void AI()
        {
            Timer++;
            if (Timer > 60)
            {
                projectile.velocity.X--;
            }
            if (projectile.velocity.X < 0 || projectile.velocity.Y < 0)
            {
                projectile.velocity.X = 0;
            }
            Color color = new Color();
            Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)(projectile.position.Y + ((projectile.height - projectile.width) / 2)), projectile.width, projectile.width);
            int count = (int)(Timer / 6);
            for (int i = 1; i <= count; i++)
            {
                int dust = Dust.NewDust(projectile.position, rectangle.Width, rectangle.Height, DustID.AncientLight, 0, 0, 100, color, 0.35f);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.1f), projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 8f, 13f, mod.ProjectileType("HyperFeatherF"), (int)((double)projectile.damage), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, -4f, 13f, mod.ProjectileType("GiantFeatherF"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 0f, 13f, mod.ProjectileType("HyperFeatherF"), (int)((double)projectile.damage), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 4f, 13f, mod.ProjectileType("GiantFeatherF"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 8f, 13f, mod.ProjectileType("HyperFeatherF"), (int)((double)projectile.damage), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 12f, 18f, mod.ProjectileType("GiantFeatherF"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, -6f, 18f, mod.ProjectileType("HyperFeatherF"), (int)((double)projectile.damage), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 0f, 18f, mod.ProjectileType("GiantFeatherF"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 6f, 18f, mod.ProjectileType("HyperFeatherF"), (int)((double)projectile.damage), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 500, 12f, 18f, mod.ProjectileType("GiantFeatherF"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
        }
    }
}