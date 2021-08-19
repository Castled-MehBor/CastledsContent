using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;

namespace CastledsContent.NPCs.Flayke.Items.Weapon.Cannon
{
    public class StarlightCertificate : ModItem
    {
        public override string Texture => Flayke.Directory + "Items/Weapon/Cannon/CannonItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Certificate");
            Tooltip.SetDefault("'A gift from the cosmos to you.'\nFires a starlight trifecta\nThe two stars will home in towards the location of where the mouse was fired\nA damaging aurora blast is created upon intersection\nUses fallen stars as ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 36;
            item.ranged = true;
            item.width = 46;
            item.height = 46;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.rare = ItemRarityID.Blue;
            item.value = 37500;
            item.UseSound = SoundID.Item61;
            item.useAmmo = AmmoID.FallenStar;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 1f;
            item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-15f, -1.75f);
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 15f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            Vector2 val = default;
            val.X = Main.MouseWorld.X;
            val.Y = Main.MouseWorld.Y;
            Vector2 val2 = val - player.Center;
            float num2 = 10f;
            float num3 = (float)Math.Sqrt(val2.X * val2.X + val2.Y * val2.Y);
            if (num3 > num2)
                num3 = num2 / num3;
            val2 *= num3;
            SpecialStarS special1 = Main.projectile[Projectile.NewProjectile(player.Center, val2.RotatedBy(MathHelper.ToRadians(15f)) * 0.75f, ModContent.ProjectileType<SpecialStarS>(), item.damage, item.knockBack, player.whoAmI, 1f, 0f)].modProjectile as SpecialStarS;
            SpecialStarS special2 = Main.projectile[Projectile.NewProjectile(player.Center, val2.RotatedBy(MathHelper.ToRadians(-15f)) * 0.75f, ModContent.ProjectileType<SpecialStarS>(), item.damage, item.knockBack, player.whoAmI, 1f, 0f)].modProjectile as SpecialStarS;
            SpecialStarS special3 = Main.projectile[Projectile.NewProjectile(player.Center, val2 * 0.75f, ModContent.ProjectileType<SpecialStarS>(), item.damage, item.knockBack, player.whoAmI, 1f, 0f)].modProjectile as SpecialStarS;
            special1.specialType = 1;
            special2.specialType = 2;
            special3.specialType = 3;
            special1.countTowards = special3.projectile;
            special2.countTowards = special3.projectile;
            return false;
        }
    }
    public class SpecialStarS : ModProjectile
    {
        Vector2[] points = new Vector2[3];
        float[] distance = new float[3];
        float rotation;
        public int specialType;
        public int eC;
        bool[] bVal = new bool[5];
        public Projectile countTowards;
        Color col;
        bool colSet;
        int afterIMGTimer;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        int posIndex;
        Vector2[] oldPosNew = new Vector2[5];
        public Color[] splode = new Color[2];
        public override string Texture => Flayke.Directory + "Items/Weapon/Cannon/Star";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.width = 16;
            projectile.height = 16;
            projectile.ignoreWater = true;
            projectile.timeLeft = 420;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            float val = projectile.velocity.Length() * 0.05f;
            #region Visual Stuff
            oldPosNew[posIndex] = projectile.Center;
            if (posIndex++ > 3)
                posIndex = 0;
            if (Main.rand.NextBool(49))
            {
                for (int a = 0; a < Main.rand.Next(3); a++)
                    Dust.NewDust(projectile.Center, projectile.width, projectile.height, 15);
            }
            if (afterIMGTimer++ > 5)
            {
                afterIMGTimer = 0;
                afterIMG.Add(new NorthAfterIMG(projectile.Center, projectile.rotation));
            }
            if (afterIMG.Count > 0)
            {
                for (int a = 0; a < afterIMG.Count; a++)
                {
                    if (afterIMG[a].timer >= 255)
                        afterIMG.Remove(afterIMG[a]);
                    afterIMG[a].timer += 5;
                }
            }
            if (!colSet)
            {
                col = SetColor();
                colSet = true;
            }
            #endregion
            switch(specialType)
            {
                case 1:
                    {
                        if(bVal[1])
                        {
                            rotation -= 0.05f;
                            if (rotation > 360)
                                rotation = 0;
                        }
                        else
                        {
                            rotation += val;
                            if (rotation < 0)
                                rotation = 360;
                        }
                    }
                    break;
                case 2:
                    {
                        if (bVal[1])
                        {
                            rotation += 0.05f;
                            if (rotation < 0)
                                rotation = 360;
                        }
                        else
                        {
                            rotation -= val;
                            if (rotation > 360)
                                rotation = 0;
                        }
                    }
                    break;
            }
            if (projectile.owner == Main.myPlayer)
            {
                if (!bVal[0])
                {
                    points[0] = projectile.Center;
                    points[1] = Main.MouseWorld;
                    bVal[0] = true;
                }
            }
            if (!bVal[1])
            {
                if ((points[1] - projectile.Center).Length() <= (points[1] - points[0]).Length() / 2)
                {
                    Main.PlaySound(SoundID.Item9.WithVolume(0.5f), projectile.Center);
                    Main.PlaySound(SoundID.Item117.WithVolume(0.5f), projectile.Center);
                    points[2] = projectile.Center;
                    projectile.velocity = Vector2.Zero;
                    bVal[1] = true;
                }
            }
            if (bVal[1])
            {
                distance[0] += 1;
                projectile.position = new Vector2(MathHelper.Lerp(points[2].X, points[1].X, distance[0] / 20), MathHelper.Lerp(points[2].Y, points[1].Y, distance[0] / 20));
                if (distance[0] == 20)
                {
                    if (!bVal[2] && countTowards != null && countTowards.modProjectile is SpecialStarS s)
                    {
                        bVal[2] = true;
                        for (int a = 0; a < s.splode.Length - 1; a++)
                            if (splode[a] == null)
                                splode[a] = col;
                        s.eC++;
                    }
                }
                if (eC == 2)
                {
                    StarBurst();
                    eC = 0;
                }
            }
            void StarBurst()
            {
                Color col = new Color(MiscUtilities.Round(MathHelper.Lerp(splode[0].R, splode[1].R, 0.5f)), MiscUtilities.Round(MathHelper.Lerp(splode[0].G, splode[1].G, 0.5f)), MiscUtilities.Round(MathHelper.Lerp(splode[0].B, splode[1].B, 0.5f)), 255);
                Blast b = Flayke.BlastEffect(projectile.Center, new Color[2] { Color.White, col }, 4).modProjectile as Blast;
                b.increment = 0.125f;
                b.projectile.friendly = true;
                b.projectile.owner = projectile.owner;
                b.projectile.damage = projectile.damage * 2;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float rot = rotation + projectile.rotation;
            Texture2D tex = specialType == 3 ? ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Cannon/Bolt") : ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Cannon/Star");
            Texture2D img = specialType == 3 ? ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Cannon/BoltIMG") : ModContent.GetTexture(Flayke.Directory + "Items/Weapon/Cannon/StarIMG");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(img, n.coord - Main.screenPosition, new Rectangle(0, 0, img.Width, img.Height), n.GetColor(col) * 1.5f, n.rotation + rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            if (bVal[2])
            {
                for (int k = 0; k < oldPosNew.Length; k++)
                {
                    Vector2 drawPos = oldPosNew[k] - Main.screenPosition;
                    Color color = (col * ((float)(oldPosNew.Length - k) / (float)oldPosNew.Length));
                    color *= 0.5f;
                    spriteBatch.Draw(tex, drawPos, new Rectangle(0, 0, tex.Width, tex.Height), color, rot, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), col, rot, new Vector2(tex.Width / 2, tex.Height / 2), 1, projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath3.WithVolume(0.75f), projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i));
                Dust dust = Dust.NewDustPerfect(position, 15);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }
        }
        Color SetColor()
        {
            int a = Main.rand.Next(0, 3);
            int[] val = new int[3];
            for(int c = 1; c < 3; c++)
            {
                if (c - 1 != a)
                    val[c - 1] = Main.rand.Next(125);
            }
            return new Color(255 - val[0], 255 - val[1], 255 - val[2]);
        }
    }
}

