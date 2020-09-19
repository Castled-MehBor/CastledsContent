using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.Sash
{
    [AutoloadEquip(EquipType.Waist)]
    public class BlueSash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Sash");
            Tooltip.SetDefault("'An enchanted sash'"
            + "\n2 defense"
            + "\n40 increased max mana"
            + "\nReduced mana sickness and regen delay"
            + "\n4% increased magic critical strike chance"
            + "\n8% increased magic damage");
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
            player.statManaMax2 += 40;
            player.manaSickReduction -= 0.15f;
            player.manaRegenDelay -= 5;
            player.magicCrit += 4;
            player.magicDamage += 0.08f;
            player.statDefense += 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(70, 0, 255);
                }
            }
        }
    }
}