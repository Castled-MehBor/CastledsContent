using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Projectiles.DualForce.NightDemon;

namespace CastledsContent.NPCs.Boss.DualForce.NightDemon
{
    [AutoloadBossHead]
    public class NightHusk : ModNPC
    {
        public bool isDecapitated;
        public bool isWarping;
        public bool isAttacking;
        public int rotationAxis;
        public int chargeDistance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grakos the Warlord");
            Main.npcFrameCount[npc.type] = 23;
        }

        public override void SetDefaults()
        {
            aiType = 10;
            npc.lifeMax = 7000;
            npc.damage = 0;
            npc.defense = 18;
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
            npc.DeathSound = SoundID.NPCDeath6;
            npc.buffImmune[31] = true;
            npc.buffImmune[39] = true;
            npc.buffImmune[44] = true;
            npc.buffImmune[72] = true;
            npc.buffImmune[153] = true;
            npc.buffImmune[20] = true;
            npc.buffImmune[69] = true;
            npc.buffImmune[24] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2");
            if (Main.expertMode)
            {
                bossBag = mod.ItemType("GrakosLockboxExpert");
            }
            else
            {
                bossBag = mod.ItemType("GrakosLockbox");
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
            if (Main.rand.Next(19) == 0)
            {
                Main.PlaySound(SoundID.PlayerHit, npc.position);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 5, 0f, 0f, 0, default, 1f);
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
                if (isDecapitated == true)
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
                    if (Timer < 300)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            if (npc.ai[1] >= 20)
                            {
                                float Speed = 9f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<LineofSight>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer == 298)
                        {
                            isAttacking = false;
                        }
                    }
                }
                //Alt Deadeye's Last Wish
                if (rotationAxis == 6)
                {
                    if (Timer < 300)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer > 3)
                        {
                            if (npc.ai[1] >= 20)
                            {
                                float Speed = 9f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<LineofSight>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer == 298)
                        {
                            isAttacking = false;
                        }
                    }
                }
                //Bottled Ichor
                if (rotationAxis == 2 || rotationAxis == 3)
                {
                    if (Timer < 66)
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
                            float Speed = 3f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 22;
                            int type = ModContent.ProjectileType<FlaskIchor>();
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            npc.frameCounter = 92;
                        }
                        if (Timer == 61)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 6f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 22;
                            int type = ModContent.ProjectileType<FlaskIchor>();
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            npc.frameCounter = 98;
                        }
                        if (Timer == 62)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 9f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 22;
                            int type = ModContent.ProjectileType<FlaskIchor>();
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                            npc.frameCounter = 104;
                        }
                        if (Timer == 63)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 12f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 22;
                            int type = ModContent.ProjectileType<FlaskIchor>();
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            npc.frameCounter = 98;
                        }
                        if (Timer == 64)
                        {
                            Main.PlaySound(SoundID.Item1, npc.position);
                            float Speed = 15f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 22;
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
                    if (Timer < 450)
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

                                float Speed = 8f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 0;
                                int type = ModContent.ProjectileType<CursedFlameRot5>();
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                        }
                        if (Timer > 125)
                        {
                            if (npc.ai[1] >= 35)
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
                        if (Timer == 448)
                        {
                            isAttacking = false;
                        }
                    }
                }
                if (Main.expertMode)
                {
                    //Vomit
                    if (rotationAxis == 7)
                    {
                        if (Timer < 130)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer < 125)
                            {
                                if (npc.ai[1] >= 35)
                                {
                                    Main.PlaySound(SoundID.Grass, npc.position);

                                    float Speed = 8f;
                                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                    int damage = 17;
                                    int type = 96;
                                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                    npc.ai[1] = 0;
                                }
                            }
                            if (Timer > 358)
                            {
                                if (npc.ai[1] >= 18)
                                {
                                    Main.PlaySound(SoundID.Grass, npc.position);

                                    float Speed = 8f;
                                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                    int damage = 17;
                                    int type = 96;
                                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                    npc.ai[1] = 0;
                                }
                            }
                            if (Timer == 128)
                            {
                                isAttacking = false;
                            }
                        }
                    }
                }
                if (Main.expertMode)
                {
                    //Rail Line
                    if (rotationAxis == 8)
                    {
                        if (Timer < 300)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer == 60)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 150;
                                npc.position.Y = Main.player[npc.target].position.Y + 150;
                            }
                            if (Timer < 240)
                            {
                                npc.position.Y = Main.player[npc.target].position.Y - Timer;
                            }
                            if (Timer == 240)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 150;
                            }
                            if (Timer > 240)
                            {
                                npc.position.X = Main.player[npc.target].position.X - Timer;
                                npc.position.Y = Main.player[npc.target].position.Y - 150;
                            }
                            if (Timer < 90)
                            {
                                if (npc.ai[1] >= 18)
                                {
                                    Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, npc.position);

                                    float Speed = 8f;
                                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                    int damage = 15;
                                    int type = 96;
                                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                    npc.ai[1] = 0;
                                }
                            }
                            if (Timer == 298)
                            {
                                isAttacking = false;
                            }
                        }
                    }
                }
                else
                {
                    //Rail Line
                    if (rotationAxis == 8)
                    {
                        if (Timer < 300)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer == 60)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 150;
                                npc.position.Y = Main.player[npc.target].position.Y + 150;
                            }
                            if (Timer < 240)
                            {
                                npc.position.Y = Main.player[npc.target].position.Y - Timer;
                            }
                            if (Timer == 240)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 150;
                            }
                            if (Timer > 240)
                            {
                                npc.position.X = Main.player[npc.target].position.X - Timer;
                                npc.position.Y = Main.player[npc.target].position.Y - 150;
                            }
                            if (Timer < 90)
                            {
                                if (npc.ai[1] >= 46)
                                {
                                    Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, npc.position);

                                    float Speed = 8f;
                                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                    int damage = 18;
                                    int type = 96;
                                    float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                    int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                    npc.ai[1] = 0;
                                }
                            }
                            if (Timer == 298)
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
        private const int Frame_Churn1 = 7;
        private const int Frame_Churn2 = 8;
        private const int Frame_Churn3 = 9;
        private const int Frame_Churn4 = 10;
        private const int Frame_Vomit1 = 11;
        private const int Frame_Vomit2 = 12;
        private const int Frame_Vomit3 = 13;
        private const int Frame_Clean1 = 14;
        private const int Frame_Clean2 = 15;
        private const int Frame_Clean3 = 16;
        //Transformation Frames
        private const int Frame_Stare = 17;
        private const int Frame_StareAlt = 18;
        private const int Frame_Death = 19;
        private const int Frame_Death1 = 20;
        private const int Frame_Death2 = 21;
        private const int Frame_Death3 = 22;
        public override void FindFrame(int frameHeight)
        {
            //Teleporting
            if (isWarping == true && isDecapitated == false && isAttacking == false)
            {
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 56)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 60)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 64)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 68)
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
                    else if (npc.frameCounter < 72)
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
            //Deadeye
            if (isAttacking == true && rotationAxis == 1)
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
            //Ichor Impulse
            if (isAttacking == true && rotationAxis == 8)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 120)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter < 130)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 140)
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
            if (isAttacking == true && rotationAxis == 6)
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
            //Vomit
            if (isAttacking == true && rotationAxis == 7)
            {
                if (npc.frameCounter == 70)
                {
                    Main.PlaySound(SoundID.NPCDeath13, npc.position);
                }
            }
            if (isAttacking == true && rotationAxis == 7)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 4)
                {
                    npc.frame.Y = Frame_Churn1 * frameHeight;
                }
                else if (npc.frameCounter < 8)
                {
                    npc.frame.Y = Frame_Churn2 * frameHeight;
                }
                else if (npc.frameCounter < 16)
                {
                    npc.frame.Y = Frame_Churn3 * frameHeight;
                }
                else if (npc.frameCounter == 24)
                {
                    npc.frame.Y = Frame_Churn4 * frameHeight;
                }
                else if (npc.frameCounter < 70)
                {
                    npc.frame.Y = Frame_Vomit1 * frameHeight;
                }
                else if (npc.frameCounter < 80)
                {
                    npc.frame.Y = Frame_Vomit2 * frameHeight;
                }
                else if (npc.frameCounter < 90)
                {
                    npc.frame.Y = Frame_Vomit3 * frameHeight;
                }
                else if (npc.frameCounter < 110)
                {
                    npc.frame.Y = Frame_Clean1 * frameHeight;
                }
                else if (npc.frameCounter < 138)
                {
                    npc.frame.Y = Frame_Clean2 * frameHeight;
                }
                else if (npc.frameCounter < 144)
                {
                    npc.frame.Y = Frame_Clean3 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Attack Animation
            //CursedFlame
            if (isAttacking == true && rotationAxis == 5)
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
            if (isAttacking == true && rotationAxis == 2)
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
            if (isAttacking == true && rotationAxis == 3)
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
            if (isDecapitated == true && !CastledWorld.downedDualForce == true)
            {
                if (npc.frameCounter == 90)
                {
                    Main.NewText("[c/45244B:It's not often that I do this...]");
                }
                if (npc.frameCounter == 120)
                {
                    Main.NewText("[c/9B1E1E:An echoing travels through your brain...]");
                }
                if (npc.frameCounter == 180)
                {
                    Main.NewText("[c/441919:Terrarian, you impressed me.]");
                }
                if (npc.frameCounter == 240)
                {
                    Main.NewText("[c/441919:You finished the test and excelled at all of those elements.]");
                }
                if (npc.frameCounter == 420)
                {
                    Main.NewText("[c/441919:You will soon receive your reward.]");
                }
                if (npc.frameCounter == 600)
                {
                    Main.NewText("[c/441919:Terrarian, remember this...]");
                }
                if (npc.frameCounter == 660)
                {
                    Main.NewText("[c/441919:YOU are the chosen one...]");
                }
                if (npc.frameCounter == 770)
                {
                    Main.NewText("[c/441919:Be careful out there, terrarian.]");
                }
            }
            {
                if (isDecapitated == true)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 70)
                    {
                        npc.frame.Y = Frame_Stare * frameHeight;
                    }
                    else if (npc.frameCounter < 80)
                    {
                        npc.frame.Y = Frame_StareAlt * frameHeight;
                    }
                    else if (npc.frameCounter < 90)
                    {
                        npc.frame.Y = Frame_Stare * frameHeight;
                    }
                    else if (npc.frameCounter < 100)
                    {
                        npc.frame.Y = Frame_StareAlt * frameHeight;
                    }
                    else if (npc.frameCounter < 590)
                    {
                        npc.frame.Y = Frame_Stare * frameHeight;
                    }
                    else if (npc.frameCounter < 660)
                    {
                        npc.frame.Y = Frame_Nizoth * frameHeight;
                    }
                    else if (npc.frameCounter < 770)
                    {
                        npc.frame.Y = Frame_NizothAim * frameHeight;
                    }
                    else if (npc.frameCounter < 800)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 900)
                    {
                        npc.frame.Y = Frame_Death1 * frameHeight;
                    }
                    else if (npc.frameCounter < 970)
                    {
                        npc.frame.Y = Frame_Death2 * frameHeight;
                    }
                    else
                    {
                        npc.life = 0;
                        CastledWorld.downedDualForce = true;
                        Main.NewText("[c/53347a:As Grakos leaves, he leaves behind potions and a departure gift.]");
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.HealingPotion, 15);
                        npc.DropBossBags();
                        Main.PlaySound(SoundID.DD2_DrakinDeath, npc.position);
                        Main.PlaySound(SoundID.NPCDeath6, npc.position);
                        Color color = new Color();
                        Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                        int count = 60;
                        for (int i = 1; i <= count; i++)
                        {
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
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
            if (NPC.AnyNPCs(mod.NPCType("LightMage")))
            {
                Main.NewText("[c/F38FFF:Terrarian, You're almost there!]");
                Main.PlaySound(SoundID.DD2_DrakinDeath, npc.position);
                Main.PlaySound(SoundID.NPCDeath6, npc.position);
                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = 60;
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                }
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 0.6f);
                return true;
            }
            else if (NPC.AnyNPCs(mod.NPCType("LightNymph")))
            {
                Main.NewText("[c/F38FFF:Terrarian, You're almost there!]");
                Main.PlaySound(SoundID.DD2_DrakinDeath, npc.position);
                Main.PlaySound(SoundID.NPCDeath6, npc.position);
                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = 60;
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 75, 0, 0, 100, color, 1.5f);
                }
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke1, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke2, 0.6f);
                Gore.NewGore(npc.position, npc.velocity, GoreID.ChimneySmoke3, 0.6f);
                return true;
            }
            else
            {
                npc.life = npc.lifeMax;
                isDecapitated = true;
                return false;
            }
        }
    }
}
