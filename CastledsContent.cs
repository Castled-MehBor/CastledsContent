using CastledsContent.Items.Placeable.MusicBox;
using CastledsContent.Items.Summon;
using CastledsContent.Items.Bags;
using CastledsContent.Items.Bags.BossBags;
using CastledsContent.Items.Weapons.Melee;
using CastledsContent.Items.Weapons.Magic;
using CastledsContent.Items.Weapons.Ranged;
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
            }
        }
        public override void Load()
        {
            //Music Box Trivial Equality Original
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEquality"), ItemType("MusicBox1"), TileType("MusicBoxEquality"));
            //Music Box Trivial Equality Remastered
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/OST/TrivialEqualityV2"), ItemType("MusicBox1V2"), TileType("MusicBoxEquality2"));

            //YABHB Compatability
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
        }
    }
}
