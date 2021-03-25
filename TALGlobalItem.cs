using Terraria;
using Terraria.GameContent.UI.Chat;
using Terraria.ID;
using CastledsContent.Utilities;
using CastledsContent.NPCs.Tarr;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CastledsContent.Items.Storage;

namespace CastledsContent
{
    public class SGlobalItem : GlobalItem
    {
        public bool addedSlot = false;
        public bool SLHighlight = false;
        private int killTime;
        public float tagExpire;
        //public int bagPickup;
        public bool flag;
        bool canStack = false;
        public bool bagTag;
        public BagPickup storage;
        public BagPickup pickup;
        private Color orC;
        #region Tarr
        public int flingTime = 0;
        //public int oldUse;
        public bool flinged = false;
        public Vector2 originalVelo = new Vector2();
        public Vector2 flingVelo = new Vector2();
        #endregion
        public override bool InstancePerEntity => true;
        public void FlingItem(Item item)
        {
            if (flingTime > 0)
            {
                flingTime--;
                item.velocity = flingVelo;
            }
            if (flingTime == 0 || flingTime < 0)
            {
                flingTime = 0;
                flinged = false;
                flingVelo = new Vector2();
                item.velocity = originalVelo;
                originalVelo = new Vector2();
            }
        }
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            SLHighlight = false;
            bagTag = false;
            if (LMan.pickupPrevent)
            {
                killTime++;
                if (killTime > 0)
                    item.TurnToAir();
            }
            if (!LMan.pickupPrevent && flag)
                item.color = orC;
            if (flinged)
                FlingItem(item);
        }
        public override bool ItemSpace(Item item, Player player)
        {
            if (pickup != null && BagPickup.NotStorage(item))
            {
                for (int a = 0; a < pickup.contained.Count; a++)
                {
                    if (pickup.contained[a].type == item.type && pickup.contained[a].prefix == item.prefix && BagPickup.EndlessStack(pickup, item.type, a))
                    {
                        canStack = true;
                        break;
                    }
                }
                if (!canStack)
                    return pickup.contained.Count < pickup.limit;
                else
                    return true;
            }
            return base.ItemSpace(item, player);
        }
        public override bool OnPickup(Item item, Player player)
        {
            if (pickup != null && BagPickup.NotStorage(item))
            {
                if (canStack)
                {
                    for (int a = 0; a < pickup.contained.Count; a++)
                    {
                        if (pickup.contained[a].type == item.type && pickup.contained[a].prefix == item.prefix && BagPickup.EndlessStack(pickup, item.type, a))
                        {
                            Main.PlaySound(SoundID.Grab);
                            pickup.contained[a].stack += item.stack;
                            ItemText.NewText(item, item.stack);
                            item.SetDefaults(ItemID.None);
                            canStack = false;
                            break;
                        }
                    }
                    BagPickup.SetList(new Item(), player.GetModPlayer<CastledPlayer>(), pickup);
                    return false;
                }
                if (!canStack && pickup.contained.Count < pickup.limit)
                {
                    Main.PlaySound(SoundID.Grab);
                    pickup.contained.Add(item.Clone());
                    ItemText.NewText(item, item.stack);
                    BagPickup.SetList(new Item(), player.GetModPlayer<CastledPlayer>(), pickup);
                    return false;
                }
            }
            return base.OnPickup(item, player);
        }
        public override GlobalItem NewInstance(Item item)
        {
            //oldUse = item.useStyle;
            if (LMan.itemCheck)
            {
                if (!LMan.numList.Contains(item.type))
                {
                    orC = item.color;
                    if (item.type < 3930)
                    {
                        LMan.numList.Add(item.type);
                        LMan.nameList.Add(item.Name);
                        if (item.Name.Contains("Banner") || item.Name.Contains("Music Box") || item.Name.Contains("Trophy"))
                            LMan.rareList.Add(-12);
                        else if (item.createTile != -1 || item.createWall != -1 && item.rare == ItemRarityID.White && !item.Name.Contains("Ore") && !item.Name.Contains("Bar") && !item.Name.Contains("Crate"))
                            LMan.rareList.Add(-13);
                        else if (item.vanity || item.dye != 0 && item.type != ItemID.DyeVat || item.hairDye != -1)
                            LMan.rareList.Add(-14);
                        else if (item.consumable && item.buffType != -1)
                            LMan.rareList.Add(-15);
                        else
                            LMan.rareList.Add(item.rare);
                    }


                    if (item.type > 3930)
                        LMan.modList.Add(item.modItem.mod.Name);
                    else if (item.type == 3930 || item.type < 3930)
                        LMan.modList.Add("Vanilla");
                    if (item.type > 3930 && !LMan.influenceModList.Contains(item.modItem.mod.Name) && !LMan.extraMods.Contains(item.modItem.mod.Name))
                    {
                        LMan.extraMods.Add(item.modItem.mod.Name);
                        //LMan.modNames.Add(item.modItem.mod.DisplayName);
                    }
                    if (item.type > 3930 && LMan.influenceModList.Contains(item.modItem.mod.Name) && !LMan.influMod.Contains(item.modItem.mod.Name))
                    {
                        LMan.influMod.Add(item.modItem.mod.Name);
                        //LMan.modNames.Add(item.modItem.mod.DisplayName);
                    }
                    item.TurnToAir();
                }
            }
            return base.NewInstance(item);
        }
        #region ContraColor
        public override bool UseItem(Item item, Player player)
        {
            if (player.GetModPlayer<SnareBoolean>().grasped)
                player.GetModPlayer<SnareBoolean>().useGrasp = true;
            return base.UseItem(item, player);
        }
        public override void UpdateInventory(Item item, Player player) 
        {
            if (!Main.playerInventory || tagExpire < 1)
            {
                bagTag = false;
                SLHighlight = false;
            }
            ContraColor(item); 
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual) { ContraColor(item); }
        public override void UpdateEquip(Item item, Player player) { ContraColor(item); }
        #endregion
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //Player player = Main.player[Main.myPlayer];

            #region
            /*
            foreach (TooltipLine item2 in tooltips)
            {
                if (item2.mod == "Terraria" && item2.Name == "ItemName")
                {
                    if (CastledsContent.ConsolePrint1.JustPressed)
                    {
                        if (item.type > 3930)
                        {
                            //Console.WriteLine
                            Main.NewText($"The selected modItem, {item.Name}, has the following values:");
                            Main.NewText($"Mod Name & Version: {item.modItem.mod} Version {item.modItem.mod.Version}");
                            if (item2.overrideColor != null)
                                Main.NewText($"overrideColor: {item2.overrideColor}");
                            Main.NewText($"Rarity: {item.modItem.item.rare}");
                        }
                        else
                        {
                            //Console.WriteLine
                            Main.NewText($"The selected item, {item.Name}, has the following values:");
                            Main.NewText($"maxStack: {item.maxStack}");
                            Main.NewText($"Rarity: {item.rare}");
                        }
                    }
                }
            }
            */
            #endregion
            if (LMan.hasSucceeded && LMan.synci1 == item.type && LMan.counter > 0 || LMan.hasSucceeded && LMan.synci2 == item.type && LMan.counter > 0 || LMan.hasSucceeded && LMan.synci3 == item.type && LMan.counter > 0 || LMan.hasSucceeded && LMan.ib1 == item.type && LMan.counter > 0 || LMan.hasSucceeded && LMan.ib2 == item.type && LMan.counter > 0)
            {
                foreach (TooltipLine item2 in tooltips)
                {
                    if (item2.mod == "Terraria" && item2.Name == "ItemName")
                    {
                        item2.overrideColor = new Color(Main.DiscoR + 35, 35, 60);
                    }
                }
                int num = -1;
                int num2 = 0;
                while (num2 < tooltips.Count)
                {
                    if (!tooltips[num2].Name.Equals("ItemName"))
                    {
                        num2++;
                        continue;
                    }
                    num = num2;
                    break;
                }
                tooltips.Insert(num + 1, new TooltipLine(mod, "Contra", "[c/ff0000:Contrabande]"));
                tooltips.Insert(num + 2, new TooltipLine(mod, "ContraTag1", "Right Click this item to discard it as contrabande.\nIf the above precaution is not done, this item will be forcefully taken, and will be destroyed if contrabande removal is successful."));
                tooltips.Insert(num + 3, new TooltipLine(mod, "ContraTag2", "You will not be able to pick this up if you throw it on the ground"));
            }
        }
        public override bool CanPickup(Item item, Player player)
        {
            if (LMan.pickupPrevent || LMan.restrictPick)
                return false;
            return base.CanPickup(item, player);
        }
        public void ContraColor(Item item)
        {
            //Player player = Main.player[Main.myPlayer];
            Color contraC = new Color(Main.DiscoR - (item.color.R / 2), item.color.G, item.color.B);
            if (IsContrabande(item))
            {
                item.color = contraC;
                CastledPlayer.hasContra = true;
                //Main.NewText("Add");
            }
            if (!LMan.finalOutcome && LMan.counter < 1)
            {
                flag = false;
                item.color = orC;
            }
            flag = true;
            //LMan.delayReset = true;
        }
        public override Color? GetAlpha(Item item, Color lightColor)
        {
            if (tagExpire > 0)
                tagExpire--;
            if (!Main.playerInventory || tagExpire < 1)
                bagTag = false;
            if (!Main.playerInventory || !Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>().SLHighlighting)
                SLHighlight = false;
            CastledsContent instance = CastledsContent.instance;
            Color flashColor = new Color(130 + instance.bagTagFlash, 130 + instance.bagTagFlash, 130 + instance.bagTagFlash);
            Color coolColor = new Color(195 + instance.bagTagFlash, 150 + instance.bagTagFlash, 255);
            if (bagTag && !SLHighlight)
                return flashColor;
            if (SLHighlight && !bagTag)
                return coolColor;
            return base.GetAlpha(item, lightColor);
        }
        #region Discard Contrabande
        public override bool CanRightClick(Item item)
        {
            if (IsContrabande(item) || Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>().SLHighlighting)
                return true;
            return base.CanRightClick(item);
        }
        public override void RightClick(Item item, Player player)
        {
            bool addedSlot = false;
            CastledPlayer modP = player.GetModPlayer<CastledPlayer>();
            if (player.GetModPlayer<CastledPlayer>().SLHighlighting)
                SLHighlight = true;
            if (IsContrabande(item))
            {
                for (int a = 0; a < 254; a++)
                {
                    if (modP.contrabande[a].IsAir)
                    {
                        if (!addedSlot)
                        {
                            //int oldStack = item.stack;
                            Main.PlaySound(SoundID.Item108, player.position);
                            modP.contrabande[a] = item.Clone();
                            if (item.stack > 1)
                            {
                                //oldStack = item.stack;
                                item.stack = 0;
                                //Main.NewText(oldStack);
                            }
                            //modP.contrabande[a].stack = oldStack;
                            //Main.NewText(modP.contrabande[a].Name);
                            //Main.NewText($"{modP.contrabande[a].Name} {modP.contrabande[a].stack}");
                            addedSlot = true;
                            if (ModContent.GetInstance<ClientConfig>().algorithmoMessage)
                                Main.NewText($"{modP.contrabande[a].Name} {modP.contrabande[a].stack} {a}");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Several switch cases that determine several values, to discard contrabande automatically. This affects every slot, including banks.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="type"></param>
        public void ForceDiscard (Player player, int type)
        {
            CastledPlayer modP = player.GetModPlayer<CastledPlayer>();
            int Slots()
            {
                switch (type)
                {
                    case 1:
                        return 58;
                    case 2:
                        return 19;
                    case 3:
                        return 9;
                    case 4:
                        return 4;
                    case 5:
                        return 4;
                    case 6:
                        return 40;
                    case 7:
                        return 40;
                    case 8:
                        return 40;
                }
                return 0;
            }
            bool SlotIsAir(int a)
            {
                switch (type)
                {
                    case 1:
                        return player.inventory[a].IsAir;
                    case 2:
                        return player.armor[a].IsAir;
                    case 3:
                        return player.dye[a].IsAir;
                    case 4:
                        return player.miscEquips[a].IsAir;
                    case 5:
                        return player.miscDyes[a].IsAir;
                    case 6:
                        return player.bank.item[a].IsAir;
                    case 7:
                        return player.bank2.item[a].IsAir;
                    case 8:
                        return player.bank3.item[a].IsAir;
                }
                return false;
            }
            bool ItemsCheck(int a)
            {
                if (!SlotIsAir(a))
                {
                    switch (type)
                    {
                        case 1:
                            return player.inventory[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.inventory[a]);
                        case 2:
                            return player.armor[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.armor[a]);
                        case 3:
                            return player.dye[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.dye[a]);
                        case 4:
                            return player.miscEquips[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.miscEquips[a]);
                        case 5:
                            return player.miscDyes[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.miscDyes[a]);
                        case 6:
                            return player.bank.item[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.bank.item[a]);
                        case 7:
                            return player.bank2.item[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.bank2.item[a]);
                        case 8:
                            return player.bank3.item[a].GetGlobalItem<SGlobalItem>().IsContrabande(player.bank3.item[a]);

                    }
                }
                return false;
            }
            void Actions(int a, int b, int action)
            {
                switch (action)
                {
                    case 1:
                        {
                            switch (type)
                            {
                                case 1:
                                    modP.contrabande[b] = player.inventory[a].Clone();
                                    break;
                                case 2:
                                    modP.contrabande[b] = player.armor[a].Clone();
                                    break;
                                case 3:
                                    modP.contrabande[b] = player.dye[a].Clone();
                                    break;
                                case 4:
                                    modP.contrabande[b] = player.miscEquips[a].Clone();
                                    break;
                                case 5:
                                    modP.contrabande[b] = player.miscDyes[a].Clone();
                                    break;
                                case 6:
                                    modP.contrabande[b] = player.bank.item[a].Clone();
                                    break;
                                case 7:
                                    modP.contrabande[b] = player.bank2.item[a].Clone();
                                    break;
                                case 8:
                                    modP.contrabande[b] = player.bank3.item[a].Clone();
                                    break;
                            }
                        }
                        break;
                    case 2:
                        {
                            switch (type)
                            {
                                case 1:
                                    player.inventory[a] = new Item();
                                    break;
                                case 2:
                                    player.armor[a] = new Item();
                                    break;
                                case 3:
                                    player.dye[a] = new Item();
                                    break;
                                case 4:
                                    player.miscEquips[a] = new Item();
                                    break;
                                case 5:
                                    player.miscDyes[a] = new Item();
                                    break;
                                case 6:
                                    player.bank.item[a] = new Item();
                                    break;
                                case 7:
                                    player.bank2.item[a] = new Item();
                                    break;
                                case 8:
                                    player.bank3.item[a] = new Item();
                                    break;
                            }
                            if (LMan.forceDiscard)
                            {
                                LMan.forceDiscard = false;
                                CastledPlayer.serves++;
                            }
                        }
                        break;
                }
            }
            for (int a = 0; a < Slots(); a++)
            {
                if (ItemsCheck(a))
                {
                    //Main.NewText($"{ItemsCheck(a)} {player.inventory[a].type}");
                    for (int b = 0; b < 254; b++)
                    {
                        //Main.NewText($"{modP.contrabande[b].Name} {addedSlot}");
                        if (modP.contrabande[b].IsAir)
                        {
                            if (!addedSlot)
                            {
                                Main.PlaySound(SoundID.Item108, player.position);
                                Actions(a, b, 1);
                                Actions(a, b, 2);
                                //Main.NewText(modP.contrabande[b].Name);
                                //Main.NewText(modP.contrabande[a].Name);
                                //Main.NewText($"{modP.contrabande[a].Name} {modP.contrabande[a].stack}");
                                addedSlot = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// A boolean that determines if a given item is contrabande. The event must take place in order for this to return a value besides false.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsContrabande(Item item)
        {
            if (LMan.hasSucceeded && LMan.counter > 0)
            {
                if (LMan.displayAltTitles)
                    return LMan.synci1 == item.type || LMan.synci2 == item.type || LMan.synci3 == item.type || LMan.ib1 == item.type || LMan.ib2 == item.type;
                else
                    return LMan.synci1 == item.type || LMan.synci2 == item.type || LMan.synci3 == item.type;
            }
            return false;
        }
    }
}