using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.NPCs.Flayke.Items.Misc
{
    public class FlaykeBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 26;
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }

        public override int BossBagNPC => NPCType<Flayke>();
        public override bool CanRightClick() => true;

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ItemID.FallenStar, Main.rand.Next(5, 9));
            player.QuickSpawnItem(ItemType<Accessory.SnowFlayke>());
            player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(5, 7));
            player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(29, 99));
            MiscUtilities.SpawnDropItem(player, 4, ItemType<Bow.Bow>());

            int itemTypeToSpawn = 0;
            switch (Main.rand.Next(3))
            {
                case 0:
                    itemTypeToSpawn = ItemType<Weapon.Icicle.Permatrator>();
                    break;
                case 1:
                    itemTypeToSpawn = ItemType<Weapon.Cannon.StarlightCertificate>();
                    break;
                case 2:
                    itemTypeToSpawn = ItemType<Weapon.Shovel.ShiveringSpade>();
                    break;
                default:
                    break;
            }

            player.QuickSpawnItem(itemTypeToSpawn);
        }
    }
}
