/* Tag #1: Skyware Boss
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System;

namespace CastledsContent.Items.Storage.SkyLeague
{
	public class SLInfoBoard : ModItem
	{
		int RCType = 0;
		string TT = "Default thing";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SL Info Board");
			Tooltip.SetDefault(CastledsContent.placeHolderTooltip);
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 32;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			int num = -1;
			int num2 = 0;
			while (num2 < tooltips.Count)
			{
				if (!tooltips[num2].Name.Equals("ItemName"))
				{
					num2++;
					continue;
				}
				num = num2;
				break;
			}
			tooltips.Insert(num + 1, new TooltipLine(mod, "SLInfoBoardTT1", TT));
		}
		void CheckRightClick()
		{
			RCType++;
			if (RCType > 2)
				RCType = 0;
			TT = RCTooltip();
			Player player = Main.player[Main.myPlayer];
			CastledPlayer mP = player.GetModPlayer<CastledPlayer>();
			bool HasHighlighted()
            {
				bool found = false;
				foreach (Item i in player.inventory)
					if (i.GetGlobalItem<SGlobalItem>().SLHighlight)
						found = true;
				return found;
            }
			string RCTooltip()
            {
				switch (RCType)
                {
					case 0:
						return "Default Thing";
					case 1:
						return "Right-Click item send thing";
					case 2:
						return "Raid shadow legends subscription thing";
                }
				return string.Empty;
            }
		}
	}
	public class Mailbox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mailbox");
			Tooltip.SetDefault(CastledsContent.placeHolderTooltip);
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 48;
			item.maxStack = 3;
			item.rare = ItemRarityID.Blue;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<MailboxT>();
		}
		public override bool CanUseItem(Player player) => Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>().MBAddress == "";

	}
	public class MailboxT : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Direction = Terraria.Enums.TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = Terraria.Enums.TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);
			ModTranslation val = CreateMapEntryName(null);
			val.SetDefault("Mailbox");
			AddMapEntry(new Color(220, 125, 90), val);
			disableSmartCursor = true;
		}
        public override void PlaceInWorld(int i, int j, Item item)
        {
			CastledPlayer player = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
			MailboxData data = new MailboxData();
			data.address = MailboxData.GenerateAddress();
			player.MBAddress = data.address;
			Main.NewText(data.address);
		}

        public override void NumDust(int i, int j, bool fail, ref int num) => num = 0;

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			CastledPlayer player = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
			player.MBAddress = "";
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<Mailbox>(), 1, false, 0, false, false);
		}
	}
	public class MailboxData
    {
		public static readonly Func<TagCompound, MailboxData> DESERIALIZER = Load;
		public string address = "";
		public List<Item> contents = new List<Item>();
		public bool hasSubscription;
		public int subscriptionStreak;
		public static string GenerateAddress()
        {
			int random = Main.rand.Next(1000000000, int.MaxValue);
			int random2 = Main.rand.Next(8);
			string b = "";
			string result = "";
			switch (random2)
            {
				case 0:
					b = "Sli";
						break;
				case 1:
					b = "Eye";
					break;
				case 2:
					b = "Zom";
					break;
				case 3:
					b = "Eat";
					break;
				case 4:
					b = "Skel";
					break;
				case 5:
					b = "Har";
					break;
				case 6:
					b = "Wof";
					break;
				case 7:
					b = "Moo";
					break;
			}
			result = $"{b}{random}";
			return result;
		}
		public TagCompound SerializeData()
		{
			return new TagCompound
			{
				{ "address", address },
				{ "contents", contents},
				{ "hasSubscription", hasSubscription },
				{ "subscriptionStreak", subscriptionStreak }
			};
		}
		public static MailboxData Load(TagCompound tag)
		{
			MailboxData data = new MailboxData
			{
				address = tag.GetString("address"),
				contents = tag.Get<List<Item>>("contents"),
				hasSubscription = tag.GetBool("hasSubscription"),
				subscriptionStreak = tag.GetInt("subscriptionStreak")
			};
			return data;
		}
	}
}*/
