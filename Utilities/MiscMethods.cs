using Terraria;
using System;

namespace CastledsContent.Utilities
{
    public static class MiscUtilities
    {
        public static void SpawnDropItem(Player player, int maxValue, int itemType, bool condition = true, int amount = 1)
        {
            if (Main.rand.Next(maxValue) == 0 && condition)
                player.QuickSpawnItem(itemType, amount);
        }
        /*
        public static bool RestrictAimBot(Player player, Item item, int itemType, bool doReturn)
        {
            if (item.type == itemType)
                player.GetModPlayer<CastledPlayer>().aimBot = doReturn;
            return doReturn;
        }*/
    }
}
