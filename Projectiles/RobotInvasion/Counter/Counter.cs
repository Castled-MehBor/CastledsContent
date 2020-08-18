using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace CastledsContent.Projectiles.RobotInvasion.Counter
{
	public class Counter : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Counter");
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 24;
			projectile.aiStyle = 0;
			projectile.light = 3f;
			projectile.scale = 0.75f;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
            Main.projFrames[projectile.type] = 21;
		}

		public override void AI()
		{
			if (CastledWorld.counterType == 1)
            {
				projectile.scale += 0.01f;
				projectile.alpha += 5;
			}
			else if (CastledWorld.counterType == 2)
			{
				projectile.timeLeft = 600;
			}
		}
		public override void PostAI()
		{
			switch (CastledWorld.counterType)
            {
				case 1:
					{
						if (CastledWorld.numberOfEnemies < 21)
						{
							projectile.frame = CastledWorld.numberOfEnemies;
						}
						break;
					}
				case 2:
					{
						projectile.frame = CastledWorld.waveDelayCountdown;
						break;
					}
			}
		}
	}
}
