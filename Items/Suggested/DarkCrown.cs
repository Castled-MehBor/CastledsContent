using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CastledsContent.Items.Accessories;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Suggested
{
    [AutoloadEquip(EquipType.Face)]
    public class DarkCrown : ModItem
    {
        public int boost;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of the Dark-Spawn");
            Tooltip.SetDefault("[c/320032:'Darkness is everywhere, above and below...']"
            + "\nIncreases maximum life by 20"
            + "\n4 defense"
            + "\n8% increased damage"
            + "\n+1 life regen"
            + "\nIncreases maximum minions by 1"
            + "\nImmunity to fall damage"
            + "\nIncreased jump speed"
            + "\nIncreased rocket boot flight time"
            + "\nAll above effects are doubled in space and hell"
            + "\nYour attacks have a chance to spawn dark-spawns from the sky or the ground"
            + "\nThe power, amount and variety of these dark-spawns scale with progression, up to Golem"
            + "\nToggle visibility to toggle dark-spawns");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.width = 34;
            item.value = 75000;
            item.expert = true;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ItemType<HarpyQueenCirclet>())
                {
                    return false;
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneSkyHeight || player.ZoneUnderworldHeight)
            {
                boost = 2;
            }
            else
            {
                boost = 1;
            }
            if (!hideVisual)
            {
                player.GetModPlayer<CastledPlayer>().darkCrown = true;
            }
            player.noFallDmg = true;
            player.maxFallSpeed += 12 * boost;
            player.jumpSpeedBoost += 1 * boost;
            player.statDefense += 4 * boost;
            player.allDamage += 0.08f * boost;
            player.lifeRegen += 1 * boost;
            player.statLifeMax2 += 20 * boost;
            player.maxMinions += 1 * boost;
        }
        #region Donator Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(250, 250, 175);
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
            list.Insert(num + 1, new TooltipLine(mod, "UserSuggestionTag", "Suggested by: MHK10"));
            foreach (TooltipLine item2 in list)
            {
                if (item2.mod == "CastledsContent" && item2.Name == "UserSuggestionTag")
                {
                    item2.overrideColor = new Color(125, 35 + Main.DiscoR / 2, 75);
                }
            }
        }
        #endregion
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<HarpyQueenCirclet>());
            recipe.AddIngredient(ItemID.SoulofNight, 25);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}