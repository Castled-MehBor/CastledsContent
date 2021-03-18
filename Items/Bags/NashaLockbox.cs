using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
using CastledsContent.Items.Weapons.Melee;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Bags
{
    public class NashaLockbox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cleansing Lockbox");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"
            + "\n'The reward for completing a difficult test.'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 20;
            item.height = 18;
            item.rare = ItemRarityID.LightRed;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.GoldCoin, 15);
            switch(Main.rand.Next(3))
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
            }
        }
    }
}
