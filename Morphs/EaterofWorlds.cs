using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Morphs
{
    [AutoloadEquip(EquipType.Balloon)]
    public class EaterofWorlds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater of Worlds");
            Tooltip.SetDefault("TEST ITEM: Transforms you into the Eater of Worlds");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.width = 30;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneCorrupt)
            {
                player.AddBuff(mod.BuffType("EaterofWorldsBuff"), 1);
            }
        }
    }
}