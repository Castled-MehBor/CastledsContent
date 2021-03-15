using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CastledsContent.Items.Accessories.RobotInvasion
{
    [AutoloadEquip(EquipType.Waist)]
    public class AimbotArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arrow-Magnetic Nanobits");
            Tooltip.SetDefault("'Don't mistake them for breath mints'"
            + "\nAll friendly wooden arrows will home in towards targets"
            + "\nThis effect is disabled if any of the following items are in your inventory:"
            + $"\n[i/s1:{ItemID.DaedalusStormbow}] [i/s1:{ItemID.Tsunami}] [i/s1:{ItemID.ChlorophyteShotbow}] [i/s1:{ItemID.Phantasm}]");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.width = 34;
            item.value = 75000;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!RestrictAimbot(player))
            {
                player.GetModPlayer<CastledPlayer>().aimBot = true;
            }
        }

        /*public override void UpdateInventory(Player player)
        {
            AccessoryInfo info = player.GetModPlayer<CastledPlayer>().info;
            if (!RestrictAimbot(player) && info.active.Contains("ReinforcedExoskeleton") && Main.hardMode)
                player.GetModPlayer<CastledPlayer>().aimBot = true;
        }*/
        bool RestrictAimbot(Player player)
        {
            List<int> restrict = new List<int>
            {
                ItemID.DaedalusStormbow,
                ItemID.Tsunami,
                ItemID.ChlorophyteShotbow,
                ItemID.Phantasm
            };
            foreach(Item item in player.inventory)
            {
                if (restrict.Contains(item.type))
                    return true;
            }
            return false;
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
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
            recipe.AddIngredient(ModContent.ItemType<RobotPlate>(), 8);
            recipe.AddIngredient(ItemID.BeeWax, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}