using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using CastledsContent.NPCs.Flayke;

namespace CastledsContent.Items.Weapons.Melee
{
    public class SonicBlade : ModItem
    {
        bool proj;
        public override void SetStaticDefaults() { DisplayName.SetDefault("Hypa' Blade"); Tooltip.SetDefault("Unleashes a piercing sonic boom every second swing"); }
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 46;
            item.height = 46;
            item.damage = 24;
            item.UseSound = SoundID.Item1;
            item.knockBack = 3.5f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 67500;
            item.rare = ItemRarityID.Orange;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 7.5f;
            item.shoot = ModContent.ProjectileType<SonicBoom>();
            item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 8);
            recipe.AddIngredient(ModContent.ItemType<Material.HarpyFeather>(), 5);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            proj = !proj;
            if (proj)
                Main.PlaySound(SoundID.Item77, player.Center);
            return proj;
        }
    }
    public class SonicBoom : ModProjectile
    {
        float[] lifetime = new float[4];
        int posIndex;
        Vector2[] oldPosNew = new Vector2[5];
        int[] penetrate = new int[2];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sonic Boom");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.width = 80;
            projectile.height = 32;
            projectile.ignoreWater = true;
            projectile.timeLeft = 420;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            lifetime[0] += (float)Math.PI / 75;
            lifetime[1] = (float)Math.Sin(lifetime[0]) / 10;
            lifetime[2] += (float)Math.PI / 35;
            lifetime[3] = (float)Math.Sin(lifetime[2]) / 2f;
            penetrate[1] = MiscUtilities.Round(MathHelper.Lerp(20, 1, lifetime[1]));
            projectile.velocity = new Vector2(MathHelper.Lerp(projectile.velocity.X, 0, lifetime[1] / 5), MathHelper.Lerp(projectile.velocity.Y, 0, lifetime[1] / 5));
            if (lifetime[1] < Math.Pow(0.1, 99) || penetrate[0] >= penetrate[1])
                projectile.Kill();
            oldPosNew[posIndex] = projectile.Center;
            if (posIndex++ > 3)
                posIndex = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            penetrate[0]++;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color fade = MiscUtilities.LerpColor(Color.Transparent, lightColor, lifetime[1] * 10);
            fade *= 0.5f;
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D img = ModContent.GetTexture("CastledsContent/Items/Weapons/Melee/SonicBoomIMG");
            for (int k = 0; k < oldPosNew.Length; k++)
            {
                Vector2 drawPos = oldPosNew[k] - Main.screenPosition;
                Color color = (fade * ((float)(oldPosNew.Length - k) / (float)oldPosNew.Length));
                spriteBatch.Draw(tex, drawPos, new Rectangle(0, 0, tex.Width, tex.Height), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), fade, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(img, projectile.Center - Main.screenPosition, new Rectangle(0, 0, img.Width, img.Height), MiscUtilities.LerpColor(fade, Color.Transparent, lifetime[3] * -1), projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale + (lifetime[3] / 2), projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            return false;
        }
    }
    public class VolcanicBlade : ModItem
    {
        bool proj;
        public override void SetStaticDefaults() { DisplayName.SetDefault("Heat Strike"); Tooltip.SetDefault("Unleashes a piercing lava wave every second swing\nEnemies erupt into lava on critical hits\nEnemies also erupt if hit by the blade on second swing"); }
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 46;
            item.height = 46;
            item.damage = 60;
            item.UseSound = SoundID.Item1;
            item.knockBack = 8f;
            item.useTime = 35;
            item.useAnimation = 35;
            item.value = 150000;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 7.5f;
            item.rare = ItemRarityID.Lime;
            item.shoot = ModContent.ProjectileType<LavaWave>();
            item.autoReuse = true;
        }
        public static void Eruption(Vector2 pos, int damage, float knockback, int owner)
        {
            Main.PlaySound(SoundID.Item45, pos);
            Main.PlaySound(SoundID.Item100.WithVolume(0.75f), pos);
            Blast b = Flayke.BlastEffect(pos, new Color[2] { Color.White, Color.OrangeRed }, 7.5f).modProjectile as Blast;
            b.increment = 0.2f;
            b.projectile.friendly = true;
            b.projectile.owner = owner;
            b.projectile.damage = MiscUtilities.Round(damage * 1.5f);
            for (int a = 0; a < Main.rand.Next(4, 6); a++)
                Projectile.NewProjectile(pos, new Vector2(0, 7.5f).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-45, 45)), default), ModContent.ProjectileType<LavaDrop>(), damage / 2, knockback / 2, owner);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (proj)
                Eruption(target.Center, item.damage, item.knockBack, player.whoAmI);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            proj = !proj;
            if (proj)
                Main.PlaySound(SoundID.Item82, player.Center);
            return proj;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
            recipe.AddIngredient(ModContent.ItemType<SonicBlade>());
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.Fireblossom, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class LavaWave : ModProjectile
    {
        public override string Texture => "CastledsContent/Items/Weapons/Melee/SonicBoom";
        float[] lifetime = new float[4];
        int[] penetrate = new int[2];
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        int timer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Wave");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.width = 80;
            projectile.height = 32;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            lifetime[0] += (float)Math.PI / 180;
            lifetime[1] = (float)Math.Sin(lifetime[0]) / 10;
            lifetime[2] += (float)Math.PI / 35;
            lifetime[3] = (float)Math.Sin(lifetime[2]) / 4.75f;
            penetrate[1] = MiscUtilities.Round(MathHelper.Lerp(45, 1, lifetime[1]));
            projectile.velocity = new Vector2(MathHelper.Lerp(projectile.velocity.X, 0, lifetime[1] / 5), MathHelper.Lerp(projectile.velocity.Y, 0, lifetime[1] / 5));
            if (lifetime[1] < Math.Pow(0.1, 99) || penetrate[0] >= penetrate[1])
                projectile.Kill();
            if (Main.rand.NextBool(MiscUtilities.Round(MathHelper.Lerp(1, 99, lifetime[1]))))
            {
                Dust dust = Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, DustID.Fire);
                dust.velocity = projectile.velocity * -1;
            }
            if (timer++ > 5)
            {
                timer = 0;
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
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            penetrate[0]++;
            projectile.velocity *= 0.95f;
            if (crit)
                VolcanicBlade.Eruption(projectile.Center, projectile.damage, projectile.knockBack, projectile.owner);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color fade = MiscUtilities.LerpColor(Color.Transparent, lightColor, lifetime[1] * 10);
            fade *= 0.5f;
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D img = ModContent.GetTexture("CastledsContent/Items/Weapons/Melee/SonicBoomIMG");
            //--Shader Drawing--
            spriteBatch.End(); spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            ArmorShaderData shader1 = GameShaders.Armor.GetShaderFromItemId(ItemID.BurningHadesDye);
            ArmorShaderData shader2 = GameShaders.Armor.GetShaderFromItemId(ItemID.InfernalWispDye);
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                {
                    DrawData imgDat = new DrawData(img, n.coord - Main.screenPosition, new Rectangle(0, 0, img.Width, img.Height), n.GetColor(Color.Wheat), n.rotation, new Vector2(img.Width / 2, img.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                    shader1.Apply(projectile, imgDat);
                    imgDat.Draw(spriteBatch);
                }
            }
            DrawData test1 = new DrawData(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), fade, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            DrawData test2 = new DrawData(img, projectile.Center - Main.screenPosition, new Rectangle(0, 0, img.Width, img.Height), MiscUtilities.LerpColor(fade, Color.Transparent, lifetime[3] * -1), projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale + (lifetime[3] / 4.75f), projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            shader1.Apply(projectile, test1);
            shader2.Apply(projectile, test2);
            test1.Draw(spriteBatch);
            test2.Draw(spriteBatch);
            spriteBatch.End(); spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            //--
            return false;
        }
    }
    public class LavaDrop : ModProjectile
    {
        int timer;
        int oldDamage;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lave Droplet");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.CloneDefaults(ProjectileID.BoneDagger);
            aiType = ProjectileID.BoneDagger;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (oldDamage < 1)
            {
                oldDamage = projectile.damage;
                projectile.damage = 0;
            }
            if (timer++ > 20)
                projectile.damage = oldDamage;
            timer++;
            Dust dust = Dust.NewDustDirect(projectile.position + projectile.velocity, 4, 4, DustID.Fire, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f);
            dust.velocity = Vector2.Zero;
            dust.noGravity = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.7f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.7f;
            }
            return false;
        }
    }
}
