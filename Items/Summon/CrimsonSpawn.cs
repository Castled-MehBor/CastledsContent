using CastledsContent.NPCs.Boss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Summon
{
    public class CrimsonSpawn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Key of the Rib Cages");
            Tooltip.SetDefault("You made five replicas..."
            + "\nA long, lost cage is somewhere in this world"
            + "\nBy unlocking the invisible lock right in front of you, you will find what terrors were locked up."
            + "\nThis invisible lock appears in the Crimson, but nowhere else...");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.maxStack = 99;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player) => player.ZoneCrimson && Main.hardMode;

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.CrimsonBoss.SkeletonGuard1>());
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.CrimsonBoss.FleshGuard1>());
            Main.PlaySound(SoundID.Unlock, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldenKey, 3);
            recipe.AddIngredient(ItemID.Ichor, 15);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}