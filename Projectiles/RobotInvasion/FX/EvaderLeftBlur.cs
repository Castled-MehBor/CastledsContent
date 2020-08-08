using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.FX
{
	public class EvaderLeftBlur : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blur Image");
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 50;
			projectile.aiStyle = 0;
			projectile.alpha = 80;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 20;
		}

		public override void AI()
		{
			if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = 1;
			}
			else
			{
				projectile.spriteDirection = -1;
			}
			if (projectile.timeLeft < 16)
			{
				projectile.alpha = 110;
			}
			if (projectile.timeLeft < 12)
			{
				projectile.alpha = 140;
			}
			if (projectile.timeLeft < 8)
			{
				projectile.alpha = 180;
			}
			if (projectile.timeLeft < 2)
			{
				projectile.alpha = 220;
			}
		}
	}
}
