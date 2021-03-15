using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Boss.DualForce.LightMage
{
    [AutoloadBossHead]
    public class LightMage : ModNPC
    {
        public bool isAttacking = false;
        public bool isBlinking = false;
        public bool isDiscording = false;
        public bool isTransforming = false;
        public bool introducingHerself = true;
        public int rotationAxis;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nasha the Nymph");
            Main.npcFrameCount[npc.type] = 21;
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
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[31] = true;
            npc.buffImmune[39] = true;
            npc.buffImmune[44] = true;
            npc.buffImmune[72] = true;
            npc.buffImmune[153] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2");
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.Next(4) == 0)
            {
                Main.PlaySound(SoundID.FemaleHit, npc.position);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 5, 0f, 0f, 0, default, 1f);
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000;
        }
        public override void NPCLoot()
        {
            Main.PlaySound(SoundID.PlayerKilled, npc.position);
        }

        public override void AI()
        {
            if (Main.rand.Next(199) == 0)
            {
                isBlinking = true;
            }

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
                if (npc.ai[1] % 240 == 6 && isAttacking == false && introducingHerself == false)
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
                if (isTransforming == true)
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
                    npc.position.Y = Main.player[npc.target].position.Y + 150;
                    npc.position.X = Main.player[npc.target].position.X - 250;

                }
                else
                if (rotationAxis == 7)
                {
                    npc.position.X = Main.player[npc.target].position.X + 300;
                    npc.position.Y = Main.player[npc.target].position.Y + 150;

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
                    if (Timer < 180)
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
                        if (Timer == 179)
                        {
                            Main.PlaySound(SoundID.DD2_WitherBeastHurt, npc.position);

                            float Speed = 7f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 20;
                            int type = mod.ProjectileType("CrystalSpear");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
                        }
                    }
                }
                if (Main.expertMode)
                {
                    if (rotationAxis == 4)
                    {
                        if (Timer < 180)
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
                            if (Timer == 178)
                            {
                                Main.PlaySound(SoundID.DD2_WitherBeastHurt, npc.position);

                                float Speed = 7f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 20;
                                int type = mod.ProjectileType("CrystalSpear");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                            }
                        }
                    }
                }
                //Pink Potions
                if (Main.expertMode)
                {
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
                                int damage = 14;
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
                                int damage = 14;
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
                                int damage = 14;
                                int type = mod.ProjectileType("PinkBottle");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                            }
                        }
                    }
                }
                else
                {
                    if (rotationAxis == 2)
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
                                float Speed = 2f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 14;
                                int type = mod.ProjectileType("PinkBottle");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            if (Timer == 61)
                            {
                                Main.PlaySound(SoundID.Item1, npc.position);
                                float Speed = 4f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 14;
                                int type = mod.ProjectileType("PinkBottle");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            if (Timer == 62)
                            {
                                Main.PlaySound(SoundID.Item1, npc.position);
                                float Speed = 6f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 14;
                                int type = mod.ProjectileType("PinkBottle");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                            }
                        }
                    }
                }
                if (rotationAxis == 5)
                {
                    if (Timer < 121)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer == 120)
                        {
                            Main.PlaySound(SoundID.Item8, npc.position);

                            float Speed = 3f;
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
                if (Main.expertMode)
                {
                    if (rotationAxis == 6)
                    {
                        if (Timer < 76)
                        {
                            Timer++;
                            if (Timer > 2)
                            {
                                isAttacking = true;
                            }
                            if (Timer == 74)
                            {
                                Main.PlaySound(SoundID.Item, npc.position);

                                float Speed = 5f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 14;
                                int type = mod.ProjectileType("HallowBolt");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            if (Timer == 74)
                            {
                                Main.PlaySound(SoundID.Item, npc.position);

                                float Speed = 5f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width), npc.position.Y + (npc.height));
                                int damage = 14;
                                int type = mod.ProjectileType("HallowBolt");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height)), vector8.X - (P.position.X + (P.width)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;
                            }
                            if (Timer == 74)
                            {
                                Main.PlaySound(SoundID.Item, npc.position);

                                float Speed = 5f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 2), npc.position.Y + (npc.width * 2));
                                int damage = 14;
                                int type = mod.ProjectileType("HallowBolt");
                                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 2f)), vector8.X - (P.position.X + (P.width * 2f)));
                                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                                npc.ai[1] = 0;

                                isAttacking = false;
                            }
                        }
                    }
                }
                if (rotationAxis == 7)
                {
                    if (Timer < 50)
                    {
                        Timer++;
                        if (Timer > 2)
                        {
                            isAttacking = true;
                        }
                        if (Timer == 48)
                        {
                            Main.PlaySound(SoundID.Item, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                            int damage = 12;
                            int type = mod.ProjectileType("HallowPellet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.25f)), vector8.X - (P.position.X + (P.width * 0.25f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 48)
                        {
                            Main.PlaySound(SoundID.Item, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 12;
                            int type = mod.ProjectileType("HallowPellet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 48)
                        {
                            Main.PlaySound(SoundID.Item, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width), npc.position.Y + (npc.height));
                            int damage = 12;
                            int type = mod.ProjectileType("HallowPellet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height)), vector8.X - (P.position.X + (P.width)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 48)
                        {
                            Main.PlaySound(SoundID.Item, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 2), npc.position.Y + (npc.width * 2));
                            int damage = 12;
                            int type = mod.ProjectileType("HallowPellet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 2f)), vector8.X - (P.position.X + (P.width * 2f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;
                        }
                        if (Timer == 48)
                        {
                            Main.PlaySound(SoundID.Item, npc.position);

                            float Speed = 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 4), npc.position.Y + (npc.width * 4));
                            int damage = 12;
                            int type = mod.ProjectileType("HallowPellet");
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 4f)), vector8.X - (P.position.X + (P.width * 4f)));
                            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                            npc.ai[1] = 0;

                            isAttacking = false;
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
        private const int Frame_EyeClosed = 5;
        private const int Frame_EyeClosed1 = 6;
        private const int Frame_Mouth = 7;
        //Transformation Frames
        private const int Frame_MorphCrystalBreak = 8;
        private const int Frame_MorphCrystalBreak1 = 9;
        private const int Frame_MorphBleed = 10;
        private const int Frame_MorphBleed1 = 11;
        private const int Frame_MorphBleedEyeClose = 12;
        private const int Frame_MorphTransform1 = 13;
        private const int Frame_MorphTransform2 = 14;
        private const int Frame_MorphTransform3 = 15;
        private const int Frame_MorphTransform4 = 16;
        private const int Frame_MorphTransform5 = 17;
        private const int Frame_MorphTransform6 = 18;
        private const int Frame_MorphNymphEyeOpen = 19;
        private const int Frame_MorphNymphNPCChange = 20;
        public override void FindFrame(int frameHeight)
        {
            //Introduction Effects
            if (introducingHerself == true)
            {
                if (CastledWorld.dualForceEncounter == 0)
                {
                    if (npc.frameCounter == 130)
                    {
                        Main.NewText("[c/F38FFF:You!]");
                    }
                    if (npc.frameCounter == 200)
                    {
                        Main.NewText("[c/F38FFF:Yes, You!]");
                    }
                    if (npc.frameCounter == 300)
                    {
                        Main.NewText("[c/F38FFF:You released us from our prison, and we can't thank you enough.]");
                    }
                    if (npc.frameCounter == 410)
                    {
                        Main.NewText("[c/F38FFF:However..]");
                    }
                    if (npc.frameCounter == 1020)
                    {
                        int num = Main.rand.Next(3);
                        if (num == 0)
                        {
                            Main.NewText("[c/F38FFF:Ready?]");
                        }
                        if (num == 1)
                        {
                            Main.NewText("[c/F38FFF:Prête?]");
                        }
                        if (num == 2)
                        {
                            Main.NewText("[c/F38FFF:Kapiche?]");
                        }
                    }
                }
                if (CastledWorld.dualForceEncounter == 1 && !CastledWorld.downedDualForce == true)
                {
                    if (npc.frameCounter == 130)
                    {
                        Main.NewText("[c/F38FFF:Well...]");
                    }
                    if (npc.frameCounter == 220)
                    {
                        Main.NewText("[c/F38FFF:Don't worry! Most challengers usually don't last as long as you did!]");
                    }
                    if (npc.frameCounter == 310)
                    {
                        Main.NewText("[c/F38FFF:Don't feel ashamed, now!]");
                        Main.NewText("[c/F38FFF:As long as you now know how the test fully works, it's all OK!]");
                    }
                    if (npc.frameCounter == 400)
                    {
                        int num = Main.rand.Next(3);
                        if (num == 0)
                        {
                            Main.NewText("[c/F38FFF:Ready?]");
                        }
                        if (num == 1)
                        {
                            Main.NewText("[c/F38FFF:Prête?]");
                        }
                        if (num == 2)
                        {
                            Main.NewText("[c/F38FFF:Kapiche?]");
                        }
                    }
                }
                //Third times the charm
                if (CastledWorld.dualForceEncounter == 2 && !CastledWorld.downedDualForce == true)
                {
                    if (npc.frameCounter == 130)
                    {
                        Main.NewText("[c/F38FFF:Well...]");
                    }
                    if (npc.frameCounter == 220)
                    {
                        Main.NewText("[c/F38FFF:You're still doing very well!]");
                    }
                    if (npc.frameCounter == 310)
                    {
                        Main.NewText("[c/F38FFF:I wish you the best of luck,]");
                        Main.NewText("[c/F38FFF:and as they say...]");
                    }
                    if (npc.frameCounter == 400)
                    {
                        Main.NewText("[c/F38FFF:Third times the charm!]");
                    }
                }
                if (CastledWorld.dualForceEncounter > 2 && !CastledWorld.downedDualForce == true || CastledWorld.downedDualForce)
                {
                    if (npc.frameCounter == 2)
                    {
                        Main.NewText($"[c/347A77:Total number of attempts: {CastledWorld.dualForceEncounter + 1}.]");
                    }
                }
            }
            //Introduction
            if (introducingHerself == true)
            {
                npc.position.X = Main.player[npc.target].position.X - 100;
                npc.position.Y = Main.player[npc.target].position.Y;
                if (CastledWorld.dualForceEncounter == 0)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 45)
                    {
                        npc.frame.Y = Frame_EyeClosed1 * frameHeight;
                    }
                    else if (npc.frameCounter < 55)
                    {
                        npc.frame.Y = Frame_EyeClosed * frameHeight;
                    }
                    else if (npc.frameCounter < 120)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 130)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 170)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 200)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 220)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 230)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 270)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 280)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 290)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 300)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 340)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 350)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 410)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 420)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 430)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 440)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 500)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 510)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 990)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 1000)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 1010)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 1020)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 1120)
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
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
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
                        CastledWorld.dualForceEncounter++;
                        introducingHerself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                //Second Attempt
                if (CastledWorld.dualForceEncounter == 1 && !CastledWorld.downedDualForce == true)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 45)
                    {
                        npc.frame.Y = Frame_EyeClosed1 * frameHeight;
                    }
                    else if (npc.frameCounter < 55)
                    {
                        npc.frame.Y = Frame_EyeClosed * frameHeight;
                    }
                    else if (npc.frameCounter < 120)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 130)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 190)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 220)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 230)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 240)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 310)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 320)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 330)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 400)
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
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
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
                        CastledWorld.dualForceEncounter++;
                        introducingHerself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                //Third Attempt
                if (CastledWorld.dualForceEncounter == 2 && !CastledWorld.downedDualForce == true)
                {
                    npc.dontTakeDamage = true;

                    npc.frameCounter++;
                    if (npc.frameCounter < 45)
                    {
                        npc.frame.Y = Frame_EyeClosed1 * frameHeight;
                    }
                    else if (npc.frameCounter < 55)
                    {
                        npc.frame.Y = Frame_EyeClosed * frameHeight;
                    }
                    else if (npc.frameCounter < 120)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 130)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 190)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 220)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 230)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 310)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 320)
                    {
                        npc.frame.Y = Frame_Mouth * frameHeight;
                    }
                    else if (npc.frameCounter < 330)
                    {
                        npc.frame.Y = Frame_Idle * frameHeight;
                    }
                    else if (npc.frameCounter < 400)
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
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
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
                        CastledWorld.dualForceEncounter++;
                        introducingHerself = false;
                        npc.dontTakeDamage = false;
                    }
                }
                //>3 attempts
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
                            int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 164, 0, 0, 100, color, 1.5f);
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
                        CastledWorld.dualForceEncounter++;
                        introducingHerself = false;
                        npc.dontTakeDamage = false;
                    }
                }
            }
            //Teleporting
            if (isDiscording == true && isTransforming == false && isAttacking == false && introducingHerself == false)
            {
                if (Main.expertMode)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter < 94)
                    {
                        npc.frame.Y = Frame_Throw1 * frameHeight;
                    }
                    else if (npc.frameCounter < 98)
                    {
                        npc.frame.Y = Frame_Throw2 * frameHeight;
                    }
                    else if (npc.frameCounter < 102)
                    {
                        npc.frame.Y = Frame_Throw3 * frameHeight;
                    }
                    else if (npc.frameCounter == 106)
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
                    else if (npc.frameCounter < 110)
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
                else
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
                    else if (npc.frameCounter < 212)
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
            if (isAttacking == true && rotationAxis == 1 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 178)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 184)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 190)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 196)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 202)
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
            if (isAttacking == true && rotationAxis == 4 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 178)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 184)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 190)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 196)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 202)
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
            if (isAttacking == true && rotationAxis == 2 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 64)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 68)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 72)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 76)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Pink Potion Alt
            if (isAttacking == true && rotationAxis == 2 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 60)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 64)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 68)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 72)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 76)
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
            if (isAttacking == true && rotationAxis == 5 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 120)
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
                else if (npc.frameCounter < 150)
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
            //Hallowed Bolt Throwing
            if (isAttacking == true && rotationAxis == 6 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 45)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 48)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 51)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 54)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 57)
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
            //Hallowed Pellet
            if (isAttacking == true && rotationAxis == 7 && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 25)
                {
                    npc.frame.Y = Frame_Throw1 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_Throw2 * frameHeight;
                }
                else if (npc.frameCounter < 35)
                {
                    npc.frame.Y = Frame_Throw3 * frameHeight;
                }
                else if (npc.frameCounter == 40)
                {
                    npc.frame.Y = Frame_Throw4 * frameHeight;
                }
                else if (npc.frameCounter < 45)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isAttacking = false;
                }
            }
            //Second Phase Animation Effects
            if (isTransforming == true && introducingHerself == false)
            {
                if (npc.frameCounter == 5)
                {
                    Main.PlaySound(SoundID.Shatter, npc.position);
                }
                else if (npc.frameCounter == 20)
                {
                    Main.PlaySound(SoundID.NPCHit, npc.position);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMRobe1"), 0.6f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMRobe2"), 0.6f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMRobe3"), 0.6f);
                }

                else if (npc.frameCounter == 300)
                {
                    Main.PlaySound(SoundID.NPCHit7, npc.position);
                }
                else if (npc.frameCounter == 365)
                {
                    Main.PlaySound(SoundID.NPCHit7, npc.position);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMGlove1"), 0.4f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMGlove2"), 0.4f);
                }
                else if (npc.frameCounter == 400)
                {
                    Main.PlaySound(SoundID.NPCHit7, npc.position);
                    Main.PlaySound(SoundID.NPCHit11, npc.position);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMGlove2"), 0.4f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DualForce/LM/LMGlove3"), 0.4f);
                }
            }
            if (isTransforming == true && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = Frame_MorphCrystalBreak * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = Frame_MorphCrystalBreak1 * frameHeight;
                }
                else if (npc.frameCounter < 37)
                {
                    npc.frame.Y = Frame_MorphBleed * frameHeight;
                }
                else if (npc.frameCounter < 45)
                {
                    npc.frame.Y = Frame_MorphBleed1 * frameHeight;
                }
                else if (npc.frameCounter < 52)
                {
                    npc.frame.Y = Frame_MorphBleedEyeClose * frameHeight;
                }
                else if (npc.frameCounter < 200)
                {
                    npc.frame.Y = Frame_MorphTransform1 * frameHeight;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 2;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.25f);
                    }
                }
                else if (npc.frameCounter < 225)
                {
                    npc.frame.Y = Frame_MorphTransform2 * frameHeight;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 3;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.35f);
                    }
                }
                else if (npc.frameCounter < 275)
                {
                    npc.frame.Y = Frame_MorphTransform3 * frameHeight;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 6;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.3f);
                    }
                }
                else if (npc.frameCounter < 300)
                {
                    npc.frame.Y = Frame_MorphTransform4 * frameHeight;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 4;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                    }
                }
                else if (npc.frameCounter < 365)
                {
                    npc.frame.Y = Frame_MorphTransform5 * frameHeight;
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 2;
                    for (int i = 1; i <= count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.4f);
                    }
                }
                else if (npc.frameCounter < 400)
                {
                    npc.frame.Y = Frame_MorphTransform6 * frameHeight;
                }
                else if (npc.frameCounter < 425)
                {
                    npc.frame.Y = Frame_MorphNymphEyeOpen * frameHeight;
                }
                else if (npc.frameCounter < 465)
                {
                    npc.frame.Y = Frame_MorphNymphNPCChange * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    npc.Transform(mod.NPCType("LightNymph"));
                }
            }
            //Blinking
            if (isBlinking == true && isTransforming == false && isAttacking == false && introducingHerself == false)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 4)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else if (npc.frameCounter < 8)
                {
                    npc.frame.Y = Frame_EyeClosed1 * frameHeight;
                }
                else if (npc.frameCounter < 12)
                {
                    npc.frame.Y = Frame_EyeClosed * frameHeight;
                }
                else if (npc.frameCounter < 16)
                {
                    npc.frame.Y = Frame_EyeClosed1 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = Frame_Idle * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                    isBlinking = false;
                }
            }
        }
        public override bool CheckDead()
        {
            npc.life = npc.lifeMax;
            isTransforming = true;
            return false;
        }
    }
}

