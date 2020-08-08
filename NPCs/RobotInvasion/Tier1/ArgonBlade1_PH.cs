using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CastledsContent.NPCs;
using System.Threading;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class ArgonBlade1_PH : ModNPC
    {
        internal NPC owner;
        public float spinSpeed = 2.12f;
        public int aiState = 0;
        public int motionBlurCounter = 0;
        public int respawnCounter = 0;
        public int attackDuration;
        public int chargeSpin = 1;
        public int miscCounter;
        public int bladeState = 1;
        public bool startedCycle = false;
        public bool isActive = false;
        public bool initialize = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade Emulation");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.scale = 1f;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.Item6;
            npc.HitSound = SoundID.DD2_LightningAuraZap;
            npc.width = 30;
            npc.height = 30;
            npc.boss = false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.5f);
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }

        public override void AI()
        {
            Timer++;
            // ai 0 = progress
            // ai 1 = offset
            // ai 2 = attack state (positive is towards the player, negative is returning)
            // ai 3 = distance
            if (owner?.active != true || !(owner.modNPC is BladeBot))
            {
                npc.active = false;
            }

            const float returntime = 60f;
            npc.ai[0] += spinSpeed;
            if (npc.ai[2] > 0)
            {
                npc.ai[2]--;
                //npc.velocity *= 0.97f; // slowdown
                if (npc.ai[2] == 0)
                {
                    npc.ai[2] = -returntime;
                }
                return;
            }

            BladeBot ownr = (BladeBot)owner.modNPC;
            Vector2 ownercenter = owner.Center;
            float val = npc.ai[0] + npc.ai[1];
            Vector2 targetpos = new Vector2()
            {
                X = ownercenter.X + (float)Math.Sin(val) * npc.ai[3] - 16,
                Y = ownercenter.Y + (float)Math.Cos(val) * npc.ai[3] - 12
            };
            if (npc.ai[2] < 0)
            {
                npc.ai[2]++;
                float p = (1 / returntime) * Math.Abs(npc.ai[2]);
                npc.position = Vector2.Lerp(npc.Center, targetpos, 1 - p);
                return;
            }
            else
                npc.position = targetpos;

            bool validtarget = BladeBot.HasValidTarget(ownr.target);
            if (initialize == false && Timer < 200)
            {
                isActive = false;
            }
            if (initialize == false && Timer > 200)
            {
                initialize = true;
                isActive = true;
            }
            //If "alive"
            if (isActive == true)
            {
                if (initialize == true && Timer > 2030 && startedCycle == false)
                {
                    spinSpeed = 1.95f;
                    motionBlurCounter++;
                    npc.damage = 50;
                    if (motionBlurCounter > 4)
                    {
                        motionBlurCounter = 0;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("ArgonMotionBlur"), 0, 0f, 255, 0f, 0f);
                    }
                }
                if (initialize == true && Timer > 2300 && startedCycle == false)
                {
                    spinSpeed = 2.12f;
                    startedCycle = true;
                    Timer = 0;
                }
            }
            //If "dead"
            else if (isActive == false)
            {
                if (initialize == true && Timer > 2030 && startedCycle == false)
                {
                    spinSpeed = 1.95f;
                    motionBlurCounter++;
                    if (motionBlurCounter > 4)
                    {
                        motionBlurCounter = 0;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("ArgonMotionBlurDead"), 0, 0f, 255, 0f, 0f);
                    }
                }
                if (initialize == true && Timer > 2300 && startedCycle == false)
                {
                    spinSpeed = 2.12f;
                    startedCycle = true;
                    Timer = 0;
                }
            }
            if (initialize == true && Timer > 2300 && startedCycle == false)
            {
                spinSpeed = 2.12f;
                startedCycle = true;
                Timer = 0;
            }
            //Permanent cycle after first special spin
            //if alive
            if (isActive == true)
            {
                if (Timer > 1250 && startedCycle == true)
                {
                    spinSpeed = 1.95f;
                    motionBlurCounter++;
                    npc.damage = 60;
                    if (motionBlurCounter > 4)
                    {
                        motionBlurCounter = 0;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("ArgonMotionBlur"), 0, 0f, 255, 0f, 0f);
                    }
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 60, 0f, 0f, 100, Color.Red, 1.25f);
                }
                if (Timer > 1550 && startedCycle == true)
                {
                    spinSpeed = 2.12f;
                    Timer = 0;
                }
            }
            //if dead
            else if (isActive == false)
            {
                if (Timer > 1250 && startedCycle == true)
                {
                    spinSpeed = 1.95f;
                    motionBlurCounter++;
                    if (motionBlurCounter > 4)
                    {
                        motionBlurCounter = 0;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("ArgonMotionBlurDead"), 0, 0f, 255, 0f, 0f);
                    }
                }
                if (Timer > 1550 && startedCycle == true)
                {
                    spinSpeed = 2.12f;
                    Timer = 0;
                }
            }
            if (npc.life < 1)
            {
                npc.life = 0;
                npc.checkDead();
            }
            if (isActive == false)
            {
                npc.alpha = 100;
                respawnCounter++;
                npc.color = Color.White;
                npc.dontTakeDamage = true;
                npc.damage = 0;

                if (respawnCounter > 800)
                {
                    respawnCounter = 0;
                    isActive = true;
                    Main.PlaySound(SoundID.Item67);
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 60, 0f, 0f, 100, Color.Red, 3f);
                }
            }
            if (isActive == true)
            {
                npc.dontTakeDamage = false;
                npc.color = Color.Red;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool CheckDead()
        {
            Main.PlaySound(SoundID.NPCHit53);
            float Speed = 0f;
            Vector2 vector8 = npc.BottomRight;
            int damage = 10;
            int type = mod.ProjectileType("GammaPlode");
            float rotation = npc.rotation;
            int num54 = Projectile.NewProjectile(vector8.X, vector8.Y + 18, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            npc.ai[1] = 0;

            npc.life = npc.lifeMax;
            isActive = false;
            return false;
        }
    }
}