using Terraria;

namespace CastledsContent.Utilities
{
    public static class MiscUtilities
    {
        public static void SpawnDropItem(Player player, int maxValue, int itemType, bool condition = true, int amount = 1)
        {
            if (Main.rand.Next(maxValue) == 0 && condition)
                player.QuickSpawnItem(itemType, amount);
        }
    }
}
