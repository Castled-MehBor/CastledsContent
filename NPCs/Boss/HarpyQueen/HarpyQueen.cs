using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Threading;
using CastledsContent.Items.Vanity;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.Boss.HarpyQueen
{
    [AutoloadBossHead]
    public class HarpyQueen : ModNPC
    {
        public float projYOffset = -2;
        public int aiState = 0;
        public int direction = 0;
        public bool castEvent = false;
        public bool hf = false;
        public bool ignoreDirection = false;
        public bool init = false;
        public int otherTimer = 0;
        public int dessusDirection;
        public int otherTimer2 = 0;
        public int otherTimer3 = 0;
        public int chargenum = 0;
        public int velocity;
        public int SAT;
        public int adrenaline = 0;
        public float momentum;
        //
        public bool schematicAttack;
        public bool doDraw;
        public int isAttackSchem;
        public int fea1;
        public int fea2;
        public int fea3;
        //
        public int adrPrevent;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Queen");
            Main.npcFrameCount[npc.type] = 36;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 6000;
            if (Main.expertMode)
            {
                npc.lifeMax = 9000;
            }
            npc.damage = 0;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.width = 150;
            npc.value = 50000;
            npc.height = 140;
            npc.npcSlots = 40f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Midas] = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Venom] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OST/HarpyQueenTheme");
            npc.dontTakeDamage = true;
            bossBag = mod.ItemType("TreasureBag3");
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 60; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1.2f);
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG5"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG6"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HarpyQueen/HarpyQueenG7"), 1f);
            }
            if (Main.rand.Next(9) == 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                }
            }
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
                if (!ignoreDirection)
                {
                    npc.spriteDirection = 2;
                }
                if (aiState == 1 || aiState == 2)
                {
                    if (npc.frameCounter == 90)
                    {
                        DustCircle();
                    }
                }
                #region Awaking
                if (aiState == 0)
                {
                    if (npc.frameCounter == 120)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 8f, npc.velocity.X * 0, 0f, mod.ProjectileType("HQAwakeProj"), 0, 0f, 255, 0f, 0f);
                    }
                    if (npc.frameCounter == 296)
                    {
                        FeatherStorm1();
                    }
                }
                #endregion
                #region Standard Flying
                if (aiState == 4)
                {
                    SAT++;
                    Timer++;
                    npc.aiStyle = 14;
                    npc.damage = 35;
                    if (SAT > 1200 && aiState == 4)
                    {
                        if (npc.position.Y < P.position.Y)
                        {
                            int num = Main.rand.Next(2);
                            if (num == 0)
                            {
                                if (Main.expertMode)
                                {
                                    aiState = 3;
                                    dessusDirection = 0;
                                    SAT = 0;
                                    npc.velocity.X = 0;
                                    npc.velocity.Y = 0;
                                    chargenum = 0;
                                }
                            }
                            if (num == 1)
                            {
                                aiState = 5;
                                SAT = 0;
                                npc.velocity.X = 0;
                                npc.velocity.Y = 0;
                                chargenum = 0;
                            }
                        }
                    }

                    //Launch feathers in directed direction
                    if (castEvent)
                    {
                        if (npc.frameCounter == 40)
                        {
                            WindFlapSoundSup();
                            FeatherStorm2();
                        }
                    }
                    //or just play standrad sound
                    else
                    {
                        if (npc.frameCounter == 33)
                        {
                            WindFlapSound();
                        }
                    }
                    #region
                    //1: Choose between charges 1 and 2, and snatching
                    if (Timer > 900 - adrenaline && adrPrevent != 3)
                    {
                        if (npc.position.X > P.position.X)
                        {
                            chargenum = 2;
                            Timer = 0;
                        }
                        else if (npc.position.X < P.position.X)
                        {
                            chargenum = 1;
                            Timer = 0;
                        }
                    }
                    //2: Choose between charges 1 and 2
                    else if (Timer > 500 - adrenaline && adrPrevent == 3)
                    {
                        if (npc.position.X > P.position.X)
                        {
                            chargenum = 2;
                            Timer = 0;
                        }
                        else if (npc.position.X < P.position.X)
                        {
                            chargenum = 1;
                            Timer = 0;
                        }
                    }
                    #endregion
                    //To the right
                    if (chargenum == 1)
                    {
                        npc.aiStyle = -1;
                        npc.spriteDirection = 2;
                        direction = 1;
                        if (Timer == 10 || Timer == 20 || Timer == 30)
                        {
                            momentum++;
                        }
                        if (Timer < 30)
                        {
                            if (adrPrevent != 3)
                            {
                                npc.position.Y -= momentum;
                            }
                        }
                        else if (Timer < 75)
                        {
                            npc.position.X -= 18;
                            doDraw = true;
                        }
                        else if (Timer == 150)
                        {
                            castEvent = true;
                            npc.frameCounter = 0;
                            DustCircle();
                        }
                        else if (Timer < 175)
                        {
                            npc.velocity.X *= 5f;
                            npc.velocity.Y *= 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                            {
                                float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f - 280)));
                                npc.velocity.X = (float)(Math.Cos(rotation) * 3.5) * -1;
                                npc.velocity.Y = (float)(Math.Sin(rotation) * 2.5) * -1;
                            }
                        }
                        else if (Timer < 375)
                        {
                            npc.position.X += 10;
                            npc.damage = 65;
                        }
                        else
                        {
                            if (schematicAttack && isAttackSchem < 2)
                            {
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 2;
                                npc.damage = 0;
                                isAttackSchem++;
                            }
                            else if (schematicAttack && isAttackSchem > 1)
                            {
                                isAttackSchem = 0;
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 0;
                                aiState = 1;
                                npc.damage = 0;
                            }
                            else
                            {
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 0;
                                aiState = 1;
                                npc.damage = 0;
                            }
                        }
                    }
                    //To the left
                    else if (chargenum == 2)
                    {
                        npc.aiStyle = -1;
                        npc.spriteDirection = 1;
                        direction = -1;
                        if (Timer == 10 || Timer == 20 || Timer == 30)
                        {
                            momentum--;
                        }
                        if (Timer < 30)
                        {
                            if (adrPrevent != 3)
                            {
                                npc.position.Y += momentum;
                            }
                        }
                        else if (Timer < 75)
                        {
                            npc.position.X += 18;
                            doDraw = true;
                        }
                        else if (Timer == 150)
                        {
                            castEvent = true;
                            npc.frameCounter = 0;
                            DustCircle();
                        }
                        else if (Timer < 175)
                        {
                            npc.velocity.X *= 5f;
                            npc.velocity.Y *= 5f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                            {
                                float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f + 280)));
                                npc.velocity.X = (float)(Math.Cos(rotation) * 3.5) * -1;
                                npc.velocity.Y = (float)(Math.Sin(rotation) * 2.5) * -1;
                            }
                        }
                        else if (Timer < 375)
                        {
                            npc.position.X -= 10;
                            npc.damage = 65;
                        }
                        else
                        {
                            if (schematicAttack && isAttackSchem < 2)
                            {
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 1;
                                npc.damage = 0;
                                isAttackSchem++;
                            }
                            else if (schematicAttack && isAttackSchem > 1)
                            {
                                isAttackSchem = 0;
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 0;
                                aiState = 2;
                                npc.damage = 0;
                            }
                            else
                            {
                                Timer = 0;
                                npc.aiStyle = 14;
                                chargenum = 0;
                                aiState = 2;
                                npc.damage = 0;
                            }
                        }
                    }
                    #region Standard Flight
                    if (chargenum < 1)
                    {
                        if (npc.position.Y < P.position.Y - 250)
                        {
                            npc.velocity.Y = 1;
                        }
                        else if (npc.position.Y > P.position.Y - 350)
                        {
                            npc.velocity.Y = -1;
                        }
                        if (npc.position.X > P.position.X + 50)
                        {
                            otherTimer++;
                            otherTimer2++;
                            if (otherTimer < 60)
                            {
                                if (otherTimer2 > 10)
                                {
                                    otherTimer2 = 0;
                                    npc.velocity.X--;
                                }
                            }
                            else if (otherTimer < 120)
                            {
                                if (otherTimer2 > 10)
                                {
                                    npc.velocity.X++;
                                    otherTimer2 = 0;
                                }
                            }
                            else if (otherTimer > 130)
                            {
                                otherTimer = 0;
                            }
                        }
                        //
                        else if (npc.position.X > P.position.X - 50)
                        {
                            otherTimer++;
                            otherTimer2++;
                            if (otherTimer < 60)
                            {
                                if (otherTimer2 > 10)
                                {
                                    otherTimer2 = 0;
                                    npc.velocity.X++;
                                    doDraw = true;
                                }
                            }
                            else if (otherTimer < 120)
                            {
                                if (otherTimer2 > 10)
                                {
                                    npc.velocity.X--;
                                    otherTimer2 = 0;
                                    doDraw = true;
                                }
                            }
                            else if (otherTimer > 130)
                            {
                                otherTimer = 0;
                                doDraw = false;
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                #region Raise Wing
                //Raise wing, and then launch identical feather storm, but in opposite direction
                if (aiState == 1 || aiState == 2)
                {
                    Timer++;
                    //To the right
                    if (aiState == 2 && Timer < 60)
                    {
                        npc.damage = 0;
                        direction = 1;
                        npc.aiStyle = -1;
                        if (Timer < 20)
                        {
                            velocity -= (int)0.1;
                            npc.velocity.X += velocity * 2;
                            npc.velocity.Y -= 2;
                        }
                        else if (Timer < 60)
                        {
                            if (npc.velocity.X < 0)
                            {
                                npc.velocity.X = 0;
                            }
                            npc.velocity.X -= velocity;
                            npc.velocity.Y = 0;
                        }
                    }
                    //To the left
                    else if (aiState == 1 && Timer < 60)
                    {
                        npc.damage = 0;
                        direction = -1;
                        npc.aiStyle = -1;
                        if (Timer < 20)
                        {
                            velocity += (int)0.1;
                            npc.velocity.X += velocity * 2;
                            npc.velocity.Y += 2;
                        }
                        else if (Timer < 60)
                        {
                            if (npc.velocity.X > 1)
                            {
                                npc.velocity.X = 0;
                            }
                            npc.velocity.X += velocity;
                            npc.velocity.Y = 0;
                        }
                    }
                    if (npc.frameCounter == 120)
                    {
                        WindFlapSoundSup();
                        velocity = 0;
                        FeatherStorm3();
                    }
                }
                #endregion
                #region Feather Trap
                if (aiState == 3)
                {
                    npc.aiStyle = -1;
                    if (dessusDirection < 1)
                    {
                        npc.position.Y -= 1;
                    }
                    else if (dessusDirection < 2)
                    {
                        npc.position.Y -= 2;
                    }
                    if (npc.frameCounter == 100)
                    {
                        WindFlapSound();
                        npc.frameCounter = 0;
                        FeatherStorm4();
                        dessusDirection++;
                    }
                }
                #endregion
                #region Enroaching Feather
                //Enroaching Feather
                if (aiState == 5)
                {
                    npc.aiStyle = -1;
                    if (npc.frameCounter == 200)
                    {
                        FeatherStorm1();
                    }
                    if (Main.expertMode)
                    {
                        if (npc.frameCounter == 134)
                        {
                            FeatherStorm1();
                        }
                    }
                }
                #endregion
                #region Damage Scaling
                if (!init)
                {
                    if (Main.expertMode)
                    {
                        fea1 = 24;
                        fea2 = 18;
                        fea3 = 12;
                        init = true;
                    }
                    else
                    {
                        fea1 = 24;
                        fea2 = 20;
                        fea3 = 16;
                        init = true;
                    }
                }
                #endregion
                #region Difficulty Scaling
                if (npc.life < npc.lifeMax * 0.75 && adrPrevent == 0)
                {
                    adrenaline = 45;
                    npc.defense += 1;
                    adrPrevent = 1;
                }
                else if (npc.life < npc.lifeMax * 0.5 && adrPrevent == 1)
                {
                    adrenaline = 90;
                    npc.damage += 15;
                    npc.defense += 3;
                    fea1 += 4;
                    fea2 += 4;
                    fea3 += 4;
                    adrPrevent = 2;
                }
                else if (npc.life < npc.lifeMax * 0.15 && adrPrevent == 2)
                {
                    if (Main.expertMode)
                    {
                        adrenaline = 150;
                        npc.damage += 25;
                        npc.defense += 5;
                        fea1 += 8;
                        fea2 += 8;
                        fea3 += 8;
                        adrPrevent = 3;
                    }
                }
                #endregion
                #region "Planned Attack"
                if (Main.expertMode)
                {
                    if (npc.life < npc.lifeMax * 0.5)
                    {
                        schematicAttack = true;
                    }
                }
                else
                {
                    if (npc.life < npc.lifeMax * 0.25)
                    {
                        schematicAttack = true;
                    }
                }
                #endregion
                if (aiState != 0)
                {
                    if (!P.ZoneSkyHeight)
                    {
                        npc.dontTakeDamage = true;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                    }
                }
                if (Main.expertMode)
                {
                    if (aiState == 3 || aiState == 5)
                    {
                        npc.dontTakeDamage = true;
                    }
                    else if (P.ZoneSkyHeight)
                    {
                        npc.dontTakeDamage = false;
                    }

                    if (aiState > 0)
                    {
                        if (chargenum > 0)
                        {
                            npc.defense = 30 * 1 + adrenaline;
                        }
                        else if (aiState == 1 || aiState == 2)
                        {
                            npc.defense = 75 * 1 + adrenaline;
                        }
                        else
                        {
                            npc.defense = 12;
                        }
                    }
                }
                else
                {

                    if (aiState > 0)
                    {
                        if (chargenum > 0)
                        {
                            npc.defense = 20 + adrenaline;
                        }
                        else if (aiState == 1 || aiState == 2)
                        {
                            npc.defense = 50 + adrenaline;
                        }
                        else
                        {
                            npc.defense = 8;
                        }
                    }
                }
            }
        }
        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }

            if (doDraw)
            {
                Texture2D texture = Main.npcTexture[npc.type];
                Rectangle rectangle = new Rectangle(npc.frame.X, npc.frame.Y, (texture.Width), (texture.Height / Main.npcFrameCount[npc.type]));
                Vector2 vector = new Vector2(((texture.Width) / 2f), ((texture.Height / Main.npcFrameCount[npc.type]) / 2f));
                float yOffset = (((1f - npc.scale) * 12f) + 4f + 31.875f) - ((npc.scale - 0.625f) * 85);
                Vector2 drawPosition = new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width) / 2f + vector.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + yOffset + vector.Y);

                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f - 15);
                for (int m = 0; m < npc.oldPos.Length; m++)
                {
                    Color color = npc.GetAlpha(Color.White) * ((float)(npc.oldPos.Length - m) / (float)npc.oldPos.Length);
                    Vector2 drawPos = new Vector2(npc.oldPos[m].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width) / 2f + vector.X, npc.oldPos[m].Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + yOffset + vector.Y);
                    spriteBatch.Draw(texture, drawPos, rectangle, color, npc.rotation, vector, npc.scale, effects, 0f);
                }
            }
            return true;
        }
        #endregion
        #region Sound Voids
        //Voids to not use the exact same lines of code
        //Sound Voids
        public void WindFlapSoundSup()
        {
            Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.35f), npc.position);
            Main.PlaySound(SoundID.Item32.WithVolume(35f), npc.position);
        }
        public void WindFlapSound()
        {
            Main.PlaySound(SoundID.Item32.WithVolume(17.5f), npc.position);
        }
        #endregion
        #region Attack Voids
        //Attack Voids
        public void FeatherStorm1()
        {
            WindFlapSoundSup();
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5f, 5f, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -5f, 5f, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -5f, -(int)7.5f, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)7.5f, -5f, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)3.75, 5, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)3.75f, -(int)3.75f, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 9, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 4, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, -7, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -5, -3, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            if (Main.expertMode)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)-7.5, 5, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)7.5, (int)-3.75, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, (int)-7.5, mod.ProjectileType("HyperFeather"), fea1 * 2, 0f, 255, 0f, 0f);
                //uh
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, (int)3.75f, (int)3.75f, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -(int)3.75f, (int)3.75f, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -(int)3.75f, -(int)3.75f, mod.ProjectileType("GiantFeather"), fea2 * 2, 0f, 255, 0f, 0f);
            }
        }
        public void FeatherStorm2()
        {
            int num = Main.rand.Next(3);
            if (num == 0)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 6f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 3f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 0f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 9, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -6, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -3, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                if (Main.expertMode)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 12f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, 15f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -15f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15 * direction, -9f, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                }
            }
            if (num == 1)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 6f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 3f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 0f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 9, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -6, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -3, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                if (Main.expertMode)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -9f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.15, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 12f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.15, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 15f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.15, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -12f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.15, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -15f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.15, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -9f, ProjectileID.HarpyFeather, fea3 * (int)1.15, 0f, 255, 0f, 0f);
                }
            }
            if (num == 2)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 6f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.25, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 3f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.25, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 0f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.25, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 9, ProjectileID.HarpyFeather, fea3, 0f, 255 * (int)1.25, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -6, ProjectileID.HarpyFeather, fea3, 0f, 255 * (int)1.25, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -3, ProjectileID.HarpyFeather, fea3, 0f, 255 * (int)1.25, 0f, 0f);
                if (Main.expertMode)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -9f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.25, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 12f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.25, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, 15f, mod.ProjectileType("GiantFeather"), fea2 * (int)1.25, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -12f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.25, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -15f, mod.ProjectileType("HyperFeather"), fea1 * (int)1.25, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5 * direction, -9f, ProjectileID.HarpyFeather, fea3 * (int)1.25, 0f, 255, 0f, 0f);
                }
            }
        }
        public void FeatherStorm3()
        {
            int num = Main.rand.Next(2);
            if (num == 0)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 9f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 6f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 3f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 0f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -3f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -6, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -9, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -12, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 13, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -14, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 14, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, -13, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                if (Main.expertMode)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 11, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -11, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    //
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 10, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -10f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -8f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 8f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 2f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -2f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 1, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -1, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                }
            }
            if (num == 1)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 9f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 10f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 12f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8 * direction, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, Main.rand.Next(-12, -5), ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, Main.rand.Next(-10, -7), ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -9f, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                if (Main.expertMode)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 15f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 5, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 4, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, 3, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 2, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, 1, ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                    //
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -15f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, -16f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -17f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -18f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -19f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9 * direction, -20f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, Main.rand.Next(1, 6), mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10 * direction, Main.rand.Next(2, 8), ProjectileID.HarpyFeather, fea3, 0f, 255, 0f, 0f);
                }
            }
        }
        public void FeatherStorm4()
        {
            switch (dessusDirection)
            {
                case 0:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 4, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 2, 7f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -2, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -4, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -10, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -12, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
                case 1:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 15, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 9, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 3, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 0, 7f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -3, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -9, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -12, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -15, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -18, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
                case 2:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 7, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 5, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 4, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 3, 7f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -4, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -5, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -7, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -9, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
                case 3:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 16f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 15f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 0, 14f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -12, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -4, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 0, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 4, 14f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, 15f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 16f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 16, 17f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
                case 4:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 6f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10, 5f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, 4f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 3f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 4, 2f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 2, 1f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -2, 2f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -4, 3f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 4f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 5f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -10, 6f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -12, 7f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
                case 5:
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 12, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 10, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 8, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 6, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 4, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, 2, 7f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -2, 8f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -4, 9f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -6, 10f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -8, 11f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -10, 12f, mod.ProjectileType("HyperFeather"), fea1, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - projYOffset, -12, 13f, mod.ProjectileType("GiantFeather"), fea2, 0f, 255, 0f);
                        //
                        break;
                    }
            }
        }
        #endregion
        public void DustCircle()
        {
            Main.PlaySound(SoundID.DD2_BookStaffCast, npc.position);
            for (int i = 0; i < 50; i++)
            {
                Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i));
                Dust dust = Dust.NewDustPerfect(position, DustID.AmberBolt);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(dust.position - npc.Center) * 8;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }
        }
        public override void NPCLoot()
        {
            CastledWorld.downedHarpyQueen = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Trophy.HQTrophy>());
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            #region Normal Mode
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SilverCoin, Main.rand.Next(400, 900));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Feather, Main.rand.Next(3, 9));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Material.HarpyFeather>(), Main.rand.Next(10, 20));

                int num = Main.rand.Next(3);
                if (num == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Weapons.Magic.HarpyStaff>());
                }
                if (num == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Weapons.Melee.HarpyArm>());
                }
                if (num == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Weapons.Ranged.HarpyGun>());
                }

                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<HarpyBreastplate>());
                }
                if (Main.rand.Next(12) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<HarpyLeggings>());
                }
                if (Main.rand.Next(8) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<HQMask>());
                }
            }
            #endregion
        }
        private const int Frame_0 = 0;
        private const int Frame_1 = 1;
        private const int Frame_2 = 2;
        private const int Frame_3 = 3;
        private const int Frame_4 = 4;
        private const int Frame_5 = 5;
        private const int Frame_6 = 6;
        private const int Frame_7 = 7;
        private const int Frame_8 = 8;
        private const int Frame_9 = 9;
        private const int Frame_10 = 10;
        //
        private const int Frame_11 = 11;
        private const int Frame_12 = 12;
        private const int Frame_13 = 13;
        private const int Frame_14 = 14;
        private const int Frame_15 = 15;
        private const int Frame_16 = 16;
        private const int Frame_17 = 17;
        private const int Frame_18 = 18;
        private const int Frame_19 = 19;
        private const int Frame_20 = 20;
        private const int Frame_21 = 21;
        //
        private const int Frame_22 = 22;
        private const int Frame_23 = 23;
        private const int Frame_24 = 24;
        private const int Frame_25 = 25;
        private const int Frame_26 = 26;
        private const int Frame_27 = 27;
        private const int Frame_28 = 28;
        private const int Frame_29 = 29;
        //
        private const int Frame_30 = 30;
        private const int Frame_31 = 31;
        private const int Frame_32 = 32;
        private const int Frame_33 = 33;
        private const int Frame_34 = 34;
        private const int Frame_35 = 35;
        public override void FindFrame(int frameHeight)
        {
            switch (aiState)
            {
                case 0:
                    {
                        npc.frameCounter++;
                        if (npc.frameCounter < 120)
                        {
                            npc.defense = 16828;
                            npc.frame.Y = Frame_0 * frameHeight;
                        }
                        else if (npc.frameCounter < 240)
                        {
                            npc.frame.Y = Frame_1 * frameHeight;
                        }
                        else if (npc.frameCounter < 280)
                        {
                            npc.frame.Y = Frame_2 * frameHeight;
                        }
                        else if (npc.frameCounter < 288)
                        {
                            npc.frame.Y = Frame_3 * frameHeight;
                        }
                        else if (npc.frameCounter < 296)
                        {
                            npc.frame.Y = Frame_4 * frameHeight;
                        }
                        else if (npc.frameCounter < 320)
                        {
                            npc.frame.Y = Frame_7 * frameHeight;
                        }
                        else if (npc.frameCounter < 340)
                        {
                            npc.frame.Y = Frame_6 * frameHeight;
                        }
                        else if (npc.frameCounter < 360)
                        {
                            npc.frame.Y = Frame_5 * frameHeight;
                        }
                        else if (npc.frameCounter > 600)
                        {
                            npc.frameCounter = 0;
                            aiState = 4;
                            npc.dontTakeDamage = false;
                            npc.defense = 12;
                        }
                        break;
                    }
                case 1:
                    {
                        npc.frameCounter++;
                        if (npc.frameCounter < 45)
                        {
                            npc.frame.Y = Frame_8 * frameHeight;
                        }
                        else if (npc.frameCounter < 65)
                        {
                            npc.frame.Y = Frame_12 * frameHeight;
                        }
                        else if (npc.frameCounter < 75)
                        {
                            npc.frame.Y = Frame_13 * frameHeight;
                        }
                        else if (npc.frameCounter < 120)
                        {
                            npc.frame.Y = Frame_14 * frameHeight;
                        }
                        else if (npc.frameCounter < 130)
                        {
                            doDraw = true;
                            npc.frame.Y = Frame_15 * frameHeight;
                        }
                        else if (npc.frameCounter < 170)
                        {
                            npc.frame.Y = Frame_16 * frameHeight;
                        }
                        else if (npc.frameCounter < 240)
                        {
                            doDraw = false;
                            npc.frame.Y = Frame_17 * frameHeight;
                        }
                        else
                        {
                            if (schematicAttack)
                            {
                                if (Main.rand.Next(4) == 0)
                                {
                                    npc.frameCounter = 0;
                                    Timer = 0;
                                    int num = Main.rand.Next(2);
                                    if (num == 0)
                                    {
                                        if (Main.expertMode)
                                        {
                                            aiState = 3;
                                        }
                                    }
                                    if (num == 1)
                                    {
                                        aiState = 5;
                                    }
                                }
                            }
                            else
                            {
                                npc.frameCounter = 0;
                                Timer = 0;
                                aiState = 4;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        npc.frameCounter++;
                        if (npc.frameCounter < 45)
                        {
                            npc.frame.Y = Frame_8 * frameHeight;
                        }
                        else if (npc.frameCounter < 65)
                        {
                            npc.frame.Y = Frame_18 * frameHeight;
                        }
                        else if (npc.frameCounter < 75)
                        {
                            npc.frame.Y = Frame_19 * frameHeight;
                        }
                        else if (npc.frameCounter < 120)
                        {
                            npc.frame.Y = Frame_20 * frameHeight;
                        }
                        else if (npc.frameCounter < 130)
                        {
                            doDraw = true;
                            npc.frame.Y = Frame_21 * frameHeight;
                        }
                        else if (npc.frameCounter < 170)
                        {
                            npc.frame.Y = Frame_22 * frameHeight;
                        }
                        else if (npc.frameCounter < 240)
                        {
                            doDraw = false;
                            npc.frame.Y = Frame_23 * frameHeight;
                        }
                        else
                        {
                            if (schematicAttack)
                            {
                                if (Main.rand.Next(4) == 0)
                                {
                                    npc.frameCounter = 0;
                                    Timer = 0;
                                    int num = Main.rand.Next(2);
                                    if (num == 0)
                                    {
                                        if (Main.expertMode)
                                        {
                                            aiState = 3;
                                        }
                                    }
                                    if (num == 1)
                                    {
                                        aiState = 5;
                                    }
                                }
                            }
                            else
                            {
                                npc.frameCounter = 0;
                                Timer = 0;
                                aiState = 4;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        npc.frameCounter++;
                        if (npc.frameCounter < 60)
                        {
                            npc.frame.Y = Frame_24 * frameHeight;
                        }
                        else if (npc.frameCounter < 68)
                        {
                            npc.frame.Y = Frame_25 * frameHeight;
                        }
                        else if (npc.frameCounter < 76)
                        {
                            npc.frame.Y = Frame_26 * frameHeight;
                        }
                        else if (npc.frameCounter < 84)
                        {
                            npc.frame.Y = Frame_27 * frameHeight;
                        }
                        else if (npc.frameCounter < 92)
                        {
                            npc.frame.Y = Frame_28 * frameHeight;
                        }
                        else if (npc.frameCounter < 100)
                        {
                            npc.frame.Y = Frame_29 * frameHeight;
                        }
                        else if (npc.frameCounter > 90 && dessusDirection > 4)
                        {
                            npc.frameCounter = 0;
                            Timer = 0;
                            aiState = 4;
                        }
                        break;
                    }
                case 4:
                    {
                        if (castEvent == true)
                        {
                            npc.frameCounter++;
                            if (npc.frameCounter < 25)
                            {
                                doDraw = true;
                                npc.frame.Y = Frame_30 * frameHeight;
                            }
                            else if (npc.frameCounter < 35)
                            {
                                npc.frame.Y = Frame_31 * frameHeight;
                            }
                            else if (npc.frameCounter < 39)
                            {
                                npc.frame.Y = Frame_32 * frameHeight;
                            }
                            else if (npc.frameCounter < 43)
                            {
                                npc.frame.Y = Frame_33 * frameHeight;
                            }
                            else if (npc.frameCounter < 55)
                            {
                                npc.frame.Y = Frame_34 * frameHeight;
                            }
                            else if (npc.frameCounter < 67)
                            {
                                npc.frame.Y = Frame_35 * frameHeight;
                            }
                            else
                            {
                                npc.frameCounter = 0;
                                castEvent = false;
                                doDraw = false;
                            }
                        }
                        else
                        {
                            npc.frameCounter++;
                            if (npc.frameCounter < 3)
                            {
                                npc.frame.Y = Frame_5 * frameHeight;
                            }
                            else if (npc.frameCounter < 6)
                            {
                                npc.frame.Y = Frame_6 * frameHeight;
                            }
                            else if (npc.frameCounter < 11)
                            {
                                npc.frame.Y = Frame_7 * frameHeight;
                            }
                            else if (npc.frameCounter < 13)
                            {
                                npc.frame.Y = Frame_6 * frameHeight;
                            }
                            else if (npc.frameCounter < 15)
                            {
                                npc.frame.Y = Frame_5 * frameHeight;
                            }
                            else if (npc.frameCounter < 18)
                            {
                                npc.frame.Y = Frame_8 * frameHeight;
                            }
                            else if (npc.frameCounter < 23)
                            {
                                npc.frame.Y = Frame_9 * frameHeight;
                            }
                            else if (npc.frameCounter < 28)
                            {
                                npc.frame.Y = Frame_10 * frameHeight;
                            }
                            else if (npc.frameCounter < 35)
                            {
                                npc.frame.Y = Frame_11 * frameHeight;
                            }
                            else if (npc.frameCounter < 45)
                            {
                                npc.frame.Y = Frame_10 * frameHeight;
                            }
                            else if (npc.frameCounter < 50)
                            {
                                npc.frame.Y = Frame_9 * frameHeight;
                            }
                            else if (npc.frameCounter < 60)
                            {
                                npc.frame.Y = Frame_8 * frameHeight;
                            }
                            else
                            {
                                npc.frameCounter = 0;
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        npc.frameCounter++;
                        if (npc.frameCounter < 60)
                        {
                            npc.frame.Y = Frame_7 * frameHeight;
                        }
                        else if (npc.frameCounter < 64)
                        {
                            npc.frame.Y = Frame_6 * frameHeight;
                        }
                        else if (npc.frameCounter < 68)
                        {
                            npc.frame.Y = Frame_5 * frameHeight;
                        }
                        else if (npc.frameCounter < 98)
                        {
                            npc.frame.Y = Frame_2 * frameHeight;
                        }
                        else if (npc.frameCounter < 140)
                        {
                            npc.frame.Y = Frame_2 * frameHeight;
                        }
                        else if (npc.frameCounter < 180)
                        {
                            npc.frame.Y = Frame_2 * frameHeight;
                        }
                        else if (npc.frameCounter < 188)
                        {
                            npc.frame.Y = Frame_3 * frameHeight;
                        }
                        else if (npc.frameCounter < 196)
                        {
                            npc.frame.Y = Frame_4 * frameHeight;
                        }
                        else if (npc.frameCounter < 204)
                        {
                            npc.frame.Y = Frame_7 * frameHeight;
                        }
                        else if (npc.frameCounter < 210)
                        {
                            npc.frame.Y = Frame_6 * frameHeight;
                        }
                        else if (npc.frameCounter < 220)
                        {
                            npc.frame.Y = Frame_5 * frameHeight;
                        }
                        else
                        {
                            npc.frameCounter = 0;
                            aiState = 4;
                        }
                        if (Main.expertMode)
                        {
                            npc.frameCounter++;
                            if (npc.frameCounter < 60)
                            {
                                npc.frame.Y = Frame_7 * frameHeight;
                            }
                            else if (npc.frameCounter < 64)
                            {
                                npc.frame.Y = Frame_6 * frameHeight;
                            }
                            else if (npc.frameCounter < 68)
                            {
                                npc.frame.Y = Frame_5 * frameHeight;
                            }
                            else if (npc.frameCounter < 98)
                            {
                                npc.frame.Y = Frame_2 * frameHeight;
                            }
                            else if (npc.frameCounter < 120)
                            {
                                npc.frame.Y = Frame_3 * frameHeight;
                            }
                            else if (npc.frameCounter < 126)
                            {
                                npc.frame.Y = Frame_4 * frameHeight;
                            }
                            else if (npc.frameCounter < 132)
                            {
                                npc.frame.Y = Frame_7 * frameHeight;
                            }
                            else if (npc.frameCounter < 138)
                            {
                                npc.frame.Y = Frame_6 * frameHeight;
                            }
                            else if (npc.frameCounter < 150)
                            {
                                npc.frame.Y = Frame_5 * frameHeight;
                            }
                            else
                            {
                                npc.frameCounter = 0;
                                aiState = 4;
                            }
                        }    
                        break;
                    }

            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}