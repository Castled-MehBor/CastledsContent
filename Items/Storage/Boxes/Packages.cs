using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System;

namespace CastledsContent.Items.Storage.Boxes
{
    #region Test Package
    public class TestBox : BagItem
    {
        public override int BagLimit { get => 10; }
        public override bool MagicPickup { get => false; }
        public override bool IsPackage { get => true; }
        public override string Texture => "CastledsContent/Items/Storage/Boxes/TestBox";
        public override void SetStaticDefaults() { DisplayName.SetDefault("Test Box"); }
        public override void SetDefaults()
        {
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<TestBoxTile>();
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
		}
    }
	internal class TestBoxTile : Package
	{
		public override int PackageType { get { return PackageData.Test; } }
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
		}
	}
    #endregion
    #region Tiny Package
    public class PackageTiny : BagItem
	{
		public override int BagLimit { get => 5; }
		public override bool MagicPickup { get => false; }
		public override bool IsPackage { get => true; }
		public override string Texture => "CastledsContent/Items/Storage/Boxes/PackageTiny";
		public override void SetStaticDefaults() { DisplayName.SetDefault("Tiny Package"); }
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<PackageTinyT>();
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
		}
	}
	internal class PackageTinyT : Package
	{
		public override int PackageType { get { return PackageData.Tiny; } }
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
		}
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
			if (!fail)
            {
				PackageData data = new PackageData(Vector2.Zero, PackageType, new BagPickup(0, false));
				foreach (PackageData d in CastledWorld.packages)
					if (d.coordinates == new Vector2(i, j))
						data = d;
				int item = Item.NewItem(i * 16, j * 16, 16, 48, data.GetPackageType(data.itemType));
				Main.item[item].GetGlobalItem<SGlobalItem>().storage = data.storage;
				foreach (PackageData d in CastledWorld.packages)
				{
					if (d == data)
					{
						CastledWorld.packages.Remove(d);
						break;
					}
				}
			}
		}
    }
	#endregion
	#region Small Package
	public class PackageSmall : BagItem
	{
		public override int BagLimit { get => 10; }
		public override bool MagicPickup { get => false; }
		public override bool IsPackage { get => true; }
		public override string Texture => "CastledsContent/Items/Storage/Boxes/PackageSmall";
		public override void SetStaticDefaults() { DisplayName.SetDefault("Small Package"); }
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<PackageSmallT>();
			item.width = 22;
			item.height = 10;
			item.rare = ItemRarityID.Blue;
		}
	}
	internal class PackageSmallT : Package
	{
		public override int PackageType { get { return PackageData.Small; } }
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
		}
	}
	#endregion
	#region Medium Package
	public class PackageMedium : BagItem
	{
		public override int BagLimit { get => 20; }
		public override bool MagicPickup { get => false; }
		public override bool IsPackage { get => true; }
		public override string Texture => "CastledsContent/Items/Storage/Boxes/PackageMedium";
		public override void SetStaticDefaults() { DisplayName.SetDefault("Medium Package"); }
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<PackageMediumT>();
			item.width = 22;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
		}
	}
	internal class PackageMediumT : Package
	{
		public override int PackageType { get { return PackageData.Medium; } }
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
		}
	}
	#endregion
	#region Large Package
	public class PackageLarge : BagItem
	{
		public override int BagLimit { get => 40; }
		public override bool MagicPickup { get => false; }
		public override bool IsPackage { get => true; }
		public override string Texture => "CastledsContent/Items/Storage/Boxes/PackageLarge";
		public override void SetStaticDefaults() { DisplayName.SetDefault("Large Package"); }
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.consumable = true;
			item.createTile = ModContent.TileType<PackageLargeT>();
			item.width = 34;
			item.height = 20;
			item.rare = ItemRarityID.Blue;
		}
	}
	internal class PackageLargeT : Package
	{
		public override int PackageType { get { return PackageData.Large; } }
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18};
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
		}
	}
	#endregion
	internal partial class Package : ModTile
    {
		public virtual int PackageType { get; protected set; }
        public override void SetDefaults()
        {
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Package");
			AddMapEntry(new Color(225, 190, 150), name);
		}
        public override void NumDust(int i, int j, bool fail, ref int num) => num = 0;
		public override void PlaceInWorld(int i, int j, Item item)
		{
			Item useItem = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
			PackageData data = new PackageData(new Vector2(i, j), PackageType, useItem.GetGlobalItem<SGlobalItem>().storage);
			CastledWorld.packages.Add(data);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			PackageData data = new PackageData(Vector2.Zero, PackageType, new BagPickup(0, false));
			foreach (PackageData d in CastledWorld.packages)
				if (d.coordinates == new Vector2(i, j))
					data = d;
			int item = Item.NewItem(i * 16, j * 16, 16, 48, data.GetPackageType(data.itemType));
			Main.item[item].GetGlobalItem<SGlobalItem>().storage = data.storage;
			foreach (PackageData d in CastledWorld.packages)
			{
				if (d == data)
				{
					CastledWorld.packages.Remove(d);
					break;
				}
			}
		}
	}
	public class PackageData : TagSerializable
	{
		public static readonly Func<TagCompound, PackageData> DESERIALIZER = Load;
		public Vector2 coordinates;
		public int itemType;
		public BagPickup storage;

		public PackageData(Vector2 coord, int type, BagPickup storage1)
        {
			coordinates = coord;
			itemType = type;
			storage = storage1;
        }

		public const short Test = 0;
		public const short Tiny = 1;
		public const short Small = 2;
		public const short Medium = 3;
		public const short Large = 4;

		public int GetPackageType(int type)
        {
			switch (type)
            {
				case 0:
					return ModContent.ItemType<TestBox>();
				case 1:
					return ModContent.ItemType<PackageTiny>();
				case 2:
					return ModContent.ItemType<PackageSmall>();
				case 3:
					return ModContent.ItemType<PackageMedium>();
				case 4:
					return ModContent.ItemType<PackageLarge>();
			}
			return ItemID.None;
		}
		public TagCompound SerializeData()
		{
			return new TagCompound
			{
				{ "type", itemType },
				{ "coord", coordinates},
				{ "SPickup", storage.doPickup },
				{ "SLimit", storage.limit },
				{ "SStorage", storage.contained }
			};
		}
		public static PackageData Load(TagCompound tag)
		{
			int limit = tag.GetInt("SLimit");
			bool pickup = tag.GetBool("SPickup");
			BagPickup bagInit = new BagPickup(limit, pickup)
			{
				contained = tag.Get<List<Item>>("SStorage")
			};

			return new PackageData(tag.Get<Vector2>("coord"), tag.GetInt("type"), bagInit);
		}
	}
}
