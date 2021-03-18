using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using CastledsContent.Utilities;
using System;
using Microsoft.Xna.Framework;
using System.Linq;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using IL.Terraria.GameContent.UI;
//using ReLogic.Reflection;
//CopyPaste this to replace old variables: LMan

namespace CastledsContent.NPCs.ItemLotteryNPC
{
    public class ItemLotteryNPC : ModNPC
    {
        #region Variables
        private int item = -1;
        private int rando = 3000;
        private int action = 0;
        private int flame;
        private int flameT;
        private bool dropdown;
        private float floater = 1;
        private int stagTimer;
        private int cycles;
        private int cycleMomentum = 160;
        private readonly int cycleMDefault = 160;
        private int duration;
        private int modMult = 1;
        private int fargCond;
        private string fargCond2;
        private int counter1Scale;
        //private bool eyeHoriz;
        private bool stupidFlag;
        private bool setupDone = false;
        private bool whyFlag;
        private bool asdf = false;
        private bool flag2;
        private bool entrance = false;
        private bool max = false;
        private bool max2 = false;
        private bool vicB = false;
        private int vic = 0;
        private Vector2 pos;
        //private ItemDisplay display = new ItemDisplay();
        //private PossessColor filter = new PossessColor();
        //private Texture2D testScreen = ModContent.GetTexture(ILTex.dir[ILTex.ColorTest]);
        //private Texture2D LED = ModContent.GetTexture(ILTex.dir[ILTex.LED]);
        //private Texture2D DisplayTest = ModContent.GetTexture("CastledsContent/NPCs/ItemLotteryNPC/DisplayTest");
        //private byte intensity = ModContent.GetInstance<AConfig>().phantomTest;
        #endregion
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Superintendent");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 50;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 86;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 255;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }
        public override void AI()
        {
            if (Evacuate())
            {
                npc.life = int.MinValue;
                npc.HitEffect();
                npc.checkDead();
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0);
            }
            if (LMan.chooseRandom)
                AddShopItem();
            //EmoteBubble.
            if (!entrance)
                Entrance();
            if (LMan.cancelEvent)
                Departure();
            //if (!CastledWorld.lotteryIsAlive)
                //display.GenerateTexture(DisplayTest, ItemID.BeeHat, ItemID.BeePants);
            //CastledWorld.lotteryIsAlive = true;
            if (entrance)
            {
                if (!flag2)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        CastledWorld.waitParti = true;
                    else
                        CastledWorld.finishParti = true;
                    flag2 = true;
                }
                if (PartiBool())
                {
                    LMan.GetItemQuota();
                    whyFlag = false;
                    counter1Scale = 1;
                    for (int a = 0; a < 255; a++)
                    {
                        if (Main.player[a].active)
                        {
                            CastledPlayer bod = Main.player[a].GetModPlayer<CastledPlayer>();
                            if (bod.parti)
                                CastledPlayer.serves = 0;
                        }
                    }
                    LMan.specialTheme = false;
                    LMan.itemCheck = true;
                    LMan.indexer = 0;
                }
            }
            if (LMan.itemCheck && !LMan.start)
            {
                for (int a = 0; a < LMan.setupShop.Count; a++)
                    LMan.setupShop[a] = -1;
                LMan.pickupPrevent = true;
                LMan.restrictPick = true;
                for (int a = 0; a < ItemLoader.ItemCount; a++)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, a);
                    LMan.indexer++;
                    
                }
                if (LMan.indexer == ItemLoader.ItemCount || LMan.indexer > ItemLoader.ItemCount)
                {
                    LMan.itemCheck = false;
                    LMan.pickupPrevent = false;
                    LMan.roundNum = 1;
                    CastledPlayer.ModItemSetup(Main.player[0]);
                    setupDone = true;
                    LMan.begin = true;
                }
                CastledWorld.finishParti = false;
                CastledWorld.waitParti = false;

            }
            else if (LMan.itemCheck && LMan.start)
            {
                if (!LMan.decideTheme)
                {
                    LMan.AdjustRarity();
                    #region Fargo Conditioning
                    if (NPC.downedAncientCultist && NPC.downedMoonlord)
                        fargCond = 4;
                    else if (NPC.downedAncientCultist)
                        fargCond = 3;
                    fargCond = Main.rand.Next(2);
                    if (fargCond == 0)
                        fargCond2 = "";
                    if (fargCond == 1)
                        fargCond2 = "Enchantment";
                    if (fargCond == 2)
                        fargCond2 = "Force";
                    if (fargCond == 3)
                        fargCond2 = "Soul";
                    #endregion
                    CheckContrabandeCondition();
                    GetTimerSpeed();
                    LMan.rarityTheme = Main.rand.Next(LMan.lowNum, LMan.highNum);
                    LMan.SetTheme(LMan.rarityTheme);
                    LMan.decideTheme = true;
                    
                    if (Main.rand.Next(6) == 0)
                        LMan.specialTheme = true;
                    if (Main.rand.Next(8) == 0 && LMan.extraMods.Count != 0 || Main.rand.Next(4) == 0 && LMan.modList.Any(x => LMan.influenceModList.Contains(x)))
                    {
                        whyFlag = true;
                        LMan.CMThemeSetup();
                        modMult = 3;
                    }

                }
                if (LMan.specialTheme)
                {
                    int muckos = Main.rand.Next(5);
                    if (muckos == 1)
                    {
                        if (Main.rand.Next(3) == 0)
                            LMan.rarityTheme = -11;
                        else
                            LMan.rarityTheme = -14;
                    }
                    if (muckos == 2)
                    {
                        if (Main.rand.Next(3) == 0)
                            LMan.rarityTheme = -12;
                        else
                            LMan.rarityTheme = -15;
                    }
                    if (muckos == 3)
                        LMan.rarityTheme = -13;
                    if (muckos == 4)
                        LMan.rarityTheme = -14;
                    if (muckos == 5)
                        LMan.rarityTheme = -15;
                    LMan.SetTheme(LMan.rarityTheme);
                    LMan.specialTheme = false;
                    LMan.forceSpecial = false;
                    whyFlag = false;
                }
                LMan.pickupPrevent = true;
                for (int a = 0; a < LMan.numList.Count; a++)
                {
                    if (!whyFlag)
                    {
                        if (LMan.Identifier(a))
                            LMan.finalList.Add(a);
                        LMan.indexer++;
                    }
                    else
                    {
                        if (LMan.modTheme != "FargowiltasSouls")
                        {
                            if (SLB(a))
                                LMan.finalList.Add(a);
                            LMan.indexer++;
                        }
                        else
                        {
                            if (SLB(a) && LMan.nameList[a].Contains(fargCond2))
                                LMan.finalList.Add(a);
                            LMan.indexer++;
                        }
                    }
                }
                if (LMan.indexer == LMan.numList.Count || LMan.indexer > LMan.numList.Count)
                {
                    LMan.itemCheck = false;
                    LMan.pickupPrevent = false;
                    LMan.start = false;
                    LMan.chooseRandom = true;
                    LMan.slowdownMultiplier = 2;
                    LMan.stagType = 1;
                }
            }
            #region Lottery
            if (LMan.chooseRandom && !LMan.begin)
            {
                LMan.Balance();
                if (LMan.finalList.Count < 1 || LMan.exceptionPacket)
                {
                    if (ModContent.GetInstance<ClientConfig>().algorithmoMessage)
                        Main.NewText($"The selected theme, '{LMan.modTheme}' does not have enough elements.");
                    whyFlag = false;
                    LMan.itemCheck = true;
                    LMan.start = true;
                    LMan.finalList.Clear();
                    LMan.chooseRandom = false;
                    LMan.decideTheme = false;
                    stagTimer = 0;
                    cycleMomentum = cycleMDefault;
                    cycles = 0;
                    LMan.stagType = 0;
                    duration = 0;
                    modMult = 1;
                    fargCond = 0;
                    fargCond2 = "";
                }
                LMan.SyncText();
                if (LMan.stagType == 1)
                {
                    LMan.sound1Mul += 0.02f;
                    LMan.i2 = Main.rand.Next(LMan.finalList);
                    LMan.i3 = Main.rand.Next(LMan.finalList);

                    stagTimer++;
                    if (stagTimer >= LMan.slowdownMultiplier)
                    {
                        LMan.i1 = Main.rand.Next(LMan.finalList);
                        Main.PlaySound(SoundID.DD2_CrystalCartImpact.WithVolume(LMan.sound1Mul));
                        stagTimer = 0;
                        cycles++;
                    }
                    if (cycles > cycleMomentum)
                    {
                        stagTimer = 0;
                        cycles = 0;
                        cycleMomentum *= 2/5;
                    }
                    if (cycleMomentum < 9 * modMult)
                    {
                        Main.PlaySound(SoundID.MenuOpen);
                        LMan.slowdownMultiplier = 3;
                        stagTimer = 0;
                        cycleMomentum = cycleMDefault / 2;
                        cycles = 0;
                        LMan.stagType = 2;
                    }
                }
                if (LMan.stagType == 2)
                {
                    LMan.sound2Mul += 0.02f;
                    CastledsContent.instance.subT2 = true;
                    LMan.i3 = Main.rand.Next(LMan.finalList);
                    stagTimer++;
                    if (stagTimer >= LMan.slowdownMultiplier)
                    {
                        LMan.i2 = Main.rand.Next(LMan.finalList);
                        Main.PlaySound(SoundID.DD2_CrystalCartImpact.WithVolume(LMan.sound2Mul));
                        stagTimer = 0;
                        cycles++;
                    }
                    if (cycles > cycleMomentum)
                    {
                        stagTimer = 0;
                        cycles = 0;
                        cycleMomentum *= 3/5;
                    }
                    if (cycleMomentum < 12 * modMult)
                    {
                        Main.PlaySound(SoundID.MenuOpen);
                        LMan.slowdownMultiplier = 4;
                        stagTimer = 0;
                        cycleMomentum = cycleMDefault / 3;
                        cycles = 0;
                        LMan.stagType = 3;
                    }
                }
                if (LMan.stagType == 3)
                {
                    LMan.sound3Mul += 0.02f;
                    CastledsContent.instance.subT3 = true;
                    stagTimer++;
                    if (stagTimer >= LMan.slowdownMultiplier)
                    {
                        LMan.i3 = Main.rand.Next(LMan.finalList);
                        Main.PlaySound(SoundID.DD2_CrystalCartImpact.WithVolume(LMan.sound3Mul));
                        stagTimer = 0;
                        cycles++;
                    }
                    if (cycles > cycleMomentum)
                    {
                        LMan.slowdownMultiplier *= 2;
                        stagTimer = 0;
                        cycles = 0;
                        cycleMomentum /= 2;
                    }
                    if (cycleMomentum < 15 * modMult)
                    {
                        if (LMan.roundNum < 8)
                            FinalizeRound();
                        else
                        {
                            if (!stupidFlag)
                            {
                                stupidFlag = true;
                                Main.NewText($"Attempting Quantum Scan...");
                            }

                            if (stupidFlag)
                            {

                                if (duration < 60)
                                {
                                    duration++;
                                    if (!LMan.addInvList)
                                    {
                                        LMan.InventoryScan();
                                        LMan.addInvList = true;
                                    }
                                }
                                else
                                {
                                    LMan.slowdownMultiplier = 4;
                                    stagTimer = 0;
                                    cycleMomentum = cycleMDefault / 3;
                                    cycles = 0;
                                    LMan.addInvList = false;
                                    LMan.displayAltTitles = true;
                                    duration = 0;
                                    LMan.stagType = 4;
                                    stupidFlag = false;
                                }
                            }
                        }
                    }
                }
                #region Inventory Scan
                if (LMan.displayAltTitles)
                {
                    if (LMan.inventoryList.Count < 1)
                    {
                        FinalizeRound();
                        LMan.displayAltTitles = false;
                        Main.NewText("No Contrabande Detected.");
                    }
                    else
                    {
                        LMan.SyncText();
                        if (LMan.stagType == 4)
                        {
                            LMan.ib2 = Main.rand.Next(LMan.inventoryList);
                            stagTimer++;
                            if (stagTimer >= LMan.slowdownMultiplier)
                            {
                                LMan.ib1 = Main.rand.Next(LMan.inventoryList);
                                Main.PlaySound(SoundID.MenuTick);
                                stagTimer = 0;
                                cycles++;
                            }
                            if (cycles > cycleMomentum)
                            {
                                LMan.slowdownMultiplier *= 2;
                                stagTimer = 0;
                                cycles = 0;
                                cycleMomentum /= 4;
                            }
                            if (cycleMomentum < 25)
                            {
                                Main.PlaySound(SoundID.MenuClose);
                                LMan.slowdownMultiplier = 4;
                                stagTimer = 0;
                                cycleMomentum = cycleMDefault / 3;
                                cycles = 0;
                                LMan.stagType = 5;
                            }
                        }
                        if (LMan.stagType == 5)
                        {
                            CastledsContent.instance.subT2a = true;
                            stagTimer++;
                            if (stagTimer >= LMan.slowdownMultiplier)
                            {
                                LMan.ib2 = Main.rand.Next(LMan.inventoryList);
                                Main.PlaySound(SoundID.MenuTick);
                                stagTimer = 0;
                                cycles++;
                            }
                            if (cycles > cycleMomentum)
                            {
                                LMan.slowdownMultiplier *= 2;
                                stagTimer = 0;
                                cycles = 0;
                                cycleMomentum /= 5;
                            }
                            if (cycleMomentum < 45)
                                FinalizeRound();
                        }
                        #endregion
                    }
                }
            }
            if (LMan.hasSucceeded)
            {
                #region Moved DrawCountdown here
                if (LMan.counter > 0)
                {
                    if (LMan.roundNum > 15)
                    {
                        if (LMan.counter == 5 && LMan.counter1 < 1)
                        {
                            Main.PlaySound(SoundID.DD2_CrystalCartImpact);
                            LMan.i1 = Main.rand.Next(LMan.finalList);
                            LMan.i2 = Main.rand.Next(LMan.finalList);
                            LMan.i3 = Main.rand.Next(LMan.finalList);
                            if (LMan.roundNum > 20)
                            {
                                LMan.ib1 = Main.rand.Next(LMan.inventoryList);
                                LMan.ib2 = Main.rand.Next(LMan.inventoryList);
                            }
                        }
                    }
                    if (CastledWorld.determineContraSp)
                    LMan.counter1 += counter1Scale;
                    else if (!CastledWorld.determineContraSp)
                    {
                        if (Main.expertMode)
                            LMan.counter1 += 10;
                        else
                            LMan.counter1 += 5;
                    }
                    if (LMan.counter1 > 60)
                    {
                        Main.PlaySound(SoundID.MenuTick);
                        LMan.counter--;
                        LMan.counter1 = 0;
                        LMan.alphaC = 0;
                    }
                }
                else if (LMan.counter < 1 && asdf)
                {
                    if (Main.expertMode && LMan.roundNum > 25)
                    {
                        pos = npc.position;
                        vicB = true;
                        LMan.ClearData(3);
                    }
                    else if (!Main.expertMode && LMan.roundNum > 15)
                    {
                        pos = npc.position;
                        vicB = true;
                        LMan.ClearData(3);
                    }
                    if (!vicB)
                    {
                        LMan.counter1 = 0;
                        LMan.roundNum++;
                        LMan.ClearData(2);
                        if (CastledWorld.determineContraSp)
                            CastledPlayer.PunishContra();
                        CastledsContent.instance.titleAlpha = 0;
                    }
                }
                if (!asdf)
                {
                    if (LMan.counter1 > 55 && LMan.counter < 2 || LMan.counter1 < 1 && LMan.counter < 1)
                    {
                        if (CastledWorld.determineContraSp)
                        {
                            LMan.forceDiscard = true;
                            LMan.ForceDiscard();
                        }
                        else
                        {
                            Main.PlaySound(SoundID.Item68, npc.position);
                            asdf = true;
                        }
                    }
                }
                #endregion
            }
            if (!LMan.hasSucceeded && LMan.counter < 1 && asdf)
            {
                if (LMan.alphaC < 255)
                    LMan.alphaC += 5;
                if (LMan.alphaC == 255 || LMan.alphaC > 255)
                {
                    LMan.alphaC = 255;
                    LMan.drawCountdown = false;
                    asdf = false;
                }
            }
            #endregion
            if (vicB)
                DepartureVictory();
        }
        public override bool CheckActive() { return false; }
        public override void NPCLoot() 
        { 
            LMan.ClearData(3);
            for (int a = 0; a < LMan.setupShop.Count; a++)
                LMan.setupShop[a] = -1;
        }
        public void CheckContrabandeCondition() 
        {
            int contraSum = 0;
            for (int a = 0; a < Main.player.Length; a++)
            {
                if (Main.player[a].active)
                {
                    CastledPlayer bod = Main.player[a].GetModPlayer<CastledPlayer>();
                    if (bod.parti)
                    {
                        //CastledPlayer.count = 0;
                        CastledPlayer.CheckQuota(Main.player[a]);
                        if (CastledPlayer.count < LMan.minItemQuota)
                        {
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                bod.parti = false;
                                Main.NewText($"{Main.player[a].name} is disqualified. [REASON] - Insufficient items");
                            }
                            else
                            {
                                LMan.cancelEvent = true;
                                LMan.departure = "Algorithmo discarded too many of your items.";
                                for (int b = 0; b < bod.contrabande.Count; b++)
                                    if (bod.contrabande[b] != null && !bod.contrabande[b].IsAir)
                                        bod.contrabande[b] = new Item();
                            }
                        }
                        contraSum += CastledPlayer.serves;
                    }
                }
                if (contraSum >= LMan.groupContra)
                {
                    LMan.cancelEvent = true;
                    LMan.departure = "Algorithmo's quota has been satisfied.";
                    if (Main.player[a].active)
                    {
                        CastledPlayer bod = Main.player[a].GetModPlayer<CastledPlayer>();
                        if (bod.parti)
                        {
                            for (int b = 0; b < bod.contrabande.Count; b++)
                                if (!bod.contrabande[b].IsAir || bod.contrabande[b] != null)
                                    bod.contrabande[b] = new Item();
                        }
                    }
                }
            }
        }
        public void FinalizeRound()
        {
            //Main.NewText($"Round {LMan.roundNum + 1} is starting shortly...");
            whyFlag = false;
            //Main.NewText($"[i/1:{LMan.funnyi1}] [i/1:{LMan.funnyi2}] [i/1:{LMan.funnyi3}]");
            if (LMan.roundNum < 16)
                LMan.counter = 8;
            else if (LMan.roundNum < 25 && LMan.roundNum > 15)
                LMan.counter = 7;
            Main.PlaySound(SoundID.Item119);
            LMan.hasSucceeded = true;
            LMan.finalOutcome = true;
            LMan.drawCountdown = true;
            stagTimer = 0;
            cycleMomentum = cycleMDefault;
            cycles = 0;
            LMan.stagType = 0;
            LMan.decideTheme = false;
            duration = 0;
            modMult = 1;
            fargCond = 0;
            fargCond2 = "";
        }
        public void GetTimerSpeed()
        {
            if (LMan.roundNum < 15)
                counter1Scale = 1;
            else if (LMan.roundNum < 25)
                counter1Scale = 2;
        }
        /// <summary>
        /// Super Long Boolean
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool SLB(int index)
        {
            if (LMan.cond != "")
            {
                if (LMan.modList[index].Equals(LMan.modTheme) && LMan.nameList[index].Contains(LMan.cond))
                {
                    if (Main.rand.Next(6) == 0)
                        return LMan.rareList[index].Equals(LMan.rarityTheme) || LMan.rareList[index].Equals(LMan.rarityTheme - 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 2) || LMan.rareList[index].Equals(LMan.rarityTheme - 2);
                    else if (Main.rand.Next(3) == 0)
                        return LMan.rareList[index].Equals(LMan.rarityTheme) || LMan.rareList[index].Equals(LMan.rarityTheme - 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 1);
                    else
                        return LMan.rareList[index].Equals(LMan.rarityTheme);
                }
            }
            else
            {
                if (LMan.modList[index].Equals(LMan.modTheme))
                {
                    if (Main.rand.Next(6) == 0)
                        return LMan.rareList[index].Equals(LMan.rarityTheme) || LMan.rareList[index].Equals(LMan.rarityTheme - 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 2) || LMan.rareList[index].Equals(LMan.rarityTheme - 2);
                    else if (Main.rand.Next(3) == 0)
                        return LMan.rareList[index].Equals(LMan.rarityTheme) || LMan.rareList[index].Equals(LMan.rarityTheme - 1) || LMan.rareList[index].Equals(LMan.rarityTheme + 1);
                    else
                        return LMan.rareList[index].Equals(LMan.rarityTheme);
                }
            }
            return false;
        }
        public bool PartiBool() => CastledWorld.finishParti && !CastledWorld.waitParti && !LMan.itemCheck && !LMan.chooseRandom && !setupDone;
        public void Entrance()
        {
            for(int a = 0; a < 15; a++)
            {
                if (LMan.setupShop.Count < 15)
                    LMan.setupShop.Add(-1);
            }
            npc.alpha -= 15;
            //float velo = 1f;
            //velo++;
            if (!max)
            {
                npc.velocity.Y += 0.005f;
                npc.velocity.Y += npc.velocity.Y / 2;
            }
            if (npc.velocity.Y > 35 || max)
            {
                max = true;
                FlyUp();
            }
            void FlyUp()
            {
                //npc.alpha += 15;
                if (npc.velocity.Y > 0 && !max2)
                    npc.velocity.Y -= 5;
                else if (npc.velocity.Y < 1 && !max2)
                    npc.velocity.Y--;
                if (npc.velocity.Y < -10 || max2)
                {
                    max2 = true;
                    npc.velocity.Y += 0.5f;
                }
                if (npc.velocity.Y == 0 || npc.velocity.Y > 0 && max2)
                {
                    npc.velocity.Y = 0;
                    max = false;
                    entrance = true;
                    for (int a = 0; a < LMan.setupShop.Count; a++)
                        LMan.setupShop[a] = -1;
                    //Departure();
                }
                if (npc.alpha < 0)
                    npc.alpha = 0;
            }
            //Main.NewText($"{npc.velocity.Y} {max} {max2}");
        }
        public void Departure()
        {
            npc.alpha += 15;
            //float velo = 1f;
            //velo++;
            npc.velocity.Y++;
            if (npc.velocity.Y > 15 && LMan.cancelEvent)
            {
                LMan.cancelEvent = false;
                Main.NewText(LMan.departure, 175, 75, 255);
                Main.NewText("Algorithmo has left!", 175, 75, 255);
                npc.life = 0;
                LMan.ClearData(3);
                for (int a = 0; a < LMan.setupShop.Count; a++)
                    LMan.setupShop[a] = -1;
            }
            //max = true;
            //Main.NewText("Test Successful");

            if (npc.alpha > 255)
                npc.alpha = 255;
            //Main.NewText($"{npc.velocity.Y} {max}");
            //Main.NewText($"{npc.velocity.Y} {max} {max2}");
        }
        public void DepartureVictory()
        {
            int scale = 0;
            int x = 0;
            int y = 0;
            vic++;
            if (vic < 120)
            {
                if (vic == 2)
                {
                    Electricity();
                    for (int a = 0; a < 3; a++)
                        Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("NPCs/ItemLotteryNPC/ItemLotteryNPC_Thruster"), 1f);
                    Main.NewText("[CS0103]: The name 'SuperMegaUltraInfinityQuantumScan' does not exist in the current context.", Color.OrangeRed);
                    Main.NewText("[Breakpoint] Executing 'SellBack'...", Color.OrangeRed);
                }
                x = Main.rand.Next(-5, 5);
                y = Main.rand.Next(-5, 5);
            }
            else if (vic < 180)
            {
                scale += 5;
                x = Main.rand.Next(-10 - scale, 10 + scale);
                y = Main.rand.Next(-10 - scale, 10 + scale);
                npc.alpha += 15;
                if (vic == 121)
                    Main.NewText("[Breakpoint] Returning to base...", Color.OrangeRed);
            }
            void Electricity()
            {
                Vector2 vec = new Vector2(npc.position.X + Main.rand.Next(-25, 25), npc.position.Y + Main.rand.Next(-25, 25));
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                Main.PlaySound(SoundID.NPCHit53);
                Main.PlaySound(SoundID.NPCHit4);
                for (int a = 0; a < 8; a++)
                    Dust.NewDust(vec, rectangle.Width, rectangle.Height, 21, 0, 0, 100, Color.White, 0.5f);
            }
            if (npc.alpha > 255 && npc.life > 0)
            {
                LMan.ClearData(3);
                Main.NewText("Algorithmo has encountered a syntax error.", 175, 75, 255);
                //Main.NewText("The machine has been defeated!", 175, 75, 255);
                CastledWorld.downedAlgorithmo = true;
                npc.life = -9001;
            }
            npc.position = new Vector2(pos.X + x, pos.Y + y);
        }
        public void AddShopItem()
        {
            if (action > 0)
            {
                action++;
                LMan.displayItemType = item;
                if (action > 240)
                {
                    action = 0;
                    rando = 3000;
                    LMan.displayItemType = -1;
                    CastledsContent.instance.ia = 0;
                }
            }
            if (rando != -1)
                rando--;
            if (rando > 0 && Main.rand.Next(rando) == 0)
                Action();
            void Action()
            {
                int[] three = new int[3];
                three[0] = LMan.synci1;
                three[1] = LMan.synci2;
                three[2] = LMan.synci3;
                rando = -1;
                for (int a = 0; a < LMan.setupShop.Count; a++)
                {
                    if (action < 1)
                    {
                        if (LMan.setupShop[a] == -1)
                        {
                            int confirm = Main.rand.Next(three);
                            if (!LMan.blacklist.Contains(confirm))
                            {
                                LMan.setupShop[a] = confirm;
                                action++;
                                item = LMan.setupShop[a];
                            }
                            else
                                LMan.displayItemType = -1;
                        }
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            float eyeHorizontal = floater / 4;
            flameT++;
            if (flameT > 6)
            {
                flame += 108;
                if (flame > 324)
                    flame = 0;
                flameT = 0;
            }
            #region Floater Code
            if (entrance)
            {
                #region the bop
                /*
                //Main.NewText(floater);
                if (floater > -25)
                {
                    if (!dropdown)
                    {
                        if (floater < 0)
                        {
                            floater *= 0.55f;
                            Main.NewText($"{floater} Apples");
                            if (floater > -0.0005f)
                                floater = 1;
                        }
                        else if (floater > 0 && floater < 20)
                        {
                            floater *= 1.15f;
                            Main.NewText($"{floater} Bananas");
                        }
                    }
                    if (floater > 20 && floater < 22)
                    {
                        floater *= 1.05f;
                        Main.NewText($"{floater} Oranges");
                    }
                    if (floater > 22 || dropdown && floater > 1)
                    {
                        floater *= 0.85f;
                        dropdown = true;
                        Main.NewText($"{floater} Grapes");
                    }
                    else if (floater < 1 && dropdown && floater > 0)
                    {
                        floater = -1;
                        Main.NewText($"{floater} Berries");
                    }
                    else if (floater < 0 && dropdown)
                    {
                        floater *= 1.35f;
                        Main.NewText($"{floater} Durian");
                    }
                }
                if (floater < -25)
                {
                    dropdown = false;
                    floater *= 0.85f;
                    Main.NewText($"{floater} Kiwi");
                }*/
                #endregion
                if (floater > -25)
                {
                    if (!dropdown)
                    {
                        if (floater < 0)
                        {
                            floater *= 0.95f;
                            //Main.NewText($"{floater} Apples");
                            if (floater > -0.9f)
                                floater = 1;
                        }
                        else if (floater > 0 && floater < 20)
                        {
                            floater *= 1.15f;
                            //Main.NewText($"{floater} Bananas");
                        }
                    }
                    if (floater > 20 && floater < 22)
                    {
                        floater *= 1.05f;
                        //Main.NewText($"{floater} Oranges");
                    }
                    if (floater > 22 || dropdown && floater > 1)
                    {
                        floater *= 0.95f;
                        dropdown = true;
                        //Main.NewText($"{floater} Grapes");
                    }
                    else if (floater < 1 && dropdown && floater > 0)
                    {
                        floater = -1;
                        //Main.NewText($"{floater} Berries");
                    }
                    else if (floater < 0 && dropdown)
                    {
                        floater *= 1.15f;
                        //Main.NewText($"{floater} Durian");
                    }
                }
                if (floater < -25)
                {
                    dropdown = false;
                    floater *= 0.85f;
                    //Main.NewText($"{floater} Kiwi");
                }
            }
            #endregion
            int FrameY()
            {
                if (!vicB)
                {
                    if (CastledWorld.determineContraSp)
                        return LMan.counter * 80;
                    else if (!LMan.hasSucceeded && LMan.counter < 1 && asdf)
                        return 720;
                    else
                        return 0;
                }
                return 800;
            }
            #region
            /*
            FilterInfo info1 = new FilterInfo
            {
                tex = testScreen,
                effectType = "Standard",
                intensity = intensity,
                restorative = false,
                rarity = 25,
                rarityType = FilterInfo.RarityOperation,
            };*/
            /*
            FilterInfo info2 = new FilterInfo
            {
                tex = LED,
                effectType = FilterInfo.Standard,
                intensity = intensity,
                restorative = false
            };
            filter.GenerateTexture(info1);
            filter.GenerateTexture(info2);
            */
            #endregion
            Vector2 floatey = new Vector2(0f, floater);
            Color alphor = new Color(Color.Transparent.R + (drawColor.R - npc.alpha), Color.Transparent.G + (drawColor.G - npc.alpha), Color.Transparent.B + (drawColor.B - npc.alpha), Color.Transparent.A + (drawColor.A - npc.alpha));
            Color spectAlph = new Color(alphor.R, alphor.G + Main.DiscoG, alphor.B + Main.DiscoB, alphor.A);
            Color softGlow = new Color((Main.DiscoR / 2) + alphor.R, 75 + alphor.G, Main.DiscoB + alphor.B, 0 + alphor.A);
            Color bowGlow = new Color(Main.DiscoR - alphor.R, Main.DiscoG - alphor.G, Main.DiscoB - alphor.B, 0 - alphor.A);
            Rectangle generalRect = new Rectangle(npc.frame.X, npc.frame.Y, 86, 80);
            Rectangle timerRect = new Rectangle(npc.frame.X, npc.frame.Y + FrameY(), 86, 80);
            Rectangle flameRect = new Rectangle(npc.frame.X, npc.frame.Y + flame, 86, 108);
            if (!vicB)
            {
                spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Thruster]), npc.Center - Main.screenPosition, new Rectangle(npc.frame.X, npc.frame.Y, 86, 108), alphor, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Flame]), npc.Center - Main.screenPosition, flameRect, new Color(alphor.R, alphor.G, alphor.B, alphor.A * 1.75f) * 2, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Base]), npc.Center - Main.screenPosition, timerRect, alphor, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Display]), npc.Center - Main.screenPosition, timerRect, spectAlph, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.SideGlow]), npc.Center - Main.screenPosition, generalRect, softGlow, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.SideGlow]), npc.Center - Main.screenPosition, generalRect, softGlow * -1, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(testScreen, npc.Center - Main.screenPosition, generalRect, drawColor * 4, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(DisplayTest, npc.Center - Main.screenPosition, generalRect, drawColor * 4, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.LED]), npc.Center - Main.screenPosition, new Rectangle(npc.frame.X, npc.frame.Y, 86, 80), alphor * 2.5f, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
            
            if (LMan.counter < 1 && !asdf)
            {
                spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Eye]), npc.Center - Main.screenPosition, generalRect, bowGlow * 3.5f, npc.rotation, floatey, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(ILTex.dir[ILTex.Pupil]), npc.Center - Main.screenPosition, generalRect, alphor * 2.5f, npc.rotation, new Vector2(floatey.X + eyeHorizontal, floatey.Y - (floater / 3)), 1f, SpriteEffects.None, 0f);
            }
            //Main.NewText($"{LMan.counter < 1 && !CastledWorld.determineContraSp && LMan.alphaC < 1} {LMan.counter} {CastledWorld.determineContraSp} {LMan.alphaC}");
            //Main.NewText(generalRect);
            return false;
        }
        bool Evacuate()
        {
            int lottery = 0;
            foreach (NPC n in Main.npc)
                if (n.type == ModContent.NPCType<ItemLotteryNPC>())
                    lottery++;
            return lottery > 1;
        }
    }
    public class ILTex
    {
        public static readonly List<string> dir = new List<string>() 
        {
            "NPCs/ItemLotteryNPC/ItemLotteryNPC2",
            "NPCs/ItemLotteryNPC/ItemLotteryNPC_Thrusters",
            "NPCs/ItemLotteryNPC/ItemLotteryNPC_ThrusterFlames",
            "NPCs/ItemLotteryNPC/ItemLotteryNPC_Glow1",
            "NPCs/ItemLotteryNPC/ItemLotteryNPC_Glow2",
            "NPCs/ItemLotteryNPC/ItemLotteryNPC_Glow3",
            "NPCs/ItemLotteryNPC/DigitalEye",
            "NPCs/ItemLotteryNPC/DigitalPupil",
            "CastledsContent/NPCs/ItemLotteryNPC/TestScreen",
            "CastledsContent/NPCs/ItemLotteryNPC/ColorScreen"
        };

        public const short Base = 0;
        public const short Thruster = 1;
        public const short Flame = 2;
        public const short SideGlow = 3;
        public const short Display = 4;
        public const short LED = 5;
        public const short Eye = 6;
        public const short Pupil = 7;
        public const short Test = 8;
        public const short ColorTest = 9;
    }
}
