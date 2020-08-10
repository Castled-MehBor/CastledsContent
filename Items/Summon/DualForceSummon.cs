using System;
using CastledsContent.Items.DualForce.Loot.Nasha;
using CastledsContent.NPCs.Boss.DualForce;
using CastledsContent.NPCs.Boss.DualForce.LightMage;
using CastledsContent.NPCs.Boss.DualForce.NightDemon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Summon
{
    public class DualForceSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Totem of the Spirits");
            Tooltip.SetDefault("A totem made from the sturdiest of materials."
            + "\nA speck of magic rests within..."
            + "\nUnleashes two ancient spirits of light and dark when used.");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 32;
            item.useTime = 600;
            item.useAnimation = 600;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.rare = ItemRarityID.LightRed;
        }
        public override Vector2? HoldoutOffset() => new Vector2(9, 2);

        public override bool CanUseItem(Player player) 
            => NPC.downedBoss3 && !NPC.AnyNPCs(NPCType<BossSummon>()) && !NPC.AnyNPCs(NPCType<LightMage>()) && !NPC.AnyNPCs(NPCType<NightDemon>()) && !NPC.AnyNPCs(NPCType<LightNymph>()) && !NPC.AnyNPCs(NPCType<NightHusk>());

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<BossSummon>());
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 100);
            recipe.AddRecipeGroup("IronBar", 15);
            recipe.AddRecipeGroup("Wood", 250);
            recipe.AddIngredient(ItemID.GuideVoodooDoll);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            //Nasha Drop Conversion

            //Orb of the Hallow
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<OrbOfHallow>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<PinkPotion>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<OrbOfHallow>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<CrystalSpear>());
            recipe.AddRecipe();

            //Pink Potion
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PinkPotion>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<OrbOfHallow>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PinkPotion>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<CrystalSpear>());
            recipe.AddRecipe();

            //Crystal Spear
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CrystalSpear>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<OrbOfHallow>());
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CrystalSpear>());
            recipe.needWater = true;
            recipe.SetResult(ItemType<PinkPotion>());
            recipe.AddRecipe();
        }
    }
}