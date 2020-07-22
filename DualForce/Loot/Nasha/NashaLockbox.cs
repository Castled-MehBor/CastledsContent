using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.DualForce.Loot.Nasha
{
    public class NashaLockbox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleansing Lockbox");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
            + "\n''The reward for completing a difficult test.'"
            + "\nContains two weapons used by the legendary nymph, Nasha.");
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
            player.QuickSpawnItem(ItemID.GoldCoin, 15);
            int num = Main.rand.Next(3);
            if (num == 0)
            {
                player.QuickSpawnItem(mod.ItemType("PinkPotion"));
                player.QuickSpawnItem(mod.ItemType("OrbOfHallow"));
            }
            if (num == 1)
            {
                player.QuickSpawnItem(mod.ItemType("PinkPotion"));
                player.QuickSpawnItem(mod.ItemType("CrystalSpear"));
            }
            if (num == 2)
            {
                player.QuickSpawnItem(mod.ItemType("OrbOfHallow"));
                player.QuickSpawnItem(mod.ItemType("CrystalSpear"));
            }
        }
    }
}
