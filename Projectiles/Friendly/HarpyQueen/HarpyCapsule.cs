using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly.HarpyQueen
{
	public class HarpyCapsule : ModProjectile
	{
		int intensity = 0;
		int timer = 0;
		int[] attacks = new int[6];
		int[] rI = new int[2];
		readonly double[] defaultRotation = new double[] { 0, 90, 180, 270, 0, 180 };
		double[] rotation = new double[] { 0, 90, 180, 270, 0, 180 };
		double[] rotationMax = new double[] { 90, 180, 270, 360, -180, 0 };
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cartillage Capsule");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 26;
			projectile.aiStyle = 1;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 2400;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.velocity.X *= 0.975f;
			projectile.velocity.Y *= 0.975f;
			rI[0] += rI[1];
			if (rI[0] >= 360)
            {
				Main.PlaySound(SoundID.Item1, projectile.position);
				rI[0] = 0;
			}
			projectile.rotation = rI[0];
			timer++;
			if (timer > 12)
			{
				intensity++;
				timer = 0;
			}
			if (intensity <= 12)
			{
				rI[1] = 1;
				attacks[4] = 999;
				attacks[5] = 999;
			}
			if (intensity > 12 && intensity < 30)
			{
				rI[1] = 2;
				attacks[4] = 60;
				attacks[5] = 80;
			}
			if (intensity > 30 && intensity < 60)
			{
				rI[1] = 4;
				attacks[4] = 40;
				attacks[5] = 60;
			}
			if (intensity > 60 && intensity < 90)
			{
				rI[1] = 8;
				attacks[4] = 20;
				attacks[5] = 40;
			}
			if (intensity > 90)
			{
				rI[1] = 12;
				attacks[4] = 10;
				attacks[5] = 30;
			}
			attacks[2]++;
			attacks[3]++;
			if (attacks[2] > attacks[4])
			{
				for (int a = 0; a < 4; a++)
					rotation[a]++;
				attacks[2] = 0;
				for (int a = 0; a < 4; a++)
				{
					Main.PlaySound(SoundID.Item32.WithVolume(3.5f), projectile.position);
					Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedBy(rotation[a], default(Vector2));
					Projectile.NewProjectileDirect(projectile.position, perturbedSpeed, ModContent.ProjectileType<HyperFeatherF>(), projectile.damage, projectile.knockBack, player.whoAmI);
				}
			}
			if (attacks[3] > attacks[5])
			{
				for (int a = 4; a < 6; a++)
					rotation[a] -= 9;
				attacks[3] = 0;
				for (int a = 4; a < 6; a++)
				{
					Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.2f), projectile.position);
					Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedBy(rotation[a], default(Vector2));
					Projectile.NewProjectileDirect(projectile.position, perturbedSpeed, ModContent.ProjectileType<GiantFeatherF>(), projectile.damage * 2, projectile.knockBack * 2, player.whoAmI);
				}
			}
			for(int a = 0; a < 6; a++)
            {
				if (a <= 3)
                {
					if (rotation[a] >= rotationMax[a])
						rotation[a] = defaultRotation[a];
				}
				if (a > 3)
					if (rotation[a] <= rotationMax[a])
						rotation[a] = defaultRotation[a];
			}
			//Main.NewText(projectile.rotation);
		}
		public override void Kill(int timeLeft)
		{
			DustCircle();
			Main.PlaySound(SoundID.Tink, projectile.position);
			Gore.NewGoreDirect(projectile.position, Vector2.Zero, ModGore.GetGoreSlot("CastledsContent/Gores/HarpyCapsule_1"));
			Gore.NewGoreDirect(projectile.position, Vector2.Zero, ModGore.GetGoreSlot("CastledsContent/Gores/HarpyCapsule_1"));
			Gore.NewGoreDirect(projectile.position, Vector2.Zero, ModGore.GetGoreSlot("CastledsContent/Gores/HarpyCapsule_2"));
			Gore.NewGoreDirect(projectile.position, Vector2.Zero, ModGore.GetGoreSlot("CastledsContent/Gores/HarpyCapsule_2"));
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
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.NPCHit7, projectile.position);
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = oldVelocity.X * -0.6f;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = oldVelocity.Y * -0.6f;
			}
			return false;
		}
		void DustCircle()
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
		}
	}
}
