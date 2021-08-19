using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using Terraria.Graphics.Shaders;

namespace CastledsContent.NPCs.Flayke.Items.Summon
{
    public class NorthStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Northern Star");
            Tooltip.SetDefault("'The universal symbol of hope'\nCalls forth a mage of the icy winds\nCan be retrieved after usage\nOnly usable in the tundra at nightime");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Green;
            item.value = 12500;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.consumable = true;
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                var lineshader = GameShaders.Armor.GetShaderFromItemId(ItemID.TwilightDye);
                lineshader.Apply(null);
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        public override bool CanUseItem(Player player) => !Main.dayTime && player.ZoneSnow && player.ZoneOverworldHeight && player.ownedProjectileCounts[ModContent.ProjectileType<NorthStarProjectile>()] < 1 && !NPC.AnyNPCs(ModContent.NPCType<Flayke>());
        public override bool UseItem(Player player)
        {
            NorthStarProjectile proj = Main.projectile[Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<NorthStarProjectile>(), 0, 0)].modProjectile as NorthStarProjectile;
            proj.setPos = player.position;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddRecipeGroup("CastledsContent:EvilBar", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class NorthStarProjectile : ModProjectile
    {
        public override string Texture => Flayke.Directory + "Items/Summon/NorthStar";
        #region State Bool Indexing
        public const int STB_SetUp = 0;
        public const int STB_Aiming = 1;
        public const int STB_Launched = 2;
        public const int STB_PendingInact = 3;
        #endregion
        #region Hover Indexing
        public const int H_X = 0;
        public const int H_Value = 1;
        public const int H_Duration = 2;
        #endregion
        bool summon;
        public bool[] stB = new bool[4];
        public float[] lifeTime = new float[3];
        public Vector2 setPos;
        public float[] hover = new float[6];
        float scale;
        float sound;
        public override void SetDefaults()
        {

            projectile.width = 22;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(0, -22.5f);
            projectile.timeLeft = 3600;
            hover[H_Value] = offset.Y + (((float)Math.Cos(0.25f * hover[H_X]) * 12) + 12);
            projectile.position = new Vector2(setPos.X + offset.X, setPos.Y + (hover[H_Value] * 2f));
            if (!stB[STB_SetUp])
            {
                if (sound++ > 20 * (hover[H_Duration] / 90))
                {
                    Main.PlaySound(SoundID.Item7, projectile.Center);
                    sound = 0;
                }
                projectile.rotation += (((float)Math.Cos(0.25f * hover[H_X]) * 90) + 90) * 0.01f;
                if (hover[H_Duration]++ <= 90)
                {
                    hover[H_X] += (float)(4 * Math.PI) / 90;
                    projectile.alpha -= 3;
                    if (projectile.alpha < 0)
                        projectile.alpha = 0;
                }
                else
                {
                    if (!summon)
                    {
                        Main.PlaySound(SoundID.Item29, projectile.Center);
                        Flayke.BlastEffect(projectile.Center, new Color[2] { Color.White, Color.DarkBlue });
                        NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y - 400, ModContent.NPCType<Flayke>());
                        summon = true;
                    }
                    projectile.rotation = 0;
                    stB[STB_SetUp] = true;
                }
            }
            GlowUpdate();
            void GlowUpdate()
            {
                lifeTime[0]++;
                if (lifeTime[0] > 60 && lifeTime[1] < 1)
                {
                    lifeTime[1] += 0.02f;
                    lifeTime[2] += (float)Math.PI / 120;
                    scale = (float)Math.Sin(lifeTime[2]) + 0.5f;
                }
                else
                {
                    if (lifeTime[2] < Math.PI)
                        lifeTime[2] += (float)Math.PI / 240;
                    scale = (float)Math.Sin(lifeTime[2]) + 0.5f;
                }
                if (lifeTime[2] == Math.PI / 2)
                    Main.PlaySound(SoundID.Item29, projectile.Center);
                if (lifeTime[0] > 300)
                    projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Item item = Main.item[Item.NewItem(projectile.getRect(), ModContent.ItemType<NorthStar>())];
            item.velocity = Vector2.Zero;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => false;
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D glow = ModContent.GetTexture(Flayke.Directory + "Items/Summon/NorthStarBehind");
            Vector2 origin = new Vector2(tex.Width / 2, tex.Height / 2);
            Rectangle rectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            Color drawColor = new Color(lightColor.R - projectile.alpha, lightColor.G - projectile.alpha, lightColor.B - projectile.alpha, lightColor.A - projectile.alpha);
            Color glowColor = new Color(Valuate(0, lightColor.R - projectile.alpha, lifeTime[1]), Valuate(0, lightColor.G - projectile.alpha, lifeTime[1]), Valuate(0, lightColor.B - projectile.alpha, lifeTime[1]), Valuate(0, lightColor.A - projectile.alpha, lifeTime[1]));
            spriteBatch.Draw(glow, projectile.Center - Main.screenPosition, new Rectangle(0, 0, glow.Width, glow.Height), glowColor * 2, projectile.rotation, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, rectangle, drawColor, projectile.rotation, origin, 1, SpriteEffects.None, 0f);
            int Valuate(float f1, float f2, float amount) => MiscUtilities.Round(MathHelper.Lerp(f1, f2, amount));
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.Width += 16;
            projHitbox.Height += 16;
            return projHitbox.Intersects(targetHitbox);
        }
    }
}
