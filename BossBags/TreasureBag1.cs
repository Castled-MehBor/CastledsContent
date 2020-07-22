using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.BossBags
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
            item.rare = 9;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("TitaniumCrystalBadge"), 1);
            player.QuickSpawnItem(ItemID.GoldCoin, 30);
            player.QuickSpawnItem(ItemID.CursedFlame, Main.rand.Next(28, 46));
            player.QuickSpawnItem(ItemID.SoulofNight, Main.rand.Next(13, 28));
            int num = Main.rand.Next(4);
            if (num == 0)
            {
                player.QuickSpawnItem(mod.ItemType("LunaBlaster"), 1);
            }
            if (num == 1)
            {
                player.QuickSpawnItem(ItemID.CursedFlames);
            }
            if (num == 2)
            {
                player.QuickSpawnItem(ItemID.ArmorBracing);
            }
            if (num == 3)
            {
                player.QuickSpawnItem(ItemID.CountercurseMantra);
            }
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("EpicQuartz"));
            }
            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("EnchantedSwordbutBetter"), 1);
            }

            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("QueenBee"), 1);
            }

            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("LaserTron"), 1);
            }

            if (Main.rand.Next(2) == 0)
            {
                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    player.QuickSpawnItem(mod.ItemType("BruhMomento"), 1);
                }
            }

            if (Main.rand.Next(2) == 0)
            {
                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss)
                {
                    player.QuickSpawnItem(mod.ItemType("BayonettaKiller"), 1);
                }
            }

            if (Main.rand.Next(2) == 0)
            {
                if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist)
                {
                    player.QuickSpawnItem(mod.ItemType("ShinyStaff"), 1);
                }
            }
        }
    }
}
