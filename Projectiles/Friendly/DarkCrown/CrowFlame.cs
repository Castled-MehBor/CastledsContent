using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.DarkCrown
{
    public class CrowFlame : ModProjectile
    {
        public bool noise = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nevermore's Breath");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }
        public float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        public override void AI()
        {
            Timer++;
            if (Timer > 120)
            {
                projectile.tileCollide = true;
            }
            if (!noise)
            {
                Main.PlaySound(SoundID.Item74.WithVolume(0.10f), projectile.position);
                noise = true;
            }
            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("DarkFlame2"), 0f, 0f, 100, default(Color), 1.2f);
            Dust val = Main.dust[num];
            val = Main.dust[num];
            Dust val2 = val;
            val2.velocity *= 0.2f;
            Main.dust[num].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item74.WithVolume(0.35f), projectile.position);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 8f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8f, 0f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6f, -6f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6f, 6f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6f, -6f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6f, 6f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -8f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8f, 0f, mod.ProjectileType("CrowFlameBlast"), (int)(projectile.damage * 0.75), 3f, projectile.owner, 0f, 0f);
        }
    }
}