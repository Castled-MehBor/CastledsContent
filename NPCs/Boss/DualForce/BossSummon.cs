using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.NPCs.Boss.DualForce
{
    public class BossSummon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coalescated Spark");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            aiType = -1;
            npc.lifeMax = 50;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 16;
            npc.height = 16;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.dontTakeDamage = true;
        }
        public float Timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void AI()
        {
            if (CastledWorld.dualForceEncounter == 0)
            {
                Timer++;
                npc.position.Y = Main.player[npc.target].position.Y - (Timer / 2);
                npc.position.X = Main.player[npc.target].position.X;

                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = (int)(Timer / 4);
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                }
                if (Timer == 2)
                {
                    Main.NewText("[c/C3C479:A floating spark hovered above the Terrarian's head...]");
                }
                if (Timer == 180)
                {
                    Main.NewText("[c/C3C479:...and as the magic raised higher and higher...]");
                }
                if (Timer == 300)
                {
                    Main.NewText("[c/C3C479:...so did the adrenaline in the Terrarian's fragile body.]");
                }
                if (Timer == 420)
                {
                    Main.NewText("[c/C3C479:Until...]");
                }
                if (Timer == 540)
                {
                    Main.NewText("[c/66ff66:Two spirits of light and dark have been released!]");
                    npc.life = 0;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("LightMage"));
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("NightDemon"));
                }
            }
            if (CastledWorld.dualForceEncounter == 1 && !CastledWorld.downedDualForce == true)
            {
                Timer++;
                npc.position.Y = Main.player[npc.target].position.Y - (Timer / 2);
                npc.position.X = Main.player[npc.target].position.X;

                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = (int)(Timer / 4);
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                }
                if (Timer == 2)
                {
                    Main.NewText("[c/C3C479:A floating spark hovered above the Terrarian's head once again...]");
                }
                if (Timer == 180)
                {
                    Main.NewText("[c/C3C479:...and as the magic raised higher and higher...]");
                }
                if (Timer == 300)
                {
                    Main.NewText("[c/C3C479:...the Terrarian was still in awe.]");
                }
                if (Timer == 420)
                {
                    Main.NewText("[c/C3C479:Until...]");
                }
                if (Timer == 540)
                {
                    Main.NewText("[c/66ff66:Two spirits of light and dark have been released!]");
                    npc.life = 0;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("LightMage"));
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("NightDemon"));
                }
            }
            if (CastledWorld.dualForceEncounter == 2 && !CastledWorld.downedDualForce == true)
            {
                Timer++;
                npc.position.Y = Main.player[npc.target].position.Y - (Timer / 2);
                npc.position.X = Main.player[npc.target].position.X;

                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = (int)(Timer / 4);
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                }
                if (Timer == 2)
                {
                    Main.NewText("[c/C3C479:The Coalescated spark hovered above the Terrarian's head...]");
                }
                if (Timer == 180)
                {
                    Main.NewText("[c/C3C479:...and as the magic raised higher and higher...]");
                }
                if (Timer == 300)
                {
                    Main.NewText("[c/C3C479:...so did the Terrarian's impatience.]");
                }
                if (Timer == 420)
                {
                    Main.NewText("[c/C3C479:Until...]");
                }
                if (Timer == 540)
                {
                    Main.NewText("[c/66ff66:Two spirits of light and dark have been released!]");
                    npc.life = 0;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("LightMage"));
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("NightDemon"));
                }
            }
            if (CastledWorld.dualForceEncounter > 2 && !CastledWorld.downedDualForce == true)
            {
                Timer++;
                npc.position.Y = Main.player[npc.target].position.Y - (Timer);
                npc.position.X = Main.player[npc.target].position.X;

                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = (int)(Timer / 4);
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                }
                if (Timer == 180)
                {
                    Main.NewText("[c/66ff66:Two spirits of light and dark have been released!]");
                    npc.life = 0;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("LightMage"));
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("NightDemon"));
                }
            }
            else if (CastledWorld.downedDualForce)
            {
                Timer++;
                npc.position.Y = Main.player[npc.target].position.Y - (Timer);
                npc.position.X = Main.player[npc.target].position.X;

                Color color = new Color();
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                int count = (int)(Timer / 4);
                for (int i = 1; i <= count; i++)
                {
                    int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 21, 0, 0, 100, color, 0.5f);
                }
                if (Timer == 180)
                {
                    Main.NewText("[c/66ff66:Two spirits of light and dark have been released!]");
                    npc.life = 0;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("LightMage"));
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("NightDemon"));
                }
            }
        }
    }
}