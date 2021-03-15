using System;
using Terraria;

namespace CastledsContent.Items.Weapons
{
    public class DualForceBuff
    {
        public bool evil;
        public DualForceBuff(bool evil1)
        {
            evil = evil1;
        }
        public int BuffInt(int value, Player player, int type)
        {
            int buff = value;
            if (type == 1)
            {
                if (Main.hardMode)
                    buff += 15;
                if (evil && (player.ZoneCorrupt || player.ZoneCrimson))
                    buff += 10;
                if (!evil && (player.ZoneHoly))
                    buff += 10;
            }
            if (type == 2)
            {
                if (Main.hardMode)
                    buff -= 6;
                if (evil && (player.ZoneCorrupt || player.ZoneCrimson))
                    buff -= 4;
                if (!evil && (player.ZoneHoly))
                    buff -= 4;
            }
            return buff;
        }
        public float BuffFloat(float value, Player player)
        {
            float buff = value;
            if (Main.hardMode)
                buff += 2;
            if (evil && (player.ZoneCorrupt || player.ZoneCrimson))
                buff += 1;
            if (!evil && (player.ZoneHoly))
                buff += 1;
            return buff;
        }
    }
}
