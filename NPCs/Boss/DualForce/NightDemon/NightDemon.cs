using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Projectiles.DualForce.NightDemon;

namespace CastledsContent.NPCs.Boss.DualForce.NightDemon
{
    [AutoloadBossHead]
    public class NightDemon : ModNPC
    {
        public bool isNotAmused;
        public bool isWarping;
        public bool isAttacking;
        public int rotationAxis;
        public bool introducingHimself = true;
        public int chargeDistance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grakos the Warlord");
            Main.npcFrameCount[npc.type] = 20;
        }

        public override void SetDefaults()
        {
            aiType = 10;
            npc.lifeMax = 7000;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 30;
            npc.height = 72;
            npc.value = Item.buyPrice(0, 0, 0, 1);
            npc.npcSlots = 20f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[31] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.buffImmune[44] = true;
            npc.buffImmune[72] = true;
            npc.buffImmune[69] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2");
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.Next(9) == 0)
            {
                Main.PlaySound(SoundID.PlayerHit, npc.position);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 5, 0f, 0f, 0, default, 1f);
            }
            if (Main.rand.Next(99) == 0)
            {
                Main.PlaySound(mod.GetSoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CultistLaugh"), npc.position);
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000;
        }
        public override void NPCLoot()
        {
            Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, npc.position);
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -500f);
                    npc.active = false;
                }
            }
            npc.netUpdate = true;
            {
                npc.ai[1]++;
                npc.ai[1] += 0;
                if (npc.ai[1] % 240 == 6 && isAttacking == false && introducingHimself == false)
                {
                    isWarping = true;
                }
                if (rotationAxis > 8)
                {
                    rotationAxis = 1;
                }
                if (npc.life < 1)
                {
                    npc.life = 0;
                    npc.checkDead();
                }
                if (isNotAmused == true)
                {
                    npc.dontTakeDamage = true;
                }
                else
                {
                    npc.dontTakeDamage = false;
                }
                //rotationAxis Positions
                if (rotationAxis == 1)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 200;
                }
                else
                if (rotationAxis == 2)
                {
                    npc.position.X = Main.player[npc.target].position.X - 200;
                }
                else
                if (rotationAxis == 3)
                {
                    npc.position.X = Main.player[npc.target].position.X + 300;
                }
                else
                if (rotationAxis == 4)
                {
                    npc.position.Y = Main.player[npc.target].position.Y + 200;

                }
                else
                if (rotationAxis == 5)
                {
                    npc.position.Y = Main.player[npc.target].position.Y + 200;
                    npc.position.X = Main.player[npc.target].position.X + 250;

                }
                else
                if (rotationAxis == 6)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 150;
                    npc.position.X = Main.player[npc.target].position.X + 250;

                }
                else
                if (rotationAxis == 7)
                {
                    npc.position.X = Main.player[npc.target].position.X - 300;
                    npc.position.Y = Main.player[npc.target].position.Y - 150;

                }
                else
                if (rotationAxis == 8)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 175;

                }
                //Deadeye's Last Wish
                if (rotationAxis == 1)
                {
                    if (Timer < 150)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            if (npc.ai[1] >= 10)
                            {
                                float Speed = 5f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<LineofSight>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer == 148)
                        {
                            isAttacking = false;
                        }
                    }
                }
                //Alt Deadeye's Last Wish
                if (rotationAxis == 6)
                {
                    if (Timer < 150)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            if (npc.ai[1] >= 10)
                            {
                                float Speed = 18f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<LineofSight>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer == 148)
                        {
                            isAttacking = false;
                        }
                    }
                }
                //Bottled Ichor

                    if (rotationAxis == 2 || rotationAxis == 3)
                    {
                        if (Timer < 63)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                npc.frameCounter = 1;
                                isAttacking = true;
                            }
                            if (Timer == 60)
                            {
                                Main.PlaySound(SoundID.Item1, npc.position);
                                float Speed = 6f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 18;
                                int type = ModContent.ProjectileType<FlaskIchor>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                npc.frameCounter = 92;
                            }
                            if (Timer == 61)
                            {
                                Main.PlaySound(SoundID.Item1, npc.position);
                                float Speed = 9f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 18;
                                int type = ModContent.ProjectileType<FlaskIchor>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                npc.frameCounter = 98;
                            }
                            if (Timer == 62)
                            {
                                Main.PlaySound(SoundID.Item1, npc.position);
                                float Speed = 12f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 18;
                                int type = ModContent.ProjectileType<FlaskIchor>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                                npc.frameCounter = 104;
                            }
                        }
                    
                }
                if (rotationAxis == 5)
                {
                    if (Timer < 182)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            npc.frameCounter = 1;
                            isAttacking = true;
                        }
                        if (Timer > 45)
                        {
                            if (npc.ai[1] >= 35)
                            {
                                Main.PlaySound(SoundID.Grass, npc.position);

                                float Speed = 5f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<CursedFlameRot5>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer == 180)
                        {
                            isAttacking = false;
                        }
                    }
                }
                if (Main.expertMode)
                {
                    if (rotationAxis == 8)
                    {
                        if (Timer < 182)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                npc.frameCounter = 1;
                                isAttacking = true;
                            }
                            if (Timer > 45)
                            {
                                if (npc.ai[1] >= 55)
                                {
                                    Main.PlaySound(SoundID.Grass, npc.position);

                                    float Speed = 5f;
                                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                    int damage = 0;
                                    int type = ModContent.ProjectileType<IchorRot8>();
                                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                    npc.ai[1] = 0;
                                }
                            }
                            if (Timer == 180)
                            {
                                isAttacking = false;
                            }
                        }
                    }
                }
            }
        }
                        //Standard Frames
        private const int Frame_Idle = 0;
        private const int Frame_Throw1 = 1;
        private const int Frame_Throw2 = 2;
        private const int Frame_Throw3 = 3;
        private const int Frame_Throw4 = 4;
        private const int Frame_Nizoth = 5;
        private const int Frame_NizothAim = 6;
        //Transformation Frames
        private const int Frame_MorphRealize1 = 7;
        private const int Frame_MorphRealize2 = 8;
        private const int Frame_MorphClean1 = 9;
        private const int Frame_MorphClean2 = 10;
        private const int Frame_MorphClean3 = 11;
        private const int Frame_MorphClean4 = 12;
        private const int Frame_PrimeChage = 13;
        private const int Frame_Envelope1 = 14;
        private const int Frame_Envelope2 = 15;
        private const int Frame_Envelope3 = 16;
        private const int Frame_Impact1 = 17;
        private const int Frame_Impact2 = 18;
        private const int Frame_MorphHuskNPCChange = 19;
        public override void FindFrame(int frameHeight)
        {
            //Introduction Effects
            if (introducingHimself == true)
            {
                if (npc.frameCounter == 540)
                {
                    Main.NewText("[c/9B1E1E:An echoing travels through your brain...]");
                }
                if (npc.frameCounter == 620)
                {
                    Main.NewText("[c/441919:This is a test which will test you in every element of combat.]");
                }
                if (npc.frameCounter == 800)
                {
                    Main.NewText("[c/441919:Agility, Resiliance and Strength.]");
                }
                if (npc.frameCounter == 920)
                {
                    Main.NewText("[c/441919:However, if you defeat both of us...]");
                }
                if (npc.frameCounter == 975)
                {
                    Main.NewText("[c/441919:You will be rewarded like a King/Queen.]");
                }
            }
            //Introduction
            if (introducingHimself == true)
            {
                npc.position.X = Main.player[npc.target].position.X + 100;
                npc.position.Y = Main.player[npc.target].position.Y;
                if (CastledWorld.dualForceEncounter == 0)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 1120)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    if (npc.frameCounter < 1140)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 1180)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 1190)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 1200)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 1210)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        introducingHimself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                if (CastledWorld.dualForceEncounter == 1 && !CastledWorld.downedDualForce == true)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 400)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 405)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 410)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 420)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 430)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 440)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        introducingHimself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                if (CastledWorld.dualForceEncounter == 2 && !CastledWorld.downedDualForce == true)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 400)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 405)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 410)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 420)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 430)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 440)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        introducingHimself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                if (CastledWorld.dualForceEncounter > 2 && !CastledWorld.downedDualForce == true || CastledWorld.downedDualForce)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 100)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 110)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 120)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 130)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 140)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        introducingHimself = false;
                        npc.dontTakeDamage = false;
                    }
                }
            }
            //Teleporting
            if (isWarping == true && isNotAmused == false && isAttacking == false && introducingHimself == false)
            {
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 196)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 200)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 204)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 208)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.DD2_LightningBugZap, npc.position);
                        rotationAxis += 1;

                            Color color = new Color();
                            Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                            int count = 35;
                            for (int i = 1; i <= count; i++)
                            {
                                int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                            }

                    }
                    else if (npc.frameCounter < 212)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        isWarping = false;
                    }
                }
            }
            //Attack Animation
            //Ichor Flask
            if (isAttacking == true && rotationAxis == 1 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 82)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 92)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 98)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 104)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //Ichor Impulse
            if (isAttacking == true && rotationAxis == 8 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 82)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 92)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 98)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 104)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //Deadeye
            if (isAttacking == true && rotationAxis == 1 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 35)
                {
                    npc.frame.Y = Frame_Nizoth * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_NizothAim * frameHeight;
                }
                else if (npc.frameCounter > 300)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //Deadeye Alt
            if (isAttacking == true && rotationAxis == 6 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 35)
                {
                    npc.frame.Y = Frame_Nizoth * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_NizothAim * frameHeight;
                }
                else if (npc.frameCounter > 300)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //CursedFlame
            if (isAttacking == true && rotationAxis == 5 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 82)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 92)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 98)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 104)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //Ichor Flask
            if (isAttacking == true && rotationAxis == 2 && introducingHimself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 82)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 86)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 92)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 98)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 104)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            {
                if (isNotAmused == true && introducingHimself == false)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 5)
                    {
                        npc.frame.Y = Frame_MorphRealize1 * frameHeight;
                        Main.PlaySound(SoundID.NPCHit, npc.position);
                    }
                    else if (npc.frameCounter < 20)
                    {
                        npc.frame.Y = Frame_MorphRealize2 * frameHeight;
                    }
                    else if (npc.frameCounter < 80)
                    {
                        npc.frame.Y = Frame_MorphClean1 * frameHeight;
                    }
                    else if (npc.frameCounter < 88)
                    {
                        npc.frame.Y = Frame_MorphClean2 * frameHeight;
                    }
                    else if (npc.frameCounter < 96)
                    {
                        npc.frame.Y = Frame_MorphClean3 * frameHeight;
                    }
                    else if (npc.frameCounter < 104)
                    {
                        npc.frame.Y = Frame_MorphClean4 * frameHeight;
                    }
                    else if (npc.frameCounter < 350)
                    {
                        npc.frame.Y = Frame_PrimeChage * frameHeight;
                    }
                    else if (npc.frameCounter < 355)
                    {
                        npc.frame.Y = Frame_Envelope1 * frameHeight;
                    }
                    else if (npc.frameCounter < 360)
                    {
                        npc.frame.Y = Frame_Envelope2 * frameHeight;
                    }
                    else if (npc.frameCounter < 375)
                    {
                        npc.frame.Y = Frame_Envelope3 * frameHeight;
                    }
                    else if (npc.frameCounter < 380)
                    {
                        npc.frame.Y = Frame_Impact1 * frameHeight;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 35;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 390)
                    {
                        npc.frame.Y = Frame_Impact2 * frameHeight;
                    }
                    else if (npc.frameCounter < 400)
                    {
                        npc.frame.Y = Frame_MorphHuskNPCChange * frameHeight;
                    }
                    else
                    {
                        Main.PlaySound(SoundID.DD2_LightningAuraZap, npc.position);
                        npc.frameCounter = 0;
                        npc.Transform(mod.NPCType("NightHusk"));
                    }
                }
            }
        }
        public override bool CheckDead()
        {
            npc.life = npc.lifeMax;
            isNotAmused = true;
            return false;
        }
    }
}
