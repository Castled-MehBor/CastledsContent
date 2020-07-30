using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.FX
{
    public class Identifier1 : ModProjectile
    {
        public bool attackStyle1;
        public bool attackStyle2;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Identifier");
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
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8f, 0f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6f, -6f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6f, 6f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6f, -6f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6f, 6f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -8f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8f, 0f, mod.ProjectileType("CoolFX1"), projectile.damage = 0, 3f, projectile.owner, 0f, 0f);
        }
    }
}