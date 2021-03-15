using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CastledsContent.Utilities;
using System.Collections.ObjectModel;

namespace CastledsContent.Items
{
    public class MinigameItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Display Apparatus");
            Tooltip.SetDefault("'A remote janitor calling device.'\nCalls in a Superintendent to clean up leftovers from everyone.\nCan only be used once every three days\nWARNING: Using this item will clear all items on the ground.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 23));
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.width = 48;
            item.rare = ItemRarityID.Quest;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = UseSound();
            item.consumable = false;
            item.noUseGraphic = true;
        }
        public Terraria.Audio.LegacySoundStyle UseSound()
        {
            int randomize = Main.rand.Next(3);
            switch (randomize)
            {
                case 1:
                    return SoundID.Item66;
                case 2:
                    return SoundID.Item67;
                case 3:
                    return SoundID.Item68;
            }
            return SoundID.Item68;
        }
        public override bool UseItem(Player player)
        {
            if (!CastledWorld.waitParti && !NPC.AnyNPCs(mod.NPCType("ItemLotteryNPC")))
            {
                player.GetModPlayer<CastledPlayer>().parti = true;
                NPC.NewNPC((int)player.position.X, (int)player.position.Y - 400, mod.NPCType("ItemLotteryNPC"));
            }
            if (CastledWorld.waitParti)
                LMan.CancelEvent();
            return true;
        }
        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && CastledWorld.waitParti)
            {
                CastledWorld.waitParti = false;
                CastledWorld.finishParti = true;
            }
            if (NPC.AnyNPCs(mod.NPCType("ItemLotteryNPC")))
                return false;
            if (player.GetModPlayer<CastledPlayer>().superintendentDelay <= 0)
            {
                Electricity();
                player.GetModPlayer<CastledPlayer>().superintendentDelay = 162000;
            }
            else
                return false;
            void Electricity()
            {
                Vector2 vec = new Vector2(player.position.X + Main.rand.Next(-25, 25), player.position.Y + Main.rand.Next(-25, 25));
                Rectangle rectangle = new Rectangle((int)player.position.X, (int)(player.position.Y + ((player.height - player.width) / 2)), player.width, player.width);
                Main.PlaySound(SoundID.NPCHit53, player.position);
                Main.PlaySound(SoundID.NPCHit4, player.position);
                for (int a = 0; a < 4; a++)
                    Dust.NewDust(vec, rectangle.Width, rectangle.Height, 21, 0, 0, 100, Color.White, 0.5f);
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            //Player player = Main.player[Main.myPlayer];
                foreach (TooltipLine item in list)
                {
                    if (item.mod == "Terraria" && item.Name == "ItemName")
                    {
                        item.overrideColor = new Color(150, 75, 5);
                    }
                }
                int num = -1;
                int num2 = 0;
                while (num2 < list.Count)
                {
                    if (!list[num2].Name.Equals("ItemName"))
                    {
                        num2++;
                        continue;
                    }
                    num = num2;
                    break;
                }
                list.Insert(num + 1, new TooltipLine(mod, "SuperintendentTag", "Minigame"));
            if (LMan.chooseRandom)
            {
                string hasContra = "[c/FF7D00:TBD]";
                if (CastledPlayer.hasContra && LMan.counter > 0)
                    hasContra = "[c/ff0000:Yes]";
                else if (!CastledPlayer.hasContra && LMan.counter > 0)
                    hasContra = "[c/00FF00:No]";

                list.Insert(num + 2, new TooltipLine(mod, "StatusTag", "Status: [c/00FF00:Online]"));
                list.Insert(num + 2, new TooltipLine(mod, "RoundTag", $"Current Round: {LMan.roundNum}"));
                list.Insert(num + 3, new TooltipLine(mod, "ItemCycleTag", $"[i/1:{LMan.synci1}] [i/1:{LMan.synci2}] [i/1:{LMan.synci3}]"));
                if (LMan.displayAltTitles)
                    list.Insert(num + 4, new TooltipLine(mod, "PocketCycleTag", $"Pocket Check : [i/1:{LMan.ib1}] [i/1:{LMan.ib2}]"));
                else
                    list.Insert(num + 5, new TooltipLine(mod, "TitleTag", $"{LMan.displayTitle}"));
                list.Insert(num + 6, new TooltipLine(mod, "BoolHasContra", $"Has Contrabande? {hasContra}"));
                #region Serves
                if (CastledPlayer.serves == 0)
                    list.Insert(num + 7, new TooltipLine(mod, "FailContraLine1", $"You have not possessed any contrabande yet"));
                else if (CastledPlayer.serves == 1)
                    list.Insert(num + 7, new TooltipLine(mod, "FailContraLine2", $"You possessed Contrabande during {CastledPlayer.serves} round"));
                else if (CastledPlayer.serves > 1)
                    list.Insert(num + 7, new TooltipLine(mod, "FailContraLine3", $"You possessed Contrabande during {CastledPlayer.serves} rounds"));
                #endregion
                #region Materialized
                list.Insert(num + 8, new TooltipLine(mod, "MaterializedTag", $"{Materialized(0)} {Materialized(1)} {Materialized(2)} {Materialized(3)} {Materialized(4)} {Materialized(5)} {Materialized(6)} {Materialized(7)} {Materialized(8)} {Materialized(9)} {Materialized(10)} {Materialized(11)} {Materialized(12)} {Materialized(13)} {Materialized(14)}"));
                string Materialized(int type)
                {
                    if (LMan.setupShop[type] != -1)
                        return $"[i/1:{LMan.setupShop[type]}]";
                    else
                        return "[-]";
                }
                #endregion
            }
            else if (!LMan.chooseRandom)
            {
                list.Insert(num + 2, new TooltipLine(mod, "RandomItemTag", "Status: [c/FF0000:Offline]"));
                list.Insert(num + 3, new TooltipLine(mod, "ChargesTag", DetermineCharge()));
            }
            foreach (TooltipLine item2 in list)
                {
                    if (item2.mod == "CastledsContent" && item2.Name == "RobotInvasionTag")
                    {
                        item2.overrideColor = new Color(90, 25, 0);
                    }
                }
            for (int a = 0; a < list.Count; a++)
                if (list[a].Name == "TitleTag" && list[a].mod == "CastledsContent")
                    list[a].overrideColor = LMan.titleColor;
            string DetermineCharge()
            {
                CastledPlayer player = Main.player[Main.myPlayer].GetModPlayer<CastledPlayer>();
                if (player.superintendentDelay <= 162000 && player.superintendentDelay  > 108000)
                    return "0/3 charges";
                if (player.superintendentDelay > 54000 && player.superintendentDelay <= 108000)
                    return "1/3 charges";
                if (player.superintendentDelay > 0 && player.superintendentDelay <= 54000)
                    return "2/3 charges";
                if (player.superintendentDelay <= 0)
                    return "[c/00FF00:Fully Charged]";
                return "Charge Info not available";
            }
        }
    }
}
