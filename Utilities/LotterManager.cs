using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CastledsContent.Items.Accessories.RobotInvasion;
using System.Linq;
using IL.Terraria.Localization;

namespace CastledsContent.Utilities
{
    public static class LMan
    {
        public static int displayItemType = -1;
        //public static bool displayItem = false;
        public static bool exceptionPacket = false;
        public static LotPacket[] packets = new LotPacket[0];
        public readonly static List<string> loadedMods = new List<string>() { };
        public static List<int> setupShop = new List<int>();
        public static List<int> blacklist = new List<int>
        {
            ModContent.ItemType<RobotPlate>(),
            ModContent.ItemType<Items.Accessories.RobotInvasion.SpikeExoskeleton.SpikeExoskeleton>(),
            ModContent.ItemType<Items.Accessories.RobotInvasion.ReinforcedExoskeleton.ReinforcedExoskeleton>(),
            ModContent.ItemType<IronShield>(),
            ModContent.ItemType<AimbotArrow>(),
            ModContent.ItemType<Items.Weapons.Melee.SpikeArm>(),
            ModContent.ItemType<Items.Weapons.Ranged.RayGun>(),
            ModContent.ItemType<NPCs.Tarr.Incinerator>(),
            ModContent.ItemType<Items.Armor.Summoner.Lunatic.LHat>(),
            ModContent.ItemType<Items.Armor.Summoner.Lunatic.LShirt>(),
            ModContent.ItemType<Items.Armor.Summoner.Lunatic.LShoes>(),
            ModContent.ItemType<Items.Armor.Summoner.HavocWasp.HWHelmet>(),
            ModContent.ItemType<Items.Armor.Summoner.HavocWasp.HWBody>(),
            ModContent.ItemType<Items.Armor.Summoner.HavocWasp.HWLegs>()
        };
        public static string cond = "";
        #region Variables
        public static int lowNum = 0;
        public static int highNum = 2;
        public static int indexer;
        public static int slowdownMultiplier;
        public static int stagType;
        public static bool hasSucceeded;
        public static bool decideTheme = false;
        public static bool specialTheme = false;
        public static bool forceDiscard = false;
        //public static bool preHandleTheme = false;
        public static int roundNum = 1;
        public static int rarityTheme;
        //public static Color contraC = new Color(Main.DiscoR - (Color.DarkRed.R), Color.DarkGreen.G / 2, Color.DarkBlue.B);

        public static float sound1Mul = 0f;
        public static float sound2Mul = 0f;
        public static float sound3Mul = 0f;
        //public static bool weaponTheme;
        public static bool itemCheck;
        public static bool forceSpecial;
        public static bool start;
        public static bool chooseRandom;
        public static bool pickupPrevent;
        public static bool finalOutcome;
        public static bool item2;
        public static bool item3;
        public static bool begin;
        public static int i1; //i, for item
        public static int i2;
        public static int i3;
        public static int ib1;
        public static int ib2;
        public static int synci1;
        public static int synci2;
        public static int synci3;
        public static int groupContra;
        //public static int range1;
        //public static string gameTooltip;
        public static string departure = "";
        public static string is1; //s, for string
        public static string is2;
        public static string is3;
        public static string ia1;
        public static string ia2;
        //public static string ttColor; //tt, for tooltip
        public static Color titleColor;
        public static string displayTitle;
        public static string modTheme = "";
        public static string fargoCondition = "";
        public static bool displayAltTitles;
        public static bool cancelEvent;

        public static int counter1;
        public static int counter;
        public static int alphaC;
        public static bool drawCountdown;
        public static bool restrictPick;
        public static bool addInvList;
        public static int minItemQuota;

        #region Lists
        public readonly static List<int> numList = new List<int>() { };
        public readonly static List<string> nameList = new List<string>() { };
        public readonly static List<int> rareList = new List<int>() { };
        public readonly static List<int> damageList = new List<int>() { };
        public readonly static List<string> classList = new List<string>() { };
        public readonly static List<int> finalList = new List<int>() { };
        public readonly static List<string> modList = new List<string>() { };
        public readonly static List<string> extraMods = new List<string>() { };
        public readonly static List<int> inventoryList = new List<int>() { };
        public readonly static List<string> influMod = new List<string>() { };
        public readonly static List<string> influenceModList = new List<string>()
        { 
            "ThoriumMod",
            "CalamityMod",
            "SacredTools",
            "AAMod",
            "ElementsAwoken",
            "FargowiltasSouls",
            "Laugicality",
            "JoostMod",
            "SpiritMod",
            "QwertysRandomContent",
            "Redemption"
        };
        public static void GetItemQuota()
        {
            int total = 0;
            int participants = 0;
            for (int a = 0; a < 255; a++)
            {
                if (IsParticpant(a))
                {
                    participants++;
                    for (int b = 0; b < 58; b++)
                    {
                        if (!Main.player[a].inventory[b].IsAir)
                            total++;
                    }
                    for (int b = 0; b < 19; b++)
                    {
                        if (!Main.player[a].armor[b].IsAir)
                            total++;
                    }
                    for (int b = 0; b < 9; b++)
                    {
                        if (!Main.player[a].dye[b].IsAir)
                            total++;
                    }
                    for (int b = 0; b < 4; b++)
                    {
                        if (!Main.player[a].miscEquips[b].IsAir)
                            total++;
                        if (!Main.player[a].miscDyes[b].IsAir)
                            total++;
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (Main.expertMode)
                {
                    minItemQuota = (total / participants) / 2;
                    groupContra = minItemQuota * (participants / 3);
                }
                else
                {
                    minItemQuota = (total / participants) / 3;
                    groupContra = minItemQuota * (participants / 2);
                }
            }
            else
            {
                if (Main.expertMode)
                {
                    minItemQuota = total / 2;
                    groupContra = minItemQuota;
                }
                else
                {
                    minItemQuota = total / 3;
                    groupContra = minItemQuota;
                }
            }
            /*
             * ((All non-air items from every inventory / # of participating players) * 0.75 or 0.5)
            */
        }
        public static void CancelEvent()
        {
            cancelEvent = true;
            for (int a = 0; a < 255; a++)
                Main.player[a].GetModPlayer<CastledPlayer>().parti = false;
            departure = "The event has been cancelled.";
        }
        public static void ForceDiscard()
        {
            for (int a = 0; a < 255; a++)
            {
                Player parti = Main.player[a];
                if (IsParticpant(a))
                {
                    for (int b = 0; b < 58; b++)
                    {
                        Item item = parti.inventory[b];
                        if (!item.IsAir)
                        {
                            SGlobalItem sg = item.GetGlobalItem<SGlobalItem>();
                            if (sg.IsContrabande(item))
                                sg.ForceDiscard(parti, 1);
                            //Main.NewText($"{sg.IsContrabande(item)} {item.Name}");
                        }
                    }
                    for (int b = 0; b < 19; b++)
                    {
                        Item item = parti.armor[b];
                        if (!item.IsAir)
                        {
                            SGlobalItem sg = item.GetGlobalItem<SGlobalItem>();
                            if (sg.IsContrabande(item))
                                sg.ForceDiscard(parti, 2);
                        }
                    }
                    for (int b = 0; b < 9; b++)
                    {
                        Item item = parti.dye[b];
                        if (!item.IsAir)
                        {
                            SGlobalItem sg = item.GetGlobalItem<SGlobalItem>();
                            if (sg.IsContrabande(item))
                                sg.ForceDiscard(parti, 3);
                        }
                    }
                    for (int b = 0; b < 4; b++)
                    {
                        Item item = parti.miscEquips[b];
                        if (!item.IsAir)
                        {
                            SGlobalItem sg = item.GetGlobalItem<SGlobalItem>();
                            if (sg.IsContrabande(item))
                                sg.ForceDiscard(parti, 4);
                        }
                    }
                    for (int b = 0; b < 4; b++)
                    {
                        Item item = parti.miscDyes[b];
                        if (!item.IsAir)
                        {
                            SGlobalItem sg = item.GetGlobalItem<SGlobalItem>();
                            if (sg.IsContrabande(item))
                                sg.ForceDiscard(parti, 5);
                        }
                    }
                    for (int b = 0; b < 40; b++)
                    {
                        Item item1 = parti.bank.item[b];
                        Item item2 = parti.bank2.item[b];
                        Item item3 = parti.bank3.item[b];
                        if (!item1.IsAir)
                        {
                            SGlobalItem sg1 = item1.GetGlobalItem<SGlobalItem>();
                            if (item1.GetGlobalItem<SGlobalItem>().IsContrabande(item1))
                                sg1.ForceDiscard(parti, 6);
                        }
                        if (!item2.IsAir)
                        {
                            SGlobalItem sg2 = item2.GetGlobalItem<SGlobalItem>();
                            if (item2.GetGlobalItem<SGlobalItem>().IsContrabande(item2))
                                sg2.ForceDiscard(parti, 7);
                        }
                        if (!item3.IsAir)
                        {
                            SGlobalItem sg3 = item3.GetGlobalItem<SGlobalItem>();
                            if (item3.GetGlobalItem<SGlobalItem>().IsContrabande(item3))
                                sg3.ForceDiscard(parti, 8);
                        }
                    }
                }
            }
        }
        public static void InventoryScan()
        {
            for (int a = 0; a < 255; a++)
            {
                Player parti = Main.player[a];
                if (IsParticpant(a))
                {
                    for (int b = 0; b < 58; b++)
                    {
                        Item item = parti.inventory[b];
                        if (!item.IsAir && item.type < 3930)
                            inventoryList.Add(item.type);
                    }
                    for (int b = 0; b < 19; b++)
                    {
                        Item item = parti.armor[b];
                        if (!item.IsAir && item.type < 3930)
                            inventoryList.Add(item.type);
                    }
                    for (int b = 0; b < 9; b++)
                    {
                        Item item = parti.dye[b];
                        if (!item.IsAir && item.type < 3930)
                            inventoryList.Add(item.type);
                    }
                    for (int b = 0; b < 4; b++)
                    {
                        Item item = parti.miscEquips[b];
                        if (!item.IsAir && item.type < 3930)
                            inventoryList.Add(item.type);
                    }
                    for (int b = 0; b < 4; b++)
                    {
                        Item item = parti.miscDyes[b];
                        if (!item.IsAir && item.type < 3930)
                            inventoryList.Add(item.type);
                    }
                    for (int b = 0; b < 40; b++)
                    {
                        Item item1 = parti.bank.item[b];
                        Item item2 = parti.bank2.item[b];
                        Item item3 = parti.bank3.item[b];
                        if (!item1.IsAir)
                            inventoryList.Add(item1.type);
                        if (!item2.IsAir)
                            inventoryList.Add(item2.type);
                        if (!item3.IsAir)
                            inventoryList.Add(item3.type);
                    }
                }
            }
        }
        #endregion
        #endregion
        public static bool IsParticpant(int index)
        {
            if (Main.player[index].active)
                return Main.player[index].GetModPlayer<CastledPlayer>().parti;
            return false;
        }
        public static void SyncText()
        {
            is1 = nameList[i1];
            is2 = nameList[i2];
            is3 = nameList[i3];
            synci1 = numList[nameList.IndexOf(is1)];
            synci2 = numList[nameList.IndexOf(is2)];
            synci3 = numList[nameList.IndexOf(is3)];

            if (stagType > 3)
            {
                ia1 = nameList[ib1];
                ia2 = nameList[ib2];
                ib1 = numList[nameList.IndexOf(ia1)];
                ib2 = numList[nameList.IndexOf(ia2)];
            }
        }
        public static void AdjustRarity()
        {
            if (NPC.downedBoss3 && !Main.hardMode)
                highNum = 3;
            else if (Main.hardMode && !NPC.downedMechBossAny)
                highNum = 4;
            else if (Main.hardMode && NPC.downedMechBossAny && !NPC.downedPlantBoss)
                highNum = 5;
            else if (Main.hardMode && NPC.downedMechBossAny && NPC.downedPlantBoss && !NPC.downedGolemBoss)
            {
                if (Main.rand.Next(5) == 0)
                    lowNum = 1;
                highNum = 7;
            }
            else if (Main.hardMode && NPC.downedMechBossAny && NPC.downedPlantBoss && NPC.downedGolemBoss && !NPC.downedAncientCultist)
            {
                if (Main.rand.Next(3) == 0)
                    lowNum = 2;
                else if (Main.rand.Next(6) == 0)
                    lowNum = 3;
                highNum = 8;
            }
            else if (Main.hardMode && NPC.downedMechBossAny && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedAncientCultist)
            {
                if (Main.rand.Next(2) == 0)
                    lowNum = 1;
                else if (Main.rand.Next(4) == 0)
                    lowNum = 5;
                highNum = 9;
            }
            else
                highNum = 2;
            if (NPC.downedBoss2 || NPC.downedQueenBee)
                highNum++;
            if (NPC.downedFishron)
                highNum++;
        }
        /// <summary>
        /// Used for setting up vanilla themes.
        /// </summary>
        /// <param name="theme"></param>
        public static void SetTheme (int theme)
        {
            switch (theme)
            {
                case -15:
                    displayTitle = "Perishable Products";
                    titleColor = new Color(100, 0, 75);
                    //ttColor = "4B966E";
                    break;
                case -14:
                    displayTitle = "Atrophic Attire";
                    titleColor = new Color(75, 95, 150);
                    //ttColor = "4B5F96";
                    break;
                case -13:
                    displayTitle = "Construction Supplies";
                    titleColor = new Color(150, 0, 25);
                    //ttColor = "960019";
                    break;
                case -12:
                    displayTitle = "Collectibles Cleanup";
                    titleColor = new Color(125, 35, 0);
                    //ttColor = "7D2300";
                    break;
                case -11:
                    displayTitle = "The Vast Reliquary";
                    titleColor = new Color(255, 175, 0);
                    //ttColor = "FFAF00";
                    break;
                case 0:
                    displayTitle = "The Expansive Library";
                    titleColor = Color.White;
                    //ttColor = "ffffff";
                    break;
                case 1:
                    displayTitle = "Glimmering Trinkets";
                    titleColor = new Color(150, 150, 255);
                    //ttColor = "9696E1";
                    break;
                case 2:
                    displayTitle = "Safekeep Security";
                    titleColor = new Color(150, 255, 150);
                    //ttColor = "96FF96";
                    break;
                case 3:
                    displayTitle = "Hot Products";
                    titleColor = new Color(255, 200, 150);
                    //ttColor = "FFC896";
                    break;
                case 4:
                    displayTitle = "Infused Artifacts";
                    titleColor = new Color(255, 150, 150);
                    //ttColor = "FF9696";
                    break;
                case 5:
                    displayTitle = "Shining Treasure";
                    titleColor = new Color(255, 150, 255);
                    //ttColor = "FF96FF";
                    break;
                case 6:
                    displayTitle = "Glory and Power";
                    titleColor = new Color(210, 160, 255);
                    //ttColor = "D2A0FF";
                    break;
                case 7:
                    displayTitle = "The Ancient Gifts";
                    titleColor = new Color(150, 255, 10);
                    //ttColor = "96FF0A";
                    break;
                case 8:
                    displayTitle = "Setstone Arsenal";
                    titleColor = new Color(255, 255, 10);
                    //ttColor = "FFFF0A";
                    break;
                case 9:
                    displayTitle = "Cosmic Contribution";
                    titleColor = new Color(5, 200, 255);
                    //ttColor = "05C8FF";
                    break;
                case 10:
                    displayTitle = "Quantum Quarry";
                    titleColor = Color.Red;
                    //ttColor = "FF0000";
                    break;
                case 11:
                    displayTitle = "Ulti-Unreal Unanimous";
                    titleColor = Color.Purple;
                    //ttColor = "7D00FF";
                    break;
            }
        }
        /// <summary>
        /// Returns a flat value based on rarity.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Identifier(int index) { return rareList[index].Equals(rarityTheme); }
        /// <summary>
        /// Clears special data from this class when specified a certain int value.
        ///  <list type="bullet">
        ///  <item><description>1. Used for initialization (Managed by ItemLotteryNPC)</description></item>
        ///  <item><description>2. Used for starting a new round (Managed by ItemLotteryNPC)</description></item>
        ///  <item><description>3. Used for wiping all data (Managed by Mod Class during Loading, Unloading or Exiting)</description></item>
        ///  </list>
        /// </summary>
        /// <param name="clearType"></param>
        public static void ClearData(int clearType)
        {
            if (clearType == 1)
            {
                finalOutcome = false;
                chooseRandom = false;
                finalList.Clear();
                nameList.Clear();
                numList.Clear();
                rareList.Clear();
                inventoryList.Clear();
                restrictPick = false;
                CastledsContent.sub1 = 0;
                CastledsContent.sub2 = 0;
                CastledsContent.sub3 = 0;
                CastledsContent.subT2 = false;
                CastledsContent.subT3 = false;
                sound1Mul = 0;
                sound2Mul = 0;
                sound3Mul = 0;
                cancelEvent = false;
            }
            if (clearType == 2)
            {
                finalList.Clear();
                start = true;
                itemCheck = true;
                finalOutcome = false;
                hasSucceeded = false;
                displayTitle = "";
                titleColor = Color.White;
                displayAltTitles = false;
                inventoryList.Clear();
                modTheme = "";
                cond = "";
                fargoCondition = "";
                lowNum = 0;
                highNum = 1;
                CastledsContent.sub1 = 0;
                CastledsContent.sub2 = 0;
                CastledsContent.sub3 = 0;
                CastledsContent.subT2 = false;
                CastledsContent.subT3 = false;
                CastledsContent.subT2a = false;
                sound1Mul = 0;
                sound2Mul = 0;
                sound3Mul = 0;
                cancelEvent = false;
            }
            if (clearType == 3)
            {
                titleColor = Color.White;
                #region Boolean Reset
                hasSucceeded = false;
                decideTheme = false;
                specialTheme = false;
                itemCheck = false;
                forceSpecial = false;
                start = false;
                chooseRandom = false;
                pickupPrevent = false;
                finalOutcome = false;
                restrictPick = false;
                addInvList = false;
                item2 = false;
                item3 = false;
                CastledsContent.subT2 = false;
                CastledsContent.subT3 = false;
                CastledsContent.subT2a = false;
                cancelEvent = false;
                CastledWorld.finishParti = false;
                CastledWorld.waitParti = false;
                begin = false;
                forceDiscard = false;
                exceptionPacket = false;
                //displayItem = false;
                #endregion
                #region Integer Reset
                displayItemType = -1;
                indexer = 0;
                slowdownMultiplier = 0;
                stagType = 0;
                roundNum = 0;
                rarityTheme = 0;
                i1 = 0;
                i2 = 0;
                i3 = 0;
                synci1 = 0;
                synci2 = 0;
                synci3 = 0;
                ib1 = 0;
                ib2 = 0;
                lowNum = 0;
                highNum = 1;
                CastledsContent.sub1 = 0;
                CastledsContent.sub2 = 0;
                CastledsContent.sub3 = 0;
                CastledsContent.titleAlpha = 0;
                CastledsContent.title2Alpha = 0;
                CastledsContent.title3Alpha = 0;
                CastledsContent.timer1 = 0;
                groupContra = 0;
                #endregion
                #region String Reset
                is1 = "";
                is2 = "";
                is3 = "";
                //ttColor = "";
                displayTitle = "";
                modTheme = "";
                ia1 = "";
                ia2 = "";
                fargoCondition = "";
                departure = "";
                cond = "";
                #endregion
                #region List Clear
                numList.Clear();
                nameList.Clear();
                rareList.Clear();
                finalList.Clear();
                inventoryList.Clear();
                modList.Clear();
                extraMods.Clear();
                influMod.Clear();
                #endregion
                #region Floats
                sound1Mul = 0;
                sound2Mul = 0;
                sound3Mul = 0;
                #endregion
            }
        }
        /// <summary>
        /// A void that will make sure that volume floats don't exceed a certain value.
        /// </summary>
        public static void Balance()
        {
            if (sound1Mul > 0.1f)
                sound1Mul = 0.1f;
            if (sound2Mul > 0.2f)
                sound2Mul = 0.2f;
            if (sound3Mul > 0.3f)
                sound3Mul = 0.3f;
        }
        /// <summary>
        /// Used for setting up modded themes. Mod Packets, sent via ModCall's will be utilised, in case if anyone wants to add some uniqueness
        /// </summary>
        public static void CMThemeSetup()
        {
            #region Biased and outdated code
            //int test1 = Main.rand.Next(4);
            //Color test = packets[test1].TitleColor(packets[test1].modColor);
            /*
            int a = Main.rand.Next(2);
            int b = 0;
            if (a == 0 || extraMods.Count == 0)
            {
                if (influMod.Count > 0)
                    modTheme = Main.rand.Next(influMod);
                else
                {
                    b = Main.rand.Next(extraMods.Count);
                    modTheme = extraMods[b];
                    displayTitle = modTheme;
                }
            }
            if (a == 1 || !modList.Any(x => influenceModList.Contains(x)))
            {
                b = Main.rand.Next(extraMods.Count);
                modTheme = extraMods[b];
                displayTitle = modTheme;
            }*/
            /*
            switch (modTheme)
            {
                case "ThoriumMod":
                    displayTitle = "Radiating Balance";
                    titleColor = new Color(0, 255, 155);
                    //ttColor = "00FF9B";
                    break;
                case "CalamityMod":
                    displayTitle = "Leaking Death";
                    titleColor = new Color(175, 25, 0);
                    //ttColor = "AF1900";
                    break;
                case "SacredTools":
                    displayTitle = "Novaniel's Demotion";
                    titleColor = new Color(50, 35, 75);
                    //ttColor = "32234B";
                    break;
                case "AAMod":
                    displayTitle = "Legends of Here";
                    titleColor = new Color(175, 15, 125);
                    //ttColor = "AF0F7D";
                    break;
                case "ElementsAwoken":
                    displayTitle = "Periodic Destruction";
                    titleColor = new Color(225, 175, 25);
                    //ttColor = "E1AF4B";
                    break;
                case "FargowiltasSouls":
                    displayTitle = "Chaos, for a price";
                    titleColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                    //ttColor = "190096";
                    break;
                case "Laugicality":
                    displayTitle = "Conductor's Playground";
                    titleColor = new Color(175, 180, 120);
                    //ttColor = "AFB478";
                    break;
                case "JoostMod":
                    displayTitle = "The True Cactillary";
                    titleColor = new Color(5, 215, Main.DiscoB - 35);
                    //ttColor = "00AA32";
                    break;
                case "SpiritMod":
                    displayTitle = "Distorted Artifacts";
                    titleColor = new Color(0, 125, 175);
                    //ttColor = "007DAA";
                    break;
                case "QwertysRandomContent":
                    displayTitle = "Chaos in the Skies";
                    titleColor = new Color(255, 255, 150);
                    //ttColor = "FFFF96";
                    break;
                case "Redemption":
                    displayTitle = "The Radical Infectious Bionic Elongation of Vanilla Progression";
                    titleColor = new Color(65, Main.DiscoG + 100, 35);
                    //ttColor = "326400";
                    break;
            }*/
            #endregion
            titleColor = Color.White;
            if (!loadedMods.Contains("CastledsContent"))
                modTheme = Main.rand.Next(loadedMods);
            else
                modTheme = "CastledsContent";
            displayTitle = modTheme;
            try
            {
                for (int a = 0; a < packets.Length; a++)
                {
                    if (!packets[a].IsEmpty())
                    {
                        if (packets[a].mod.Name == modTheme)
                        {
                            if (packets[a].modtitle != null)
                                displayTitle = packets[a].modtitle;
                            if (packets[a].modColor != null)
                                titleColor = packets[a].TitleColor(packets[a].modColor);
                            if (packets[a].conditions != null && Main.rand.Next(4) == 2)
                                cond = Main.rand.Next(packets[a].conditions);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.NewText($"[{modTheme}] - An error has occured while trying to set this as the theme.", Color.OrangeRed);
                Main.NewText(e.Message, Color.Aquamarine);
                exceptionPacket = true;
            }
        }
    }
    /// <summary>
    /// Received and modified through mod calls. These are used in the LotterManager class, for adding custom titles, colors and keywords for a certain mod theme.
    /// </summary>
    public class LotPacket
    {
        public Mod mod;
        public string modtitle;
        public Color? modColor;
        public List<string> conditions = new List<string> { };
        public List<int> blacklist = new List<int> { };
        public Color TitleColor(Color? color)
        {
            if (color == null)
                return Color.White;
            else
                return (Color)color;
        }
        public bool IsEmpty() { return mod == null && modtitle == null && modColor == null && conditions == null; }
    }
}
