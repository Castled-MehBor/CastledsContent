using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.NPCs
{
    public class StrangeAircraftEmulate : ModNPC
    {
        public int radar = 0;
        public bool hasLanded = false;
        public bool setup = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Emulation");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 50;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 98;
            npc.height = 120;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 20f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.dontTakeDamage = true;
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void AI()
        {
            npc.spriteDirection = 1;

            if (setup == false)
            {
                Timer++;
            }
            if (hasLanded == false)
            {
                if (Timer < 60)
                {
                    npc.position.Y = Main.player[npc.target].position.Y - 900;
                }
                else if (Timer > 0 && Timer < 120)
                {
                    npc.position.Y += 10;
                }
                else if (Timer > 120 && Timer < 150)
                {
                    npc.position.Y += 8;
                }
                else if (Timer > 150 && Timer < 180)
                {
                    npc.position.Y -= 8;
                }
                else
                {
                    hasLanded = true;
                    Timer = 0;
                }
            }
            if (hasLanded == true && setup == false)
            {
                if (Timer == 60 && Timer < 300)
                {
                    CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Establishing connection to [REDACTED]...", false, false);
                }
                else if (Timer == 300 && Timer < 900)
                {
                    CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Sending NET packet...", false, false);
                }
                else if (Timer == 900 && Timer < 1200)
                {
                    setup = true;
                    CombatText.NewText(base.npc.getRect(), Color.SpringGreen, "Relay Startup Initiating...", false, false);
                    Main.NewText("[c/af4bff:Wave 1: uhh]");
                    Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("RobotRadar"), 0, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCap1"), 0, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 85f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCap2"), 0, 0f, 255, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 115f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCapT"), 0, 0f, 255, 0f, 0f);
                    Timer = 0;
                }
            }
        }
        //Standard Frames
        private const int Frame_1 = 0;
        private const int Frame_2 = 1;
        private const int Frame_3 = 2;
        private const int Frame_4 = 3;
        private const int Frame_5 = 4;
        private const int Frame_6 = 5;
        private const int Frame_7 = 6;
        private const int Frame_8 = 7;
        //Transformation Frames
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 4)
            {
                npc.frame.Y = Frame_1 * frameHeight;
            }
            else if (npc.frameCounter < 8)
            {
                npc.frame.Y = Frame_2 * frameHeight;
            }
            else if (npc.frameCounter < 12)
            {
                npc.frame.Y = Frame_3 * frameHeight;
            }
            else if (npc.frameCounter < 16)
            {
                npc.frame.Y = Frame_4 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = Frame_5 * frameHeight;
            }
            else if (npc.frameCounter < 24)
            {
                npc.frame.Y = Frame_6 * frameHeight;
            }
            else if (npc.frameCounter < 28)
            {
                npc.frame.Y = Frame_7 * frameHeight;
            }
            else if (npc.frameCounter < 32)
            {
                npc.frame.Y = Frame_8 * frameHeight;
            }
            else
            {
                if (setup == true)
                {
                    radar++;
                    if (radar > 6)
                    {
                        radar = 0;
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("RobotRadar"), 0, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 55f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCap1"), 0, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 85f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCap2"), 0, 0f, 255, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X - 240, npc.Center.Y - 115f, npc.velocity.X * 0, 0f, mod.ProjectileType("DeathCapT"), 0, 0f, 255, 0f, 0f);
                    }
                    npc.frameCounter = 0;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
    }
}