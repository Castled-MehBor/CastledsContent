using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class CursedFlameRot5 : ModProjectile
    {
        public bool attackStyle1;
        public bool attackStyle2;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flame");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 75;
            projectile.light = 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override bool PreAI()
        {
            if (Timer == 1)
            {
                int num = Main.rand.Next(2);
                if (num == 0)
                {
                    attackStyle1 = true;
                }
                if (num == 1)
                {
                    attackStyle2 = true;
                }
            }
            return true;
        }
        public override void AI()
        {
            Timer++;
            if (Timer == 2)
            {
                if (attackStyle1 == true)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 14f, mod.ProjectileType("WarningProj"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, 13f, mod.ProjectileType("WarningProj"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, 13f, mod.ProjectileType("WarningProj"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                }
                else if (attackStyle2 == true)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, -13f, mod.ProjectileType("WarningProj"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, -13f, mod.ProjectileType("WarningProj"), (int)((double)projectile.damage * 0), 3f, projectile.owner, 0f, 0f);
                }
            }
            if (Timer > 75)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            if (attackStyle1 == true)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -14f, mod.ProjectileType("TrueCursedFireball"), projectile.damage = 20, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, -13f, 96, projectile.damage = 16, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, -13f, 96, projectile.damage = 16, 3f, projectile.owner, 0f, 0f);
            }
            else if (attackStyle2 == true)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, -13f, 96, projectile.damage = 22, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, -13f, 96, projectile.damage = 22, 3f, projectile.owner, 0f, 0f);
            }
        }
    }
}