using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Bags.BossBags
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
            player.QuickSpawnItem(ItemType<Weapons.Ranged.DeadeyeScroll>());
            player.QuickSpawnItem(ItemType<Weapons.Magic.PreciousFlame>());
            player.QuickSpawnItem(ItemType<Placeable.MusicBox.MusicBox1>());
        }
    }
}
