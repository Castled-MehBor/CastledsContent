using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs
{
	class CursedPickaxe : ModNPC
	{
		public override string Texture => "Terraria/NPC_" + NPCID.CursedSkull;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ye' old Cursed Skull");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.CursedSkull];
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CursedSkull);
			aiType = NPCID.CursedSkull;
			animationType = NPCID.CursedSkull;
        }
	}
}