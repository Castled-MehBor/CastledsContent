using Terraria;
using System;
using Microsoft.Xna.Framework;

namespace CastledsContent.Utilities
{
    public struct MiscUtilities
    {
        public static void SpawnDropItem(Player player, int maxValue, int itemType, bool condition = true, int amount = 1)
        {
            if (Main.rand.Next(maxValue) == 0 && condition)
                player.QuickSpawnItem(itemType, amount);
        }
        public static int Round(float f) => (int)Math.Round(f);
        public static int LerpRound(float f1, float f2, float scale) => Round(MathHelper.Lerp(f1, f2, scale));
        /// <summary>
        /// Damage Literal
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int DL(int i)
        {
            float divident = Main.expertMode ? 0.4f : 0.5f;
            return (int)Math.Round(i * divident);
        }
        public static Color LerpColor(Color c1, Color c2, float lerp) => new Color(LerpRound(c1.R, c2.R, lerp), LerpRound(c1.G, c2.G, lerp), LerpRound(c1.B, c2.B, lerp), LerpRound(c1.A, c2.A, lerp));
    }
}
