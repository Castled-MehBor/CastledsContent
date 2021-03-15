using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.RobotInvasion.ReinforcedExoskeleton
{
    public class ReinforcedExoskeleton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Exoskeleton");
            Tooltip.SetDefault("'Your tinkering skills have been put to good use!'"
            + "\n8 defense"
            + "\n10% increased damage reduction"
            + "\nPartial Thorns effect"
            + "\nAfter not being hit for a minute (30 seconds if a boss is alive), you will active a shield program"
            + "\nThis effect only activates upon taking more than 10% of your health in damage"
            + "\nWhile this program is active, you will regenerate life significantly quicker and become impervious"
            + "\nThis effect lasts for 5 seconds"
            + $"\nDuring hardmode, Arrow-Magnetic Nanobits [i/s1:{ModContent.ItemType<AimbotArrow>()}] will work in your inventory while this is equipped");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 36;
            item.value = 112500;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ModContent.ItemType<SpikeExoskeleton.SpikeExoskeleton>() || player.armor[i].type == ModContent.ItemType<IronShield>())
                {
                    return false;
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessoryInfo info = player.GetModPlayer<CastledPlayer>().info;
            info.active.Add("ReinforcedExoskeleton");
            info.visual[1] = !hideVisual;
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.AddIngredient(ModContent.ItemType<IronShield>());
            recipe.AddIngredient(ModContent.ItemType<RobotPlate>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SpikeExoskeleton.SpikeExoskeleton>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}