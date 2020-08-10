using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.BoneDweller
{
    public class BoneDwellerSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fragile Seismograph");
            Tooltip.SetDefault("Used for measuring the strength of earthquakes"
+ "\nIt's very fragile, be careful!"
+ "\nAttempts to detect the Bone Dweller"
+ "\nUse inside the dungeon");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 40;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            item.useAnimation = 25;
            item.useTime = 25;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item14;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneDungeon;
        }

        public override bool UseItem(Player player)
        {
            if (CastledWorld.downedbossHead)
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.BossHead>());
                Main.PlaySound(SoundID.Tink, player.position, 0);
                return true;
            }
            else
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.BossHead>());
                Main.PlaySound(SoundID.Tink, player.position, 0);
                Main.PlaySound(SoundID.Roar, player.position, 0);
                return true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 3);
            recipe.AddIngredient(ItemID.Spike, 6);
            recipe.AddIngredient(ItemID.Bone, 18);
            recipe.AddIngredient(ItemID.GoldenKey, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}