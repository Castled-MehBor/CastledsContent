using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs
{
    public class NpcLootMothron : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Mothron)
            {
                if (Main.rand.Next(2) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("UsedHeroMagazine"), 1);
                    }
                }
            }
        }
    }
}