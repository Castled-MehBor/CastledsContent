using CastledsContent.Items.Weapons.Melee;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Utilities;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Bags.BossBags
{
    public class TreasureBag1 : ModItem
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
            player.QuickSpawnItem(ItemType<Accessories.TitaniumCrystalBadge>());
            player.QuickSpawnItem(ItemID.GoldCoin, 30);
            player.QuickSpawnItem(ItemID.CursedFlame, Main.rand.Next(28, 46));
            player.QuickSpawnItem(ItemID.SoulofNight, Main.rand.Next(13, 28));

            int itemTypeToSpawn = 0;
            switch (Main.rand.Next(4))
            {
                case 0:
                    itemTypeToSpawn = ItemType<LunaBlaster>();
                    break;
                case 1:
                    itemTypeToSpawn = ItemID.CursedFlames;
                    break;
                case 2:
                    itemTypeToSpawn = ItemID.ArmorBracing;
                    break;
                case 3:
                    itemTypeToSpawn = ItemID.CountercurseMantra;
                    break;
                default:
                    break;
            }

            player.QuickSpawnItem(itemTypeToSpawn);

            MiscUtilities.SpawnDropItem(player, 2, ItemType<Material.EpicQuartz>());
            MiscUtilities.SpawnDropItem(player, 2, ItemType<BruhMomento>(), NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
            MiscUtilities.SpawnDropItem(player, 2, ItemType<BayonettaKiller>(), NPC.downedGolemBoss);
            MiscUtilities.SpawnDropItem(player, 2, ItemType<ShinyStaff>(), NPC.downedAncientCultist);
            MiscUtilities.SpawnDropItem(player, 9, ItemType<EnchantedSwordbutBetter>());
            MiscUtilities.SpawnDropItem(player, 9, ItemType<QueenBee>());
            MiscUtilities.SpawnDropItem(player, 9, ItemType<LaserTron>());
        }
    }
}
