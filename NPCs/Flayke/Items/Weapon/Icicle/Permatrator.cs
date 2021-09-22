using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using CastledsContent.Utilities;

namespace CastledsContent.NPCs.Flayke.Items.Weapon.Icicle
{
	public class Permatrator : ModItem
	{
		public override string Texture => Flayke.Directory + "Items/Weapon/Icicle/IcicleItem";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Permatrator");
			Tooltip.SetDefault("'The freezing cold belonging of a more vicious ice mage.'\nWield an energized icicle\nFires an ice beam that deviates and ricochets at a 90 degree angle on hit\nThis ice beam has a base mana cost of 10\nFires close-ranged ice shards if an enemy is hit by the icicle directly");
		}
		public override void SetDefaults()
		{
			item.damage = 30;
			item.magic = true;
			item.width = 40;
			item.height = 40;
			item.knockBack = 3.75f;
			item.rare = ItemRarityID.Blue;
			item.value = 37500;
		}
		public override void HoldItem(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<IcicleProjectileHeld>()] < 1)
				Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<IcicleProjectileHeld>(), item.damage, item.knockBack, player.whoAmI);
		}
	}
	public class IcicleProjectileHeld : ModProjectile
	{
		float lerp;
		float rot;
		int[] meleeVal = new int[2] { 0, -1 };
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Permatrator");
		}

		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.light = 0.2f;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 2;
		}
		public override void AI()
		{
			Player valo = Main.player[projectile.owner];
			PermatratorClass valP = valo.GetModPlayer<PermatratorClass>();
			if (meleeVal[1] < 0)
				meleeVal[1] = projectile.damage;
			projectile.damage = meleeVal[1];
			if (meleeVal[0] >= 1)
			{
				meleeVal[0]--;
				projectile.damage = 0;
			}
			if (lerp > 0)
				lerp -= 0.0495f;
			projectile.direction = Main.MouseWorld.X > valo.position.X ? 1 : -1;
			if (valo.dead || valo.HeldItem.type != ModContent.ItemType<Permatrator>())
			{
				projectile.Kill();
			}
			if (valo.HeldItem.type == ModContent.ItemType<Permatrator>())
			{
				projectile.timeLeft = 4;
				valo.direction = Main.MouseWorld.X > projectile.Center.X ? 1 : -1;
				if (Main.MouseWorld.Y < projectile.Center.Y - 75)
					valo.bodyFrame.Y = valo.bodyFrame.Height * 2;
				else if (Main.MouseWorld.Y > projectile.Center.Y + 75)
					valo.bodyFrame.Y = valo.bodyFrame.Height * 4;
				else
					valo.bodyFrame.Y = valo.bodyFrame.Height * 3;
			}
			projectile.ai[0] += 1f;
			if (Main.player[projectile.owner].controlUseTile && projectile.ai[0] > 39f && projectile.ai[0] < 41f)
			{
				projectile.ai[0] = 38f;
			}
			if (Main.myPlayer == projectile.owner)
			{
				projectile.rotation = (float)Math.Atan2((double)((float)Main.mouseY + Main.screenPosition.Y - projectile.Center.Y), (double)((float)Main.mouseX + Main.screenPosition.X - projectile.Center.X)) + 39.25f;
				rot = projectile.rotation;
			}

			valo.heldProj = projectile.whoAmI;
			projectile.position.X = valo.Center.X - (projectile.width / 2);
			projectile.position.Y = valo.Center.Y - (projectile.height / 2);
			if (meleeVal[0] < 1 && !Main.isMouseLeftConsumedByUI && Main.mouseLeft && valP.permaTimer < 1)
			{
				if (valo.CheckMana(10, true))
				{
					valP.permaTimer = 60;
					FlakLaser();
				}
			}
			void FlakLaser()
			{
				Main.PlaySound(SoundID.Item122.WithPitchVariance(1.25f), projectile.Center);
				lerp = 0.99f;
				Main.PlaySound(SoundID.Item, projectile.Center);
				#region laser
				Vector2 val = default;
				val.X = Main.MouseWorld.X;
				val.Y = Main.MouseWorld.Y;
				Vector2 val2 = val - projectile.Center;
				float num2 = 10f;
				float num3 = (float)Math.Sqrt(val2.X * val2.X + val2.Y * val2.Y);
				if (num3 > num2)
					num3 = num2 / num3;
				val2 *= num3;
				FlakLaser las = Main.projectile[Projectile.NewProjectile(projectile.Center, val2 * -1, ModContent.ProjectileType<FlakLaser>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 1f, 0f)].modProjectile as FlakLaser;
				las.mother = true;
				#endregion
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.Item27.WithVolume(0.75f), projectile.Center);
			for (int a = 0; a < Main.rand.Next(3, 5); a++)
				Projectile.NewProjectile(projectile.Center, new Vector2(7.5f, 0).RotatedBy(rot + Main.rand.NextFloat(60.2f, 62.2f), default), ProjectileID.CrystalShard, projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
			meleeVal[0] = 60;
			lerp = 0.99f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => false;
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
			Texture2D img = ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Icicle/IcicleProjectileHeldIMG");
			Vector2 shake = new Vector2(MathHelper.Lerp(0, Main.rand.Next(5), lerp), MathHelper.Lerp(0, Main.rand.Next(5), lerp));
			Color color = new Color(Val(lightColor.R), Val(lightColor.G), Val(lightColor.B), Val(lightColor.A));
			int Val(int i) => MiscUtilities.Round(MathHelper.Lerp(0, i, lerp));
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), lightColor, projectile.rotation, new Vector2((tex.Width / 2) + shake.X, (tex.Height / 2) + shake.Y), 0.625f, Main.MouseWorld.X > projectile.Center.X ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
			spriteBatch.Draw(img, projectile.Center - Main.screenPosition, new Rectangle(0, 0, img.Width, img.Height), color, projectile.rotation, new Vector2((img.Width / 2) + shake.X, (img.Height / 2) + shake.Y), 0.625f, Main.MouseWorld.X > projectile.Center.X ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

		}
	}
	public class FlakLaser : ModProjectile
	{
		public bool mother;
		bool spawn;
		int damage = -1;
		int counter;
		int delay;
		int count;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flak-E Laser");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.extraUpdates = 100;
			projectile.timeLeft = 300;
		}
		public override void AI()
		{
			if (damage < 0)
			{
				damage = projectile.damage;
				projectile.damage = 0;
			}
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 9f)

				if (projectile.localAI[0] > 9f)
				{
					for (int i = 0; i < 4; i++)
					{
						counter++;
						projectile.position -= projectile.velocity * ((float)i * 0.25f);
						projectile.alpha = 255;
						if (counter >= 90)
						{
							int dust = Dust.NewDust(projectile.Center, 1, 1, 135, 0f, 0f, 0, default(Color), 1f);
							Main.dust[dust].noGravity = true;
							Main.dust[dust].position = projectile.Center;
							Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
							Main.dust[dust].velocity *= 0.2f;
						}
					}
					if (counter >= delay)
						projectile.damage = damage;
				}
			if (counter < 60)
				projectile.tileCollide = false;
			else
				projectile.tileCollide = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!spawn)
			{
				float[] rot = new float[2] { 90, -90 };
				count++;
				if (mother)
                {
					Main.PlaySound(SoundID.Item60, projectile.Center);
					rot = new float[2] { 0, 0 };
				}
				if (count < 4)
				{
					FlakLaser f1 = Main.projectile[Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(rot[0]), default), ModContent.ProjectileType<FlakLaser>(), projectile.damage, projectile.knockBack / 2, projectile.owner)].modProjectile as FlakLaser;
					FlakLaser f2 = Main.projectile[Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(rot[1]), default), ModContent.ProjectileType<FlakLaser>(), projectile.damage, projectile.knockBack / 2, projectile.owner)].modProjectile as FlakLaser;
					f1.delay = MiscUtilities.Round(delay * 0.9f);
					if (f1.delay < 90)
						f1.delay = 90;
					f1.delay = MiscUtilities.Round(delay * 0.9f);
					if (f2.delay < 90)
						f2.delay = 90;
					f1.count = count;
					f2.count = count;
				}
				projectile.damage = 0;
				spawn = true;
			}
		}
	}
	public class PermatratorClass : ModPlayer
	{
		public int permaTimer;
		public override void PostUpdateEquips()
		{
			if (permaTimer > 0)
				permaTimer--;
		}
	}
}