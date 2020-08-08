using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace CastledsContent.NPCs.RobotInvasion.Tier1
{
    public class BladeBot : ModNPC
    {
        NPC orbit0;
        NPC orbit1;
        internal Player target;
        public int aiState = -1;
        public int blink = 0;
        public bool startedAttack = false;
        public bool hasLanded = false;
        public int motionBlurCounter = 0;
        public int attackDuration;
        public int chargeType = 1;
        public float knockbackMemory = 0.5f;
        public bool initialize = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade Drone");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 750;
            if (Main.expertMode)
            {
                npc.lifeMax = 1250;
            }
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 50;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (numPlayers > 1)
            {
                double num = 0.35;
                double num2 = 1.0 + num;
                for (int i = 2; i < numPlayers; i++)
                {
                    num += (1.0 - num) / 3.0;
                    num2 += num;
                }
                npc.lifeMax = (int)(1250.0 * num2);
                npc.damage = (int)(npc.damage * 1f);
            }
            else
            {
                npc.lifeMax = 1250;
                npc.damage = (int)(npc.damage * 0.75f);
            }
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < npc.lifeMax * 0.35)
            {
                Main.PlaySound(SoundID.NPCHit53);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Cyan, 3f);
            }
        }
        public override void NPCLoot()
        {
            if (NPC.AnyNPCs(mod.NPCType("CleanupShip_PH")) && CastledWorld.numberOfEnemies > 0)
            {
                Main.PlaySound(SoundID.Grab, (int)npc.position.X, (int)npc.position.Y, 17);
                CastledWorld.numberOfEnemies -= 10;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, npc.velocity.X * 0.01f, -2f, mod.ProjectileType("Counter"), 0, 0f, 255, 0f, 0f);
            }
        }
        public override void AI()
        {
            const float distanceFromOwner = 64;
            DespawnHandler();

            if (initialize == false)
            {
                orbit0 = Helpers.NewNPCDirect<ArgonBlade1_PH>(npc.Center, ai1: MathHelper.Pi, ai3: distanceFromOwner);
                //orbit0.position.X += distanceFromOwner; // one from one side
                if (orbit0.modNPC is ArgonBlade1_PH t)
                    t.owner = npc;
                orbit0.position = npc.position;
                //orbit0.AI();

                orbit1 = Helpers.NewNPCDirect<ArgonBlade1_PH>(npc.Center, ai3: distanceFromOwner);
                //orbit1.position.Y -= distanceFromOwner; // one from another side
                if (orbit1.modNPC is ArgonBlade1_PH t2)
                    t2.owner = npc;

                orbit1.position = npc.position;
                initialize = true;
            }
            if (aiState > 1)
            {
                npc.knockBackResist = 0f;
            }
            if (aiState == 1)
            {
                knockbackMemory = npc.knockBackResist;
            }
            if (npc.life < npc.lifeMax * 0.35)
            {
                if (Main.rand.Next(59) == 0)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, Color.Red, 3f);
                }
            }

            Timer++;
            npc.alpha = blink;
            if (hasLanded == false)
            {
                npc.dontTakeDamage = true;
                if (Timer < 60)
                {
                    motionBlurCounter++;
                    if (motionBlurCounter > 4)
                    {
                        motionBlurCounter = 0;
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                    }
                    npc.position.Y += 2;
                }
                if (Timer > 60)
                {
                    hasLanded = true;
                    aiState++;
                }
            }
            if (aiState == 0)
            {
                blink++;
                if (blink > 100)
                {
                    blink = 0;
                }
            }
            if (chargeType > 5)
            {
                chargeType = 1;
            }
            if (aiState == 0 && Timer > 200)
            {
                aiState = 1;
                Timer = 0;
                blink = 0;
            }
            {
                if (aiState == 1)
                {
                    npc.dontTakeDamage = false;
                    npc.damage = 30;
                    if (Timer < 300)
                    {
                        npc.velocity.X *= 1f;
                        npc.velocity.Y *= 1f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                        {
                            float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                            npc.velocity.X = (float)(Math.Cos(rotation) * 3.4) * -1;
                            npc.velocity.Y = (float)(Math.Sin(rotation) * 3.4) * -1;
                        }
                    }
                }
                if (Timer > 300)
                {
                    npc.damage = 0;
                    aiState = 2;
                    blink += 2;

                    if (aiState == 2)
                    {
                        npc.velocity.X *= 0f;
                        npc.velocity.Y *= 0f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                        {
                            float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                            npc.velocity.X = (float)(Math.Cos(rotation) * 0) * -1;
                            npc.velocity.Y = (float)(Math.Sin(rotation) * 0) * -1;
                        }

                        if (blink > 100)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("Identifier1"), 0, 0f, 255, 0f, 0f);
                            blink = 0;
                            aiState = 3;
                            Timer = 0;
                            chargeType++;
                        }
                    }
                }
                if (aiState == 3)
                {
                    if (chargeType == 1)
                    {
                        if (startedAttack == false)
                        {
                            blink++;
                        }
                        if (blink > 100)
                        {
                            blink = 0;
                            startedAttack = true;
                            Main.PlaySound(SoundID.MenuTick);
                        }
                        if (startedAttack == true)
                        {
                            Timer++;
                            if (Timer < 120)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 400;
                                npc.position.Y = Main.player[npc.target].position.Y;
                            }
                            if (Timer > 180)
                            {
                                npc.damage = 50;
                                motionBlurCounter++;
                                attackDuration++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                                }
                                npc.position.X -= 16;
                                if (attackDuration > 60)
                                {
                                    aiState = 1;
                                    Timer = 0;
                                    attackDuration = 0;
                                }
                            }
                        }
                    }
                    if (chargeType == 2)
                    {
                        if (startedAttack == false)
                        {
                            blink++;
                        }
                        if (blink > 100)
                        {
                            blink = 0;
                            startedAttack = true;
                            Main.PlaySound(SoundID.MenuTick);
                        }
                        if (startedAttack == true)
                        {
                            Timer++;
                            if (Timer < 120)
                            {
                                npc.position.X = Main.player[npc.target].position.X - 400;
                                npc.position.Y = Main.player[npc.target].position.Y;
                            }
                            if (Timer > 180)
                            {
                                npc.damage = 50;
                                motionBlurCounter++;
                                attackDuration++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                                }
                                npc.position.X += 16;
                                if (attackDuration > 60)
                                {
                                    aiState = 1;
                                    Timer = 0;
                                    attackDuration = 0;
                                }
                            }
                        }
                    }
                    if (chargeType == 3)
                    {
                        if (startedAttack == false)
                        {
                            blink++;
                        }
                        if (blink > 100)
                        {
                            blink = 0;
                            startedAttack = true;
                            Main.PlaySound(SoundID.MenuTick);
                        }
                        if (startedAttack == true)
                        {
                            Timer++;
                            if (Timer < 120)
                            {
                                npc.position.X = Main.player[npc.target].position.X - 250;
                                npc.position.Y = Main.player[npc.target].position.Y - 250;
                            }
                            if (Timer > 180)
                            {
                                npc.damage = 50;
                                motionBlurCounter++;
                                attackDuration++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                                }
                                npc.position.X += 8;
                                npc.position.Y += 8;
                                if (attackDuration > 60)
                                {
                                    aiState = 1;
                                    Timer = 0;
                                    attackDuration = 0;
                                }
                            }
                        }
                    }
                    if (chargeType == 4)
                    {
                        if (startedAttack == false)
                        {
                            blink++;
                        }
                        if (blink > 100)
                        {
                            blink = 0;
                            startedAttack = true;
                            Main.PlaySound(SoundID.MenuTick);
                        }
                        if (startedAttack == true)
                        {
                            Timer++;
                            if (Timer < 120)
                            {
                                npc.position.X = Main.player[npc.target].position.X;
                                npc.position.Y = Main.player[npc.target].position.Y - 300;
                            }
                            if (Timer > 180)
                            {
                                npc.damage = 50;
                                motionBlurCounter++;
                                attackDuration++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                                }
                                npc.position.Y += 8;
                                if (attackDuration > 60)
                                {
                                    aiState = 1;
                                    Timer = 0;
                                    attackDuration = 0;
                                }
                            }
                        }
                    }
                    if (chargeType == 5)
                    {
                        if (startedAttack == false)
                        {
                            blink++;
                        }
                        if (blink > 100)
                        {
                            blink = 0;
                            startedAttack = true;
                            Main.PlaySound(SoundID.MenuTick);
                        }
                        if (startedAttack == true)
                        {
                            Timer++;
                            if (Timer < 120)
                            {
                                npc.position.X = Main.player[npc.target].position.X + 250;
                                npc.position.Y = Main.player[npc.target].position.Y - 250;
                            }
                            if (Timer > 180)
                            {
                                npc.damage = 50;
                                motionBlurCounter++;
                                attackDuration++;
                                if (motionBlurCounter > 4)
                                {
                                    motionBlurCounter = 0;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 26f, npc.velocity.X * 0.01f, 0f, mod.ProjectileType("BladeBotMotionBlur"), 0, 0f, 255, 0f, 0f);
                                }
                                npc.position.X -= 8;
                                npc.position.Y += 8;
                                if (attackDuration > 60)
                                {
                                    aiState = 1;
                                    Timer = 0;
                                    attackDuration = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DespawnHandler()
        {
            if (!ValidOrFind())
            {
                npc.velocity.Y += 1;
            }
        }

        private bool ValidOrFind() => HasValidTarget(target) || TryFindTarget();

        internal static bool HasValidTarget(Player target) => target?.active == true && !(target.dead || target.ghost);

        private bool TryFindTarget()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player p = Main.player[i];
                if (HasValidTarget(p))
                {
                    target = p;
                    return true;
                }
            }
            return false;
        }
    }
}