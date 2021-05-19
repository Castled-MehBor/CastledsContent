using CastledsContent.UI;
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
using CastledsContent.Utilities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace CastledsContent
{
    public class CastledsContent : Mod
    {
        public static Mod Instance;
        static internal CastledsContent instance;
        internal int bagTagFlash = 0;
        internal int bagTagMult = 1;
        public static string placeHolderTooltip = "The uh the fuckin' uh";
        #region Mod Vars
        public const int Calamity = 0;
        public const int Thorium = 1;
        public const int Fargo = 2;
        public const int FargoSoul = 3;
        #endregion
        #region Values for DrawAnimationVertical Clones
        internal int Frame1 = 0;
        internal int FrameCounter1 = 0;
        internal int Frame2 = 0;
        internal int FrameCounter2 = 0;
        internal int Frame3 = 0;
        internal int FrameCounter3 = 0;
        internal int Frame1Inv = 0;
        internal int FrameCounter1Inv = 0;
        internal int Frame2Inv = 0;
        internal int FrameCounter2Inv = 0;
        internal float[] pedestal = new float[4] { 0, 1, 0, 1 };
        internal List<PIH> images = new List<PIH>();
        #endregion
        #region Variables
        private int packetNum = 0;
        internal int titleAlpha = 0;
        internal int title2Alpha = 0;
        internal int title3Alpha = 0;
        internal int timer1 = 0;
        internal int sub1 = 0;
        internal int sub2 = 0;
        internal int sub3 = 0;
        internal int sub1a = 0;
        internal int sub2a = 0;
        internal int ia = 0;
        internal bool subT2 = false;
        internal bool subT3 = false;
        //internal bool subT1a = false;
        internal bool subT2a = false;
        internal bool hqtest = false;
        internal int modCond = 3;
        internal int changeType = 1;
        internal static ModHotKey JoinMinigame;
        internal static ModHotKey PresetNavigate;
        internal static ModHotKey SpecialHotkey;
        #endregion
        #region UI
        private GameTime _lastUpdateUiGameTime;
        internal UserInterface TabletState;
        internal TabletButton tabletUI;
        #endregion
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                //DualForce
                bossChecklist.Call("AddBoss", 5.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.DualForce.LightMage.LightNymph>(), ModContent.NPCType<NPCs.Boss.DualForce.NightDemon.NightHusk>() }, this, "Nasha and Grakos", (Func<bool>)(() => CastledWorld.downedDualForce), ModContent.ItemType<DualForceSummon>(), new List<int>() { ModContent.ItemType<MusicBox1>(), }, new List<int>() { ModContent.ItemType<CrystalSpear>(), ModContent.ItemType<OrbOfHallow>(), ModContent.ItemType<PinkPotion>(), ModContent.ItemType<NashaLockbox>(), ModContent.ItemType<NashaLockboxExpert>(), ModContent.ItemType<DeadeyeScroll>(), ModContent.ItemType<PreciousFlame>(), ModContent.ItemType<GrakosLockbox>(), ModContent.ItemType<GrakosLockboxExpert>() }, "Use a [i:" + ItemType("DualForceSummon") + "] anywhere", "You either win or learn, meaning you are never truley defeated.", "CastledsContent/NPCs/Boss/DualForce/DualForceBossLog");
                //HarpyQueen
                bossChecklist.Call("AddBoss", 4.9f, new List<int>() { ModContent.NPCType<NPCs.Boss.HarpyQueen.HarpyQueen>() }, this, "Harpy Queen", (Func<bool>)(() => CastledWorld.downedHarpyQueen), ModContent.ItemType<Items.Placeable.SkywareArtifact>(), new List<int>() { ModContent.ItemType<HQTrophy>(), ModContent.ItemType<HQMask>(), ModContent.ItemType<MusicBox2>(), },  new List<int>() { ModContent.ItemType<HarpyGun>(), ModContent.ItemType<HarpyArm>(), ModContent.ItemType<HarpyStaff>(), ModContent.ItemType<TreasureBag3>(), ModContent.ItemType<HarpyQueenCirclet>(), ModContent.ItemType<Items.Material.HarpyFeather>(), ItemID.Feather, ModContent.ItemType<HarpyBreastplate>(), ModContent.ItemType<HarpyLeggings>() }, "Can spawn sleeping in space after Queen Bee has been defeated, or you can put three feathers on a [i:" + ItemType("SkywareArtifact") + "]", "...", "CastledsContent/NPCs/Boss/HarpyQueen/HarpyQueenBossLog");
                bossChecklist.Call("AddEvent", 1.99f, new List<int>() { ModContent.NPCType<NPCs.ItemLotteryNPC.ItemLotteryNPC>() }, this, "Superintendent", (Func<bool>)(() => CastledWorld.downedAlgorithmo), ModContent.ItemType<Items.MinigameItem>(), ItemID.None, ItemID.None, "Use a fully-charged [i:" + ItemType("MinigameItem") + "].", "", "CastledsContent/NPCs/ItemLotteryNPC/ItemLotteryNPCBossLog", "CastledsContent/Content/RobotInvasion_Icon", (Func<bool>)(() => NPC.downedSlimeKing));            }
            #region Pedestal Behind Image Loading
            for (int a = 0; a < Main.itemTexture.Length; a++)
                images.Add(new PIH(a, Main.itemTexture[a] == null));
            foreach (PIH p in instance.images)
                if (!p.voiden)
                    p.CreateTexture();
            #endregion

        }
        public override void Load()
        {
            Instance = this;
            instance = this;
            //Main.instance.LoadProjectile(ModContent.ProjectileType<Items.Summon.DistortedFlask.DistortedFlaskExplosion>());
            LMan.ClearData(3);
            #region Prepare Packet Array
            int modCount = ModLoader.Mods.Length;
            Array.Resize(ref LMan.packets, modCount);
            for (int a = 0; a < modCount; a++)
            {
                LMan.loadedMods.Add(ModLoader.Mods[a].Name);
                LotPacket packet = new LotPacket
                {
                    mod = null,
                    modtitle = null,
                    modColor = null,
                    conditions = null
                };
                LMan.packets[a] = packet;
            }
            #endregion
            JoinMinigame = RegisterHotKey("Join Minigame (NOT FUNCTIONAL | WIP)", "/");
            PresetNavigate = RegisterHotKey("Preset Navigation Hotkey", "G");
            SpecialHotkey = RegisterHotKey("Speciall Hotkey", "F");
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
                #region Initialize UI
                TabletState = new UserInterface();
                tabletUI = new TabletButton();
                tabletUI.Activate();
                #endregion
                LoadClient();
            }
        }
        private void LoadClient()
        {
            AddEquipTexture(null, EquipType.Body, "TarrArm", "CastledsContent/NPCs/Tarr/Textures/TarrArm_Body", "CastledsContent/NPCs/Tarr/Textures/TarrArm_Arms");
            AddEquipTexture(null, EquipType.Body, "TarrArmBreached", "CastledsContent/NPCs/Tarr/Textures/TarrArmBreached_Body", "CastledsContent/NPCs/Tarr/Textures/TarrArmBreached_Arms");
            AddEquipTexture(null, (EquipType)2, "HarpyLeggings_FemaleLegs", "CastledsContent/Items/Vanity/HarpyLeggings_FemaleLegs", "", "");
            #region Simple Exoskeleton Textures
            AddEquipTexture(new Items.Accessories.RobotInvasion.SpikeExoskeleton.SpikeExoskeleton(), EquipType.Body, "ExoBody", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton/SpikeExoskeleton_Body", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton/SpikeExoskeleton_Arms");
            AddEquipTexture(new Items.Accessories.RobotInvasion.SpikeExoskeleton.SpikeExoskeleton(), EquipType.Legs, "ExoLegs", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton/SpikeExoskeleton_Legs");
            AddEquipTexture(new Items.Accessories.RobotInvasion.SpikeExoskeleton.SpikeExoskeleton(), (EquipType)2, "ExoLegsF", "CastledsContent/Items/Accessories/RobotInvasion/SpikeExoskeleton/SpikeExoskeleton_FemaleLegs");
            #endregion
            #region Reinforced Exoskeleton Textures
            AddEquipTexture(new Items.Accessories.RobotInvasion.ReinforcedExoskeleton.ReinforcedExoskeleton(), EquipType.Body, "ExoBodyX", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_Body", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_Body_Arms");
            AddEquipTexture(new Items.Accessories.RobotInvasion.ReinforcedExoskeleton.ReinforcedExoskeleton(), (EquipType)1, "ExoBodyFX", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_FemaleBody", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_FemaleBody_Arms");
            AddEquipTexture(new Items.Accessories.RobotInvasion.ReinforcedExoskeleton.ReinforcedExoskeleton(), EquipType.Legs, "ExoLegsX", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_Legs");
            AddEquipTexture(new Items.Accessories.RobotInvasion.ReinforcedExoskeleton.ReinforcedExoskeleton(), (EquipType)2, "ExoLegsFX", "CastledsContent/Items/Accessories/RobotInvasion/ReinforcedExoskeleton/ReinforcedExoskeleton_FemaleLegs");
            #endregion
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup lasergroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Suitable Medium", new int[]
            {
                ItemID.Ruby,
                ItemID.Diamond
            });
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
            RecipeGroup.RegisterGroup("CastledsContent:RoboGemGroup", lasergroup);

        }
        #region Currently only related to ItemLotteryMinigame
        public override void Unload()
        {
            Instance = this;
            instance = this;
            LMan.ClearData(3);
            LMan.blacklist.Clear();
        }
        public override object Call(params object[] args)
        {
            try
            {
                LotPacket packet = new LotPacket
                {
                    mod = args[0] as Mod,
                    modtitle = args[1] as string,
                    modColor = args[2] as Color?,
                    conditions = args[3] as List<string>,
                    blacklist = args[4] as List<int>
                };
                if (packet.blacklist != null && packet.blacklist.Count > 0)
                {
                    foreach (int i in packet.blacklist)
                        LMan.blacklist.Add(i);
                }
                LMan.packets[packetNum] = packet;
                packetNum++;
                return "Success";
            }
            catch (Exception)
            {
                return "Failure";
            }
        }
        public override void PreSaveAndQuit()
        {
            LMan.ClearData(3);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            bagTagFlash += 3 * bagTagMult;
            if (bagTagFlash > 125)
                bagTagMult = -1;
            if (bagTagFlash < 0)
                bagTagMult = 1;
            if (Main.mouseItem != null && !Main.mouseItem.IsAir)
            Main.mouseItem.GetGlobalItem<SGlobalItem>().bagTag = false;
            CastledPlayer mP = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
            #region Pedestal Offset Modification
            pedestal[1] += 0.4901960784313725f;
            if (pedestal[1] > 300 && pedestal[0] < 0.02)
                pedestal[1] = 0;
            Vector2 velocity = new Vector2(0f, (float)Math.Sin((double)((Math.PI * 2) * pedestal[1] / 300f)) * 0.5f);
            //sin(-2pi*x/300)*0.5 + 0.5
            Vector2 velocityTwo = new Vector2(0f, (float)(Math.Sin((double)((Math.PI * -2) * pedestal[1] / 300f)) * 0.5f) + 0.5f);
            //\sin(2pi*x/300)*0.5
            //velocity = Vector2.UnitY * velocity.Length();
            pedestal[0] = velocity.Y;
            pedestal[3] = velocityTwo.Y;
            #endregion
            #region PresetUI
            Mod mod = ModLoader.GetMod("ProjectB");
            if (mP.drawUI)
            {
                var presetLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Fancy UI"));
                var presetState = new LegacyGameInterfaceLayer("ProjectB: Interface Logic 1",
                    delegate
                    {
                        DrawPresetUI(mP);
                        //DrawLotteryText1();
                        //DrawLotteryText2();
                        //DrawLotteryText3();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(presetLayer, presetState);
            }
            #endregion
            #region Tablet UI
            if (Main.player[Main.myPlayer].GetModPlayer<NPCs.Tarr.IncPlayer>().godMode)
            {
                int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                if (mouseTextIndex != -1)
                {
                    layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                        "CastledsContent: Interface Logic 1",
                        delegate
                        {
                            if (_lastUpdateUiGameTime != null && TabletState?.CurrentState != null)
                            {
                                TabletState.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                            }
                            return true;
                        },
                           InterfaceScaleType.UI));
                }
            }
            #endregion
            if (CastledWorld.waitParti)
            {
                var waitLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var waitState = new LegacyGameInterfaceLayer("CastledsContent: Interface Logic 1",
                    delegate
                    {
                        DrawLotteryWait();
                        //DrawLotteryText1();
                        //DrawLotteryText2();
                        //DrawLotteryText3();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(waitLayer, waitState);
            }
            if (mP.parti)
            {
                if (LMan.begin)
                {
                    var beLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var beState = new LegacyGameInterfaceLayer("CastledsContent: Interface Logic 1",
                        delegate
                        {
                            DrawLotteryBegin();
                            //DrawLotteryText1();
                            //DrawLotteryText2();
                            //DrawLotteryText3();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(beLayer, beState);
                }
                if (LMan.displayItemType != -1)
                {
                    var iLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var iState = new LegacyGameInterfaceLayer("CastledsContent: Interface Logic 1",
                        delegate
                        {
                            DrawShopItem();
                            //DrawLotteryText1();
                            //DrawLotteryText2();
                            //DrawLotteryText3();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(iLayer, iState);
                }
                if (LMan.chooseRandom)
                {
                    var lotteryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                    var lotteryState = new LegacyGameInterfaceLayer("CastledsContent: Interface Logic 1",
                        delegate
                        {
                            DrawLotteryText();
                            //DrawLotteryText1();
                            //DrawLotteryText2();
                            //DrawLotteryText3();
                            return true;
                        },
                        InterfaceScaleType.UI);
                    layers.Insert(lotteryLayer, lotteryState);
                }
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (TabletState?.CurrentState != null)
                TabletState.Update(gameTime);
        }
        internal void ShowTabletUI()
        {
            TabletState?.SetState(tabletUI);
        }
        internal void HideTabletUI()
        {
            TabletState?.SetState(null);
        }
        private static void DrawLotteryWait()
        {
            DynamicSpriteFont fontType = Main.fontMouseText;
            string wait = "Waiting for Players...";
            string husk = "";
            Players();
            void Players()
            {
                husk.Insert(0, "Participants: ");
                for (int a = 0; a < 255; a++)
                    if (LMan.IsParticpant(a))
                        husk.Insert(0, $"{Main.player[a].name},");
            }
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, wait, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 210)), Color.White, 0f, Main.fontMouseText.MeasureString(wait) / 2, 2f, SpriteEffects.None, 0f);
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, husk, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 130)), Color.White, 0f, Main.fontMouseText.MeasureString(husk) / 2, 0.85f, SpriteEffects.None, 0f);
        }
        private static void DrawLotteryBegin()
        {
            //Main.NewText(Main.hoverItemName);
            instance.timer1++;
            int multiplier = 2;
            DynamicSpriteFont fontType = Main.fontMouseText;
            Color GetTitleAlpha(Color newColor)
            {
                instance.title2Alpha += 1 * multiplier;
                int r = newColor.R + (int)((double)instance.title2Alpha * (LMan.titleColor.R * 0.02));
                int g = newColor.G + (int)((double)instance.title2Alpha * (LMan.titleColor.G * 0.02));
                int b = newColor.B + (int)((double)instance.title2Alpha * (LMan.titleColor.B * 0.02));
                int num = newColor.A + (int)((double)instance.title2Alpha * 0.4);
                if (num < 0)
                {
                    num = 0;
                }
                if (num > 255)
                {
                    num = 255;
                }
                if (instance.title2Alpha > 255)
                    instance.title2Alpha = 255;
                return new Color(r, g, b, num);
            }
            Color GetTitleAlpha1(Color newColor)
            {
                instance.title3Alpha += 1 * multiplier;
                int r = newColor.R + (int)((double)instance.title3Alpha * (LMan.titleColor.R * 0.02));
                int g = newColor.G + (int)((double)instance.title3Alpha * (LMan.titleColor.G * 0.02));
                int b = newColor.B + (int)((double)instance.title3Alpha * (LMan.titleColor.B * 0.02));
                int num = newColor.A + (int)((double)instance.title3Alpha * 0.4);
                if (num < 0)
                {
                    num = 0;
                }
                if (num > 255)
                {
                    num = 255;
                }
                if (instance.title3Alpha > 255)
                    instance.title3Alpha = 255;
                return new Color(r, g, b, num);
            }
            string refrain = $"Refrain from possessing {LMan.groupContra} Contrabande in total.";
            string maintain = $"Maintain {LMan.minItemQuota} items in your inventory.";
            string personal = "Don't forget to check your personal storage!";
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, refrain, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 210)), GetTitleAlpha(Color.Transparent), 0f, Main.fontMouseText.MeasureString(refrain) / 2, 2f, SpriteEffects.None, 0f);
            if (instance.timer1 > 300)
                multiplier = -2;
            if (instance.timer1 > 180)
            {
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, maintain, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 160)), GetTitleAlpha1(Color.Transparent), 0f, Main.fontMouseText.MeasureString(maintain) / 2, 1f, SpriteEffects.None, 0f);
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, personal, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 140)), GetTitleAlpha1(Color.Transparent), 0f, Main.fontMouseText.MeasureString(personal) / 2, 0.75f, SpriteEffects.None, 0f);
            }
            if (instance.timer1 > 420)
            {
                LMan.begin = false;
                LMan.itemCheck = true;
                LMan.start = true;
                instance.timer1 = 0;
            }
        }
        private static void DrawLotteryText()
        {
            if (LMan.chooseRandom)
            {
                //Player player = Main.player[Main.myPlayer];
                //Vector2 vec = new Vector2(player.position.X, player.position.Y - 50);
                if (LMan.counter > 0 && LMan.hasSucceeded)
                    CastledPlayer.CheckBanks(Main.player[Main.myPlayer]);
                //Player player = Main.player[Main.myPlayer];
                Color redColor = new Color(255, 0, 0, 0);
                Color orangeColor = new Color(255, 128, 0, 0);
                Color yellowColor = new Color(255, 255, 0, 0);
                Color greenColor = new Color(0, Main.rand.Next(195, 255), 0, Main.rand.Next(0, 15));
                Color blueColor = new Color(0, 0, Main.rand.Next(195, 255), Main.rand.Next(0, 15));
                if (!LMan.finalOutcome)
                {
                    redColor = GetSubAlpha(Color.Transparent, 1);
                    if (instance.subT2)
                        orangeColor = GetSubAlpha(Color.Transparent, 2);
                    if (instance.subT3)
                        yellowColor = GetSubAlpha(Color.Transparent, 3);
                    if (LMan.displayAltTitles)
                    {
                        greenColor = GetSubAlpha(Color.Transparent, 4);
                        blueColor = GetSubAlpha(Color.Transparent, 5);
                    }
                }
                DynamicSpriteFont fontType = Main.fontMouseText;
                string value1 = $"{LMan.is1}";
                string value2 = $"{LMan.is2}";
                string value3 = $"{LMan.is3}";
                string value4 = $"{LMan.ia1}";
                string value5 = $"{LMan.ia2}";
                string countUp = $"{LMan.counter}";
                string nextRound = $"Round {LMan.roundNum + 1} is starting shortly...";
                string Contrabande()
                {
                    if (CastledWorld.determineContraSp && Main.netMode == NetmodeID.MultiplayerClient)
                        return "Someone has Contrabande!";
                    else if (DetermineClient())
                        return $"You have Contrabande{CastledPlayer.BankContra()}!";
                    else
                        return "No one possesses Contrabande...";
                    bool DetermineClient()
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            return CastledPlayer.hasContra && CastledWorld.determineContraSp;
                        else
                            return CastledWorld.determineContraSp;
                    }
                }

                Color GetTitleAlpha(Color newColor)
                {
                    instance.titleAlpha += 1;
                    int r = newColor.R + (int)((double)instance.titleAlpha * (LMan.titleColor.R / 100));
                    int g = newColor.G + (int)((double)instance.titleAlpha * (LMan.titleColor.G / 100));
                    int b = newColor.B + (int)((double)instance.titleAlpha * (LMan.titleColor.B / 100));
                    #region Commented Alpha
                    /*
                    int num = newColor.A + (int)((double)titleAlpha * 0.4);
                    if (num < 0)
                    {
                        num = 0;
                    }
                    if (num > 255)
                    {
                        num = 255;
                    }
                    Main.NewText($"{r} {g} {b} {num}");
                    if (titleAlpha > 255)
                        titleAlpha = 255;
                    */
                    #endregion
                    if (instance.titleAlpha > 100)
                        instance.titleAlpha = 100;
                    return new Color(r, g, b, 0);
                }
                Color GetSubAlpha(Color newColor, int type)
                {
                    switch (type)
                    {
                        case 1:
                            instance.sub1 += 1;
                            int r = newColor.R + (int)((double)instance.sub1 * 2.5);
                            int g = newColor.G + (int)((double)instance.sub1 * 2.5);
                            int b = newColor.B + (int)((double)instance.sub1 * 2.5);
                            int num = newColor.A + (int)((double)instance.sub1 * 0.4);
                            if (num < 0)
                            {
                                num = 0;
                            }
                            if (num > 255)
                            {
                                num = 255;
                            }
                            if (instance.sub1 > 255)
                                instance.sub1 = 255;
                            return new Color(r, g, b, num);
                        case 2:
                            instance.sub2 += 1;
                            int r2 = newColor.R + (int)((double)instance.sub2 * 2.5);
                            int g2 = newColor.G + (int)((double)instance.sub2 * 2.5);
                            int b2 = newColor.B + (int)((double)instance.sub2 * 2.5);
                            int num2 = newColor.A + (int)((double)instance.sub2 * 0.4);
                            if (num2 < 0)
                            {
                                num2 = 0;
                            }
                            if (num2 > 255)
                            {
                                num2 = 255;
                            }
                            if (instance.sub2 > 255)
                                instance.sub2 = 255;
                            return new Color(r2, g2, b2, num2);
                        case 3:
                            instance.sub3 += 1;
                            int r3 = newColor.R + (int)((double)instance.sub3 * 2.5);
                            int g3 = newColor.G + (int)((double)instance.sub3 * 2.5);
                            int b3 = newColor.B + (int)((double)instance.sub3 * 2.5);
                            int num3 = newColor.A + (int)((double)instance.sub3 * 0.4);
                            if (num3 < 0)
                            {
                                num3 = 0;
                            }
                            if (num3 > 255)
                            {
                                num3 = 255;
                            }
                            if (instance.sub3 > 255)
                                instance.sub3 = 255;
                            return new Color(r3, g3, b3, num3);
                        case 4:
                            instance.sub1a += 1;
                            int r1a = newColor.R + (int)((double)instance.sub1a * Main.rand.Next(0, 1));
                            int g1a = newColor.G + (int)((double)instance.sub1a * Main.rand.Next(0, 2));
                            int b1a = newColor.B + (int)((double)instance.sub1a * Main.rand.Next(0, 1));
                            int num1a = newColor.A + (int)((double)instance.sub1a * Main.rand.Next(0, 1));
                            if (num1a < 0)
                            {
                                num1a = 0;
                            }
                            if (num1a > 255)
                            {
                                num1a = 255;
                            }
                            if (instance.sub1a > 255)
                                instance.sub1a = 255;
                            return new Color(r1a, g1a, b1a, num1a);
                        case 5:
                            instance.sub2a += 1;
                            int r2a = newColor.R + (int)((double)instance.sub2a * Main.rand.Next(0, 1));
                            int g2a = newColor.G + (int)((double)instance.sub2a * Main.rand.Next(0, 1));
                            int b2a = newColor.B + (int)((double)instance.sub2a * Main.rand.Next(0, 2));
                            int num2a = newColor.A + (int)((double)instance.sub2a * Main.rand.Next(0, 1));
                            if (num2a < 0)
                            {
                                num2a = 0;
                            }
                            if (num2a > 255)
                            {
                                num2a = 255;
                            }
                            if (instance.sub2a > 255)
                                instance.sub2a = 255;
                            return new Color(r2a, g2a, b2a, num2a);
                    }
                    return Color.White;
                }
                Color TimerColor()
                {
                    if (LMan.roundNum < 15)
                    {
                        if (CastledWorld.determineContraSp)
                            return Color.DarkRed;
                        else
                            return Color.White;
                    }
                    else
                    {
                        if (CastledWorld.determineContraSp)
                            return new Color(Main.DiscoR - 35 / 2, 0, Main.DiscoB - 150);
                        else
                            return new Color(Main.DiscoR / 2, 55, Main.DiscoB - 75 / 2);
                    }
                }
                int VibrateTimer(int? type, int type2)
                {
                    bool con = ModContent.GetInstance<ClientConfig>().contraSprite;
                    switch (type2)
                    {
                        case 1:
                            switch (type)
                            {
                                case 1:
                                    {
                                        if (CastledWorld.determineContraSp)
                                            return Main.rand.Next(-2, 2);
                                        else
                                            return 0;
                                    }
                                case 2:
                                    {
                                        if (con)
                                        {
                                            if (CastledWorld.determineContraSp)
                                                return Main.rand.Next(-54, -46);
                                            else
                                                return -50;
                                        }
                                        else
                                        {
                                            if (CastledWorld.determineContraSp)
                                                return Main.rand.Next(146, 154);
                                            else
                                                return 150;
                                        }
                                    }
                            }
                            break;
                        case 2:
                            if (con)
                                return 70;
                            else
                                return -130;
                        case 3:
                            if (con)
                                return 90;
                            else
                                return -110;
                    }
                    return 0;
                }
                int inverse = instance.titleAlpha;
                Color GetTimerColor(Color newColor, int type)
                {
                    if (LMan.alphaC > 0)
                    {
                        inverse -= 2;
                        int r = newColor.R + (int)((double)inverse * (LMan.titleColor.R * 0.02));
                        int g = newColor.G + (int)((double)inverse * (LMan.titleColor.G * 0.02));
                        int b = newColor.B + (int)((double)inverse * (LMan.titleColor.B * 0.02));
                        int num = newColor.A + (int)((double)inverse * 0.4);
                        if (num < 0)
                        {
                            num = 0;
                        }
                        if (num > 255)
                        {
                            num = 255;
                        }
                        if (inverse > 255)
                            inverse = 255;
                        return new Color(r, g, b, num);
                    }
                    else
                    {
                        switch (type)
                        {
                            case 1:
                                return TimerColor();
                            case 2:
                                return Color.White;
                        }
                    }
                    return Color.White;
                }
                if (ModContent.GetInstance<ClientConfig>().contraSprite)
                {
                    #region Draw Item Sprites
                    Texture2D item1 = Main.itemTexture[LMan.synci1];
                    Texture2D item2 = Main.itemTexture[LMan.synci2];
                    Texture2D item3 = Main.itemTexture[LMan.synci3];
                    Texture2D item1a = Main.itemTexture[LMan.ib1];
                    Texture2D item2a = Main.itemTexture[LMan.ib2];
                    Rectangle item1Rect = new Rectangle(0, 0, item1.Width, item1.Height);
                    Rectangle item2Rect = new Rectangle(0, 0, item2.Width, item2.Height);
                    Rectangle item3Rect = new Rectangle(0, 0, item3.Width, item3.Height);
                    Rectangle item1aRect = new Rectangle(0, 0, item1a.Width, item1a.Height);
                    Rectangle item2aRect = new Rectangle(0, 0, item2a.Width, item2a.Height);
                    Vector2 itemPos = new Vector2((float)(Main.screenWidth / 2 - 95), (float)(Main.screenHeight / 2 + 20));
                    Vector2 item2Pos = new Vector2((float)(Main.screenWidth / 2 + 215), (float)(Main.screenHeight / 2 + 20));
                    Vector2 item3Pos = new Vector2((float)(Main.screenWidth / 2 + 60), (float)(Main.screenHeight / 2 - 50));
                    Vector2 item1aPos = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + 20));
                    Vector2 item2aPos = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 20));
                    /*
                    Color redNew = redColor;
                    Color orangeNew = orangeColor;
                    Color yellowNew = yellowColor;
                    */
                    if (Main.itemAnimationsRegistered.Contains(LMan.synci1))
                    {
                        int FrameCount = Main.itemAnimations[LMan.synci1].FrameCount;
                        int TicksPerFrame = Main.itemAnimations[LMan.synci1].TicksPerFrame;
                        Update();
                        void Update()
                        {
                            if (++instance.FrameCounter1 >= TicksPerFrame)
                            {
                                instance.FrameCounter1 = 0;
                                if (++instance.Frame1 >= FrameCount)
                                {
                                    instance.Frame1 = 0;
                                }
                            }
                        }
                        item1Rect = item1.Frame(1, FrameCount, 0, instance.Frame1);
                    }
                    if (Main.itemAnimationsRegistered.Contains(LMan.synci2))
                    {
                        int FrameCount = Main.itemAnimations[LMan.synci2].FrameCount;
                        int TicksPerFrame = Main.itemAnimations[LMan.synci2].TicksPerFrame;
                        Update();
                        void Update()
                        {
                            if (++instance.FrameCounter2 >= TicksPerFrame)
                            {
                                instance.FrameCounter2 = 0;
                                if (++instance.Frame2 >= FrameCount)
                                {
                                    instance.Frame2 = 0;
                                }
                            }
                        }
                        item2Rect = item2.Frame(1, FrameCount, 0, instance.Frame2);
                    }
                    if (Main.itemAnimationsRegistered.Contains(LMan.synci3))
                    {
                        int FrameCount = Main.itemAnimations[LMan.synci3].FrameCount;
                        int TicksPerFrame = Main.itemAnimations[LMan.synci3].TicksPerFrame;
                        Update();
                        void Update()
                        {
                            if (++instance.FrameCounter3 >= TicksPerFrame)
                            {
                                instance.FrameCounter3 = 0;
                                if (++instance.Frame3 >= FrameCount)
                                {
                                    instance.Frame3 = 0;
                                }
                            }
                        }
                        item3Rect = item3.Frame(1, FrameCount, 0, instance.Frame3);
                    }
                    if (Main.itemAnimationsRegistered.Contains(LMan.ib1))
                    {
                        int FrameCount = Main.itemAnimations[LMan.ib1].FrameCount;
                        int TicksPerFrame = Main.itemAnimations[LMan.ib1].TicksPerFrame;
                        Update();
                        void Update()
                        {
                            if (++instance.FrameCounter1Inv >= TicksPerFrame)
                            {
                                instance.FrameCounter1Inv = 0;
                                if (++instance.Frame1Inv >= FrameCount)
                                {
                                    instance.Frame1Inv = 0;
                                }
                            }
                        }
                        item1aRect = item1a.Frame(1, FrameCount, 0, instance.Frame1Inv);
                    }
                    if (Main.itemAnimationsRegistered.Contains(LMan.ib2))
                    {
                        int FrameCount = Main.itemAnimations[LMan.ib2].FrameCount;
                        int TicksPerFrame = Main.itemAnimations[LMan.ib2].TicksPerFrame;
                        Update();
                        void Update()
                        {
                            if (++instance.FrameCounter2Inv >= TicksPerFrame)
                            {
                                instance.FrameCounter2Inv = 0;
                                if (++instance.Frame2Inv >= FrameCount)
                                {
                                    instance.Frame2Inv = 0;
                                }
                            }
                        }
                        item2aRect = item2a.Frame(1, FrameCount, 0, instance.Frame2Inv);
                    }
                    Main.spriteBatch.Draw(item1, itemPos, item1Rect, redColor, 0f, new Vector2(((item1Rect.Width + 60) / 2) + 25, ((item1Rect.Height + 90) / 2) + 50), 1f, SpriteEffects.None, 0f);
                    if (instance.subT2)
                        Main.spriteBatch.Draw(item2, item2Pos, item2Rect, orangeColor, 0f, new Vector2(((item2Rect.Width + 60) / 2) + 35, ((item2Rect.Height + 90) / 2) + 50), 1f, SpriteEffects.None, 0f);
                    if (instance.subT3)
                        Main.spriteBatch.Draw(item3, item3Pos, item3Rect, yellowColor, 0f, new Vector2(((item3Rect.Width + 60) / 2) + 30, ((item3Rect.Height + 90) / 2) + 50), 1f, SpriteEffects.None, 0f);
                    if (LMan.displayAltTitles)
                    {
                        Main.spriteBatch.Draw(item1a, item1aPos, item1aRect, greenColor, 0f, new Vector2(((item1aRect.Width + 60) / 2) + 90, ((item1aRect.Height + 90) / 2) - 20), 1f, SpriteEffects.None, 0f);
                        if (instance.subT2a)
                            Main.spriteBatch.Draw(item2a, item2aPos, item2aRect, blueColor, 0f, new Vector2(((item2aRect.Width + 60) / 2) - 150, ((item2aRect.Height + 90) / 2) - 60), 1f, SpriteEffects.None, 0f);
                    }
                    #endregion
                }
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, Main.fontDeathText, LMan.displayTitle, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 210)), GetTitleAlpha(Color.Transparent), 0f, Main.fontDeathText.MeasureString(LMan.displayTitle) / 2, 1.15f, SpriteEffects.None, 0f);
                //Main.NewText(Main.itemTexture.Length);
                if (LMan.drawCountdown)
                {
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, countUp, new Vector2((float)(Main.screenWidth / 2 - VibrateTimer(1, 1)), (float)(Main.screenHeight / 2 - VibrateTimer(2, 1))), GetTimerColor(Color.Transparent, 1), 0f, Main.fontMouseText.MeasureString(countUp) / 2, 2f, SpriteEffects.None, 0f);
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, Contrabande(), new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + VibrateTimer(null, 2))), GetTimerColor(Color.Transparent, 2), 0f, Main.fontMouseText.MeasureString(Contrabande()) / 2, 0.75f, SpriteEffects.None, 0f);
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, nextRound, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + VibrateTimer(null, 3))), GetTimerColor(Color.Transparent, 2), 0f, Main.fontMouseText.MeasureString(nextRound) / 2, 0.75f, SpriteEffects.None, 0f);
                }
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, value1, new Vector2((float)(Main.screenWidth / 2 - 150), (float)(Main.screenHeight / 2 - 35)), redColor, 0f, Main.fontMouseText.MeasureString(value1) / 2, 1f, SpriteEffects.None, 0f);
                if (instance.subT2)
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, value2, new Vector2((float)(Main.screenWidth / 2 + 150), (float)(Main.screenHeight / 2 - 35)), orangeColor, 0f, Main.fontMouseText.MeasureString(value2) / 2, 1f, SpriteEffects.None, 0f);
                if (instance.subT3)
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, value3, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 105)), yellowColor, 0f, Main.fontMouseText.MeasureString(value3) / 2, 1f, SpriteEffects.None, 0f);
                if (LMan.displayAltTitles)
                {
                    DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, value4, new Vector2((float)(Main.screenWidth / 2 - 120), (float)(Main.screenHeight / 2 + 35)), greenColor, 0f, Main.fontMouseText.MeasureString(value4) / 2, 1f, SpriteEffects.None, 0f);
                    if (instance.subT2a)
                        DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, value5, new Vector2((float)(Main.screenWidth / 2 + 120), (float)(Main.screenHeight / 2 + 35)), blueColor, 0f, Main.fontMouseText.MeasureString(value5) / 2, 1f, SpriteEffects.None, 0f);
                }
                //Main.instance.DrawPlayer(player, vec, player.fullRotation, player.fullRotationOrigin, 0f);
                // - Main.fontDeathText.MeasureString(displayTitle).X
                //DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, displayTitle, new Vector2((float)(Main.screenWidth / 2 - 160), (float)(Main.screenHeight / 2 - 210)), titleColor, 0f, Main.fontMouseText.MeasureString(displayTitle), 1.8f, SpriteEffects.None, 0f);
                CastledWorld.determineContraSp = false;
            }
        }
        private static void DrawShopItem()
        {
            //Main.NewText(Main.hoverItemName);
            DynamicSpriteFont fontType = Main.fontMouseText;
            #region Draw Item
            Color GetTitleItemAlpha(Color newColor)
            {
                instance.ia += 5;
                int r = newColor.R + (int)((double)instance.ia * (Color.White.G * 0.02));
                int g = newColor.G + (int)((double)instance.ia * (Color.White.A * 0.02));
                int b = newColor.B + (int)((double)instance.ia * (Color.White.B * 0.02));
                int num = newColor.A + (int)((double)instance.ia * 0.4);
                if (num < 0)
                {
                    num = 0;
                }
                if (num > 255)
                {
                    num = 255;
                }
                if (instance.ia > 255)
                    instance.ia = 255;
                return new Color(r, g, b, num);
            }
            Texture2D item1 = Main.itemTexture[LMan.displayItemType];
            Rectangle item1Rect = new Rectangle(0, 0, item1.Width, item1.Height);
            Vector2 itemPos = new Vector2((float)(Main.screenWidth / 2 + 60), (float)(Main.screenHeight / 2 - 190));
            if (Main.itemAnimationsRegistered.Contains(LMan.displayItemType))
            {
                int FrameCount = Main.itemAnimations[LMan.displayItemType].FrameCount;
                int TicksPerFrame = Main.itemAnimations[LMan.displayItemType].TicksPerFrame;
                Update();
                void Update()
                {
                    if (++instance.FrameCounter1 >= TicksPerFrame)
                    {
                        instance.FrameCounter1 = 0;
                        if (++instance.Frame1 >= FrameCount)
                        {
                            instance.Frame1 = 0;
                        }
                    }
                }
                item1Rect = item1.Frame(1, FrameCount, 0, instance.Frame1);
            }
            Main.spriteBatch.Draw(item1, itemPos, item1Rect, GetTitleItemAlpha(Color.Transparent), 0f, new Vector2(((item1Rect.Width + 60) / 2) + 25, ((item1Rect.Height + 90) / 2) + 50), 1f, SpriteEffects.None, 0f);
            #endregion
            string refrain = $"An item has been materialized";
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, refrain, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 250)), GetTitleItemAlpha(Color.Transparent), 0f, Main.fontMouseText.MeasureString(refrain) / 2, 1f, SpriteEffects.None, 0f);
        }
        private static void DrawPresetUI(CastledPlayer modP)
        {
            #region
            bool configHelp = ModContent.GetInstance<ClientConfig>().vanityItemHelp;
            Player player = Main.player[Main.myPlayer];
            DynamicSpriteFont fontType = Main.fontMouseText;
            PlayerPreset preset = modP.presets[modP.preset];
            string write = "Write Name";
            string input = Main.chatText;
            string gok;
            string help = "";
            string num = $"#{modP.preset + 1}";
            if (!preset.made)
                gok = "Empty Preset";
            else
                gok = preset.name;
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, gok, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 120)), Color.White, 0f, Main.fontMouseText.MeasureString(gok) / 2, 1f, SpriteEffects.None, 0f);
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, num, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 100)), Color.White, 0f, Main.fontMouseText.MeasureString(num) / 2, 1f, SpriteEffects.None, 0f);
            if (modP.writeName || modP.changeName)
            {
                if (!modP.drawPreview && configHelp)
                    help = "Put the name you want to set in the chat box, then hold Left and Right Click to save it.";
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, write, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 170)), Color.White, 0f, Main.fontMouseText.MeasureString(write) / 2, 1f, SpriteEffects.None, 0f);
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, input, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 - 150)), Color.White, 0f, Main.fontMouseText.MeasureString(input) / 2, 1f, SpriteEffects.None, 0f);
            }
            if (modP.drawPreview && configHelp)
                help = "Use your Preset Navigation hotkey to navigate through this menu.\nHold certain keys, and then press the hotkey to perform an action:\nUp - Go to the next preset\nDown - Go to the previous preset\nRight - Change the name of an existing preset\nHold Left and Right Click to save a preset (unless renaming one)\nSimply switch to a preset, and then hold O and your Navigation Hotkey to use it!\nHold the Delete key and press the Navigation hotkey to erase a preset";
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, help, new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2 + 120)), Color.White, 0f, Main.fontMouseText.MeasureString(help) / 2, 0.75f, SpriteEffects.None, 0f);
            if (preset.made && modP.drawPreview)
            {
                string DirectoryGender()
                {
                    string genderSt = "Male";

                    if (!preset.preview.Male)
                        genderSt = "Female";
                    return $"CastledsContent/Content/CharPreset/ClothStyle{genderSt}";
                }
                Color GetIconAlpha()
                {
                    int r = Color.Transparent.R + 65;
                    int g = Color.Transparent.G + 65;
                    int b = Color.Transparent.B + 155;
                    return new Color(r, g, b, 175);
                }
                Color GetAlpha()
                {
                    int r = Color.Transparent.R + 45;
                    int g = Color.Transparent.G + 45;
                    int b = Color.Transparent.B + 125;
                    return new Color(r, g, b, 125);
                }
                Texture2D gender = ModContent.GetTexture(DirectoryGender());
                Rectangle iconRect = new Rectangle(0, 0, 34, 36);
                Main.spriteBatch.Draw(ModContent.GetTexture("CastledsContent/Content/CharPreset/PresetPreviewBG"), new Vector2((float)(Main.screenWidth / 2) - 25, (float)(Main.screenHeight / 2 - 220)), new Rectangle(0, 0, 52, 76), GetAlpha(), 0f, preset.preview.fullRotationOrigin, 1f, SpriteEffects.None, 0f);
                Main.instance.DrawPlayer(preset.preview, new Vector2(player.position.X, player.position.Y - 175), preset.preview.fullRotation, preset.preview.fullRotationOrigin, 0f);

                Main.spriteBatch.Draw(ModContent.GetTexture("CastledsContent/Content/CharPreset/GenderIcon"), new Vector2((float)(Main.screenWidth / 2) - 15, (float)(Main.screenHeight / 2 - 235)), new Rectangle(0, 0, 32, 32), GetIconAlpha(), 0f, preset.preview.fullRotationOrigin, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(gender, new Vector2((float)(Main.screenWidth / 2) - 12.5f, (float)(Main.screenHeight / 2 - 232.5f)), iconRect, Color.White, 0f, preset.preview.fullRotationOrigin, 0.75f, SpriteEffects.None, 0f);
            }
            #endregion
            #region
            /*
            int newGender = 1 + 18 / (3 * 3) * (10 / 5 / 2) - 1;
            int equip = newGender * newGender - 1;
            Player newPlayer = new Player
            {
                #region
                direction = -1,
                gravDir = 1f,
                socialIgnoreLight = true,
                #endregion
            };
            newPlayer.Male = GetGender();
            bool GetGender()
            {
                switch (newGender)
                {
                    case 1:
                        {
                            newPlayer.skinVariant = 3;
                            newPlayer.shirtColor = Color.Maroon;
                            newPlayer.underShirtColor = Color.DarkRed;
                            newPlayer.pantsColor = Color.Navy;
                            newPlayer.shoeColor = Color.Black;
                            for (int a = 0; a < newPlayer.armor.Length; a++)
                            {
                                Item item = new Item();
                                switch (a)
                                {
                                    case 10:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.TaxCollectorHat);
                                        }
                                        break;
                                    case 13:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.Toolbelt);
                                        }
                                        break;
                                    case 14:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(GetEquip());
                                        }
                                        break;
                                    case 15:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.GoldWatch);
                                        }
                                        break;
                                }
                            }
                            for (int a = 0; a < newPlayer.dye.Length; a++)
                            {
                                Item item = new Item();
                                switch (a)
                                {
                                    case 1:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveGoldDye);
                                        }
                                        break;
                                    case 3:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveSilverDye);
                                        }
                                        break;
                                    case 4:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveSilverDye);
                                        }
                                        break;
                                    case 5:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveCopperDye);
                                        }
                                        break;
                                }
                            }
                            return true;
                        }
                    case 2:
                        {
                            newPlayer.hairColor = Color.LightGoldenrodYellow;
                            newPlayer.pantsColor = Color.BurlyWood;
                            newPlayer.shoeColor = Color.DarkOliveGreen;
                            newPlayer.hair = 88;
                            for (int a = 0; a < newPlayer.armor.Length; a++)
                            {
                                Item item = new Item();
                                switch (a)
                                {
                                    case 10:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(GetEquip());
                                        }
                                        break;
                                    case 11:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.Yoraiz0rShirt);
                                        }
                                        break;
                                    case 13:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.Shackle);
                                        }
                                        break;
                                    case 14:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.FlowerBoots);
                                        }
                                        break;
                                }
                            }
                            for (int a = 0; a < newPlayer.dye.Length; a++)
                            {
                                Item item = new Item();
                                switch (a)
                                {
                                    case 0:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveSilverDye);
                                        }
                                        break;
                                    case 1:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveGoldDye);
                                        }
                                        break;
                                    case 3:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.ReflectiveCopperDye);
                                        }
                                        break;
                                    case 4:
                                        {
                                            player.armor[a] = item;
                                            player.armor[a].SetDefaults(ItemID.BrownAndBlackDye);
                                        }
                                        break;
                                }
                            }
                            return false;
                        }
                }
                return true;
            }
            int GetEquip()
            {
                switch (equip)
                {
                    case 0:
                        return ItemID.ManaRegenerationBand;
                    case 3:
                        return ItemID.ScarecrowHat;
                }
                return ItemID.None;
            }
            //Don't worry about these lines below.
            Vector2 drawOffset = new Vector2(player.position.X + 75, player.position.Y - 175);
            newPlayer.PlayerFrame();
            Main.instance.DrawPlayer(newPlayer, drawOffset, preset.preview.fullRotation, preset.preview.fullRotationOrigin, 0f);
            */
            #endregion
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

            #region Angler Quest Accessories
            #region Sextant
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Sextant);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.WeatherRadio);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Sextant);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.FishermansGuide);
            recipe.AddRecipe();
            #endregion
            #region Weather Radio
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WeatherRadio);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.Sextant);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WeatherRadio);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.FishermansGuide);
            recipe.AddRecipe();
            #endregion
            #region Fisherman's Pocket Guide
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.FishermansGuide);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.Sextant);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.FishermansGuide);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.WeatherRadio);
            recipe.AddRecipe();
            #endregion
            #endregion
        }
        public static string ModName(int type)
        {
            switch(type)
            {
                case 0:
                    return "CalamityMod";
                case 1:
                    return "ThoriumMod";
                case 2:
                    return "Fargowiltas";
                case 3:
                    return "FargowiltasSouls";
            }
            return string.Empty;
        }
        public static bool ModLoaded(int type) => ModLoader.GetMod(ModName(type)) != null;
    }
    class RemakePHIList : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "pedrefresh"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            CastledsContent.instance.images.Clear();
            for (int a = 0; a < Main.itemTexture.Length; a++)
                CastledsContent.instance.images.Add(new PIH(a, Main.itemTexture[a] == null));
            foreach (PIH p in CastledsContent.instance.images)
                if (!p.voiden)
                    p.CreateTexture();
        }
    }
    class RefreshDuration : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "setzero"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (Main.player[Main.myPlayer].GetModPlayer<NPCs.Tarr.IncPlayer>().godMode)
                Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>().superintendentDelay = 0;
        }
    }
    class QuestReset : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "resetquests"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            CastledPlayer player = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
            for (int a = 0; a < player.witchQuest.Length; a++)
                player.witchQuest[a] = 0;
            Main.NewText("Quests have been reset");
        }
    }
    public class CompatRecipes : GlobalRecipe
    {
        readonly Mod fargoS = ModLoader.GetMod(CastledsContent.ModName(CastledsContent.FargoSoul));
        public override void OnCraft(Item item, Recipe recipe)
        {
            Player player = Main.player[Main.myPlayer];
            if (CastledsContent.ModLoaded(CastledsContent.FargoSoul) && item.type == fargoS.ItemType("AnglerEnchantment"))
            {
                player.QuickSpawnItem(ItemID.AnglerHat);
                player.QuickSpawnItem(ItemID.AnglerVest);
                player.QuickSpawnItem(ItemID.AnglerPants);
            }
        }
    }
    /// <summary>
    /// Pedestal Image Helper
    /// </summary>
    public class PIH
    {
        public Texture2D tex;
        public Color texColor;
        public int index;
        public bool voiden;
        public PIH(int dex, bool v) { index = dex; voiden = v; }
        public void CreateTexture()
        {
            tex = new Texture2D(Main.graphics.GraphicsDevice, Main.itemTexture[index] == null ? 1 : Main.itemTexture[index].Width, Main.itemTexture[index] == null ? 1 : Main.itemTexture[index].Height);
            Color[] array = new Color[Main.itemTexture[index].Width * Main.itemTexture[index].Height];
            Main.itemTexture[index].GetData(array);
            GetMeanColor();
            for (int a = 0; a < array.Length; a++)
            {
                if (array[a] != Color.Transparent)
                    array[a] = texColor;
            }
            tex.SetData(array);
        }
        void GetMeanColor()
        {
            Color[] array = new Color[Main.itemTexture[index].Width * Main.itemTexture[index].Height];
            int[] rgb = new int[4];
            Main.itemTexture[index].GetData(array);
            for(int a = 0; a < array.Length; a++)
            {
                if (array[a] != Color.Transparent)
                {
                    rgb[0] += array[a].R;
                    rgb[1] += array[a].G;
                    rgb[2] += array[a].B;
                    rgb[3] += array[a].A;
                }
            }
            texColor = new Color(rgb[0] / array.Length, rgb[1] / array.Length, rgb[2] / array.Length, rgb[3] / array.Length);
        }
    }
}
