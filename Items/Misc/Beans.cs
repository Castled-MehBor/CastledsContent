using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Misc
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
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime);
            {
                return true;
            }
        }

        public override bool UseItem(Player player)
        {
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod != null)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Beanos"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Beanos"));
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Beanos"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EyeofCthulhu);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EyeofCthulhu);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EyeofCthulhu);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsHead);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsBody);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.EaterofWorldsTail);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronHead);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronHead);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronHead);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.KingSlime);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.KingSlime);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.KingSlime);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DungeonGuardian);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DungeonGuardian);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DungeonGuardian);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.WallofFlesh);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Retinazer);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Retinazer);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Retinazer);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Spazmatism);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Spazmatism);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Spazmatism);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronPrime);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronPrime);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronPrime);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.TheDestroyer);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.TheDestroyer);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.TheDestroyer);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.QueenBee);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.QueenBee);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.QueenBee);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Golem);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Golem);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Golem);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BrainofCthulhu);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BrainofCthulhu);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BrainofCthulhu);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MourningWood);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MourningWood);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MourningWood);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Pumpking);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Pumpking);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Pumpking);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Everscream);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Everscream);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Everscream);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.IceQueen);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.IceQueen);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.IceQueen);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SantaNK1);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SantaNK1);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.SantaNK1);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DukeFishron);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DukeFishron);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DukeFishron);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MoonLordCore);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MoonLordCore);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.MoonLordCore);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerVortex);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerVortex);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerVortex);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerStardust);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerStardust);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerStardust);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerNebula);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerNebula);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerNebula);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerSolar);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerSolar);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.LunarTowerSolar);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.CultistBoss);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.CultistBoss);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.CultistBoss);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.GoblinSummoner);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCorruption);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicCrimson);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("TheDevourerofGods"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("TheDevourerofGods"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SupremeCalamitas"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Yharon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Yharon"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.PirateShip);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2OgreT3);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2OgreT3);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2OgreT3);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2DarkMageT3);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2DarkMageT3);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2DarkMageT3);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2DarkMageT3);
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Astrageldon"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("AstrumDeus"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("BrimstoneWaifu"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Bumblefuck"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Calamitas"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CeaselessVoid"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("CosmicWraith"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Crabulon"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Cryogen"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("HiveMind"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Leviathan"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Siren"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Perforator"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PlaguebringerGoliath"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PolterGhast"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PolterGhast"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PolterGhast"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("PolterGhast"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Providence"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Providence"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Providence"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Providence"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Providence"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Scavenger"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Scavenger"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Scavenger"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Scavenger"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("Scavenger"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGod"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGod"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGod"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGod"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGod"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodCore"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodCore"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodCore"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodCore"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodCore"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodRun"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodRun"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodRun"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodRun"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("SlimeGodRun"));
                //dont mind me, just putting a separator for each boss
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("StormWeaver"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("StormWeaver"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("StormWeaver"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("StormWeaver"));
                NPC.SpawnOnPlayer(player.whoAmI, calamityMod.NPCType("StormWeaver"));
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