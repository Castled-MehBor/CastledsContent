using CastledsContent.Items.Epic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.BossBags
{
    public class TreasureBag2 : ModItem
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
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Cyan;
            item.expert = true;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemType<CrimtaneScrap>());
            player.QuickSpawnItem(ItemID.GoldCoin, 45);
            player.QuickSpawnItem(ItemID.Ichor, Main.rand.Next(30, 52));
            player.QuickSpawnItem(ItemID.SoulofNight, Main.rand.Next(18, 36));

            int itemTypeToSpawn = 0;
            switch (Main.rand.Next(4))
            {
                case 0:
                    itemTypeToSpawn = ItemType<RapidBlaster>();
                    break;
                case 1:
                    itemTypeToSpawn = ItemID.GoldenShower;
                    break;
                case 2:
                    itemTypeToSpawn = ItemID.ThePlan;
                    break;
                case 3:
                    itemTypeToSpawn = ItemID.MedicatedBandage;
                    break;
                default:
                    break;
            }
            player.QuickSpawnItem(itemTypeToSpawn);

            MiscUtilities.SpawnDropItem(player, 2, ItemType<EpicQuartz>());
            MiscUtilities.SpawnDropItem(player, 2, ItemType<BruhMomento>(), NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
            MiscUtilities.SpawnDropItem(player, 2, ItemType<BayonettaKiller>(), NPC.downedGolemBoss);
            MiscUtilities.SpawnDropItem(player, 2, ItemType<ShinyStaff>(), NPC.downedAncientCultist);
            MiscUtilities.SpawnDropItem(player, 9, ItemType<EnchantedSwordbutBetter>());
            MiscUtilities.SpawnDropItem(player, 9, ItemType<QueenBee>());
            MiscUtilities.SpawnDropItem(player, 9, ItemType<LaserTron>());
        }
    }
}
