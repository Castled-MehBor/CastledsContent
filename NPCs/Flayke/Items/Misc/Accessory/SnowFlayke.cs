using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ModLoader;
using CastledsContent.Utilities;

namespace CastledsContent.NPCs.Flayke.Items.Misc.Accessory
{
    public class SnowFlayke : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Flayke");
            Tooltip.SetDefault("'A polished emblem made of snow and ice'"
            + "\nIncreased defensive capabilities while in the tundra"
            + "\nIncreased damage by 8% whenever it rains"
            + "\nIncreases effectiveness of above effects while standing in a blizzard"
            + "\nEvery three seconds while using a weapon, a damaging, spinning circle of ice shards forms at the cursor position."
            + "\nToggle visibility to toggle this effect");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.width = 28;
            item.value = 15000;
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) 
        { 
            SnowFlaykeP mP = player.GetModPlayer<SnowFlaykeP>();  
            mP.snowFlayke = true;
            mP.doSnowFlayke = !hideVisual;
        }
    }
    public class SnowFlaykeP : ModPlayer
    {
        public bool snowFlayke;
        public bool doSnowFlayke;
        public int snowFlaykeT;

        public override void ResetEffects()
        {
            snowFlayke = false;
            doSnowFlayke = false;
        }
        public override void PostUpdateEquips()
        {
            if (snowFlayke)
            {
                int mult = Blizzard(player) ? 2 : 1;
                if (player.ZoneSnow)
                {
                    player.statDefense += 3 * mult;
                    player.endurance += 0.06f * mult;
                }
                if (Main.raining)
                    player.allDamage += 0.08f * mult;
                if (doSnowFlayke && snowFlaykeT >= 180)
                    SnowFlaykeProj();
            }
        }
        void SnowFlaykeProj()
        {
            snowFlaykeT = 0;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SnowFlaykeProj>()] < 1)
            {
                Main.PlaySound(Terraria.ID.SoundID.Item82, Main.MouseWorld);
                Main.PlaySound(Terraria.ID.SoundID.NPCDeath7.WithVolume(0.75f), Main.MouseWorld);
                for (int i = 0; i < 10; i++)
                {
                    Vector2 position = Main.MouseWorld + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 10 * i));
                    Dust dust = Dust.NewDustPerfect(position, 135);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(dust.position - Main.MouseWorld) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }
                SnowFlaykeProj proj = Projectile.NewProjectileDirect(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SnowFlaykeProj>(), player.HeldItem.damage, 0, player.whoAmI).modProjectile as SnowFlaykeProj;
                proj.left = Main.MouseWorld.X < player.position.X;
            }
        }
        bool Blizzard(Player player) => player.ZoneSnow && player.ZoneOverworldHeight && Main.raining;
    }
    public class SnowFlaykeI : GlobalItem
    {
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (player.controlUseItem && item.damage > 0)
            {
                SnowFlaykeP mP = player.GetModPlayer<SnowFlaykeP>();
                if (mP.doSnowFlayke)
                    mP.snowFlaykeT++;
            }
            return base.UseTimeMultiplier(item, player);
        }
    }
    public class SnowFlaykeProj : ModProjectile
    {
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public bool left;
        float[] spin = new float[4];
        float spinner = 360;
        int lifeTime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Shards");
        }
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.alpha = 100;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.friendly = true;
        }
        public override void AI()
        {
            projectile.timeLeft = 120;
            if (spin[0]++ > 75)
                spin[1]++;
            if (spin[0] > 90)
                projectile.Kill();
            spin[2] += (float)Math.PI / 90;
            spin[3] += (float)Math.Sin(spin[2]) * 0.5f;
            spin[3] *= left ? -1 : 0.33f;
            spinner += spin[3];
            if (spinner > 360)
                spinner = 0 + spin[3];
            if (lifeTime++ > 5)
            {
                lifeTime = 0;
                if (spin[1] == 0)
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D glow = ModContent.GetTexture(Flayke.Directory + "Items/Misc/Accessory/SnowFlaykeProjIMG");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                {
                    for(int a = 0; a < 6; a++)
                        spriteBatch.Draw(glow, n.coord - Main.screenPosition, new Rectangle(0, 0, glow.Width, glow.Height), n.GetColor(Color.CornflowerBlue), spinner + Rot(a), new Vector2((glow.Width / 2) + 32.5f, glow.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                }
            }
            for(int a = 0; a < 6; a++)
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(MiscUtilities.Round(lightColor.R - spin[1] * 17), MiscUtilities.Round(lightColor.G - spin[1] * 17), MiscUtilities.Round(lightColor.B - spin[1] * 17), MiscUtilities.Round(lightColor.A - spin[1] * 17)), spinner + Rot(a), new Vector2((glow.Width / 2) + 25, glow.Height / 2), 1, projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            float Rot(int a)
            {
                switch (a)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 90;
                    case 2:
                        return 59.75f;
                    case 3:
                        return 180;
                    case 4:
                        return 331.9f;
                    case 5:
                        return 45;
                    case 6:
                        return 315;
                }
                return 0;
            }
            return false;
        }
    }
}