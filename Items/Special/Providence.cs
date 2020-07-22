using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Special
{
    public class Providence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/00FFC1:Profaned Hard Drive]");
            Tooltip.SetDefault("'Do the Provi-dance!'"
            + "\nYou now move as fast as the profaned goddess"
            + "\nDefense increased by 25"
            + "\nLife regen is increased by 15"
            + "\nGrants immunity to knockback & fall damage"
            + "\nIncreases maximum life by 150"
            + "\nGrants immunity to nearly every single vanilla debuff");
        }

        public override void SetDefaults()
        {
            item.width = 62;
            item.width = 30;
            item.value = 750000;
            item.rare = (11);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.runAcceleration += 12.5f;
            player.statDefense += 25;
            player.lifeRegen += 15;
            player.noFallDmg = true;
            player.noKnockback = true;
            player.statLifeMax2 += 150;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Burning] = true;
            //Provvanned!!
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.WindPushed] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.OgreSpit] = true;
        }

        public override void AddRecipes()
        {
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(calamityMod.ItemType("BlissfulBombardier"), 2);
                recipe.AddIngredient(calamityMod.ItemType("HolyCollider"), 2);
                recipe.AddIngredient(calamityMod.ItemType("MoltenAmputator"), 2);
                recipe.AddIngredient(calamityMod.ItemType("PurgeGuzzler"), 2);
                recipe.AddIngredient(calamityMod.ItemType("TelluricGlare"), 2);
                recipe.AddIngredient(calamityMod.ItemType("SolarFlare"), 2);
                recipe.AddIngredient(calamityMod.ItemType("UeliaceBar"), 45);
                recipe.AddIngredient(calamityMod.ItemType("UnholyEssence"), 175);
                recipe.AddIngredient(calamityMod.ItemType("DivineGeode"), 75);
                recipe.AddIngredient(calamityMod.ItemType("BlazingCore"), 5);
                recipe.AddIngredient(calamityMod.ItemType("ElysianWings"));
                recipe.AddIngredient(calamityMod.ItemType("ElysianAegis"));
                recipe.AddIngredient(mod.ItemType("SpeedItem1"));
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}