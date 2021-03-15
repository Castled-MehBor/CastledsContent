using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.NightDemon
{
    public class IchorRot8 : ModProjectile
    {
        public bool attackStyle1;
        public bool attackStyle2;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.timeLeft = 1;
            projectile.light = 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }

        public override bool PreAI()
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
            return true;
        }

        public override void Kill(int timeLeft)
        {
            if (attackStyle1 == true)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 5f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, 0f, ProjectileID.GoldenShowerHostile, projectile.damage = 18, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, -4f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, 4f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, -4f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, 4f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -5f, ProjectileID.GoldenShowerHostile, projectile.damage = 15, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, 0f, ProjectileID.GoldenShowerHostile, projectile.damage = 18, 3f, projectile.owner, 0f, 0f);
            }
            else if (attackStyle2 == true)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8f, ProjectileID.GoldenShowerHostile, projectile.damage = 25, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8f, 0f, ProjectileID.GoldenShowerHostile, projectile.damage = 25, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -7f, ProjectileID.GoldenShowerHostile, projectile.damage = 25, 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -7f, 0f, ProjectileID.GoldenShowerHostile, projectile.damage = 25, 3f, projectile.owner, 0f, 0f);
            }
        }
    }
}