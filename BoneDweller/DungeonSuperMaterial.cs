using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.BoneDweller
{
    public class DungeonSuperMaterial : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forgotten Dungeon Pieces");
            Tooltip.SetDefault("Once was an almighty staff wielded by the mage but alas, he has became one of THEM...");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 38;
            item.maxStack = 99;
            item.value = 25000;
            item.rare = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 6);
            recipe.AddIngredient(ItemID.AquaScepter);
            recipe.AddIngredient(ItemID.MagicMissile);
            recipe.AddIngredient(ItemID.WaterBolt);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(mod.ItemType("Hurricane"));
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 6);
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.WaterBucket, 3);
            recipe.AddIngredient(ItemID.WaterCandle, 3);
            recipe.AddIngredient(ItemID.Bone, 75);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(mod.ItemType("AquaEclipse"));
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 6);
            recipe.AddIngredient(ItemID.Handgun);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Bone, 75);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(mod.ItemType("LockSmith"));
            recipe.AddRecipe();
        }
    }
}
