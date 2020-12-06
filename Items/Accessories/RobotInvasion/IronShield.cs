using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    public class IronShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Shield");
            Tooltip.SetDefault("'If this were to have been a component, we would all be doomed...'"
            + "\n5 defense"
            + "\nWhen you get hit, you have a chance of activating the shield"
            + "\nWhile the shield is active, you will regenerate significantly quicker and ignore knockback."
            + "\nThis has a 15 second cooldown");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.width = 40;
            item.value = 27500;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CastledPlayer>().ironShield = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ItemType<SpikeExoskeleton>() || player.armor[i].type == ItemType<ReinforcedExoskeleton>())
                {
                    return false;
                }
            }
            return true;
        }
        #region Robot Invasion Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(60, 60, 60);
                }
            }
            int num = -1;
            int num2 = 0;
            while (num2 < list.Count)
            {
                if (!list[num2].Name.Equals("ItemName"))
                {
                    num2++;
                    continue;
                }
                num = num2;
                break;
            }
            list.Insert(num + 1, new TooltipLine(mod, "RobotInvasionTag", "Robot Database"));
            foreach (TooltipLine item2 in list)
            {
                if (item2.mod == "CastledsContent" && item2.Name == "RobotInvasionTag")
                {
                    item2.overrideColor = new Color(90, 25, 0);
                }
            }
        }
        #endregion
    }
}