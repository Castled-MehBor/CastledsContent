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
			//sorry for the spaghetti code :/
			//Enemy Counter
			if (CastledWorld.counterType == 1)
            {
				if (CastledWorld.numberOfEnemies == 20)
				{
					projectile.frame = 20;
				}
				if (CastledWorld.numberOfEnemies == 19)
				{
					projectile.frame = 19;
				}
				if (CastledWorld.numberOfEnemies == 18)
				{
					projectile.frame = 18;
				}
				if (CastledWorld.numberOfEnemies == 17)
				{
					projectile.frame = 17;
				}
				if (CastledWorld.numberOfEnemies == 16)
				{
					projectile.frame = 16;
				}
				if (CastledWorld.numberOfEnemies == 15)
				{
					projectile.frame = 15;
				}
				if (CastledWorld.numberOfEnemies == 14)
				{
					projectile.frame = 14;
				}
				if (CastledWorld.numberOfEnemies == 13)
				{
					projectile.frame = 13;
				}
				if (CastledWorld.numberOfEnemies == 12)
				{
					projectile.frame = 12;
				}
				if (CastledWorld.numberOfEnemies == 11)
				{
					projectile.frame = 11;
				}
				if (CastledWorld.numberOfEnemies == 10)
				{
					projectile.frame = 10;
				}
				if (CastledWorld.numberOfEnemies == 9)
				{
					projectile.frame = 9;
				}
				if (CastledWorld.numberOfEnemies == 8)
				{
					projectile.frame = 8;
				}
				if (CastledWorld.numberOfEnemies == 7)
				{
					projectile.frame = 7;
				}
				if (CastledWorld.numberOfEnemies == 6)
				{
					projectile.frame = 6;
				}
				if (CastledWorld.numberOfEnemies == 5)
				{
					projectile.frame = 5;
				}
				if (CastledWorld.numberOfEnemies == 4)
				{
					projectile.frame = 4;
				}
				if (CastledWorld.numberOfEnemies == 3)
				{
					projectile.frame = 3;
				}
				if (CastledWorld.numberOfEnemies == 2)
				{
					projectile.frame = 2;
				}
				if (CastledWorld.numberOfEnemies == 1)
				{
					projectile.frame = 1;
				}
				if (CastledWorld.numberOfEnemies == 0)
				{
					projectile.frame = 0;
				}
			}
			//Countdown
			if (CastledWorld.counterType == 2)
			{
				if (CastledWorld.waveDelayCountdown == 10)
				{
					projectile.frame = 10;
				}
				if (CastledWorld.waveDelayCountdown == 9)
				{
					projectile.frame = 9;
				}
				if (CastledWorld.waveDelayCountdown == 8)
				{
					projectile.frame = 8;
				}
				if (CastledWorld.waveDelayCountdown == 7)
				{
					projectile.frame = 7;
				}
				if (CastledWorld.waveDelayCountdown == 6)
				{
					projectile.frame = 6;
				}
				if (CastledWorld.waveDelayCountdown == 5)
				{
					projectile.frame = 5;
				}
				if (CastledWorld.waveDelayCountdown == 4)
				{
					projectile.frame = 4;
				}
				if (CastledWorld.waveDelayCountdown == 3)
				{
					projectile.frame = 3;
				}
				if (CastledWorld.waveDelayCountdown == 2)
				{
					projectile.frame = 2;
				}
				if (CastledWorld.waveDelayCountdown == 1)
				{
					projectile.frame = 1;
				}
				if (CastledWorld.waveDelayCountdown == 0)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}
