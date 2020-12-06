using CastledsContent.Items.Placeable.MusicBox;
using CastledsContent.Items.Summon;
using CastledsContent.Items.Bags;
using CastledsContent.Items.Bags.BossBags;
using CastledsContent.Items.Weapons.Melee;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
using CastledsContent.Items.Accessories;
using CastledsContent.Items.Placeable.Trophy;
using CastledsContent.Items.Vanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent
{
    public class CastledsContent : Mod
    {

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddMiniBossWithInfo", "Bone Dweller", 5.5f, (Func<bool>)(() => CastledWorld.downedbossHead), "Can very rarely spawn in the Dungeon, or can alternitavely be detected using a [i:" + ModContent.ItemType<BoneDwellerSummon>() + "] inside the dungeon.");
                bossChecklist.Call("AddBossWithInfo", "Corrupt Guardians", 6.5f, (Func<bool>)(() => CastledWorld.downedCorruptGuardians), "Their deep slumber can be broken by shattering an [i:" + ModContent.ItemType<CorruptSummon>() + "] at the Corruption.");
                bossChecklist.Call("AddBossWithInfo", "Crimtane Prisoners", 6.5f, (Func<bool>)(() => CastledWorld.downedCrimsonPrisoners), "They can be unleashed from their quarantine by using [i:" + ModContent.ItemType<CrimsonSpawn>() + "] at the Crimson.");

                //DualForce
                bossChecklist.Call("AddBoss", 5.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.DualForce.LightMage.LightNymph>(), ModContent.NPCType<NPCs.Boss.DualForce.NightDemon.NightHusk>() }, this, "Nasha and Grakos", (Func<bool>)(() => CastledWorld.downedDualForce), ModContent.ItemType<DualForceSummon>(), new List<int>() { ModContent.ItemType<MusicBox1>(), }, new List<int>() { ModContent.ItemType<CrystalSpear>(), ModContent.ItemType<OrbOfHallow>(), ModContent.ItemType<PinkPotion>(), ModContent.ItemType<NashaLockbox>(), ModContent.ItemType<NashaLockboxExpert>(), ModContent.ItemType<DeadeyeScroll>(), ModContent.ItemType<PreciousFlame>(), ModContent.ItemType<GrakosLockbox>(), ModContent.ItemType<GrakosLockboxExpert>() }, "Use a [i:" + ItemType("DualForceSummon") + "] anywhere", "You either win or learn, meaning you are never truley defeated.", "CastledsContent/NPCs/Boss/DualForce/DualForceBossLog");
                //HarpyQueen
                bossChecklist.Call("AddBoss", 4.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.HarpyQueen.HarpyQueen>() }, this, "Harpy Queen", (Func<bool>)(() => CastledWorld.downedHarpyQueen), ModContent.ItemType<Items.Placeable.SkywareArtifact>(), new List<int>() { ModContent.ItemType<HQTrophy>(), ModContent.ItemType<HQMask>(), ModContent.ItemType<MusicBox1>(), }, new List<int>() { ModContent.ItemType<AvianHijack>(), ModContent.ItemType<QueenJudgement>(), ModContent.ItemType<MonarchPrecision>(), ModContent.ItemType<TreasureBag3>(), ModContent.ItemType<HarpyQueenCirclet>(), ModContent.ItemType<Items.Material.HarpyFeather>(), ItemID.Feather, ModContent.ItemType<HarpyBreastplate>(), ModContent.ItemType<HarpyLeggings>() }, "Can spawn sleeping in space after Queen Bee has been defeated, or you can put three feathers on a [i:" + ItemType("SkywareArtifact") + "]", "...", "CastledsContent/NPCs/Boss/HarpyQueen/HarpyQueenBossLog");
            }
        }
        public override void Load()
        {
            #region Music Boxes
            //Music Box Trivial Equality Original
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEquality"), ItemType("MusicBox1"), TileType("MusicBoxEquality"));
            //Music Box Trivial Equality Remastered
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2"), ItemType("MusicBox1V2"), TileType("MusicBoxEquality2"));
            //Music Box Feather Gauntlet
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/HarpyQueenTheme"), ItemType("MusicBox2"), TileType("MusicBoxFG"));
            #endregion

            #region YABHB Compatability
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                //Nasha
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                GetTexture("UI/YABHB/Nasha/NashaLeftBar"),
                GetTexture("UI/YABHB/Nasha/NashaMidBar"),
                GetTexture("UI/YABHB/Nasha/NashaRightBar"),
                GetTexture("UI/YABHB/Nasha/NashaMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 48, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(0f, 1f, 0f),
                new Color(0.5f, 1f, 0f),
                new Color(1f, 1f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("LightMage"));

                //Nasha Phase 2
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Nasha/NashaLeftBar"),
                    GetTexture("UI/YABHB/Nasha/NashaMidBar"),
                    GetTexture("UI/YABHB/Nasha/NashaRightBar"),
                    GetTexture("UI/YABHB/Nasha/NashaMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 48, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                    new Color(1f, 1f, 0f),
                    new Color(1f, 0.5f, 0f),
                    new Color(1f, 0f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("LightNymph"));

                //Grakos
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Grakos/GrakosLeftBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosRightBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 40, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(0f, 1f, 0f),
                new Color(0.5f, 1f, 0f),
                new Color(1f, 1f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("NightDemon"));

                //Grakos Phase 2
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Grakos/GrakosLeftBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosRightBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 40, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(1f, 1f, 0f),
                new Color(1f, 0.5f, 0f),
                new Color(1f, 0f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("NightHusk"));
            }
            #endregion

            if (!Main.dedServ)
            {
                LoadClient();
            }
        }
        private void LoadClient()
        {
            AddEquipTexture(null, (EquipType)2, "HarpyLeggings_FemaleLegs", "CastledsContent/Items/Vanity/HarpyLeggings_FemaleLegs", "", "");
            AddEquipTexture(new Items.Accessories.RobotInvasion.SpikeExoskeleton(), EquipType.Body, "ExoStrap", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton_Body", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton_Arms");
            AddEquipTexture(new Items.Accessories.RobotInvasion.SpikeExoskeleton(), EquipType.Body, "ExoStrapF", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton_FemaleBody", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton_FemaleBody_Arms");
            AddEquipTexture(new Items.Accessories.RobotInvasion.ReinforcedExoskeleton(), EquipType.Body, "ZenoSuit", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton_Body", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton_Arms");
        }
        #region RecipeGroups
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup ilgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Iron Bar", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup stgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup gpgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup dcgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup epithtissue = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Boss Tissue", new int[]
            {
                ItemID.TissueSample,
                ItemID.ShadowScale
            });
            RecipeGroup evilore = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar Ore", new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre
            });
            RecipeGroup.RegisterGroup("CastledsContent:OneTierBar", group);
            RecipeGroup.RegisterGroup("CastledsContent:TwoTierBar", ilgroup);
            RecipeGroup.RegisterGroup("CastledsContent:ThreeTierBar", stgroup);
            RecipeGroup.RegisterGroup("CastledsContent:FourTierBar", gpgroup);
            RecipeGroup.RegisterGroup("CastledsContent:EvilBar", dcgroup);
            RecipeGroup.RegisterGroup("CastledsContent:EvilDrop", epithtissue);
            RecipeGroup.RegisterGroup("CastledsContent:EvilOre", evilore);

        }
        #endregion
        public override void AddRecipes()
        {
            //recipe.AddIngredient(ModContent.ItemType<>());
            #region Enchanted Sword
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.CopperShortsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.EnchantedSword);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.TinShortsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.EnchantedSword);
            recipe.AddRecipe();
            #endregion

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.Diamond, 2);
            recipe.AddRecipeGroup("CastledsContent:EvilOre", 25);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(ItemID.Arkhalis);
            recipe.AddRecipe();
        }
    }
}

#region Old Code
/*
using CastledsContent.Items.Placeable.MusicBox;
using CastledsContent.Items.Summon;
using CastledsContent.Items.Bags;
using CastledsContent.Items.Bags.BossBags;
using CastledsContent.Items.Weapons.Melee;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
using CastledsContent.Items.Accessories;
using CastledsContent.Items.Placeable.Trophy;
using CastledsContent.Items.Vanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent
{
    public class CastledsContent : Mod
    {

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddMiniBossWithInfo", "Bone Dweller", 5.5f, (Func<bool>)(() => CastledWorld.downedbossHead), "Can very rarely spawn in the Dungeon, or can alternitavely be detected using a [i:" + ModContent.ItemType<BoneDwellerSummon>() + "] inside the dungeon.");
                bossChecklist.Call("AddBossWithInfo", "Corrupt Guardians", 6.5f, (Func<bool>)(() => CastledWorld.downedCorruptGuardians), "Their deep slumber can be broken by shattering an [i:" + ModContent.ItemType<CorruptSummon>() + "] at the Corruption.");
                bossChecklist.Call("AddBossWithInfo", "Crimtane Prisoners", 6.5f, (Func<bool>)(() => CastledWorld.downedCrimsonPrisoners), "They can be unleashed from their quarantine by using [i:" + ModContent.ItemType<CrimsonSpawn>() + "] at the Crimson.");

                //DualForce
                bossChecklist.Call("AddBoss", 5.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.DualForce.LightMage.LightNymph>(), ModContent.NPCType<NPCs.Boss.DualForce.NightDemon.NightHusk>() }, this, "Nasha and Grakos", (Func<bool>)(() => CastledWorld.downedDualForce), ModContent.ItemType<DualForceSummon>(), new List<int>() { ModContent.ItemType<MusicBox1>(), }, new List<int>() { ModContent.ItemType<CrystalSpear>(), ModContent.ItemType<OrbOfHallow>(), ModContent.ItemType<PinkPotion>(), ModContent.ItemType<NashaLockbox>(), ModContent.ItemType<NashaLockboxExpert>(), ModContent.ItemType<DeadeyeScroll>(), ModContent.ItemType<PreciousFlame>(), ModContent.ItemType<GrakosLockbox>(), ModContent.ItemType<GrakosLockboxExpert>() }, "Use a [i:" + ItemType("DualForceSummon") + "] anywhere", "You either win or learn, meaning you are never truley defeated.", "CastledsContent/NPCs/Boss/DualForce/DualForceBossLog");
                //HarpyQueen
                bossChecklist.Call("AddBoss", 4.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.HarpyQueen.HarpyQueen>() }, this, "Harpy Queen", (Func<bool>)(() => CastledWorld.downedHarpyQueen), ModContent.ItemType<Items.Placeable.SkywareArtifact>(), new List<int>() { ModContent.ItemType<HQTrophy>(), ModContent.ItemType<HQMask>(), ModContent.ItemType<MusicBox1>(), }, new List<int>() { ModContent.ItemType<AvianHijack>(), ModContent.ItemType<QueenJudgement>(), ModContent.ItemType<MonarchPrecision>(), ModContent.ItemType<TreasureBag3>(), ModContent.ItemType<HarpyQueenCirclet>(), ModContent.ItemType<Items.Material.HarpyFeather>(), ItemID.Feather, ModContent.ItemType<HarpyBreastplate>(), ModContent.ItemType<HarpyLeggings>() }, "Can spawn sleeping in space after Queen Bee has been defeated, or you can put three feathers on a [i:" + ItemType("SkywareArtifact") + "]", "...", "CastledsContent/NPCs/Boss/HarpyQueen/HarpyQueenBossLog");
            }
        }
        public override void Load()
        {
            #region Music Boxes
            //Music Box Trivial Equality Original
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEquality"), ItemType("MusicBox1"), TileType("MusicBoxEquality"));
            //Music Box Trivial Equality Remastered
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2"), ItemType("MusicBox1V2"), TileType("MusicBoxEquality2"));
            //Music Box Feather Gauntlet
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/HarpyQueenTheme"), ItemType("MusicBox2"), TileType("MusicBoxFG"));
            #endregion

            #region YABHB Compatability
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                //Nasha
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                GetTexture("UI/YABHB/Nasha/NashaLeftBar"),
                GetTexture("UI/YABHB/Nasha/NashaMidBar"),
                GetTexture("UI/YABHB/Nasha/NashaRightBar"),
                GetTexture("UI/YABHB/Nasha/NashaMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 48, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(0f, 1f, 0f),
                new Color(0.5f, 1f, 0f),
                new Color(1f, 1f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("LightMage"));

                //Nasha Phase 2
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Nasha/NashaLeftBar"),
                    GetTexture("UI/YABHB/Nasha/NashaMidBar"),
                    GetTexture("UI/YABHB/Nasha/NashaRightBar"),
                    GetTexture("UI/YABHB/Nasha/NashaMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 48, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                    new Color(1f, 1f, 0f),
                    new Color(1f, 0.5f, 0f),
                    new Color(1f, 0f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("LightNymph"));

                //Grakos
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Grakos/GrakosLeftBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosRightBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 40, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(0f, 1f, 0f),
                new Color(0.5f, 1f, 0f),
                new Color(1f, 1f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("NightDemon"));

                //Grakos Phase 2
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("UI/YABHB/Grakos/GrakosLeftBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosRightBar"),
                    GetTexture("UI/YABHB/Grakos/GrakosMidFill"));
                yabhb.Call("hbLoopMidBar", true);
                yabhb.Call("hbSetMidBarOffsetY", 0);
                yabhb.Call("hbSetBossHeadCentre", 40, 28);
                yabhb.Call("hbSetFillDecoOffset", 10);
                yabhb.Call("hbSetColours",
                new Color(1f, 1f, 0f),
                new Color(1f, 0.5f, 0f),
                new Color(1f, 0f, 0f));
                yabhb.Call("hbFinishSingle", NPCType("NightHusk"));
            }
            #endregion

            if (!Main.dedServ)
            {
                LoadClient();
            }
        }
        private void LoadClient()
        {
            AddEquipTexture(null, (EquipType)2, "HarpyLeggings_FemaleLegs", "CastledsContent/Items/Vanity/HarpyLeggings_FemaleLegs", "", "");
        }
    }
}
*/
#endregion
