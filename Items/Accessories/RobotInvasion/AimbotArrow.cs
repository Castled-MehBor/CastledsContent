using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    public class AimbotArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auto Aim Nanobots");
            Tooltip.SetDefault("'Curious magnet-like devices that direct projectiles to a target'"
            + "\nAll friendly wooden arrows will home in towards targets"
            + "\nYou cannot have this effect if any of the following items are in your inventory:"
            + $"\n[i/s1:{ItemID.DaedalusStormbow}] [i/s1:{ItemID.Tsunami}] [i/s1:{ItemID.ChlorophyteShotbow}] [i/s1:{ItemID.Phantasm}]");
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
            if (!player.GetModPlayer<CastledPlayer>().restrictAimbot)
            {
                player.GetModPlayer<CastledPlayer>().aimBot = true;
            }
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
                    item2.overrideColor = new Color(4, 60 + Main.DiscoB / 2, 35);
                }
            }
        }
        #endregion
    }
}