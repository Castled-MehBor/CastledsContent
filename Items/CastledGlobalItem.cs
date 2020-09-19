using CastledsContent.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items
{
	public class CastledGlobalItem : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "crate" && arg == ItemID.FloatingIslandFishingCrate)
			{
				if (Main.rand.Next(13) == 0)
                {
					player.QuickSpawnItem(ItemType<SkywareArtifact>());
				}
			}
		}
    }
}