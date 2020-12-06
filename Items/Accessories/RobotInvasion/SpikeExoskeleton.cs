using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    public class SpikeExoskeleton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sturdy Exoskeleton");
            Tooltip.SetDefault("'The incredibly resilient plating of an unfinished creation'"
            + "\n3 defense"
            + "\n2% increased damage reduction"
            + "\nPowerful Thorns effect");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 26;
            item.value = 27500;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CastledPlayer p = player.GetModPlayer<CastledPlayer>();
            p.spikeExo = true;

            p.ExoAccessory = true;
            if (hideVisual)
            {
                p.ExoHideVanity = true;
            }
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ItemType<IronShield>() || player.armor[i].type == ItemType<ReinforcedExoskeleton>())
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

        public class ExoStrap : EquipTexture
        {
            public override bool DrawBody()
            {
                return false;
            }
        }
    }
}