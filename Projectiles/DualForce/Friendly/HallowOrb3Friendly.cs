using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.DualForce.Friendly
{
    public class HallowOrb3Friendly : ModProjectile
    {
        public bool attackStyle1;
        public bool attackStyle2;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Light");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 100;
            projectile.timeLeft = 25;
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
            if (Timer > 25)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            if (attackStyle1 == true)
            {
                Main.PlaySound(SoundID.DD2_WitherBeastDeath, projectile.position);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 13f, -13f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -13f, 13f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -13f, -13f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 13f, 13f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            }
            else if (attackStyle2 == true)
            {
                Main.PlaySound(SoundID.DD2_WitherBeastDeath, projectile.position);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 14f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 14f, 0f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -14f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -14f, 0f, mod.ProjectileType("LightSparkFriendly"), (int)((double)projectile.damage * 1.5), 3f, projectile.owner, 0f, 0f);
            }
        }
    }
}