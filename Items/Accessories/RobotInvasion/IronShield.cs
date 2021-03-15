using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    [AutoloadEquip(EquipType.Shield)]
    public class IronShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Shield");
            Tooltip.SetDefault("'You can't breath with a heart of iron.'"
            + "\n5 defense"
            + "\nReceiving more than 10% of your health in damage has a chance of activating the shield"
            + "\nWhile the shield is active, you will regenerate life significantly quicker and ignore knockback."
            + "\nThis effect lasts for 5 seconds"
            + "\nThis has a 15 second cooldown");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.width = 24;
            item.value = 37500;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ModContent.ItemType<SpikeExoskeleton.SpikeExoskeleton>() || player.armor[i].type == ModContent.ItemType<ReinforcedExoskeleton.ReinforcedExoskeleton>())
                {
                    return false;
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessoryInfo info = player.GetModPlayer<CastledPlayer>().info;
            info.active.Add("IronShield");
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
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
            recipe.AddIngredient(ModContent.ItemType<RobotPlate>(), 12);
            recipe.AddIngredient(ItemID.Obsidian, 5);
            recipe.AddRecipeGroup("CastledsContent:RoboGemGroup", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}