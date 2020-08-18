using Terraria.ModLoader;

namespace CastledsContent.Projectiles.RobotInvasion.FX
{
	public class RobotRadar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radar");
		}

		public override void SetDefaults()
		{
			projectile.width = 488;
			projectile.height = 1100;
			projectile.aiStyle = 0;
			projectile.scale = 0.01f;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 400;
			projectile.alpha = 100;
		}
		public float Timer
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void AI()
		{
			Timer++;
			if (Timer < 100)
            {
				projectile.scale += 0.01f;
				projectile.alpha -= (int)0.5;
				projectile.position.Y -= 2;
			}
			else if (Timer < 200)
            {
				projectile.alpha += 4;
				projectile.scale += 0.01f;
				projectile.position.Y -= 2;
			}
		}
	}
}
