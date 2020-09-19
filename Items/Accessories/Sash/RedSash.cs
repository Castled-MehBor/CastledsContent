using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.Sash
{
    [AutoloadEquip(EquipType.Waist)]
    public class RedSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Sash");
            Tooltip.SetDefault("'An enchanted sash'"
            + "\n3 defense"
            + "\n15% increased melee speed"
            + "\n4% increased melee critical strike chance"
            + "\n8% increased melee damage");
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
            player.statDefense += 3;
            player.meleeSpeed += 0.15f;
            player.meleeCrit += 4;
            player.meleeDamage += 0.08f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(255, 0, 45);
                }
            }
        }
    }
}