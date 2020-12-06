using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    public class ReinforcedExoskeleton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Exoskeleton");
            Tooltip.SetDefault("'You almost created a sentient droid...'"
            + "\n8 defense"
            + "\n2% increased damage reduction"
            + "\nPowerful Thorns effect"
            + "\nWhen you get hit, you have a chance of activating the shield program"
            + "\nWhile this program is active, you will regenerate significantly quicker and ignore knockback."
            + "\nThis has a 15 second cooldown");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.width = 26;
            item.value = 27500;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CastledPlayer p = player.GetModPlayer<CastledPlayer>();
            p.spikeExo = true;
            p.ironShield = true;

            p.ZenoAccessory = true;
            if (hideVisual)
            {
                p.ZenoHideVanity = true;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ItemType<SpikeExoskeleton>() || player.armor[i].type == ItemType<IronShield>())
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
                    item2.overrideColor = new Color(125, 35 + Main.DiscoR / 2, 50);
                }
            }
        }
        #endregion
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 3);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.Spike, 5);
            recipe.AddIngredient(ItemType<IronShield>());
            recipe.AddIngredient(ItemType<SpikeExoskeleton>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}