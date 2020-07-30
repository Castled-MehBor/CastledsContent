using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.FX
{
	public class CleanupShipPulse : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulse Image");
		}

		public override void SetDefaults()
		{
			projectile.width = 210;
			projectile.height = 128;
			projectile.aiStyle = 0;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
		}

		public override void AI()
		{
			projectile.scale += 0.01f;
			projectile.alpha += 5;
		}
	}
}
