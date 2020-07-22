using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.DualForce.Loot.Grakos
{
    public class GrakosLockboxExpert : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Lockbox");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
            + "\n''The reward for completing an extreme test.'"
            + "\nContains two weapons used by the demonic overlord, Grakos.");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 20;
            item.height = 18;
            item.rare = ItemRarityID.LightRed;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.GoldCoin, 75);
            player.QuickSpawnItem(mod.ItemType("PreciousFlame"));
            player.QuickSpawnItem(mod.ItemType("DeadeyeScroll"));
            player.QuickSpawnItem(mod.ItemType("MusicBox1"));
        }
    }
}
