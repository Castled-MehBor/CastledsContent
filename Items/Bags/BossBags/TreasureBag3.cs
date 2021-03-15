using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.NPCs.Boss.HarpyQueen;
using CastledsContent.Utilities;
using CastledsContent.Items.Vanity;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Bags.BossBags
{
    public class TreasureBag3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }

        public override int BossBagNPC => NPCType<HarpyQueen>();
        public override bool CanRightClick() => true;

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ItemType<Accessories.HarpyQueenCirclet>());
            player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(13, 17));
            player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(29, 99));
            player.QuickSpawnItem(ItemID.Feather, Main.rand.Next(6, 12));
            player.QuickSpawnItem(ItemType<Material.HarpyFeather>(), Main.rand.Next(18, 28));

            int itemTypeToSpawn = 0;
            switch (Main.rand.Next(3))
            {
                case 0:
                    itemTypeToSpawn = ItemType<Weapons.Magic.HarpyStaff>();
                    break;
                case 1:
                    itemTypeToSpawn = ItemType<Weapons.Ranged.HarpyGun>();
                    break;
                case 2:
                    itemTypeToSpawn = ItemType<Weapons.Melee.HarpyArm>();
                    break;
                default:
                    break;
            }

            player.QuickSpawnItem(itemTypeToSpawn);

            MiscUtilities.SpawnDropItem(player, 9, ItemType<HarpyBreastplate>());
            MiscUtilities.SpawnDropItem(player, 9, ItemType<HarpyLeggings>());
            MiscUtilities.SpawnDropItem(player, 8, ItemType<HQMask>());
        }
    }
}
