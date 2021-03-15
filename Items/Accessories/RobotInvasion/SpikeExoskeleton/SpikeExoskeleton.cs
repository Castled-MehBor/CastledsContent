using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.RobotInvasion.SpikeExoskeleton
{
    public class SpikeExoskeleton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Simple Exoskeleton");
            Tooltip.SetDefault("'A simple, but effective outer covering, suitable for humans.'"
            + "\n3 defense"
            + "\n2% increased damage reduction"
            + "\nPartial Thorns effect");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.value = 37500;
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ModContent.ItemType<IronShield>() || player.armor[i].type == ModContent.ItemType<ReinforcedExoskeleton.ReinforcedExoskeleton>())
                {
                    return false;
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessoryInfo info = player.GetModPlayer<CastledPlayer>().info;
            info.active.Add("SpikeExoskeleton");
            info.visual[0] = !hideVisual;
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
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
            recipe.AddIngredient(ModContent.ItemType<RobotPlate>(), 10);
            recipe.AddIngredient(ItemID.Obsidian, 8);
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public class ExoStrap : EquipTexture
        {
            public override bool DrawBody()
            {
                return false;
            }
        }
    }
}