using CastledsContent.NPCs.TemporaryBoss;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Summon
{
    public class Beans : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Beans");
            Tooltip.SetDefault("Not consumable"
+ "\nL I T E R A L L Y breaks the very fabric of existance on use"
+ "\nDon't use this unless you hate your computer"
+ "\nAlso it has to be night time."
+ "\n If you are using cheat sheet instead of legitimately using Calamity mod to use this, then you have small pp.");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 1;
            item.rare = (-12);
            item.useAnimation = 5;
            item.useTime = 5;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
        }

        public override bool CanUseItem(Player player) => !Main.dayTime;

        public override bool UseItem(Player player)
        {
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod != null)
            {

                for (int i = 0; i < 28; i++)
                    NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);

                int[] npcs =
                {
                    NPCType<Beanos>(), NPCID.EyeofCthulhu, NPCID.EaterofWorldsHead, NPCID.SkeletronHead, NPCID.KingSlime, NPCID.DungeonGuardian,
                    NPCID.Retinazer, NPCID.Spazmatism, NPCID.SkeletronPrime, NPCID.TheDestroyer, NPCID.QueenBee, NPCID.Golem, NPCID.BrainofCthulhu,
                    NPCID.Plantera, NPCID.MourningWood, NPCID.Pumpking, NPCID.Everscream, NPCID.IceQueen, NPCID.SantaNK1, NPCID.DukeFishron,
                    NPCID.MoonLordCore, NPCID.LunarTowerVortex, NPCID.LunarTowerStardust, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar,
                    NPCID.CultistBoss, NPCID.DD2OgreT3, NPCID.DD2Betsy, NPCID.DD2DarkMageT3, calamityMod.NPCType("Yharon"), calamityMod.NPCType("TheDevourerofGods")
                };
                foreach (int npcType in npcs)
                    for (int i = 0; i < 3; i++)
                        NPC.SpawnOnPlayer(player.whoAmI, npcType);

                int[] npcs2 =
                {
                    NPCID.GoblinSummoner, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.PirateShip
                };
                foreach (int npcType in npcs2)
                    for (int i = 0; i < 9; i++)
                        NPC.SpawnOnPlayer(player.whoAmI, npcType);

                int[] npcs3 =
                {
                    calamityMod.NPCType("Astrageldon"), calamityMod.NPCType("AstrumDeus"), calamityMod.NPCType("BrimstoneWaifu"),
                    calamityMod.NPCType("Bumblefuck"), calamityMod.NPCType("Calamitas"), calamityMod.NPCType("CeaselessVoid"),
                    calamityMod.NPCType("CosmicWraith"), calamityMod.NPCType("Crabulon"), calamityMod.NPCType("Cryogen"),
                    calamityMod.NPCType("HiveMind"), calamityMod.NPCType("Leviathan"), calamityMod.NPCType("Siren"),
                    calamityMod.NPCType("Perforator"), calamityMod.NPCType("PlaguebringerGoliath"), calamityMod.NPCType("PolterGhast"),
                    calamityMod.NPCType("Providence"), calamityMod.NPCType("Scavenger"), calamityMod.NPCType("SlimeGod"),
                    calamityMod.NPCType("SlimeGodCore"), calamityMod.NPCType("SlimeGodRun"), calamityMod.NPCType("StormWeaver")
                };
                foreach (int npcType in npcs3)
                    for (int i = 0; i < 6; i++)
                        NPC.SpawnOnPlayer(player.whoAmI, npcType);

                NPC.SpawnOnPlayer(player.whoAmI, NPCID.WallofFlesh);
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SupremeCalamitas"));

                Main.PlaySound(SoundID.Roar, player.position, 0);
                return true;
            }
            else
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Bunny);
                return true;
            }
        }

        public override void AddRecipes()
        {
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(calamityMod.ItemType("ShadowspecBar"), 99);
                recipe.AddIngredient(mod.ItemType("CalamitySupremeYharonToken"), 20);
                recipe.AddIngredient(calamityMod.ItemType("BossRush"));
                recipe.AddIngredient(mod.ItemType("VictoryRoyaleToken"), 75);
                recipe.AddIngredient(ItemID.Amethyst, 90);
                recipe.AddIngredient(ItemID.Ruby, 90);
                recipe.AddIngredient(ItemID.Emerald, 90);
                recipe.AddIngredient(ItemID.Sapphire, 90);
                recipe.AddIngredient(ItemID.Amber, 90);
                recipe.AddTile(calamityMod.TileType("DraedonsForge"));
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}