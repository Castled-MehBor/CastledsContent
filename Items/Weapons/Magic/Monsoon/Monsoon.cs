using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Magic.Monsoon
{
    public class Monsoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monsoon");
            Tooltip.SetDefault("'Feel the wrath of the ocean!'\nCast bolts of water magic that create rain clouds upon death which explode into even more clouds.\nNot releasing the projectile after a second will automatically release it.\nRight-Click to kill the projectile\nA maximum of nine stationary clouds may exist at a time.");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;
            item.channel = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 35;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 75000;
            item.rare = ItemRarityID.LightRed;
            item.mana = 14;
            item.autoReuse = false;
            item.UseSound = SoundID.Item21;
            item.shoot = ProjectileType<MonsoonP>();
            item.shootSpeed = 36f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddIngredient(ItemID.NimbusRod);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //int numberProjectiles = 3 + Main.rand.Next(2);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
    public class MonsoonP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydromancy Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.MagicMissile);
            aiType = ProjectileID.NebulaBlaze2;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (Main.mouseRight && owner.HeldItem.type == ModContent.ItemType<Monsoon>())
                projectile.Kill();
            if (projectile.ai[1] < 45)
            {
                projectile.ai[1]++;
                projectile.ai[0] = 0f;
            }
            if (projectile.ai[1] >= 45)
            {
                projectile.aiStyle = 1;
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
                    projectile.velocity.X = (projectile.velocity.X * (float)(num149 + 1) + num146) / (float)num149;
                    projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 + 1) + num147) / (float)num149;
                }
                #endregion
            }
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            for (int a = 0; a < 3; a++)
                Projectile.NewProjectile(new Vector2(projectile.position.X + Main.rand.Next(40) - 20f, projectile.position.Y + Main.rand.NextFloat(40) - 20f), Vector2.Zero, ProjectileType<RainCloud>(), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
    public class RainCloud : ModProjectile
    {
        int frameCount;
        Vector2 origin;
        bool originDetermined = false;
        public override string Texture => "Terraria/Projectile_" + ProjectileID.RainCloudRaining;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rain Cloud");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 28;
            projectile.light = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.CloneDefaults(ProjectileID.RainCloudRaining);
            aiType = ProjectileID.RainCloudRaining;
            Main.projFrames[projectile.type] = 6;
        }
        public override void AI()
        {
            if (Main.player[projectile.owner].ownedProjectileCounts[projectile.type] > 9)
                projectile.Kill();
                if (!originDetermined)
                DetermineOrigin();
            if (projectile.timeLeft < 90)
                projectile.position = new Vector2(origin.X + Main.rand.Next(-3, 3), origin.Y + Main.rand.Next(-3, 3));
            frameCount++;
            if (frameCount > 20)
            {
                projectile.frame++;
                frameCount = 0;
            }
            if (projectile.frame > 5)
            {
                projectile.frame = 1;
            }
        }
        void DetermineOrigin()
        {
            origin = projectile.position;
            originDetermined = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item21, projectile.position);
            Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(Main.rand.Next(5) - 2.5f, Main.rand.Next(5) - 2.5f), ProjectileID.RainCloudRaining, projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}