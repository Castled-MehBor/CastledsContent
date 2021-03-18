using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Accessories
{
    public class HarpyQueenCirclet : ModItem
    {
        public int spaceBoost;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Queen's Circlet");
            Tooltip.SetDefault("2 defense"
            + "\n6% increased damage"
            + "\nImmunity to fall damage"
            + "\nIncreased jump speed"
            + "\nAll above effects are doubled in space"
            + "\nYour attacks have a chance to spawn feathers from the sky"
            + "\nToggle visibility to toggle feathers");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.width = 18;
            item.value = 17500;
            item.expert = true;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ModContent.ItemType<Suggested.DarkCrown>())
                {
                    return false;
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneSkyHeight)
            {
                spaceBoost = 2;
            }
            else
            {
                spaceBoost = 1;
            }
            if (!hideVisual)
            {
                player.GetModPlayer<CastledPlayer>().harpyCrown = true;
            }
            player.statDefense += 2 * spaceBoost;
            player.allDamage += 0.06f * spaceBoost;
            player.noFallDmg = true;
            player.jumpSpeedBoost += 1.25f * spaceBoost;
            player.wingTimeMax += 50 * spaceBoost;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];
            int num = -1;
            int num2 = 0;
            while (num2 < tooltips.Count)
            {
                if (!tooltips[num2].Name.Equals("ItemName"))
                {
                    num2++;
                    continue;
                }
                num = num2;
                break;
            }
            tooltips.Insert(num + 1, new TooltipLine(mod, "CircletGenderTooltip", player.Male ? "You're the king of the skies now!" : "You're the queen of the skies now!"));
        }
    }
}