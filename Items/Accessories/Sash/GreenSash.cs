using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.Sash
{
    [AutoloadEquip(EquipType.Waist)]
    public class GreenSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Sash");
            Tooltip.SetDefault("'An enchanted sash'"
            + "\n2 defense"
            + "\n6% increased ranged critical strike chance"
            + "\n10% increased ranged damage");
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
            player.rangedCrit += 6;
            player.rangedDamage += 0.10f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(0, 255, 150);
                }
            }
        }
    }
}