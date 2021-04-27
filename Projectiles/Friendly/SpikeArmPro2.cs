using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Projectiles.Friendly
{
	public class SpikeArmPro2 : ModProjectile
	{
		private bool isStaggered;
		private bool hasPrimed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spike Arm");
		}

		public override void SetDefaults()
		{
			projectile.width = 108;
			projectile.height = 108;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.light = 0.2f;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 2;
			Main.projFrames[projectile.type] = 2;
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
			projectile.direction = Main.MouseWorld.X > val.position.X ? 1 : -1;
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
				projectile.rotation = (float)Math.Atan2((double)((float)Main.mouseY + Main.screenPosition.Y - projectile.Center.Y), (double)((float)Main.mouseX + Main.screenPosition.X - projectile.Center.X)) + 7;
			}

			val.heldProj = projectile.whoAmI;
			projectile.position.X = val.Center.X - 54f;
			projectile.position.Y = val.Center.Y - 54f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			isStaggered = true;
			Player val = Main.player[projectile.owner];
			projectile.timeLeft += 10;
			if (val.GetModPlayer<CastledPlayer>().spikeArmCooldown < 1 && !hasPrimed)
            {
				projectile.frame = 1;
				Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact.WithVolume(1.25f));
				hasPrimed = true;
			}
		}
        public override void Kill(int timeLeft)
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

			Player plyr = Main.player[projectile.owner];
			if (isStaggered && plyr.GetModPlayer<CastledPlayer>().spikeArmCooldown < 1 && hasPrimed)
            {
				Main.PlaySound(SoundID.Item92, projectile.position);
				Main.PlaySound(SoundID.Item88, projectile.position);
				for (int i = 0; i < 50; i++)
				{
					Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i));
					Dust dust = Dust.NewDustPerfect(position, DustID.Electric);
					dust.noGravity = true;
					dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 6;
					dust.noLight = false;
					dust.fadeIn = 1f;
				}
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, val2.X, val2.Y, ModContent.ProjectileType<SpikeBall>(), projectile.damage * 3, projectile.knockBack * 3, projectile.owner, 1f, 0f);
				plyr.GetModPlayer<CastledPlayer>().spikeArmCooldown = 180;
			}
		}
    }
}
