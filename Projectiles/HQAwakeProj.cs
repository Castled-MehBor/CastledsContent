using Terraria.ModLoader;

namespace CastledsContent.Projectiles
{
	public class HQAwakeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulse Image");
		}

		public override void SetDefaults()
		{
			projectile.width = 150;
			projectile.height = 140;
			projectile.aiStyle = 0;
			projectile.scale = 1f;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
		}
		public override void AI()
		{

			projectile.scale += 0.01f;
			projectile.alpha += 5;
		}
	}
}
