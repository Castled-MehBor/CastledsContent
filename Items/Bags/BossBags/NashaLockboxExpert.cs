using CastledsContent.Items.Placeable.MusicBox;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Bags.BossBags
{
    public class NashaLockboxExpert : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleansing Lockbox");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
            + "\n''The reward for completing an extreme test.'"
            + "\nContains two weapons used by the legendary nymph, Nasha.");
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

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.GoldCoin, 50);
            player.QuickSpawnItem(ItemType<MusicBox1>());

            switch (Main.rand.Next(3))
            {
                case 0:
                    player.QuickSpawnItem(ItemType<PinkPotion>());
                    player.QuickSpawnItem(ItemType<OrbOfHallow>());
                    break;
                case 1:
                    player.QuickSpawnItem(ItemType<PinkPotion>());
                    player.QuickSpawnItem(ItemType<CrystalSpear>());
                    break;
                case 2:
                    player.QuickSpawnItem(ItemType<OrbOfHallow>());
                    player.QuickSpawnItem(ItemType<CrystalSpear>());
                    break;
                default:
                    break;
            }
        }
    }
}
