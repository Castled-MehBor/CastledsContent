using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Material
{
    public class EpicQuartz : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal of Ascension");
            Tooltip.SetDefault("This very crystal holds the power of ascending already-existing weapons.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 999;
            item.value = 100000;
            item.rare = ItemRarityID.Yellow;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.BeamSword);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<Weapons.Melee.EnchantedSwordbutBetter>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeesKnees);
            recipe.AddIngredient(ItemID.BeeGun);
            recipe.AddIngredient(ItemID.Beenade, 50);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<Weapons.Ranged.QueenBee>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpaceGun);
            recipe.AddIngredient(ItemID.LaserRifle);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<Weapons.Magic.LaserTron>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 250);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<Weapons.Ranged.BayonettaKiller>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FetidBaghnakhs);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemType<Weapons.Melee.BruhMomento>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DiamondStaff);
            recipe.AddIngredient(ItemID.FragmentNebula, 25);
            recipe.AddIngredient(ItemID.FragmentVortex, 25);
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(ItemType<Weapons.Magic.ShinyStaff>());
            recipe.AddRecipe();
        }
    }
}
