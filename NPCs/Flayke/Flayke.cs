using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using Terraria.Graphics.Shaders;

namespace CastledsContent.NPCs.Flayke
{
    [AutoloadBossHead]
    public class Flayke : ModNPC
    {
        #region AI States for convenience
        public const int State_Glide = 1;
        public const int State_Recoil = 2;
        public const int State_Attack1 = 3;
        public const int State_Attack2Glide = 4;
        public const int State_BorealisGlide = 5;
        public const int State_IceCube = 6;
        public const int State_Blizzard = 7;
        public const int State_Idle = 8;
        public const int State_Transition = 9;
        public const int State_Summon = 10;
        #endregion
        public static string Directory = "CastledsContent/NPCs/Flayke/";
        public static Color definingColor = new Color(15, Main.DiscoG - 175, Main.DiscoB + 175);
        int AIState = State_Summon;
        readonly Vector2 Frame = new Vector2(38, 62);
        Vector3[] framing = new Vector3[6];
        #region Framing constants
        /// <summary>
        /// FrameHead
        /// </summary>
        const int FHe = 0;
        /// <summary>
        /// FrameHair
        /// </summary>
        const int FHa = 1;
        /// <summary>
        /// FrameBodyUniversal
        /// </summary>
        const int FBU = 2;
        /// <summary>
        /// FrameLowerUniversal
        /// </summary>
        const int FLU = 3;
        /// <summary>
        /// FrameBodyPhase2
        /// </summary>
        const int FB2 = 4;
        #endregion
        Vector2[] posVal = new Vector2[2];
        bool prevention;
        float[] snowBallin = new float[3];
        float[] attackVal = new float[11];
        float[] specAttackVal = new float[12];
        readonly float[] distance = new float[4] { 1200, 1100, 1000, 900 };
        int lockDirection;
        int specProj = -1;
        int[] soundTimer = new int[2];
        float[] summonVal = new float[3];
        bool scaledStuff;
        int specialAttack;
        bool[] borPosTracked = new bool[2];
        bool altBody;
        bool cameraPanStuff;
        bool[] blink = new bool[4];
        Vector2[] oldPosNew = new Vector2[6];
        int posIndex;
        Vector2 borPos;
        List<Projectile> telegraphs = new List<Projectile>();
        List<Wind> wind = new List<Wind>();
        int[] vfxTimer = new int[2];
        List<Cloud> clouds = new List<Cloud>();
        List<Borealis> borealis = new List<Borealis>();
        int colorAugment = 255;
        bool despawn;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flayke");
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 62;
            npc.lifeMax = 3000;
            npc.damage = 0;
            npc.defense = 6;
            npc.knockBackResist = 0f;
            npc.value = 75000;
            npc.npcSlots = 10f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Stinky] = true;
            npc.buffImmune[BuffID.Lovestruck] = true;
            bossBag = ModContent.ItemType<Items.Misc.FlaykeBag>();
        }
        public override void AI()
        {
            #region Targetting crap
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(faceTarget: false);
                if (!player.active || player.dead)
                    despawn = true;
                return;
            }
            #endregion
            if (Main.dayTime)
                despawn = true;
            if (despawn)
            {
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                colorAugment += 5;
                if (colorAugment >= 255)
                    npc.active = false;
            }
            Vector3 color = BH() && AIState != State_Transition ? new Vector3(0, Main.DiscoG + 75, Main.DiscoB + 125) : new Vector3(Color.LightSkyBlue.R, Color.LightSkyBlue.G, Color.LightSkyBlue.B);
            Lighting.AddLight(npc.Center, new Vector3(color.X * 0.05f, color.Y * 0.05f, color.Z * 0.05f));
            oldPosNew[posIndex] = npc.Center;
            if (posIndex++ > 4)
                posIndex = 0;
            if (!scaledStuff)
            {
                int health = Main.expertMode ? 4500 : 3500;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    foreach (Player p in Main.player)
                    {
                        if (p != null && p.active && !p.dead)
                        {
                            if (p.position.X < npc.position.X + 2500 && p.position.X > npc.position.X - 2500 && p.position.Y < npc.position.Y + 2500 && p.position.Y > npc.position.Y - 2500)
                                health += p.statLifeMax2 * 2;
                        }
                    }
                }
                npc.lifeMax = health;
                npc.life = health;
                scaledStuff = true;
            }
            if (lockDirection != 0)
                npc.direction = lockDirection;
            else
                npc.direction = Target().position.X > npc.position.X ? 1 : -1;
            npc.spriteDirection = npc.direction;
            if (blink[1] && !blink[0] && Main.rand.NextBool(299))
                blink[0] = true;
            if (blink[0])
                Blink();
            if (borealis.Count > 0)
            {
                for (int a = 0; a < borealis.Count; a++)
                {
                    Borealis b = borealis[a];
                    if (AIState != State_BorealisGlide && AIState != State_Summon)
                    {
                        b.Execute();
                        if (b.ended)
                            borealis.Remove(b);
                    }
                    Lighting.AddLight(b.coord, new Vector3(b.color.R * 0.075f, b.color.G * 0.075f, b.color.B * 0.075f));
                }
            }
            if (BH() && !blink[2])
            {
                blink[2] = true;
                blink[3] = true;
            }
            if (blink[3])
                AIState = State_Transition;
            snowBallin[1] += FacingLeft() ? -0.25f : 0.25f;
            if (attackVal[0] < 3 && AIState != State_IceCube && AIState != State_Blizzard && AIState != State_Transition && AIState != State_Summon)
            {
                if (snowBallin[0] < 0.75f)
                    snowBallin[0] += 0.025f;
            }
            else
                if (snowBallin[0] > 0)
                snowBallin[0] -= 0.025f;
            VelocityCompensate();
            if (AIState != State_Transition && framing[5].Z++ > 10)
            {
                framing[5].Z = 0;
                framing[5].Y += framing[5].Y > 288 ? -360 : 72;
            }
            if (Main.rand.NextBool(49))
            {
                Vector2[] vec = new Vector2[2] { new Vector2(npc.Center.X - (npc.width * 0.75f), npc.Center.X + (npc.width * 0.75f)), new Vector2(npc.Center.Y - (npc.height / 4), npc.Center.Y + (npc.height * 0.825f)) };
                Dust dust = Dust.NewDustPerfect(new Vector2(Main.rand.NextFloat(vec[0].X, vec[0].Y), Main.rand.NextFloat(vec[1].X, vec[1].Y)), ModContent.DustType<Mist1>());
                dust.customData = new MistArray();
                if (dust.customData is MistArray m)
                {
                    m.arr = new int[5] { 0, 0, 0, 0, 0 };
                    m.arr[2] = Main.rand.Next(174);
                    m.arr[1] = Main.rand.Next(1, 3);
                    m.arr[0] = Main.rand.Next(2);
                }
                int a = Main.rand.Next(4);
                switch (a)
                {
                    case 0:
                        a = 0;
                        break;
                    case 1:
                        a = 10;
                        break;
                    case 2:
                        a = 20;
                        break;
                    case 3:
                        a = 30;
                        break;
                }
                dust.frame = new Rectangle(0, a, 12, 10);
            }
            #region Update Visuals
            if (clouds.Count > 0)
            {
                for (int a = 0; a < clouds.Count; a++)
                {
                    Cloud c = clouds[a];
                    c.UpdateVelocity();
                    c.GetColor();
                    if (Main.rand.NextBool(149))
                        c.CreateWind(wind);
                }
            }
            if (wind.Count > 0)
            {
                for (int a = 0; a < wind.Count; a++)
                {
                    Wind w = wind[a];
                    w.Update();
                }
            }
            #endregion
            switch (AIState)
            {
                case State_Summon:
                    {
                        npc.direction = 0;
                        summonVal[2]++;
                        npc.dontTakeDamage = true;
                        if (summonVal[2] > 160)
                        {
                            #region Camera Pan Stuff
                            if (!cameraPanStuff)
                            {
                                Vector4 pos = new Vector4(npc.position.X - 1250, npc.position.X + 1250, npc.position.Y - 1250, npc.position.Y + 1250);
                                foreach (Player p in Main.player)
                                {
                                    if (p != null && p.active && !p.dead)
                                    {
                                        if (p.position.X > pos.X && p.position.X < pos.Y && p.position.Y > pos.Z && p.position.Y < pos.W)
                                        {
                                            CameraPanPlayer.RegisterCameraPan(p, npc);
                                            p.GetModPlayer<CameraPanPlayer>().currentAction = 1;
                                        }
                                    }
                                }
                                cameraPanStuff = true;
                            }
                            #endregion
                            if (colorAugment > 0)
                                colorAugment -= 1;
                            if (colorAugment < 1)
                                colorAugment = 0;
                        }
                        if (summonVal[2] <= 160 && vfxTimer[1]++ > 15 && borealis.Count < 10)
                        {
                            if (!borPosTracked[0])
                            {
                                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                                borPosTracked[0] = true;
                                borPos = npc.Center;
                            }
                            borealis.Add(new Borealis(new Vector2(borPos.X - 400, borPos.Y - 100), new Color(0, Main.DiscoG + 125, Main.DiscoB + 125, 255), FacingLeft(), borealis.Count >= 5));
                            vfxTimer[1] = 0;
                        }
                        if (summonVal[2] > 160 && summonVal[0] <= Math.PI)
                        {
                            summonVal[0] += (float)Math.PI / 240;
                            summonVal[1] = (float)Math.Sin(summonVal[0]) * 1.5f;
                            npc.position.Y += summonVal[1];
                            Idle(false, true, false);
                        }
                        if (summonVal[2] > 160 && summonVal[0] >= Math.PI)
                        {
                            vfxTimer[1] = 0;
                            posVal[0] = Vector2.Zero;
                            borPos = Vector2.Zero;
                            borPosTracked[0] = false;
                            npc.dontTakeDamage = false;
                            AIState = State_Glide;
                            GlideEffect();
                        }
                    }
                    break;
                case State_Idle:
                    {
                        framing[FBU].Y = framing[FLU].Y;
                        framing[FBU].X = framing[FLU].X;
                        lockDirection = 0;
                        if (!prevention)
                        {
                            prevention = true;
                            posVal[0] = npc.position;
                        }
                        Idle();
                    }
                    break;
                case State_Glide:
                    {
                        if (!prevention)
                        {
                            prevention = true;
                            posVal[0] = GlidePosition();
                        }
                        Glide();
                    }
                    break;
                case State_Recoil:
                    {
                        Recoil();
                        if (attackVal[0] >= 3)
                            framing[FHe].Y = 0;
                    }

                    break;
                case State_Attack1:
                    Attack1();
                    break;
                case State_Attack2Glide:
                    {
                        if (!prevention)
                        {
                            prevention = true;
                            attackVal[1] = (float)Math.PI;
                            posVal[0] = GlidePosition(true);
                        }
                        Glide(true);
                    }
                    break;
                case State_BorealisGlide:
                    {
                        attackVal[2]++;
                        if (attackVal[2] == 80)
                            attackVal[4] = borPosTracked[1] ? -1 : 1;
                        if (attackVal[2] <= 160 && vfxTimer[1]++ > 15 && borealis.Count < 10)
                        {
                            if (!borPosTracked[0])
                            {
                                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                                borPosTracked[0] = true;
                                borPos = BorealisGlidePosition();
                                borPosTracked[1] = borPos.X < npc.Center.X;
                            }
                            borealis.Add(new Borealis(new Vector2(borPos.X + (borPosTracked[1] ? -1250 : 150), borPos.Y - 75), new Color(0, Main.DiscoG + 125, Main.DiscoB + 125, 255), FacingLeft(), borealis.Count >= 5));
                            vfxTimer[1] = 0;
                        }
                        npc.direction = borPosTracked[1] ? -1 : 1;
                        if (!prevention && attackVal[2] < 150)
                        {
                            prevention = true;
                            posVal[0] = BorealisGlidePosition();
                        }
                        if (attackVal[2] <= 150)
                        {
                            CastLinger(borPos);
                            Glide(true, true);
                            framing[FLU].Y = Frame.Y * (BH() ? 7 : 6);
                            framing[FBU].Y = BH() ? Frame.Y : 0;
                            framing[FBU].X = Frame.X;
                        }
                        if (attackVal[2] <= 180 && attackVal[2] >= 150)
                        {
                            framing[FLU].Z++;
                            switch (framing[FLU].Z)
                            {
                                case 12:
                                    {
                                        framing[FBU].Y = Frame.Y * (BH() ? 11 : 10);
                                        framing[FBU].X = framing[FLU].X = 0;
                                        framing[FLU].Y = Frame.Y * (BH() ? 9 : 8);
                                        framing[FLU].X = 0;
                                    }
                                    break;
                                case 24:
                                    {
                                        framing[FLU].X = Frame.X;
                                        framing[FBU].X = framing[FLU].X = Frame.X;
                                    }
                                    break;
                            }
                            if (attackVal[2] == 180)
                            {
                                attackVal[3] = Main.rand.NextBool(1) ? 1 : 0;
                                posVal[0] = new Vector2(npc.position.X, npc.position.Y);
                            }
                        }
                        if (attackVal[2] <= 300 && attackVal[2] > 180)
                        {
                            framing[FLU].X = Frame.X;
                            BorealisGlide();
                        }
                        if (attackVal[2] >= 300 && attackVal[2] < 360)
                            Idle(true);
                        if (attackVal[2] > 360)
                        {
                            attackVal[0] = 0;
                            attackVal[1] = 0;
                            attackVal[2] = 0;
                            if (Main.expertMode && npc.life < npc.lifeMax * 0.5)
                            {
                                specialAttack += specialAttack > 1 ? -2 : 1;
                                AIState = specialAttack == 1 ? State_IceCube : State_Blizzard;
                            }
                            else
                            {
                                posVal[0] = GlidePosition();
                                GlideEffect();
                                borPosTracked[0] = false;
                                borPosTracked[1] = false;
                            }
                        }
                    }
                    break;
                case State_IceCube:
                    {
                        if (soundTimer[1] < 1)
                        {
                            soundTimer[1]++;
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                        }
                        lockDirection = npc.direction;
                        specAttackVal[4]++;
                        if (specAttackVal[4] < 120)
                        {
                            altBody = true;
                            framing[FHe].Y = Frame.Y * 3;
                            framing[FB2].Y = Frame.Y;
                            if (framing[FB2].Z++ > 30)
                            {
                                framing[FB2].X += Frame.X;
                                if (framing[FB2].X > Frame.X)
                                    framing[FB2].X = 0;
                                framing[FB2].Z = 0;
                            }
                            Idle(false);
                            npc.velocity.X = 0;
                        }
                        if (specAttackVal[4] >= 120 && specAttackVal[4] < 160)
                        {
                            framing[FHe].Y = 0;
                            framing[FB2].X = Frame.X * 2;
                            Recoil(true);
                            if (specAttackVal[4] == 120)
                            {
                                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/IceBolt"));
                                specAttackVal[5]++;
                                specProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceCore>(), 0, 0, 255, 0f, 0f);
                            }
                        }
                        if (specProj != -1 && Main.projectile[specProj].active)
                        {
                            if (specAttackVal[4] >= 160)
                            {
                                altBody = false;
                                Idle();
                            }
                        }
                        if (specProj != -1 && !Main.projectile[specProj].active)
                            specAttackVal[5] = 2;
                        if (specAttackVal[5] == 2)
                        {
                            if (specAttackVal[6] == 0)
                                specAttackVal[6] = specAttackVal[4];
                            else if (specAttackVal[4] >= specAttackVal[6] + 60)
                            {
                                framing[FB2] = Vector3.Zero;
                                lockDirection = 0;
                                for (int a = 0; a < specAttackVal.Length; a++)
                                    specAttackVal[a] = 0;
                                specProj = -1;
                                soundTimer[1] = 0;
                                posVal[0] = GlidePosition();
                                GlideEffect();
                            }
                        }
                        ArcticWind();
                    }
                    break;
                case State_Blizzard:
                    {
                        if (soundTimer[1] < 1)
                        {
                            soundTimer[1]++;
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                        }
                        lockDirection = npc.direction;
                        specAttackVal[4]++;
                        npc.velocity.X = 0;
                        if (specAttackVal[4] <= 45)
                        {
                            Idle();
                            if (specAttackVal[4] == 45)
                                altBody = true;
                        }
                        if (specAttackVal[4] > 45 && altBody)
                        {
                            framing[FB2].Z++;
                            switch (framing[FB2].Z)
                            {
                                case 1:
                                    {
                                        framing[FLU].Y = Frame.Y * 7;
                                        framing[FB2].Y = Frame.Y * 2;
                                        framing[FLU].X = framing[FB2].X = 0;
                                    }
                                    break;
                                case 11:
                                    framing[FLU].X = framing[FB2].X = Frame.X;
                                    break;
                                case 26:
                                    {
                                        framing[FHe].Y = Frame.Y * 3;
                                        framing[FLU].X = framing[FB2].X = Frame.X * 2;
                                    }
                                    break;
                                case 66:
                                    {
                                        framing[FB2].Y = Frame.Y * 3;
                                        framing[FB2].X = 0;
                                    }
                                    break;
                                case 71:
                                    {
                                        framing[FHe].Y = 0;
                                        framing[FB2].X = Frame.X;
                                    }
                                    break;
                                case 81:
                                    framing[FB2].X = Frame.X * 2;
                                    break;
                            }
                            if (framing[FB2].Z > 91)
                            {
                                framing[FHe].Y = 0;
                                altBody = false;
                                framing[FB2] = Vector3.Zero;
                            }
                        }
                        if (specAttackVal[4] >= 120 && specAttackVal[4] < 160)
                        {
                            framing[FB2].Z++;
                            if (specAttackVal[4] == 120)
                            {
                                Main.PlaySound(SoundID.Item60, npc.Center);
                                specAttackVal[8] = Main.rand.NextFloat(0.25f, 5);
                                specAttackVal[5]++;
                                specProj = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<BlizzardComet>(), MiscUtilities.DL(50), 0, 255, 0f, 0f);
                                if (Main.projectile[specProj].modProjectile is BlizzardComet b)
                                {
                                    b.values[1] = npc.Center.Y;
                                    b.values[2] = Main.rand.NextFloat(0.25f, 10);
                                    b.values[3] = npc.direction;
                                }
                            }
                        }
                        if (specProj != -1 && Main.projectile[specProj].active)
                        {
                            Idle(framing[FB2].Z > 105);
                            specAttackVal[10] += (float)(16 * Math.PI) / 600;
                            specAttackVal[11] = npc.Center.Y + 150 + (75 * ((float)(specAttackVal[8] * Math.Sin(specAttackVal[10]))));
                            if (specAttackVal[9]++ > 15)
                            {
                                Main.PlaySound(SoundID.Item8.WithVolume(0.25f), new Vector2(npc.Center.X + (FacingLeft() ? -75 : 75), specAttackVal[11]));
                                Projectile.NewProjectile(new Vector2(npc.Center.X + (FacingLeft() ? -75 : 75), specAttackVal[11]), new Vector2(FacingLeft() ? -3.75f : 3.75f, 0), ModContent.ProjectileType<Snowflake>(), MiscUtilities.DL(20), 0, 255, 0f, 0f);
                                specAttackVal[9] = 0;
                            }
                        }
                        if (specProj != -1 && !Main.projectile[specProj].active)
                            specAttackVal[5] = 2;
                        if (specAttackVal[5] == 2)
                        {
                            if (specAttackVal[6] == 0)
                                specAttackVal[6] = specAttackVal[4];
                            else if (specAttackVal[4] >= specAttackVal[6] + 60)
                            {
                                lockDirection = 0;
                                for (int a = 0; a < specAttackVal.Length; a++)
                                    specAttackVal[a] = 0;
                                specProj = -1;
                                framing[FB2] = Vector3.Zero;
                                soundTimer[1] = 0;
                                posVal[0] = GlidePosition();
                                GlideEffect();
                            }
                        }
                        ArcticWind(true);
                    }
                    break;
                case State_Transition:
                    {
                        npc.dontTakeDamage = true;
                        altBody = true;
                        blink[1] = false;
                        if (colorAugment > 0)
                            colorAugment -= 25;
                        if (colorAugment < 0)
                            colorAugment = 0;
                        switch (framing[FB2].Z)
                        {
                            case 0:
                                {
                                    colorAugment = 255;
                                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Hit"));
                                    Gore.NewGoreDirect(npc.Center, Vector2.Zero, mod.GetGoreSlot("Gores/Flayke/FlaykeBow"));
                                    if (telegraphs.Count > 0)
                                    {
                                        for (int a = 0; a < telegraphs.Count; a++)
                                        {
                                            Projectile p = telegraphs[a];
                                            p.Kill();
                                            telegraphs.Remove(p);
                                        }
                                    }
                                    framing[FLU].X = 0;
                                    framing[FLU].Y = Frame.Y;
                                    framing[FHe].Y = Frame.Y;
                                }
                                break;
                            case 20:
                                {
                                    framing[FHa].Y = Frame.Y;
                                    framing[FLU].Y = Frame.Y * 5;
                                    framing[FLU].X = Frame.X * 2;
                                }
                                break;
                            case 35:
                                {
                                    framing[FB2].X = Frame.X;
                                    framing[FLU].X = 0;
                                }
                                break;
                            case 40:
                                {
                                    framing[FB2].X = Frame.X * 2;
                                    framing[FHa].Y = Frame.Y * 2;
                                }
                                break;
                            case 60:
                                {
                                    framing[FHa].Y = Frame.Y * 3;
                                    framing[FHe].Y = Frame.Y * 3;
                                    framing[FB2].X = Frame.X;
                                }
                                break;
                            case 100:
                                {
                                    framing[FHe].Y = Frame.Y;
                                    framing[FB2].X = 0;
                                }
                                break;
                            case 115:
                                framing[FHe].Y = 0;
                                break;
                        }
                        framing[FB2].Z++;
                        if (framing[FB2].Z > 145)
                        {
                            blink[3] = false;
                            npc.dontTakeDamage = false;
                            framing[FB2] = Vector3.Zero;
                            altBody = false;
                            attackVal[0] = 0;
                            GlideEffect();
                        }
                    }
                    break;
            }
            #region Actions
            void Idle(bool alt = false, bool ani = true, bool posChange = true)
            {
                blink[1] = true;
                if (!alt)
                    attackVal[2] += (float)(32 * Math.PI) / 180;
                else
                    attackVal[7] += (float)(32 * Math.PI) / 180;
                npc.damage = 0;
                if (posChange)
                    npc.position.Y = posVal[0].Y + ((float)(2 * (Math.Sin(0.05f * (alt ? attackVal[7] : attackVal[2])) * 0.5f) * 15));
                if ((alt ? attackVal[7] : attackVal[2]) > 180)
                {
                    if (!alt)
                        attackVal[2] = 0;
                    else
                        attackVal[7] = 0;
                    prevention = false;
                    if (attackVal[0] > 2)
                    {
                        if (attackVal[0] > 5)
                            attackVal[0] = 0;
                        AIState = State_Attack2Glide;
                    }
                }
                framing[FLU].Y = Frame.Y * (BH() ? 5 : 4);
                if (framing[FLU].Z++ > 20)
                {
                    framing[FLU].X += Frame.X;
                    if (framing[FLU].X > (Frame.X * 2))
                        framing[FLU].X = 0;
                    framing[FLU].Z = 0;
                }
                if (ani)
                {
                    framing[FBU].X = framing[FLU].X;
                    framing[FBU].Y = framing[FLU].Y;
                }
            }
            void Glide(bool alt = false, bool alt2 = false)
            {
                blink[1] = false;
                framing[FHe].Y = 0;
                framing[FLU].Y = Frame.Y * (BH() ? 1 : 0);
                lockDirection = !alt && attackVal[0] < 3 ? npc.direction : 0;
                attackVal[1] += alt ? (float)(2 * Math.PI) / 150 : (float)Math.PI / 60;
                double sine = 0.5 * Math.Cos(attackVal[1]) + 0.5;
                double velocity = sine * ((posVal[0] - npc.position).Length() / (alt ? 22.5 : 12.5));
                if (attackVal[1] < (alt ? 3 * Math.PI : Math.PI))
                {
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                    float rotation = (float)Math.Atan2(vector8.Y - posVal[0].Y, vector8.X - posVal[0].X);
                    npc.velocity.X = (float)(Math.Cos(rotation) * velocity) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * velocity) * -1;
                }
                if (attackVal[1] < Math.PI / 3)
                    framing[FLU].X = Frame.X;
                else
                    framing[FLU].X = Frame.X * 2;
                if (alt || attackVal[0] >= 3)
                {
                    framing[FBU].Y = framing[FLU].Y;
                    framing[FBU].X = framing[FLU].X;
                }
                if (attackVal[1] >= (alt ? 3 * Math.PI : Math.PI))
                {
                    attackVal[1] = 0;
                    framing[FLU].Z = 0;
                    attackVal[2] = 0;
                    if (!alt2)
                        AIState = State_Attack1;
                    posVal[1] = Vector2.Zero;
                    prevention = false;
                }
            }
            void BorealisGlide()
            {
                blink[1] = false;
                attackVal[1] += (float)(6 * Math.PI) / 180;
                double sine = attackVal[3] == 1 ? 0.5 * Math.Sin(0.5 * attackVal[1]) : -1 * (0.5 * Math.Sin(0.5 * attackVal[1]));
                npc.position.X += attackVal[4] == -1 ? -7.5f : 7.5f;
                switch (ProjValue())
                {
                    case 1:
                        {
                            Main.PlaySound(SoundID.Item76, npc.Center);
                            int a = Projectile.NewProjectile(npc.Center, new Vector2(0, -7.5f), ModContent.ProjectileType<BorealisFragment>(), MiscUtilities.DL(35), 0, 255, 0f, 0f);
                            if (Main.projectile[a].modProjectile is BorealisFragment b)
                                b.target = Target();
                        }
                        break;
                    case -1:
                        {
                            Main.PlaySound(SoundID.Item76, npc.Center);
                            int a = Projectile.NewProjectile(npc.Center, new Vector2(0, 7.5f), ModContent.ProjectileType<BorealisFragment>(), MiscUtilities.DL(35), 0, 255, 0f, 0f);
                            if (Main.projectile[a].modProjectile is BorealisFragment b)
                                b.target = Target();
                        }
                        break;
                }
                int ProjValue()
                {
                    if (sine > 0.4998)
                        return 1;
                    if (sine < -0.4998)
                        return -1;
                    return 0;
                }
                if (attackVal[1] < 6 * Math.PI)
                    npc.position.Y += (float)sine * 5;
                else
                {
                    attackVal[4] = 0;
                    attackVal[3] = 0;
                    posVal[0] = Vector2.Zero;
                }
            }
            void Recoil(bool alt = false, bool ani = true)
            {
                blink[1] = false;
                if (ani)
                {
                    framing[FLU].Y = Frame.Y * (!alt ? (BH() ? 1 : 0) : (BH() ? 3 : 2));
                    framing[FLU].X = alt ? Frame.X * 2 : 0;
                }
                attackVal[1] += (float)Math.PI / 40;
                float sine = (float)(0.5 * Math.Cos(attackVal[1]) + 0.5);
                if (attackVal[1] < Math.PI)
                {
                    if (attackVal[0] >= 3)
                    {
                        npc.position.X += attackVal[6] * sine;
                        npc.position.Y += attackVal[5] * sine;
                    }
                    else
                        npc.position.X += FacingLeft() ? 3.25f * sine : -3.25f * sine;
                }
                else
                {
                    if (attackVal[0] >= 3)
                        lockDirection = 0;
                    prevention = false;
                    attackVal[5] = 0;
                    attackVal[1] = 0;
                    attackVal[0]++;
                    if (attackVal[0] == 3)
                        AIState = State_Idle;
                    else if (attackVal[0] > 5 && (npc.life < npc.lifeMax * 0.5 || Main.expertMode))
                        AIState = State_BorealisGlide;
                    else if (!alt)
                        GlideEffect();
                    framing[FLU].Z = 0;
                }
            }
            void GlideEffect()
            {
                Projectile p = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<Blast>(), 0, 0);
                if (p.modProjectile is Blast b)
                {
                    b.increment = 0.05f;
                    b.lerpColor = new Color[2] { Color.White, Color.CornflowerBlue };
                    b.maxSize = 1;
                }
                Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Glide"));
                AIState = State_Glide;
            }
            void CastLinger(Vector2 pos)
            {
                if (soundTimer[0]++ > 120)
                {
                    soundTimer[0] = 0;
                    Main.PlaySound(SoundLoader.customSoundType, pos, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/CastLinger"));
                }
            }
            void Attack1()
            {
                framing[FLU].Z++;
                blink[1] = false;
                attackVal[2]++;
                if (attackVal[0] < 3)
                {
                    framing[FBU].Y = framing[FLU].Y = Frame.Y * (BH() ? 3 : 2);
                    if (framing[FLU].Z < 25)
                        framing[FBU].X = framing[FLU].X = 0;
                    else
                    {
                        framing[FHe].Y = Frame.Y;
                        framing[FBU].X = framing[FLU].X = Frame.X;
                    }
                    if (attackVal[2] < 2)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                        Vector2 vec = new Vector2(npc.Center.X + (FacingLeft() ? -10 : 10), npc.Center.Y);
                        for (int i = 0; i < 15; i++)
                        {
                            Vector2 position = vec + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i));
                            Dust dust = Dust.NewDustPerfect(position, 76);
                            dust.noGravity = true;
                            dust.velocity = Vector2.Normalize(dust.position - vec) * 2.5f;
                            dust.noLight = false;
                            dust.fadeIn = 1f;
                        }
                    }
                    if (attackVal[2] > 30)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/IceBolt"));
                        for (int a = 0; a < 3; a++)
                        {
                            int b = Projectile.NewProjectile(new Vector2(npc.position.X + (FacingLeft() ? -75 : 75), npc.position.Y + 20), new Vector2(FacingLeft() ? -3.5f : 3.5f, 0), ModContent.ProjectileType<SnowFlayke>(), MiscUtilities.DL(25), 0, 255, 0f, 0f);
                            Projectile p = Main.projectile[b];
                            p.ai[0] = a;
                        }
                        framing[FBU].X = Frame.X * 2;
                        framing[FHe].Y = Frame.Y * 2;
                        AIState = State_Recoil;
                    }
                }
                if (attackVal[0] >= 3)
                {
                    framing[FLU].Y = Frame.Y * (BH() ? 7 : 6);
                    if (framing[FLU].Z < 45)
                    {
                        framing[FBU].Y = framing[FLU].Y;
                        framing[FBU].X = framing[FLU].X;
                        if (framing[FLU].Z < 10)
                            framing[FLU].X = 0;
                        else if (framing[FLU].Z < 25)
                            framing[FLU].X = Frame.X;
                        else
                            framing[FLU].X = Frame.X * 2;
                    }
                    if (framing[FLU].Z >= 50)
                    {
                        if (framing[FLU].Z < 60)
                        {
                            framing[FBU].Y = Frame.Y * (BH() ? 9 : 8);
                            framing[FBU].X = 0;
                        }
                        else if (framing[FLU].Z < 75)
                            framing[FBU].X = Frame.X;
                        else
                        {
                            framing[FHe].Y = Frame.Y * 3;
                            framing[FBU].X = Frame.X * 2;
                        }
                    }
                    if (attackVal[2] < 2)
                    {
                        Main.PlaySound(SoundID.Item60, npc.Center);
                        telegraphs.Clear();
                        lockDirection = npc.direction;
                        Vector2 val = default;
                        val.X = Target().position.X;
                        val.Y = Target().position.Y;
                        Vector2 val2 = val - npc.Center;
                        float num2 = 10f;
                        float num3 = (float)Math.Sqrt(val2.X * val2.X + val2.Y * val2.Y);
                        if (num3 > num2)
                            num3 = num2 / num3;
                        val2 *= num3;
                        if (attackVal[8] != 1)
                        {
                            attackVal[9] = val2.X;
                            attackVal[10] = val2.Y;
                            attackVal[8] = 1;
                        }
                        float[] rotations = new float[] { 0, 15, 30, -15, -30 };
                        for (int a = 0; a < 5; a++)
                        {
                            Vector2 perturbedSpeed = new Vector2(attackVal[9] * 0.5f, attackVal[10] * 0.5f).RotatedBy(MathHelper.ToRadians(rotations[a]), default);
                            telegraphs.Add(Projectile.NewProjectileDirect(npc.Center, perturbedSpeed, ModContent.ProjectileType<ArrowIce>(), 0, 0, 255));
                        }
                    }
                    if (attackVal[2] > 60)
                    {
                        #region Icicle Attack
                        Main.PlaySound(SoundID.Item1, npc.Center);
                        foreach (Projectile p in telegraphs)
                            p.Kill();
                        float[] rotations = new float[] { 0, 15, 30, -15, -30 };
                        for (int a = 0; a < 5; a++)
                        {
                            Vector2 perturbedSpeed = new Vector2(attackVal[9] * 0.75f, attackVal[10] * 0.75f).RotatedBy(MathHelper.ToRadians(rotations[a]), default);
                            Projectile.NewProjectileDirect(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), MiscUtilities.DL(25), 0, 255, 0f, 0f);
                        }
                        #endregion
                        attackVal[6] = -1 * (attackVal[9] * 0.5f);
                        attackVal[5] = -1 * (attackVal[10] * 0.5f);
                        attackVal[8] = 0;
                        framing[FHe].Y = 0;
                        AIState = State_Recoil;
                    }
                }
            }
            void ArcticWind(bool alt = false)
            {
                if (Main.rand.NextBool(4))
                    CreateWindVisual(Main.rand.NextBool(2));
                foreach (Player p in Main.player)
                {
                    if (p != null && p.active && !p.dead)
                    {
                        if (p.position.X < npc.position.X + (alt ? 2250 : 1500) && p.position.X > npc.position.X - (alt ? 2250 : 1500))
                        {
                            if (!alt)
                            {
                                CastLinger(new Vector2(npc.position.X - 900, npc.position.Y + 75));
                                CastLinger(new Vector2(npc.position.X + 900, npc.position.Y + 75));
                                if (p.position.X < npc.position.X - 900)
                                    p.velocity.X += 0.225f;
                                if (p.position.X > npc.position.X + 900)
                                    p.velocity.X -= 0.225f;
                            }
                            else
                            {
                                if (p.position.Y < npc.position.Y - 150)
                                {
                                    p.wingTime = 0;
                                    p.rocketTime = 0;
                                    p.velocity.Y += 0.275f;
                                }
                                if (FacingLeft())
                                {
                                    CastLinger(new Vector2(npc.position.X - 625, npc.position.Y - 75));
                                    CastLinger(new Vector2(npc.position.X - 1250, npc.position.Y + 75));
                                    CastLinger(new Vector2(npc.position.X - 50, npc.position.Y + 75));
                                    if (p.position.X < npc.position.X - 1250)
                                        p.velocity.X += 0.225f;
                                    if (p.position.X > npc.position.X - 50)
                                        p.velocity.X -= 0.225f;
                                }
                                else
                                {
                                    CastLinger(new Vector2(npc.position.X + 625, npc.position.Y - 75));
                                    CastLinger(new Vector2(npc.position.X + 1250, npc.position.Y + 75));
                                    CastLinger(new Vector2(npc.position.X + 50, npc.position.Y + 75));
                                    if (p.position.X > npc.position.X + 1250)
                                        p.velocity.X -= 0.225f;
                                    if (p.position.X < npc.position.X + 50)
                                        p.velocity.X += 0.225f;
                                }
                            }
                        }
                    }
                }
                if (specAttackVal[2]++ > 20)
                {
                    specAttackVal[2] = 0;
                    specAttackVal[3] += specAttackVal[3] > 2 ? -3 : 1;
                    if (!alt)
                    {
                        int a = Projectile.NewProjectile(new Vector2(npc.position.X + distance[(int)specAttackVal[3]], npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                        int b = Projectile.NewProjectile(new Vector2(npc.position.X - distance[(int)specAttackVal[3]], npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                        for (int c = 0; c < Main.rand.Next(0, 4); c++)
                            if (Main.projectile[a].modProjectile is FlaykeBeacon f)
                                f.AddWind(wind, false);
                        for (int c = 0; c < Main.rand.Next(0, 4); c++)
                            if (Main.projectile[b].modProjectile is FlaykeBeacon f)
                                f.AddWind(wind, true);
                    }
                    else
                    {
                        Projectile.NewProjectile(new Vector2(npc.position.X, npc.position.Y - 150), new Vector2(10, 0), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                        Projectile.NewProjectile(new Vector2(npc.position.X, npc.position.Y - 150), new Vector2(-10, 0), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                        if (FacingLeft())
                        {
                            int c = Projectile.NewProjectile(new Vector2(npc.position.X + (distance[(int)specAttackVal[3]] - 900), npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                            int d = Projectile.NewProjectile(new Vector2(npc.position.X - distance[(int)specAttackVal[3]] - 500, npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                            for (int e = 0; e < Main.rand.Next(0, 4); e++)
                                if (Main.projectile[c].modProjectile is FlaykeBeacon f)
                                    f.AddWind(wind, false);
                            for (int e = 0; e < Main.rand.Next(0, 4); e++)
                                if (Main.projectile[d].modProjectile is FlaykeBeacon f)
                                    f.AddWind(wind, true);
                        }
                        else
                        {
                            int c = Projectile.NewProjectile(new Vector2(npc.position.X + distance[(int)specAttackVal[3]] + 500, npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                            int d = Projectile.NewProjectile(new Vector2(npc.position.X - (distance[(int)specAttackVal[3]] - 900), npc.position.Y - 175), new Vector2(0, 10), ModContent.ProjectileType<FlaykeBeacon>(), 0, 0, 255, 0f, 0f);
                            for (int e = 0; e < Main.rand.Next(0, 4); e++)
                                if (Main.projectile[c].modProjectile is FlaykeBeacon f)
                                    f.AddWind(wind, false);
                            for (int e = 0; e < Main.rand.Next(0, 4); e++)
                                if (Main.projectile[d].modProjectile is FlaykeBeacon f)
                                    f.AddWind(wind, true);
                        }
                    }
                }
                void CreateWindVisual(bool left)
                {
                    float MinX()
                    {
                        if (FacingLeft())
                            return left ? 50 : -1550;
                        else
                            return left ? 1550 : -50;
                    }
                    if (alt)
                    {
                        Vector2 x2 = new Vector2(npc.position.X + (left ? 50 : -50), npc.position.X + MinX());
                        Vector2 y2 = new Vector2(npc.position.Y - 350, npc.position.Y - 550);
                        Vector2 GetRandAlt()
                        {
                            if (left)
                                return new Vector2(Main.rand.NextFloat(x2.X, x2.Y), Main.rand.NextFloat(y2.Y, y2.X));
                            else
                                return new Vector2(Main.rand.NextFloat(x2.Y, x2.X), Main.rand.NextFloat(y2.Y, y2.X));
                        }
                        if (vfxTimer[0]++ > 5 && clouds.Count < 30)
                        {
                            Vector2 coord = GetRandAlt();
                            clouds.Add(new Cloud(coord, Main.rand.Next(1, 4), Main.rand.NextFloat(0.15f, 1.5f), Main.rand.NextBool(2), Velocity()));
                            vfxTimer[0] = 0;
                            float Velocity()
                            {
                                if (coord.X < npc.position.X)
                                    return Main.rand.NextFloat(0, 2.5f);
                                return Main.rand.NextFloat(-2.5f, 0);
                            }
                        }
                    }
                }
            }
            void Blink()
            {
                switch (framing[FHe].Z)
                {
                    case 0:
                        framing[FHe].Y = 0;
                        break;
                    case 5:
                        framing[FHe].Y = Frame.Y * 3;
                        break;
                    case 10:
                        framing[FHe].Y = Frame.Y * 2;
                        break;
                    case 15:
                        framing[FHe].Y = Frame.Y * 3;
                        break;
                    case 20:
                        {
                            blink[0] = false;
                            framing[FHe].Y = 0;
                        }
                        break;
                }
                framing[FHe].Z++;
            }
            Vector2 GlidePosition(bool alt = false) => FacingLeft() ? new Vector2(Target().position.X + ((alt ? -325 : 325) + posVal[1].X), (alt ? npc.position.Y : Target().position.Y) + posVal[1].Y) : new Vector2(Target().position.X + ((alt ? 325 : -325) + posVal[1].X), (alt ? npc.position.Y : Target().position.Y) + posVal[1].Y);
            Vector2 BorealisGlidePosition(bool descent = false) => new Vector2(Target().position.X + (FacingLeft() ? 300 : -300), Target().position.Y + (descent ? 250 : -250));
            #endregion
            void VelocityCompensate()
            {
                switch (AIState)
                {
                    case State_Attack1:
                        {
                            posVal[1].X += Target().velocity.X / 2.25f;
                            posVal[1].Y += Target().velocity.Y / 2.25f;
                        }
                        break;
                    case State_Recoil:
                        {
                            posVal[1].X += Target().velocity.X / 2.25f;
                            posVal[1].Y += Target().velocity.Y / 2.25f;
                        }
                        break;
                }
            }
        }
        public override void NPCLoot()
        {
            if (!despawn)
            {
                CastledWorld.downedFlayke = true;
                Main.PlaySound(SoundID.Item88, npc.Center);
                Main.PlaySound(SoundID.NPCHit5, npc.Center);
                Projectile p = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<Blast>(), 0, 0);
                if (p.modProjectile is Blast b)
                {
                    b.increment = 0.5f;
                    b.lerpColor = new Color[2] { Color.White, Color.Violet };
                    b.maxSize = 12.5f;
                }
                for (int a = 1; a < 6; a++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(npc.Center, new Vector2(3.5f, 0).RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(360))), ModContent.ProjectileType<FlaykeGore>(), 0, 0);
                    if (proj.modProjectile is FlaykeGore f)
                        f.type = a;
                }
                for (int a = 0; a < 5; a++)
                    Projectile.NewProjectileDirect(npc.Center, new Vector2(4.75f, 0).RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(360))), ModContent.ProjectileType<FlaykeRemnant>(), 0, 0);
                #region loot
                if (Main.expertMode)
                {
                    npc.value = 12500;
                    npc.DropBossBags();
                    if (Main.rand.NextBool(9))
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Misc.Trophy.FlaykeTrophy>());
                }
                else
                {
                    if (Main.rand.NextBool(4))
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Misc.Bow.Bow>());
                }
                #endregion
            }
        }
        public override bool CheckDead()
        {
            if (despawn)
            {
                npc.life = npc.lifeMax;
                return false;
            }
            return base.CheckDead();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D wings = ModContent.GetTexture(Directory + "Assets/Wings");
            if (wind.Count > 0)
            {
                for (int a = 0; a < wind.Count; a++)
                {
                    Wind w = wind[a];
                    if (w.ended)
                        wind.Remove(w);
                    else
                    {
                        Texture2D tex = ModContent.GetTexture("CastledsContent/NPCs/Flayke/Wind/" + WindName());
                        spriteBatch.Draw(tex, w.coord - Main.screenPosition, new Rectangle(0, (int)w.frameVal[1] * 100, 100, 100), w.AlphaColor() * w.scale, 0f, Vector2.Zero, w.scale, w.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    }
                    string WindName()
                    {
                        if (!w.altWind)
                            return "Wind" + (w.type == 2 ? "2" : "1");
                        return "Clouds/WindAlt" + (w.type == 2 ? "2" : "1");
                    }
                }
            }
            if (clouds.Count > 0)
            {
                for (int a = 0; a < clouds.Count; a++)
                {
                    Cloud c = clouds[a];
                    if (c.ended)
                        clouds.Remove(c);
                    else
                    {
                        Texture2D tex = ModContent.GetTexture("CastledsContent/NPCs/Flayke/Wind/Clouds/Cloud" + c.type);
                        spriteBatch.Draw(tex, c.coord - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), c.AlphaColor() * c.scale, 0f, Vector2.Zero, c.scale, c.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    }
                }
            }
            if (borealis.Count > 0)
            {
                for (int a = 0; a < borealis.Count; a++)
                {
                    Borealis b = borealis[a];
                    b.Update();
                    if (b.ended)
                        borealis.Remove(b);
                    else if (!b.front)
                    {
                        Texture2D tex = ModContent.GetTexture("CastledsContent/NPCs/Flayke/Borealis");
                        spriteBatch.Draw(tex, b.coord - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), b.AlphaColor(), 0f, Vector2.Zero, 1, b.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    }
                }
            }
            //Wings
            for (int k = 0; k < oldPosNew.Length; k++)
            {
                Vector2 drawPos = oldPosNew[k] - Main.screenPosition;
                Color color = (BH() && AIState != State_Transition ? new Color(0, Main.DiscoG + 125, Main.DiscoB + 125) : Color.CornflowerBlue) * ((float)(oldPosNew.Length - k) / (float)oldPosNew.Length);
                color = new Color(color.R - colorAugment, color.G - colorAugment, color.B - colorAugment, color.A - colorAugment);
                //--Drawing test--
                spriteBatch.End(); spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
                DrawData test = new DrawData(wings, drawPos, new Rectangle(0, (int)framing[5].Y, 72, 72), color, npc.rotation, new Vector2(wings.Width / 2, -172 + (wings.Height / 2)), npc.scale, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye);
                shader.Apply(npc, test);
                test.Draw(spriteBatch);
                spriteBatch.End(); spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
                //--
            }
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (borealis.Count > 0)
            {
                for (int a = 0; a < borealis.Count; a++)
                {
                    Borealis b = borealis[a];
                    b.Update();
                    if (b.ended)
                        borealis.Remove(b);
                    else if (b.front)
                    {
                        Texture2D tex = ModContent.GetTexture("CastledsContent/NPCs/Flayke/Borealis");
                        spriteBatch.Draw(tex, b.coord - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), b.AlphaColor(), 0f, Vector2.Zero, 1, b.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    }
                }
            }
            Vector2 offset = new Vector2(19, 31);
            Color useColor = new Color(drawColor.R - colorAugment, drawColor.G - colorAugment, drawColor.B - colorAugment, drawColor.A - colorAugment);
            #region Doubling Effect Left
            Vector2 doublingPos1 = new Vector2(npc.Center.X + (CastledsContent.instance.pedestal[3] * 4.25f), npc.Center.Y);
            Vector2 doublingPos2 = new Vector2(npc.Center.X + (CastledsContent.instance.pedestal[3] * -4.25f), npc.Center.Y);
            float alpha = 0.425f;
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Head"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FHe].X, (int)framing[FHe].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Hair"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FHa].X, (int)framing[FHa].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/LowerUniv"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FLU].X, (int)framing[FLU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            if (!altBody)
            {
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUnivExt"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUniv"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            else
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperP2"), doublingPos1 - Main.screenPosition, new Rectangle((int)framing[FB2].X, (int)framing[FB2].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            #endregion
            #region Doubling Effect Right
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Head"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FHe].X, (int)framing[FHe].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Hair"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FHa].X, (int)framing[FHa].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/LowerUniv"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FLU].X, (int)framing[FLU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            if (!altBody)
            {
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUnivExt"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUniv"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            else
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperP2"), doublingPos2 - Main.screenPosition, new Rectangle((int)framing[FB2].X, (int)framing[FB2].Y, (int)Frame.X, (int)Frame.Y), useColor * alpha, 0, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            #endregion
            #region Drawing the NPC
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Head"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FHe].X, (int)framing[FHe].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Hair"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FHa].X, (int)framing[FHa].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/LowerUniv"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FLU].X, (int)framing[FLU].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            if (!altBody)
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUniv"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperP2"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FB2].X, (int)framing[FB2].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/Deco/Snowball"), new Vector2(npc.Center.X + (FacingLeft() ? -10 : 10), npc.Center.Y) - Main.screenPosition, new Rectangle(0, 0, 24, 24), useColor, snowBallin[1], new Vector2(12.5f, 12.5f), snowBallin[0], !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            if (!altBody)
                spriteBatch.Draw(ModContent.GetTexture(Directory + "Assets/UpperUnivExt"), npc.Center - Main.screenPosition, new Rectangle((int)framing[FBU].X, (int)framing[FBU].Y, (int)Frame.X, (int)Frame.Y), useColor, 0f, offset, 1, !FacingLeft() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            #endregion
        }
        public static Projectile BlastEffect(Vector2 pos, Color[] fadeInOut, float maxSize = 1)
        {
            Projectile p = Projectile.NewProjectileDirect(pos, Vector2.Zero, ModContent.ProjectileType<Blast>(), 0, 0);
            if (p.modProjectile is Blast b)
            {
                b.increment = 0.05f;
                b.lerpColor = fadeInOut;
                b.maxSize = maxSize;
            }
            return p;
        }
        bool BH() => npc.life < npc.lifeMax * 0.5f;
        public Player Target() => Main.player[npc.target];
        public bool FacingLeft() => npc.direction == -1;
    }
    public class SnowFlayke : ModProjectile
    {
        double distance = 9.87;
        int stuff = 255;
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/IceBolt";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Bolt");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 58;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.coldDamage = true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, new Vector3(0, (Main.DiscoG + 125) * 0.005f, (Main.DiscoB + 125) * 0.005f));
            distance += 0.2;
            if (stuff > 0)
                stuff -= 1;
            switch (projectile.ai[0])
            {
                case 1:
                    projectile.velocity.Y = (float)(0.75 * Math.Sin(Math.Sqrt(distance)));
                    break;
                case 2:
                    projectile.velocity.Y = (float)(-0.75 * Math.Sin(Math.Sqrt(distance)));
                    break;
            }
            if (Main.rand.NextBool(19))
            {
                Dust a = Dust.NewDustPerfect(new Vector2(projectile.Center.X + (projectile.direction == -1 ? 75 : -75), projectile.Center.Y), ModContent.DustType<DiamonDust>());
                a.frame = new Rectangle(0, Main.rand.NextBool(3) ? 20 : 0, 10, 20);
                a.velocity.X = projectile.velocity.X * -2;
                a.customData = Main.rand.NextFloat(projectile.velocity.X * -1f);
                a.color = new Color(0 + stuff, (Main.DiscoG + 125) + stuff, (Main.DiscoB + 125) + stuff);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor) => new Color(0 + stuff, Main.DiscoG + 75 + stuff, Main.DiscoB + 125 + stuff);
    }
    public class BorealisFragment : ModProjectile
    {
        float iceSpin;
        int[] lifeTime = new int[3];
        public Player target;
        Vector2 targetPos;
        bool[] locked = new bool[2];
        float rotLock;
        float aura = 0;
        int dustDelay;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/BorealisFragment";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Borealis Fragment");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.coldDamage = true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, new Vector3(0, (Main.DiscoG + 125) * 0.005f, (Main.DiscoB + 125) * 0.005f));
            lifeTime[0]++;
            if (dustDelay++ > 15)
            {
                dustDelay = 0;
                Dust a = Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), ModContent.DustType<DiamonDust>());
                a.frame = new Rectangle(0, Main.rand.NextBool(3) ? 20 : 0, 10, 20);
                a.customData = Main.rand.NextFloat(3.75f);
                a.color = new Color(0, Main.DiscoG + 125, Main.DiscoB + 125);
            }
            if (lifeTime[0] < 90)
            {
                projectile.rotation += 1.25f;
                projectile.velocity *= new Vector2(0.975f, 0.975f);
            }
            if (lifeTime[0] > 150)
            {
                if (!locked[1])
                {
                    projectile.rotation += 0.125f;
                    if (!locked[0])
                    {
                        locked[0] = true;
                        rotLock = NewToRotation(target.position, projectile.position);
                        targetPos = target.position;
                    }
                    if (projectile.rotation < rotLock + 25 && projectile.rotation > rotLock - 25)
                    {
                        if (!locked[1])
                            Main.PlaySound(SoundID.Item8, projectile.Center);
                        locked[1] = true;
                        projectile.rotation = rotLock;
                    }
                }
                else
                {
                    lifeTime[1]++;
                    if (lifeTime[1] > 75)
                    {
                        if (projectile.position.X > targetPos.X - 75 && projectile.position.X < targetPos.X + 75 && projectile.position.Y > targetPos.Y - 75 && projectile.position.Y < targetPos.Y + 75)
                            projectile.Kill();
                        else
                        {
                            Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width * 0.5f), projectile.position.Y + (projectile.height * 0.5f));
                            float rotation = (float)Math.Atan2(vector8.Y - targetPos.Y, vector8.X - targetPos.X);
                            projectile.velocity.X = (float)(Math.Cos(rotation) * 5) * -1;
                            projectile.velocity.Y = (float)(Math.Sin(rotation) * 5) * -1;
                        }
                    }
                }
            }
            if (lifeTime[2]++ > 5)
            {
                lifeTime[2] = 0;
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
            if (targetPos != null)
            {
                iceSpin += 0.01f;
                if (iceSpin > 359)
                    iceSpin = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D aur = ModContent.GetTexture(Flayke.Directory + "Projectiles/BorealisFragmentBehind");
            Texture2D crosshair = ModContent.GetTexture(Flayke.Directory + "Assets/BorealisCrosshair");
            Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), lightColor, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            if (lifeTime[1] > 0 && aura < 1)
                aura += 0.025f;
            Color glowy = new Color(0, Main.DiscoG + 75, Main.DiscoB + 125);
            Color color = new Color(MiscUtilities.Round(MathHelper.Lerp(glowy.R, 0, aura / 1.5f)), MiscUtilities.Round(MathHelper.Lerp(glowy.G, 0, aura / 1.5f)), MiscUtilities.Round(MathHelper.Lerp(glowy.B, 0, aura / 1.5f)), MiscUtilities.Round(MathHelper.Lerp(255, 0, aura / 1.5f)));
            spriteBatch.Draw(aur, projectile.Center - Main.screenPosition, new Rectangle(0, 0, aur.Width, aur.Height), color, projectile.rotation, new Vector2(aur.Width / 2, aur.Height / 2), aura, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            //AfterIMG
            Texture2D star = ModContent.GetTexture(Flayke.Directory + "Projectiles/BorealisFragmentBehind");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(star, n.coord - Main.screenPosition, new Rectangle(0, 0, star.Width, star.Height), n.GetColor(glowy), n.rotation, new Vector2(star.Width / 2, star.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            #region Crosshair Drawing
            if (targetPos != null)
            {
                spriteBatch.Draw(crosshair, targetPos - Main.screenPosition, new Rectangle(0, 0, crosshair.Width, crosshair.Height), glowy, 90 + iceSpin, new Vector2(crosshair.Width / 2 + 20, crosshair.Height / 2), 0.75f, SpriteEffects.None, 0);
                spriteBatch.Draw(crosshair, targetPos - Main.screenPosition, new Rectangle(0, 0, crosshair.Width, crosshair.Height), glowy, 180 + iceSpin, new Vector2(crosshair.Width / 2 + 20, crosshair.Height / 2), 0.75f, SpriteEffects.None, 0);
                spriteBatch.Draw(crosshair, targetPos - Main.screenPosition, new Rectangle(0, 0, crosshair.Width, crosshair.Height), glowy, 270 + iceSpin, new Vector2(crosshair.Width / 2 + 20, crosshair.Height / 2), 0.75f, SpriteEffects.None, 0);
            }
            #endregion
            return false;
        }
        public static float NewToRotation(Vector2 targ, Vector2 origin) => (float)Math.Atan2(targ.Y - origin.Y, targ.X - origin.X);
        public override Color? GetAlpha(Color lightColor) => new Color(0, Main.DiscoG + 75, Main.DiscoB + 125);
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundLoader.customSoundType, projectile.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/IceBoltCrash"));
            Projectile pr = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ModContent.ProjectileType<Blast>(), 0, 0);
            foreach (Player p in Main.player)
            {
                PlayerDeathReason dR = new PlayerDeathReason
                {
                    SourceCustomReason = $"The ashes of {p.name} glide alongst the wind."
                };
                if (p.position.X > projectile.position.X - 100 && p.position.X < projectile.position.X + 100 && p.position.Y > projectile.position.Y - 100 && p.position.Y < projectile.position.Y + 100)
                    p.Hurt(dR, MiscUtilities.DL(50), projectile.direction);
            }
            if (pr.modProjectile is Blast b)
            {
                b.increment = 0.175f;
                b.lerpColor = new Color[2] { Color.LawnGreen, Color.CadetBlue };
                b.maxSize = 5f;
            }
        }
    }
    public class IceKnife : ModProjectile
    {
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/IceKnife";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Knife");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 78;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 0.9f;
            Main.projFrames[projectile.type] = 4;
            projectile.coldDamage = true;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            if (projectile.frameCounter++ > 10)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture(Flayke.Directory + "Projectiles/IceKnife/IceKnifeAfterIMG");
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Rectangle refRect = new Rectangle(0, 0, projectile.width, projectile.height);
            Rectangle rectangle = new Rectangle(refRect.X, refRect.Y, (tex.Width), (tex.Height / Main.projFrames[projectile.type]));
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(tex, drawPos, rectangle, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item50, projectile.Center);
            int a = Projectile.NewProjectile(projectile.Center, new Vector2(projectile.velocity.X, 0).RotatedBy(MathHelper.ToRadians(180 + projectile.rotation)), ModContent.ProjectileType<IceKnifeAlt>(), MiscUtilities.DL(25), 0, 255, 0f, 0f);
            if (Main.projectile[a].modProjectile is IceKnifeAlt i)
                i.rotato = projectile.velocity.X * 25f;
            Main.projectile[a].velocity.X = projectile.velocity.X * -1;
            Main.projectile[a].velocity.Y = projectile.velocity.Y * -1;
        }
    }
    public class Snowflake : ModProjectile
    {
        int timer;
        int imgTimer;
        float rotation;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => Flayke.Directory + "Projectiles/Snowflake";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Flayke");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 26;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 0.9f;
            projectile.coldDamage = true;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            timer += 5;
            if (timer > 255)
                timer = 255;
            rotation += projectile.velocity.X < 1 ? -0.75f : 0.75f;
            if (rotation >= 360 || rotation <= -360)
                rotation = 0;
            if (imgTimer++ > 5)
            {
                imgTimer = 0;
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
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(tex, n.coord - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), n.GetColor(lightColor), n.rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Color color = new Color(lightColor.R - (255 - timer), lightColor.G - (255 - timer), lightColor.B - (255 - timer), lightColor.A - (255 - timer));
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), color, rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0f);
        }
    }
    public class IceKnifeAlt : ModProjectile
    {
        public float rotato;
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/IceKnife/IceKnifeAlt";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Knife");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 52;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 0.9f;
            projectile.coldDamage = true;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            projectile.velocity *= new Vector2(0.95f, 0.95f);
            projectile.rotation += rotato;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.75f;
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
    public class ArrowIce : ModProjectile
    {
        int val;
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/ArrowIce";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Telegraph");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 26;
            projectile.light = 0.15f;
            projectile.timeLeft = 1200;
            projectile.alpha = 255;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (val++ < 51)
                projectile.alpha -= 5;
            projectile.velocity *= new Vector2(0.975f, 0.975f);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27, projectile.Center);
            for (int a = 0; a < (Main.rand.NextBool(1) ? 3 : 2); a++)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Ice);
            Gore.NewGore(projectile.position, Vector2.Zero, mod.GetGoreSlot("Gores/Flayke/ArrowIce/ArrowIce" + Main.rand.Next(1, 4)), 1f);
        }
    }
    public class FlaykeBeacon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Northern Light");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 300;
            projectile.penetrate = 6;
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)

                if (projectile.localAI[0] > 9f)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        projectile.position -= projectile.velocity * ((float)i * 0.25f);
                        projectile.alpha = 255;
                        /*int dust = Dust.NewDust(projectilePosition, 1, 1, 135, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = projectilePosition;
                        Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                        Main.dust[dust].velocity *= 0.2f;*/
                    }
                }
        }
        public void AddWind(List<Wind> wind, bool left)
        {
            Vector2 x = new Vector2(projectile.position.X + (left ? -500 : 500), projectile.position.X + (left ? 75 : -75));
            Vector2 y = new Vector2(projectile.position.Y + 500, projectile.position.Y - 50);
            Wind gust = new Wind(GetRand(), Main.rand.NextBool(2) ? 2 : 1, left, 0.5f);
            Vector2 GetRand()
            {
                if (left)
                    return new Vector2(Main.rand.NextFloat(x.X, x.Y), Main.rand.NextFloat(y.Y, y.X));
                else
                    return new Vector2(Main.rand.NextFloat(x.Y, x.X), Main.rand.NextFloat(y.Y, y.X));
            }
            gust.SetSize();
            wind.Add(gust);
        }
    }
    public class IceCore : ModProjectile
    {
        int[] values = new int[3];
        int[] iceCone = new int[2];
        float iceSpin;
        float iceX;
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/IceBauble";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Bauble");
        }

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.ignoreWater = true;
            projectile.timeLeft = 6000;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.coldDamage = true;
        }
        public override void AI()
        {
            if (iceX < Math.PI * 2)
                iceX += (float)Math.PI / 45;
            else
                iceX = 0;
            if (iceCone[0]++ > 30)
            {
                iceCone[0] = 0;
                iceCone[1] += 24;
                if (iceCone[1] > 72)
                    iceCone[1] = 0;
            }
            iceSpin += 0.01f;
            if (iceSpin > 359)
                iceSpin = 0;
            projectile.velocity.X *= 0.1f;
            projectile.velocity.Y *= 0.1f;
            values[0]++;
            if (values[0] > 60)
            {
                if (values[1]++ > 60)
                {
                    values[1] = 0;
                    values[2] += values[2] > 0 ? -1 : 1;
                    Projectile.NewProjectile(new Vector2(projectile.Center.X + (values[2] == 1 ? -35 : 35), projectile.Center.Y + (values[2] == 1 ? -35 : 35)), new Vector2(values[2] == 1 ? 0 : -1.25f, values[2] == 1 ? 1.25f : 0), ModContent.ProjectileType<SnowEssence>(), 0, 0, 255, 0f, 0f);
                    Projectile.NewProjectile(new Vector2(projectile.Center.X + (values[2] == 1 ? 35 : -35), projectile.Center.Y + (values[2] == 1 ? 35 : -35)), new Vector2(values[2] == 1 ? 0 : 1.25f, values[2] == 1 ? -1.25f : 0), ModContent.ProjectileType<SnowEssence>(), 0, 0, 255, 0f, 0f);
                }
                if (values[0] == 255)
                {
                    Main.PlaySound(SoundLoader.customSoundType, projectile.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Flayke/Cast"));
                    Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ModContent.ProjectileType<IceCube>(), MiscUtilities.DL(50), 0, 255, 0f, 0f);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D ice = ModContent.GetTexture(Flayke.Directory + "Assets/BaubleIce");
            Texture2D projection = ModContent.GetTexture(Flayke.Directory + "Assets/BaubleProjector");
            Texture2D cube = ModContent.GetTexture(Flayke.Directory + "Projectiles/IceQbe");
            spriteBatch.Draw(ice, projectile.Center - Main.screenPosition, new Rectangle(0, 0, ice.Width, ice.Height), lightColor * 1.75f, (float)(Math.Cos(iceX) + 0.5f) / 2, new Vector2(ice.Width / 2, ice.Height / 2), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(projection, projectile.Center - Main.screenPosition, new Rectangle(0, iceCone[1], projection.Width, 24), lightColor, 90 + iceSpin, new Vector2(projection.Width / 2 + (projectile.width / 2), projection.Height / 2), 0.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(projection, projectile.Center - Main.screenPosition, new Rectangle(0, iceCone[1], projection.Width, 24), lightColor, 180 + iceSpin, new Vector2(projection.Width / 2 + (projectile.width / 2), projection.Height / 2), 0.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(projection, projectile.Center - Main.screenPosition, new Rectangle(0, iceCone[1], projection.Width, 24), lightColor, 270 + iceSpin, new Vector2(projection.Width / 2 + (projectile.width / 2), projection.Height / 2), 0.75f, SpriteEffects.None, 0);
            if (values[0] < 255)
                spriteBatch.Draw(cube, projectile.Center - Main.screenPosition, new Rectangle(0, 0, cube.Width, cube.Height), new Color(lightColor.R - (255 - values[0]), lightColor.G - (255 - values[0]), lightColor.B - (255 - values[0]), lightColor.A - (255 - values[0])), 0, new Vector2(cube.Width / 2, cube.Height / 2), 1, SpriteEffects.None, 0);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath3, projectile.Center);
            Gore.NewGoreDirect(projectile.Center, Vector2.Zero, mod.GetGoreSlot("Gores/Flayke/Bauble/BaubleIce"));
            for (int a = 0; a < 3; a++)
                Gore.NewGoreDirect(projectile.Center, Vector2.Zero, mod.GetGoreSlot("Gores/Flayke/Bauble/BaubleProjector" + Main.rand.Next(4)));
        }
    }
    public class SnowEssence : ModProjectile
    {
        int time;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Essence");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.alpha = 1200;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 30;
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.coldDamage = true;
        }
        public override void AI()
        {
            time++;
            if (time >= 30)
                projectile.Kill();
            int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 187, 0f, 0f, 100, Color.Yellow, 1f);
            Dust val = Main.dust[num];
            val = Main.dust[num];
            Dust val2 = val;
            val2.velocity *= 0.2f;
            Main.dust[num].noGravity = true;
        }
    }
    public class IceCube : ModProjectile
    {
        public int[] val = new int[3];
        readonly float[] rotation = new float[15] { 0, 24, 48, 72, 96, 130, 154, 178, 202, 226, 250, 274, 298, 322, 346 };
        bool a1;
        public override string Texture => "CastledsContent/NPCs/Flayke/Projectiles/IceQbe";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Cube");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.width = 48;
            projectile.height = 48;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.coldDamage = true;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            projectile.rotation = 0;
            projectile.velocity.Y += 0.075f;
        }
        public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R - projectile.alpha, lightColor.G - projectile.alpha, lightColor.B - projectile.alpha, lightColor.A - projectile.alpha);
        public override void Kill(int timeLeft)
        {
            if (!a1)
            {
                Main.projectile[val[1]].Kill();
                a1 = true;
            }
            for (int a = 0; a < 15; a++)
                Projectile.NewProjectile(projectile.Center, new Vector2(5, 0).RotatedBy(MathHelper.ToRadians(rotation[a])), ModContent.ProjectileType<IceShard>(), MiscUtilities.DL(25), 0, 255, 0f, 0f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
    public class BlizzardComet : ModProjectile
    {
        public float[] values = new float[5];
        public List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => Flayke.Directory + "Projectiles/NorthComet";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Northern Comet");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.width = 42;
            projectile.height = 42;
            projectile.ignoreWater = true;
            projectile.timeLeft = 540;
            projectile.tileCollide = false;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            if (values[4]++ > 5)
            {
                values[4] = 0;
                afterIMG.Add(new NorthAfterIMG(projectile.Center, projectile.rotation));
            }
            if (values[2]++ > 60)
            {
                values[2] = 0;
                Main.PlaySound(SoundID.Item9, projectile.Center);
                Projectile.NewProjectile(projectile.position, new Vector2(values[3] == -1 ? -1.25f : 1.25f, 3.25f), ModContent.ProjectileType<BlizzardStar>(), MiscUtilities.DL(25), 0, 255, 0f, 0f);
            }
            Vector3 col = new Vector3(Color.Wheat.R, Color.Wheat.G, Color.Wheat.B);
            col *= new Vector3(0.005f, 0.005f, 0.005f);
            Lighting.AddLight(projectile.Center, col);
            if (afterIMG.Count > 0)
            {
                for (int a = 0; a < afterIMG.Count; a++)
                {
                    if (afterIMG[a].timer >= 255)
                        afterIMG.Remove(afterIMG[a]);
                    afterIMG[a].timer += 5;
                }
            }
            if (values[1] < 1)
                values[1] = projectile.position.Y;
            values[0] += (float)(200 * Math.PI) / 1000;
            projectile.position.X += values[3] == -1 ? -3.75f : 3.75f;
            projectile.position.Y = values[1] + (float)(10 * Math.Sin(0.05 * values[0]));
            if (values[4]++ > 1000)
                projectile.Kill();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D star = ModContent.GetTexture(Flayke.Directory + "Projectiles/NorthCometBehind");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(star, n.coord - Main.screenPosition, new Rectangle(0, 0, star.Width, star.Height), n.GetColor(Color.Wheat), n.rotation, new Vector2(star.Width / 2, star.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return true;
        }
    }
    public class BlizzardStar : ModProjectile
    {
        int timer;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => Flayke.Directory + "Projectiles/NorthStar";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Northern Star");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 1f;
            projectile.width = 30;
            projectile.height = 78;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
            if (timer++ > 5)
            {
                timer = 0;
                afterIMG.Add(new NorthAfterIMG(projectile.Center, projectile.rotation));
            }
            Vector3 col = new Vector3(Color.Wheat.R, Color.Wheat.G, Color.Wheat.B);
            col *= new Vector3(0.005f, 0.005f, 0.005f);
            Lighting.AddLight(projectile.Center, col);
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
            Texture2D star = ModContent.GetTexture(Flayke.Directory + "Projectiles/NorthStarBehind");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(star, n.coord - Main.screenPosition, new Rectangle(0, 0, star.Width, star.Height), n.GetColor(Color.Wheat), n.rotation, new Vector2(star.Width / 2, star.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath3, projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i));
                Dust dust = Dust.NewDustPerfect(position, 222);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(dust.position - projectile.Center) * 8.75f;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }
        }
    }
    public class IceShard : ModProjectile
    {
        int timer;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => Flayke.Directory + "Projectiles/IceShard";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Shard");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 1f;
            projectile.width = 14;
            projectile.height = 24;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.coldDamage = true;
            projectile.friendly = false;
            projectile.hostile = true;
        }
        public override void AI()
        {
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D star = ModContent.GetTexture(Flayke.Directory + "Projectiles/IceShardBehind");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(star, n.coord - Main.screenPosition, new Rectangle(0, 0, star.Width, star.Height), n.GetColor(Color.CornflowerBlue), n.rotation, new Vector2(star.Width / 2, star.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item50, projectile.Center);
            for (int a = Main.rand.Next(4); a < 0; a++)
                Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, 80);
        }
    }
    public class Wind
    {
        public Vector2 coord;
        public int type;
        public float[] frameVal = new float[4];
        public float scale;
        public bool altWind;
        public bool flipped;
        public bool ended;
        int alpha;
        public readonly List<float> sizes = new List<float>
        {
            0.25f,
            0.5f,
            1,
            1.5f,
            2
        };
        public void Update()
        {
            if (alpha < 255)
                alpha += 5;
            if (frameVal[2] < 1)
            {
                frameVal[3] = type == 2 ? 7 : 10;
                frameVal[2] += GetTimerScale(scale);
            }
            if (frameVal[0]++ > (type == 2 ? 15 : 10))
            {
                frameVal[1]++;
                frameVal[0] = 0;
            }
            if (frameVal[1] > frameVal[3])
                ended = true;
        }
        public float GetTimerScale(float scale)
        {
            switch (scale)
            {
                case 0.5f:
                    return 2;
                case 0.75f:
                    return 1.5f;
                case 1:
                    return 1;
                case 2:
                    return 0.5f;
                case 3:
                    return 0.25f;
            }
            return 0;
        }
        public void SetSize() { scale = Main.rand.Next(sizes); }
        public Color AlphaColor()
        {
            int val = alpha;
            Color a = new Color(val, val, val, val);
            return a;
        }
        public Wind(Vector2 v, int t, bool fx, float sc) { coord = v; type = t; flipped = fx; scale = sc; }
    }
    public class Blast : ModProjectile
    {
        public Color[] lerpColor = new Color[2];
        public float increment;
        public float maxSize;
        float timer;
        public override string Texture => Flayke.Directory + "Projectiles/BlastEffect";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chilling Blast");
        }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = 0;
            projectile.scale = 1f;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            projectile.timeLeft = 600;
            timer += increment;
            if (timer >= maxSize)
                projectile.Kill();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            float scale = timer / maxSize;
            Color col = new Color(MiscUtilities.Round(MathHelper.Lerp(lerpColor[0].R, lerpColor[1].R, scale)), MiscUtilities.Round(MathHelper.Lerp(255, 0, scale)), MiscUtilities.Round(MathHelper.Lerp(lerpColor[0].B, lerpColor[1].B, scale)));
            Color drawColor = new Color(MiscUtilities.Round(MathHelper.Lerp(col.R, 0, scale)), MiscUtilities.Round(MathHelper.Lerp(col.G, 0, scale)), MiscUtilities.Round(MathHelper.Lerp(col.B, 0, scale)), MiscUtilities.Round(MathHelper.Lerp(col.A, 0, scale)));
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, 100, 100), drawColor, 0, new Vector2(50, 50), scale, SpriteEffects.None, 0);
            return false;
        }
    }
    internal class Cloud
    {
        public Vector2 coord;
        public int type;
        public float scale;
        public bool flipped;
        public float velocity;
        public bool ended;
        int[] vals = new int[4];
        public void UpdateVelocity()
        {
            if (vals[0] < 51)
                velocity *= 1.05f;
            if (vals[0] >= 51 && vals[0] < 70)
                velocity *= 0.9f;
            coord.X += velocity / 2;
        }
        public void GetColor()
        {
            vals[0]++;
            if (vals[0] >= 51)
            {
                vals[1] += vals[2] > 0 ? -5 : 5;
                if (vals[3] < 0 && vals[1] > 125)
                    vals[3]++;
                if (vals[3] > 0 && vals[1] < 1)
                    vals[3]--;
            }
            if (vals[0] < 51)
                vals[2] += 5;
            if (vals[0] > 240)
            {
                vals[2] -= 5;
                if (vals[0] > 292)
                    ended = true;
            }
        }
        public void CreateWind(List<Wind> wind)
        {
            Wind gust = new Wind(new Vector2(coord.X, coord.Y + 75), Main.rand.NextBool(2) ? 2 : 1, flipped, 0.75f);
            gust.altWind = true;
            wind.Add(gust);
        }
        public Color AlphaColor()
        {
            int val = vals[2];
            Color a = new Color(val, val, val, val);
            return a;
        }
        public Cloud(Vector2 c, int t, float s, bool f, float v) { coord = c; type = t; scale = s; flipped = f; velocity = v; }
    }
    internal class Borealis
    {
        public Vector2 coord;
        float posY;
        public bool flipped;
        public bool ended;
        bool executed;
        public bool front;
        public Color color;
        bool trackedY;
        int alpha = 255;
        double stuff;
        public void Update()
        {
            if (!executed)
            {
                if (!trackedY)
                {
                    posY = coord.Y;
                    trackedY = true;
                }
                if (alpha > 1)
                    alpha -= 5;
                stuff += 0.025;
                coord.Y = posY + (float)(30 * Math.Sin(stuff));
            }
        }
        public void Execute()
        {
            executed = true;
            alpha += 5;
            if (alpha >= 255)
                ended = true;
        }
        public Color AlphaColor()
        {
            int val = alpha;
            Color a = new Color(color.R - val, color.G - val, color.B - val, 255 - val);
            return a;
        }
        public Borealis(Vector2 v, Color c, bool f = false, bool fr = false) { coord = v; flipped = f; front = fr; color = c; }
    }
    public class Mist1 : ModDust
    {
        public override bool Update(Dust dust)
        {
            if (dust.customData is MistArray update)
            {
                dust.scale -= update.arr[1] / 100;
                update.arr[2] += update.arr[1];
                dust.rotation += update.arr[0] / 10;
                dust.position.Y += (update.arr[1] / 3) + 0.125f;
            }
            if (dust.scale < 0.01f)
                dust.active = false;
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            if (dust.customData is MistArray update)
                return new Color(lightColor.R - update.arr[2], lightColor.G - update.arr[2], lightColor.B - update.arr[2], lightColor.A - update.arr[2]);
            return base.GetAlpha(dust, lightColor);
        }
    }
    public class MistArray
    {
        public int[] arr;
    }
    public class FlaykeRemnant : ModProjectile
    {
        int timer;
        List<NorthAfterIMG> afterIMG = new List<NorthAfterIMG>();
        public override string Texture => Flayke.Directory + "Projectiles/Gore/FlaykeRemnant";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remnant of Flayke");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 1f;
            projectile.width = 8;
            projectile.height = 8;
            projectile.ignoreWater = true;
            projectile.timeLeft = 90;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.95f;
            projectile.velocity.Y *= 0.95f;
            if (timer++ > 5)
            {
                timer = 0;
                afterIMG.Add(new NorthAfterIMG(projectile.Center, projectile.rotation));
            }
            Lighting.AddLight(projectile.Center, new Vector3(0.5f, 0.5f, 0.5f));
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
            Texture2D frag = ModContent.GetTexture(Flayke.Directory + "Projectiles/Gore/FlaykeRemnant");
            if (afterIMG.Count > 0)
            {
                foreach (NorthAfterIMG n in afterIMG)
                    spriteBatch.Draw(frag, n.coord - Main.screenPosition, new Rectangle(0, 0, frag.Width, frag.Height), n.GetColor(Color.Wheat), n.rotation, new Vector2(frag.Width / 2, frag.Height / 2), 1 - (n.timer / 255), projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
            return true;
        }
    }
    public class FlaykeGore : ModProjectile
    {
        public int type;
        int timeLeft;
        public override string Texture => Flayke.Directory + "Projectiles/Gore/Gore1";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remnant of Flayke");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
            projectile.scale = 1f;
            projectile.width = 20;
            projectile.height = 20;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            float velX = Math.Abs(projectile.velocity.X) / 2;
            float velY = Math.Abs(projectile.velocity.Y) / 2;
            projectile.velocity.X *= 0.975f;
            projectile.velocity.Y *= 0.975f;
            projectile.rotation += velX + velY;
            if (timeLeft++ > 300)
                projectile.alpha++;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture(Flayke.Directory + "Projectiles/Gore/Gore" + type);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(lightColor.R - projectile.alpha, lightColor.G - projectile.alpha, lightColor.B - projectile.alpha, lightColor.A - projectile.alpha), projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1, projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }
    public class NorthAfterIMG
    {
        public float timer;
        public Vector2 coord;
        public float rotation;
        public NorthAfterIMG(Vector2 c, float r) { coord = c; rotation = r; }
        public Color GetColor(Color input)
        {
            float fluct = 1 - (timer / 255);
            Color transition = new Color(MiscUtilities.Round(MathHelper.Lerp(255, input.R, fluct)), MiscUtilities.Round(MathHelper.Lerp(255, input.G, fluct)), MiscUtilities.Round(MathHelper.Lerp(255, input.B, fluct)), MiscUtilities.Round(MathHelper.Lerp(255, input.A, fluct)));
            Color a = new Color(MiscUtilities.Round(MathHelper.Lerp(transition.R, 0, fluct)), MiscUtilities.Round(MathHelper.Lerp(transition.G, 0, fluct)), MiscUtilities.Round(MathHelper.Lerp(transition.B, 0, fluct)), MiscUtilities.Round(MathHelper.Lerp(transition.A, 0, fluct)));
            return a;
        }
    }
    public class CameraPan
    {
        public int whoIAm;
        public Vector2 panPos;
        public CameraPan(int w) { whoIAm = w; }
    }
    public class CameraPanPlayer : ModPlayer
    {
        public const int NPC = 1;
        public CameraPan panAction;
        public int currentAction;
        public Vector2 currentPanPos;
        public Entity entity;
        public float[] panVals = new float[3];
        public override void ModifyScreenPosition()
        {
            if (currentAction == 1)
            {
                panVals[0]++;
                if (panVals[0] <= 240)
                    Pan("Flayke", true);
                else
                {
                    currentAction = 0;
                    currentPanPos = Vector2.Zero;
                    entity = null;
                    for (int a = 0; a < panVals.Length; a++)
                        panVals[a] = 0;
                }
            }
            void Pan(string panType, bool entityLock = false)
            {
                if (!entityLock)
                    panAction.panPos = currentPanPos;
                else
                {
                    if (entity != null && entity.active)
                        panAction.panPos = entity.Center;
                }
                switch (panType)
                {
                    case "Flayke":
                        {
                            if (panVals[0] < 90 || panVals[0] >= 150)
                                panVals[1] += (float)Math.PI / 180;
                            panVals[2] = (float)Math.Sin(panVals[1]);
                            LerpCameraPan(panVals[2]);
                        }
                        break;
                }
                void LerpCameraPan(float lerpVal)
                {
                    if (Main.myPlayer == panAction.whoIAm)
                    {
                        Player player = Main.LocalPlayer;
                        if (player.active && player != null && !player.dead)
                            Main.screenPosition = new Vector2(Lerp(player.MountedCenter.X, panAction.panPos.X) - (Main.screenWidth / 2), Lerp(player.MountedCenter.Y, panAction.panPos.Y) - (Main.screenHeight / 2));
                        float Lerp(float f1, float f2) => MathHelper.Lerp(f1, f2, lerpVal);
                    }
                }
            }
        }
        public static void RegisterCameraPan(Player player, Entity val)
        {
            CameraPanPlayer modP = player.GetModPlayer<CameraPanPlayer>();
            modP.panAction = new CameraPan(player.whoAmI);
            modP.entity = val;
        }
    }
}

