using CastledsContent.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable.Morphs
{
    [AutoloadEquip(EquipType.Wings)]
    public class EyeofCthulhu : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Cthulhu");
            Tooltip.SetDefault("TEST ITEM: Transforms you into the Eye of Cthulhu");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.width = 28;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Main.dayTime)
            {
                player.AddBuff(BuffType<EyeofCthulhuBuff>(), 1);
                player.wingTimeMax = 9999;
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (!Main.dayTime)
            {
                ascentWhenFalling = 0.08f;
                ascentWhenRising = 0.03f;
                maxCanAscendMultiplier = 1f;
                maxAscentMultiplier = 1f;
                constantAscend = 0.135f;
            }
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (!Main.dayTime)
            {
                speed = 5f;
                acceleration = 12f;
            }
        }
    }
}