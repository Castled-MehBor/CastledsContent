using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.HarpyQueen
{
	public class HarpyGunProjectile : ModProjectile
	{
		//int fire = 0;
		bool set = false;
		object[] values = new object[2];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinion Talon");
		}

		public override void SetDefaults()
		{
			projectile.width = 150;
			projectile.height = 150;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 2;
			Main.projFrames[projectile.type] = 4;
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
			if (!set)
            {
				values[0] = projectile.damage;
				values[1] = projectile.knockBack;
				projectile.damage = 0;
				projectile.knockBack = 0;
				set = true;
            }
			projectile.ai[0]++;
			if (projectile.ai[0]  > 10)
            {
				projectile.ai[0] = 0;
				projectile.frame++;
				if (projectile.frame == 3)
					Main.PlaySound(SoundID.Item32.WithVolume(3.5f), projectile.position);
				if (projectile.frame > 3)
                {
					projectile.frame = 0;
					FireFeatherBall();
				}
            }
			if (val.dead || !val.channel)
			{
				projectile.Kill();
			}
			if (val.channel)
			{
				projectile.timeLeft = 4;
			}
			projectile.ai[0] += 1f;
			if (Main.player[projectile.owner].controlUseTile && projectile.ai[0] > 39f && projectile.ai[0] < 41f)
			{
				projectile.ai[0] = 38f;
			}
			if (Main.myPlayer == projectile.owner)
			{
				projectile.rotation = (float)Math.Atan2((double)((float)Main.mouseY + Main.screenPosition.Y - projectile.Center.Y), (double)((float)Main.mouseX + Main.screenPosition.X - projectile.Center.X));
			}

			val.heldProj = projectile.whoAmI;
			projectile.position.X = val.Center.X - 75;
			projectile.position.Y = val.Center.Y - 70;
		}
		void FireFeatherBall()
        {
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
			val2 *= num3;
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, val2.X, val2.Y, ModContent.ProjectileType<FeatherBallNew>(), (int)values[0], (float)values[1], projectile.owner, 1f, 0f);
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Player val = Main.player[projectile.owner];
			Vector2 origin = new Vector2(80, 75);
			SpriteEffects flip = SpriteEffects.None;
			if (Main.MouseWorld.X < val.position.X)
				flip = SpriteEffects.FlipVertically;
			if (Main.MouseWorld.X > val.position.X)
				flip = SpriteEffects.None;
			Texture2D texture = ModContent.GetTexture("CastledsContent/Projectiles/Friendly/HarpyQueen/HarpyGunProjectile");
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.height * projectile.frame, projectile.width, projectile.height), lightColor, projectile.rotation, origin, 1f, flip, 0f);
		}
	}
	public class FeatherBallNew : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Feather Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
        public override void AI()
        {
			float velX = Math.Abs(projectile.velocity.X) / 2;
			float velY = Math.Abs(projectile.velocity.Y) / 2;
			projectile.velocity.X *= 0.975f;
			projectile.velocity.Y *= 0.975f;
			projectile.rotation += velX + velY;
		}
        public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			for (int a = 0; a < 3; a++)
            {
				Dust.NewDustPerfect(projectile.position, DustID.BubbleBlock);
				Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
				Projectile.NewProjectileDirect(projectile.position, perturbedSpeed, ModContent.ProjectileType<HomingFeather>(), projectile.damage, projectile.knockBack, player.whoAmI);
			}
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
	public class HomingFeather : ModProjectile
	{
		int timer = 0;
		public override string Texture => "CastledsContent/Projectiles/Friendly/HarpyQueen/GlazedFeather";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glazed Feather");
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 26;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
        public override void AI()
        {
			timer++;
			if (timer < 30)
            {
				projectile.velocity.X *= 0.975f;
				projectile.velocity.Y *= 0.975f;
			}
			if (timer > 30)
            {
				#region Vanilla Chlorophyte Bullet AI
				for (int i = 0; i < 200; i++)
				{
					NPC target = Main.npc[i];
					if (!target.friendly)
					{
						float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
						float shootToY = target.position.Y - projectile.Center.Y + (target.height / 2);
						float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

						if ((distance < Main.screenWidth / 2 || distance < Main.screenHeight / 2) && !target.friendly && target.active)
						{
							distance = 3f / distance;

							shootToX *= distance * 5;
							shootToY *= distance * 5;
						}
					}
				}

				float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
				float num133 = projectile.localAI[0];
				if (num133 == 0f)
				{
					projectile.localAI[0] = num132;
					num133 = num132;
				}
				float num134 = projectile.position.X;
				float num135 = projectile.position.Y;
				float num136 = 300f;
				bool flag3 = false;
				int num137 = 0;
				if (projectile.ai[1] == 0f)
				{
					for (int num138 = 0; num138 < 200; num138++)
					{
						if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
						{
							float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
							float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
							float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
							if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
							{
								num136 = num141;
								num134 = num139;
								num135 = num140;
								flag3 = true;
								num137 = num138;
							}
						}
					}
					if (flag3)
					{
						projectile.ai[1] = (float)(num137 + 1);
					}
					flag3 = false;
				}
				if (projectile.ai[1] > 0f)
				{
					int num142 = (int)(projectile.ai[1] - 1f);
					if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
					{
						float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
						float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
						if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 10000f)
						{
							flag3 = true;
							num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
							num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
						}
					}
					else
					{
						projectile.ai[1] = 0f;
					}
				}
				if (!projectile.friendly)
				{
					flag3 = false;
				}
				if (flag3)
				{
					float num145 = num133;
					Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num146 = num134 - vector10.X;
					float num147 = num135 - vector10.Y;
					float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
					num148 = num145 / num148;
					num146 *= num148;
					num147 *= num148;
					int num149 = 8;
					projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
					projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
				}
				#endregion
			}
		}
    }
}
