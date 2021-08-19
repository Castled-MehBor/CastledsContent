using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;

namespace CastledsContent.NPCs.Flayke.Items.Weapon.Shovel
{
    public class ShiveringSpade : ModItem
    {
        bool variation;
        public override string Texture => Flayke.Directory + "Items/Weapon/Shovel/ShovelItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shivering Spade");
			Tooltip.SetDefault("'A legion of workers to undo nature's mockings.'\nWield an enchanted shovel\nIf timed correctly, you can deflect hostile projectiles\nCan throw three snowballs in the cursor direction\nThis effect requires you having at least three snowballs in your inventory");
        }
        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
			item.useTime = 60;
			item.useAnimation = 60;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 35;
            item.knockBack = 7.5f;
			item.value = 37500;
            item.melee = true;
            item.rare = ItemRarityID.Blue;
			item.shoot = ProjectileID.PurificationPowder;
			item.autoReuse = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			variation = !variation;
			ShovelProjectile spade = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<ShovelProjectile>(), item.damage, item.knockBack, player.whoAmI)].modProjectile as ShovelProjectile;
			spade.variation = variation;
			spade.left[0] = player.direction != 1;
			return false;
		}
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<ShovelProjectile>()] < 1;
    }
	public class ShovelProjectile : ModProjectile
	{
		public bool variation;
		public bool[] left = new bool[2];
		public bool[] snowball = new bool[2];
		bool sound;
		bool afterIMG;
		float rotRef;
		float sine = 4.7123889803846898576939650749193f;
		float invis;
		int damage = -1;
		int rotIndex;
		float[] oldRot = new float[5];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shivering Spade");
		}

		public override void SetDefaults()
		{
			projectile.width = 108;
			projectile.height = 108;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 2;
		}
		public override void AI()
		{
			Player valo = Main.player[projectile.owner];
			if (!snowball[0])
            {
				for (int a = 0; a < valo.inventory.Length; a++)
				{
					Item i = valo.inventory[a];
					if (i != null && !i.IsAir)
                    {
						if (i.type == ItemID.Snowball && i.stack >= 3)
						{
							i.stack -= 3;
							snowball[1] = true;
						}
					}
				}
				snowball[0] = true;
			}
			valo.direction = projectile.direction = left[0] ? -1 : 1;
			if (projectile.damage > 0 && damage < 0)
				damage = projectile.damage;
			if (valo.dead || valo.HeldItem.type != ModContent.ItemType<ShiveringSpade>())
				projectile.Kill();
			if (valo.HeldItem.type == ModContent.ItemType<ShiveringSpade>())
				projectile.timeLeft = 4;
			projectile.ai[0] += 1f;
			if (Main.player[projectile.owner].controlUseTile && projectile.ai[0] > 39f && projectile.ai[0] < 41f)
				projectile.ai[0] = 38f;
			#region Rotation Stuff
			if (Main.myPlayer == projectile.owner && rotRef == 0)
				rotRef = (float)Math.Atan2((double)((float)Main.mouseY + Main.screenPosition.Y - projectile.Center.Y), (double)((float)Main.mouseX + Main.screenPosition.X - projectile.Center.X)) + 39.25f;
			if (sine <= (float)Math.PI * 2.5f)
				sine += (float)Math.PI / 60;
			else
				projectile.Kill();
			if (sine >= Math.PI * 1.75)
            {
				invis += 0.022f;
				if (!left[1] && !variation)
                {
					left[1] = true;
					left[0] = !left[0];
				}
			}
			float scale = (float)Math.Sin(sine * 1.5f);
			projectile.rotation = MathHelper.Lerp(rotRef + (variation ? 0.75f : -0.75f), rotRef + (variation ? -1.25f : 1.25f), scale);
			oldRot[rotIndex] = projectile.rotation;
			if (rotIndex++ > 3)
				rotIndex = 0;
			#endregion
			if (scale <= 0.75f && scale >= 0.125f)
			{
				if (!sound)
                {
					Main.PlaySound(SoundID.Item82.WithVolume(0.75f), projectile.Center);
					Main.PlaySound(SoundID.Item1.WithVolume(0.5f), projectile.Center);
					sound = true;
					afterIMG = true;
                }
				projectile.damage = damage;
				DeflectLaser laser = Main.projectile[Projectile.NewProjectile(projectile.Center, new Vector2(-7.5f, 0).RotatedBy(projectile.rotation + 61.2f), ModContent.ProjectileType<DeflectLaser>(), 0, 0, projectile.owner)].modProjectile as DeflectLaser;
				laser.snowball[0] = snowball[1];
				if (snowball[1] && scale <= 0.45f && scale >= 0.2f)
                {
					Main.PlaySound(SoundID.Item1, projectile.Center);
					Projectile.NewProjectile(projectile.Center, new Vector2(7.5f, 0).RotatedBy(projectile.rotation + 61.2f), ProjectileID.SnowBallFriendly, projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
				}
			}
			else
				projectile.damage = 0;
			valo.heldProj = projectile.whoAmI;
			projectile.position.X = valo.Center.X - (projectile.width / 2);
			projectile.position.Y = valo.Center.Y - (projectile.height / 2);
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Texture2D tex = Main.projectileTexture[projectile.type];
			Texture2D snow = ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Shovel/ShovelProjectileExtra");
			Color inv = new Color(MiscUtilities.LerpRound(lightColor.R, 0, invis), MiscUtilities.LerpRound(lightColor.G, 0, invis), MiscUtilities.LerpRound(lightColor.B, 0, invis), MiscUtilities.LerpRound(lightColor.A, 0, invis));
			#region AfterIMG
			if (afterIMG)
            {
				for (int k = 0; k < oldRot.Length; k++)
				{
					Vector2 drawPos = projectile.Center - Main.screenPosition;
					Color color = (inv * ((float)(oldRot.Length - k) / (float)oldRot.Length));
					color *= 0.5f;
					spriteBatch.Draw(tex, drawPos, new Rectangle(0, 0, tex.Width, tex.Height), color, oldRot[k], new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				}
			}
			#endregion
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), inv, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			if (snowball[1])
			spriteBatch.Draw(snow, projectile.Center - Main.screenPosition, new Rectangle(0, 0, snow.Width, snow.Height), inv, projectile.rotation, new Vector2(snow.Width / 2, snow.Height / 2), projectile.scale, projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			return false;
        }
    }
	public class DeflectLaser : ModProjectile
	{
		public bool[] snowball = new bool[2];
		int counter;
		public Vector2 oldMousePos;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deflection Visualization");
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.extraUpdates = 100;
			projectile.timeLeft = 300;
		}
		public override void AI()
		{
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 9f)
            {
				if (projectile.localAI[0] > 9f)
				{
					for (int i = 0; i < 4; i++)
					{
						counter++;
						projectile.position -= projectile.velocity * ((float)i * 0.25f);
						projectile.alpha = 255;
						if (counter > 90 && counter < 175)
						{
							if (counter >= 135 && snowball[0] && !snowball[1])
							{
								Dust dust = Dust.NewDustPerfect(projectile.Center, 76);
								dust.velocity = Vector2.Zero;
								dust.noGravity = true;
								snowball[1] = true;
							}
							#region Deflection
							foreach (Projectile p in Main.projectile)
							{
								if (p.hostile && !p.friendly && p.damage > 0)
								{
									if (projectile.Hitbox.Intersects(p.Hitbox))
									{
										Main.PlaySound(SoundID.NPCHit42.WithVolume(0.75f), projectile.Center);
										#region Visual & Consequence?
										Projectile effect = Projectile.NewProjectileDirect(p.Center, p.velocity, p.type, p.damage, p.knockBack, p.owner);
										effect.Kill();
										#endregion
										p.velocity.X = projectile.velocity.X *= -1.25f;
										p.velocity.Y = projectile.velocity.Y *= -1.25f;
										p.owner = projectile.owner;
										p.friendly = true;
									}
								}
							}
							#endregion
						}
                    }
				}
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = oldVelocity.X * 1f;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = oldVelocity.Y * 1f;
			}
			return false;
		}
    }
}