using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.Sash
{
    [AutoloadEquip(EquipType.Waist)]
    public class YellowSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yellow Sash");
            Tooltip.SetDefault("'An enchanted sash'"
            + "\n2 defense"
            + "\nIncreased maximum minions by 1"
            + "\n10% increased minion knockback"
            + "\n6% increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.width = 24;
            item.value = 7500;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.maxMinions += 1;
            player.minionKB += 0.10f;
            player.minionDamage += 0.06f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(255, 190, 0);
                }
            }
        }
    }
}