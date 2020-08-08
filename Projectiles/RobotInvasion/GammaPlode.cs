using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion
{
	public class GammaPlode : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma Blast");
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 0;
			projectile.scale = 0.5f;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
		}

		public override void AI()
		{
			projectile.scale += 0.05f;
			projectile.alpha += 3;
		}
	}
}
