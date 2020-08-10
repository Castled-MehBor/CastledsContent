using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Bags
{
    public class GrakosLockbox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Lockbox");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
            + "\n''The reward for completing a difficult test.'"
            + "\nContains two weapons used by the demonic overlord, Grakos.");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 20;
            item.height = 18;
            item.rare = ItemRarityID.LightRed;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.GoldCoin, 40);
            player.QuickSpawnItem(ItemType<PreciousFlame>());
            player.QuickSpawnItem(ItemType<DeadeyeScroll>());
        }
    }
}
