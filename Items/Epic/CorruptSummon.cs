﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Epic
{
    public class CorruptSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Smoke Quartz");
            Tooltip.SetDefault("A mineral of the distant past..."
            + "\nIt has lost many of it's characteristics."
            + "\nThe sound of it's shattering could awaken disasterous entities"
            + "\nBecomes very fragile in the Corruption, but nowhere else...");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.width = 18;
            item.height = 30;
            item.maxStack = 99;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = 1;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneCorrupt && Main.hardMode;
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("CorruptionBoss"));
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("BreatherHead"));
            Main.PlaySound(SoundID.Shatter, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 8);
            recipe.AddIngredient(ItemID.ShadowScale, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}