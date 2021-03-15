using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.HarpyQueen
{
	public class HarpyArmProjectile : ModProjectile
	{
		bool set = false;
		bool chargeFull = false;
		object[] values = new object[2];
		float precision = 140;
		int direction = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harpy Queen's Arm");
		}

		public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 90;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.friendly = true;
			//projectile.alpha = 255;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 30;
			Main.projFrames[projectile.type] = 5;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			projHitbox.Width += 16;
			projHitbox.Height += 16;
			return projHitbox.Intersects(targetHitbox);
		}

		public override void AI()
		{
			Player val = Main.player[projectile.owner];
			projectile.rotation = 0;
			direction = val.direction == -1 ? -1 : 1;
			projectile.spriteDirection = direction * -1;
			if (!set)
			{
				values[0] = projectile.damage;
				values[1] = projectile.knockBack;
				projectile.damage = 0;
				projectile.knockBack = 0;
				set = true;
			}
			if (val.dead)
			{
				projectile.Kill();
			}
			if (val.channel)
			{
				if (precision > 20)
					precision--;
				if (precision <= 20 && !chargeFull)
                {
					Main.PlaySound(SoundID.DD2_BookStaffCast, projectile.position);
					for (int i = 0; i < 15; i++)
					{
						Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i));
						Dust dust = Dust.NewDustPerfect(position, DustID.AmberBolt);
						dust.noGravity = true;
						dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 4;
						dust.noLight = false;
						dust.fadeIn = 1f;
					}
					chargeFull = true;
				}
				projectile.timeLeft = 30;
				val.bodyFrame.Y = val.bodyFrame.Height;
			}
			projectile.ai[0] += 1f;
			if (Main.player[projectile.owner].controlUseTile && projectile.ai[0] > 39f && projectile.ai[0] < 41f)
			{
				projectile.ai[0] = 38f;
			}
			if (!val.channel)
            {
				if (projectile.timeLeft > 26)
					val.bodyFrame.Y = val.bodyFrame.Height;
				if (projectile.timeLeft >= 22 && projectile.timeLeft < 26)
					val.bodyFrame.Y = val.bodyFrame.Height * 2;
				if (projectile.timeLeft >= 18 && projectile.timeLeft < 22)
					val.bodyFrame.Y = val.bodyFrame.Height * 2;
				if (projectile.timeLeft >= 14 && projectile.timeLeft < 18)
					val.bodyFrame.Y = val.bodyFrame.Height * 3;
				if (projectile.timeLeft <= 14)
					val.bodyFrame.Y = val.bodyFrame.Height * 4;
				switch (projectile.timeLeft)
                {
					case 29:
						projectile.frame++;
						break;
					case 24:
						projectile.frame++;
						break;
					case 18:
						{
							Attack();
							projectile.frame++;
						}
						break;
					case 12:
                        {
							projectile.damage = 0;
							projectile.knockBack = 0;
							projectile.frame++;
						}
						break;
					case 6:
						projectile.frame++;
						break;
				}
            }

			val.heldProj = projectile.whoAmI;
			projectile.position.X = val.Center.X - 45;
			projectile.position.Y = val.Center.Y - 80;
		}
		void Attack()
		{
			Player player = Main.player[projectile.owner];
			int power = 4;
			Main.PlaySound(SoundID.Item32.WithVolume(7.5f), projectile.position);
			Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.15f), projectile.position);
			projectile.damage = (int)values[0];
			projectile.knockBack = (float)values[1];
			int direction = player.direction == 1 ? 1 : -1;
		    Vector2 val = default;
			val.X = Main.MouseWorld.X;
			val.Y = Main.MouseWorld.Y;
			Vector2 val2 = val - projectile.Center;
			float num2 = 10f;
			float num3 = (float)Math.Sqrt(val2.X * val2.X + val2.Y * val2.Y);
			if (num3 > num2)
			{
				num3 = num2 / num3;
			}
			if (precision < 120)
				power++;
			if (precision < 95)
				power++;
			if (precision < 70)
				power++;
			if (precision < 40)
				power++;
			if (precision <= 20)
				power++;
			val2 *= num3;
			for (int a = 0; a < power; a++)
            {
				Vector2 perturbedSpeed = new Vector2(val2.Y, val2.X).RotatedByRandom(MathHelper.ToRadians(precision));
				perturbedSpeed = perturbedSpeed.RotatedBy(225, default);
				Projectile.NewProjectileDirect(projectile.position, perturbedSpeed, ModContent.ProjectileType<HyperFeatherF>(), projectile.damage, projectile.knockBack, player.whoAmI);
			}
			if (precision < 60)
            {
				for (int a = 0; a < 2; a++)
				{
					Vector2 perturbedSpeed = new Vector2(val2.Y, val2.X).RotatedByRandom(MathHelper.ToRadians(precision));
					perturbedSpeed = perturbedSpeed.RotatedBy(225, default);
					Projectile.NewProjectileDirect(projectile.position, perturbedSpeed, ModContent.ProjectileType<GiantFeatherF>(), projectile.damage * 2, projectile.knockBack * 2, player.whoAmI);
				}
			}

			//Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, val2.X, val2.Y, ModContent.ProjectileType<FeatherBallNew>(), (int)values[0], (float)values[1], projectile.owner, 1f, 0f);
		}
	}
}
