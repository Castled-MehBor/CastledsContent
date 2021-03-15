using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    public class RobotPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Robotic Plating");
            Tooltip.SetDefault("'The leftovers of robot testing grounds'\nUsed to craft various robot-related items");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 14500;
            item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
        }
        #region Robot Invasion Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(215, 135, 95);
                }
            }
        }
        #endregion
    }
}
