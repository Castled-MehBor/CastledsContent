using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Boss.DualForce.LightMage
{
    [AutoloadBossHead]
    public class LightNymph : ModNPC
    {
        public bool isAttacking = false;
        public bool isDiscording = false;
        public bool isThanking = false;
        public int predictionTimer;
        public int rotationAxis;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nasha the Nymph");
            Main.npcFrameCount[npc.type] = 27;
        }

        public override void SetDefaults()
        {
            aiType = 10;
            npc.lifeMax = 7000;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 34;
            npc.height = 72;
            npc.value = Item.buyPrice(0, 0, 0, 1);
            npc.npcSlots = 20f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.buffImmune[31] = true;
            npc.buffImmune[39] = true;
            npc.buffImmune[44] = true;
            npc.buffImmune[72] = true;
            npc.buffImmune[153] = true;
            npc.buffImmune[20] = true;
            npc.buffImmune[69] = true;
            npc.buffImmune[70] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2");
            if (Main.expertMode)
            {
                bossBag = mod.ItemType("NashaLockboxExpert");
            }
            else
            {
                bossBag = mod.ItemType("NashaLockbox");
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.Next(12) == 0)
            {
                Main.PlaySound(SoundID.FemaleHit, npc.position);
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000;
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
                if (npc.ai[1] % 240 == 6 && isAttacking == false)
                {
                    isDiscording = true;
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
                if (isThanking == true)
                {
                    npc.dontTakeDamage = true;
                }
                else
                {
                    npc.dontTakeDamage = false;
                }
                //Defense Increase
                if (npc.life < npc.lifeMax * 0.75)
                {
                    npc.defense = 12;
                }
                if (npc.life < npc.lifeMax * 0.5)
                {
                    npc.defense = 15;
                }
                if (npc.life < npc.lifeMax * 0.25)
                {
                    npc.defense = 18;
                }
                if (npc.life < npc.lifeMax * 0.10)
                {
                    npc.defense = 25;
                }
                //rotationAxis Positions
                if (rotationAxis == 1)
                {
                    npc.position.Y = Main.player[npc.target].position.Y + 200;
                }
                else
                if (rotationAxis == 2)
                {
                    npc.position.X = Main.player[npc.target].position.X + 200;
                }
                else
                if (rotationAxis == 3)
                {
                    npc.position.X = Main.player[npc.target].position.X - 300;
                }
                else
                if (rotationAxis == 4)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 200;

                }
                else
                if (rotationAxis == 5)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 200;
                    npc.position.X = Main.player[npc.target].position.X - 250;

                }
                else
                if (rotationAxis == 6)
                {
                    if (predictionTimer < 95)
                    {
                        npc.position.Y = Main.player[npc.target].position.Y;
                        npc.position.X = Main.player[npc.target].position.X - 100;
                    }
                    if (predictionTimer == 95)
                    {
                        int num = Main.rand.Next(2);
                        if (num == 0)
                        {
                            npc.position.Y = Main.player[npc.target].position.Y - 100;
                        }
                        if (num == 1)
                        {
                            npc.position.Y = Main.player[npc.target].position.Y + 100;
                        }
                    }
                }
                else
                if (rotationAxis == 7)
                {
                    if (predictionTimer < 95)
                    {
                        npc.position.Y = Main.player[npc.target].position.Y;
                        npc.position.X = Main.player[npc.target].position.X + 100;
                    }
                    if (predictionTimer == 95)
                    {
                        int num = Main.rand.Next(2);
                        if (num == 0)
                        {
                            npc.position.Y = Main.player[npc.target].position.Y - 100;
                        }
                        if (num == 1)
                        {
                            npc.position.Y = Main.player[npc.target].position.Y + 100;
                        }
                    }
                }
                else
                if (rotationAxis == 8)
                {
                    npc.position.X = Main.player[npc.target].position.X + 125;
                    npc.position.Y = Main.player[npc.target].position.Y - 100;

                }
                //Attacks
                //Crystal Spear
                if (rotationAxis == 1)
                {
                    if (Timer < 90)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            float Speed = 18f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 0;
                            int type = mod.ProjectileType("WarningProj");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 88)
                        {
                            Main.PlaySound(SoundID.DD2_WitherBeastHurt, npc.position);

                            float Speed = 11f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 32;
                            int type = mod.ProjectileType("CrystalSpear");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                if (rotationAxis == 4)
                {
                    if (Timer < 90)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            float Speed = 18f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 0;
                            int type = mod.ProjectileType("WarningProj");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 88)
                        {
                            Main.PlaySound(SoundID.DD2_WitherBeastHurt, npc.position);

                            float Speed = 11f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 32;
                            int type = mod.ProjectileType("CrystalSpear");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                //Pink Potions
                if (rotationAxis == 2 || rotationAxis == 3)
                {
                    if (Timer < 63)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer == 60)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 3f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("PinkBottle");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                        }
                        if (Timer == 61)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 6f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("PinkBottle");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                        }
                        if (Timer == 62)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 9f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("PinkBottle");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                        if (Timer == 62)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 12f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("PinkBottle");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                        if (Timer == 62)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 15f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("PinkBottle");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                if (rotationAxis == 5)
                {
                    if (Timer < 60)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer == 58)
                        {
                            Main.PlaySound(SoundID.Item8, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 0;
                            int type = mod.ProjectileType("HallowOrb1");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                        if (Timer == 58)
                        {
                            Main.PlaySound(SoundID.Item8, npc.position);

                            float Speed = 10f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 0;
                            int type = mod.ProjectileType("HallowOrb1");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                if (rotationAxis == 6)
                {
                    if (Timer < 300)
                    {
                        predictionTimer++;
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer == 298)
                        {
                            Main.PlaySound(SoundID.Item88, npc.position);

                            float Speed = 10f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 35;
                            int type = mod.ProjectileType("LGauntlet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                if (Main.expertMode)
                {
                    if (rotationAxis == 7)
                    {
                        if (Timer < 300)
                        {
                            predictionTimer++;
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer > 125)
                            {
                                float Speed = 18f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = mod.ProjectileType("WarningProj");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            if (Timer == 298)
                            {
                                Main.PlaySound(SoundID.Item88, npc.position);

                                float Speed = 11f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 45;
                                int type = mod.ProjectileType("GauntletofRight");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                            }
                        }
                    }
                }
                if (Main.expertMode)
                {
                    if (rotationAxis == 8)
                    {
                        if (Timer < 118)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer == 116)
                            {
                                Main.PlaySound(SoundID.Item8, npc.position);

                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("HallowLantorch"));

                                isAttacking = false;
                            }
                        }
                    }
                }
            }
        }
        //Standard Frames
        //Standard Frames
        private const int Frame_Idle = 0;
        private const int Frame_Throw1 = 1;
        private const int Frame_Throw2 = 2;
        private const int Frame_Throw3 = 3;
        private const int Frame_Throw4 = 4;
        private const int Frame_SemiClose = 5; //Unused
        private const int Frame_EyeClosed = 6; //Unused
        private const int Frame_Mouth = 7;
        private const int Frame_Punch1 = 8;
        private const int Frame_Punch2 = 9;
        private const int Frame_Punch3 = 10;
        //Transformation Frames
        private const int Frame_PurpleHair1 = 11;
        private const int Frame_PurpleHair2 = 12;
        private const int Frame_PurpleHair3 = 13;
        private const int Frame_PurpleHair4 = 14;
        private const int Frame_PurpleHair5 = 15;
        private const int Frame_RobeChange1 = 16;
        private const int Frame_RobeChange2 = 17;
        private const int Frame_RobeChange3 = 18;
        private const int Frame_RobeChange4 = 19;
        private const int Frame_ThankA1 = 20;
        private const int Frame_ThankA2 = 21;
        private const int Frame_Goodbye1 = 22;
        private const int Frame_GoodbyeA1 = 23;
        private const int Frame_GoodbyeA2 = 24;
        private const int Frame_Teleport1 = 25;
        private const int Frame_Teleport2 = 26;
        public override void FindFrame(int frameHeight)
        {
            //Teleporting
            if (isDiscording == true && isThanking == false && isAttacking == false)
            {
                if (Main.expertMode)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 36)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 40)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 44)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 48)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 52)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        predictionTimer = 0;
                        isDiscording = false;
                    }
                }
                else
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 46)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 50)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 54)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 58)
                    {
                        npc.frame.Y = Frame_Throw4 * frameHeight;
                        Main.PlaySound(SoundID.Item8, npc.position);
                        rotationAxis += 1;

                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 15;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
                        }
                    }
                    else if (npc.frameCounter < 62)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                        Timer = 0;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                        isDiscording = false;
                    }
                }
            }
            //Attack Animation
            //Spear Throwing
            if (isAttacking == true && rotationAxis == 1)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 90)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 94)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 98)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 102)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 106)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Spear Throwing Alternate
            if (isAttacking == true && rotationAxis == 4)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 90)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 94)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 98)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 102)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 106)
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
            //Pink Potion
            if (isAttacking == true && rotationAxis == 2)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 46)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 50)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 54)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 58)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 62)
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
            //Pink Potion Alt
            if (isAttacking == true && rotationAxis == 3)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 46)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 50)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 54)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 58)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 62)
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
            //Hallowed Orb
            if (isAttacking == true && rotationAxis == 5)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_Mouth * frameHeight;

                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 6;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1f);
                    }
                }
                else if (npc.frameCounter < 40)
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
            //Gauntlet Unleash Left
            if (isAttacking == true && rotationAxis == 6)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_Punch1 * frameHeight;
                }
                else if (npc.frameCounter < 40)
                {
                    npc.frame.Y = Frame_Punch2 * frameHeight;
                }
                else if (npc.frameCounter < 50)
                {
                    npc.frame.Y = Frame_Punch3 * frameHeight;
                }
                else if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_Punch2 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = Frame_Punch1 * frameHeight;
                }
                else if (npc.frameCounter < 80)
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
            //Gauntlet Unleash Right
            if (isAttacking == true && rotationAxis == 7)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_Punch1 * frameHeight;
                }
                else if (npc.frameCounter < 40)
                {
                    npc.frame.Y = Frame_Punch2 * frameHeight;
                }
                else if (npc.frameCounter < 50)
                {
                    npc.frame.Y = Frame_Punch3 * frameHeight;
                }
                else if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_Punch2 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = Frame_Punch1 * frameHeight;
                }
                else if (npc.frameCounter < 80)
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
            //Lantern
            if (isAttacking == true && rotationAxis == 8)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 66)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 72)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 78)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 84)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Farewell Animation
            if (isThanking == true)
            {
                if (npc.frameCounter == 190)
                {
                    Main.NewText("[c/F38FFF:Terrarian, I have good news...]");
                }
                if (npc.frameCounter == 320)
                {
                    Main.NewText("[c/F38FFF:You have finished and excelled at the test!]");
                }
                if (npc.frameCounter == 420)
                {
                    Main.NewText("[c/F38FFF:You excelled at every element: Agility, Resistance and Strength.]");
                }
                if (npc.frameCounter == 490)
                {
                    Main.NewText("[c/F38FFF:And as promised, I will gift you your reward.]");
                }
                if (npc.frameCounter == 670)
                {
                    Main.NewText("[c/F38FFF:You are the chosen one, Terrarian.]");
                }
                if (npc.frameCounter == 820)
                {
                    Main.NewText("[c/F38FFF:You are ready for the rest of the spirits...]");
                }
                if (npc.frameCounter == 900)
                {
                    Main.NewText("[c/F38FFF:I wish you the best of luck!]");
                }
            }

            if (isThanking == true)
            {
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 45)
                    {
                        npc.frame.Y = Frame_PurpleHair1 * frameHeight;
                    }
                    else if (npc.frameCounter < 50)
                    {
                        npc.frame.Y = Frame_PurpleHair2 * frameHeight;
                    }
                    else if (npc.frameCounter < 55)
                    {
                        npc.frame.Y = Frame_PurpleHair3 * frameHeight;
                    }
                    else if (npc.frameCounter < 60)
                    {
                        npc.frame.Y = Frame_PurpleHair4 * frameHeight;
                    }
                    else if (npc.frameCounter < 120)
                    {
                        npc.frame.Y = Frame_PurpleHair5 * frameHeight;
                    }
                    else if (npc.frameCounter < 150)
                    {
                        npc.frame.Y = Frame_RobeChange1 * frameHeight;
                    }
                    else if (npc.frameCounter < 155)
                    {
                        npc.frame.Y = Frame_RobeChange2 * frameHeight;
                    }
                    else if (npc.frameCounter < 160)
                    {
                        npc.frame.Y = Frame_RobeChange3 * frameHeight;
                    }
                    else if (npc.frameCounter < 170)
                    {
                        npc.frame.Y = Frame_RobeChange4 * frameHeight;
                    }
                    else if (npc.frameCounter < 190)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 200)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 210)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 220)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 320)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 330)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 340)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 350)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 420)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 430)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 480)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 490)
                    {
                        npc.frame.Y = Frame_ThankA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 540)
                    {
                        npc.frame.Y = Frame_ThankA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 600)
                    {
                        npc.frame.Y = Frame_Goodbye1 * frameHeight;
                    }
                    else if (npc.frameCounter < 660)
                    {
                        npc.frame.Y = Frame_GoodbyeA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 670)
                    {
                        npc.frame.Y = Frame_GoodbyeA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 720)
                    {
                        npc.frame.Y = Frame_GoodbyeA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 730)
                    {
                        npc.frame.Y = Frame_GoodbyeA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 790)
                    {
                        npc.frame.Y = Frame_GoodbyeA2 * frameHeight;
                    }
                    else if (npc.frameCounter < 920)
                    {
                        npc.frame.Y = Frame_GoodbyeA1 * frameHeight;
                    }
                    else if (npc.frameCounter < 950)
                    {
                        npc.frame.Y = Frame_Teleport1 * frameHeight;
                    }
                    else if (npc.frameCounter < 1100)
                    {
                        npc.frame.Y = Frame_Teleport2 * frameHeight;
                    }
                    else
                    {
                        npc.life = 0;
                        CastledWorld.downedDualForce = true;
                        Main.NewText("[c/53347a:As Nasha leaves, she leaves behind potions and a departure gift.]");
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.HealingPotion, 15);
                        npc.DropBossBags();
                        Main.PlaySound(SoundID.Item4, npc.position);
                        Main.PlaySound(SoundID.NPCDeath6, npc.position);
                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 60;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
                        }
                        Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 0.6f);
                        Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 0.6f);
                        Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 0.6f);
                        npc.frameCounter = 0;
                    }
                }
            }
        }
        public override bool CheckDead()
        {
            if (NPC.AnyNPCs(mod.NPCType("NightDemon")))
            {
                Main.NewText("[c/441919:You have to get past me if you want to be successful!]");
                Main.PlaySound(SoundID.Item4, npc.position);
                Main.PlaySound(SoundID.NPCDeath6, npc.position);
                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = 60;
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
                }
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 0.6f);
                return true;
            }
            else if (NPC.AnyNPCs(mod.NPCType("NightHusk")))
            {
                Main.NewText("[c/441919:You have to get past me if you want to be successful!]");
                Main.PlaySound(SoundID.Item4, npc.position);
                Main.PlaySound(SoundID.NPCDeath6, npc.position);
                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = 60;
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
                }
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 0.6f);
                return true;
            }
            else
            {
                npc.life = npc.lifeMax;
                isThanking = true;
                return false;
            }
        }
    }
}

