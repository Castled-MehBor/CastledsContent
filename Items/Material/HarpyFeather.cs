using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Material
{
	public class HarpyFeather : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glazed Feather");
			Tooltip.SetDefault("The delecate yet sharp remain of a sapian creature."
			+ "\nIt's texture is invaluable in crafting clothing.");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 24;
			item.maxStack = 99;
			item.value = 7500;
			item.rare = ItemRarityID.Orange;
		}
        public override void AddRecipes()
        {
            #region Vanity
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.AncientCloth, 5);
			recipe.AddRecipe();
			#region Familiar Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.FamiliarWig);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.FamiliarShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.FamiliarPants);
			recipe.AddRecipe();
			#endregion
            #region Archaeologist Set
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Leather);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ArchaeologistsHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Leather);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ArchaeologistsJacket);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Leather);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ArchaeologistsPants);
			recipe.AddRecipe();
            #endregion
            #region Bee Set
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk);
			recipe.AddIngredient(ItemID.BeeWax, 4);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BeeHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk);
			recipe.AddIngredient(ItemID.BeeWax, 4);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BeeShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk);
			recipe.AddIngredient(ItemID.BeeWax, 4);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BeePants);
			recipe.AddRecipe();
			#endregion
			#region Buccaneer Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BuccaneerBandana);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BuccaneerShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BuccaneerPants);
			recipe.AddRecipe();
			#endregion
			#region Sailor Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.SailorHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.SailorShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Sail, 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.SailorPants);
			recipe.AddRecipe();
			#endregion
			#region Mummy Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.MummyMask);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.MummyShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.MummyPants);
			recipe.AddRecipe();
			#endregion
			#region Scarecrow Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Hay, 15);
			recipe.AddIngredient(ItemID.SpookyWood, 25);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ScarecrowHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Hay, 15);
			recipe.AddIngredient(ItemID.SpookyWood, 25);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ScarecrowShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Hay, 15);
			recipe.AddIngredient(ItemID.SpookyWood, 25);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ScarecrowPants);
			recipe.AddRecipe();
			#endregion
			#region Elf Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.Ectoplasm);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ElfHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.Ectoplasm);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ElfShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddIngredient(ItemID.Ectoplasm);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.ElfPants);
			recipe.AddRecipe();
			#endregion
			#region Rain Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddTile(TileID.Loom);
			recipe.needWater = true;
			recipe.SetResult(ItemID.RainHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddTile(TileID.Loom);
			recipe.needWater = true;
			recipe.SetResult(ItemID.RainCoat);
			recipe.AddRecipe();
			#endregion
			#region Lamia Set
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 4);
			recipe.AddIngredient(ItemID.FossilOre, 35);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.LamiaHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 4);
			recipe.AddIngredient(ItemID.FossilOre, 35);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.LamiaShirt);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.AncientCloth, 4);
			recipe.AddIngredient(ItemID.FossilOre, 35);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.LamiaPants);
			recipe.AddRecipe();
			#endregion

			#region Single Pieces
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Bunny, 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BunnyHood);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 3);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.Kimono);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 3);
			recipe.AddIngredient(ItemID.Goldfish);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.RobotHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 1);
			recipe.AddIngredient(ItemID.Silk, 4);
			recipe.AddTile(TileID.Loom);
			recipe.needWater = true;
			recipe.SetResult(ItemID.UmbrellaHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 5);
			recipe.AddIngredient(ItemID.Pumpkin, 75);
			recipe.AddIngredient(ItemID.LivingFireBlock, 25);
			recipe.AddIngredient(ItemID.SpookyWood, 100);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.UmbrellaHat);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 2);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.GiantBow);
			recipe.AddRecipe();
			#endregion
			#endregion
			#region Potion
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(ItemID.GravitationPotion, 3);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(ItemID.FeatherfallPotion, 3);
			recipe.AddRecipe();
			#endregion
			#region Wing Materials
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 8);
			recipe.AddIngredient(ItemID.Feather, 20);
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(ItemID.GiantHarpyFeather);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 8);
			recipe.AddIngredient(ItemID.SoulofFright, 10);
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(ItemID.FireFeather);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 8);
			recipe.AddIngredient(ItemID.FrostCore);
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(ItemID.IceFeather);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 8);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ItemID.Ectoplasm, 5);
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(ItemID.BoneFeather);
			recipe.AddRecipe();
			#endregion
            #region Thread
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Cobweb, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.BlackThread, 3);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Cobweb, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.PinkThread, 3);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.Cobweb, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(ItemID.GreenThread, 3);
			recipe.AddRecipe();
			#endregion
		}
    }
}
