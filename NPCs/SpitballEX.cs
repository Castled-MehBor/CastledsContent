using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs
{
    class SpitballEX : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.VileSpit;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Spit");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.VileSpit];
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.VileSpit);
            aiType = NPCID.VileSpit;
            animationType = NPCID.VileSpit;
            npc.dontTakeDamage = true;
        }
    }
}