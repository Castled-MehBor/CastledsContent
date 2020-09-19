// Tag #1: Skyware Boss
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace CastledsContent.Tiles
{
	public class SkywareArtifactT : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			ModTranslation val = CreateMapEntryName(null);
			val.SetDefault("Skyware Artifact");
			AddMapEntry(new Color(223, 223, 110), val);
			dustType = 64;
			disableSmartCursor = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 24 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("SkywareArtifact"), 1, false, 0, false, false);
		}
        public override void NearbyEffects(int i, int j, bool closer)
        {
			if (closer)
            {
				if (Main.LocalPlayer.ZoneSkyHeight)
				{
					Player localPlayer = Main.LocalPlayer;
					if (!localPlayer.dead)
					{
						localPlayer.AddBuff(mod.BuffType("AntiAntiGravity"), 60, true);
					}
				}
			}
		}
		#region Remove this if necessary
		public override void RightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			Tile val = Main.tile[i, j];
			int num = localPlayer.FindItem(ItemID.Feather);
			if (localPlayer.ZoneSkyHeight && NPC.downedQueenBee)
            {
				if (!NPC.AnyNPCs(mod.NPCType("HarpyQueen")))
				{
					for (int b = 0; b < 58; b++)
					{
						if (num != -1 && localPlayer.inventory[num].stack > 2 && localPlayer.inventory[b].type == ItemID.Feather && localPlayer.inventory[b].stack > 0)
						{
							int npcID = NPC.NewNPC(i * 16, j * 16 - 175, mod.NPCType("HarpyQueen"));
							localPlayer.inventory[b].stack -= 3;
							Main.PlaySound(SoundID.Item117);
							break;
						}
					}
				}
			}
		}
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ItemID.Feather;
		}
		#endregion
	}
}
