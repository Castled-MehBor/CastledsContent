using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories.Sash
{
    [AutoloadEquip(EquipType.Waist)]
    public class GraySash : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sash");
            Tooltip.SetDefault("'With no color, comes a lack of power'"
            + "\nCan be dyed to provide power");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.width = 24;
            item.value = 7500;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.vanity = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(50, 50, 50);
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 12);
            recipe.AddIngredient(ItemType<Material.HarpyFeather>(), 6);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.RedDye);
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(ItemType<RedSash>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.GreenDye);
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(ItemType<GreenSash>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.BlueDye);
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(ItemType<BlueSash>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.YellowDye);
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(ItemType<YellowSash>());
            recipe.AddRecipe();
        }
    }
}