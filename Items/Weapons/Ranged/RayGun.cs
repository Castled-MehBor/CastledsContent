using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Collections.Generic;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class RayGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ray Gun");
            Tooltip.SetDefault("'Abstraction; light extraction'"
            +"\nFires a piercing ray that slides off tiles"
            +"\nThe ray deals less damage per enemy hit");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.width = 30;
            item.height = 20;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 25000;
            item.noMelee = false;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.DD2_LightningAuraZap.WithVolume(2f);
            item.shoot = ModContent.ProjectileType<Projectiles.Friendly.RayGun>();
            item.shootSpeed = 18f;
            item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -1);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 15f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddIngredient(ModContent.ItemType<Accessories.RobotInvasion.RobotPlate>(), 8);
            recipe.AddRecipeGroup("CastledsContent:RoboGemGroup", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        #region Robot Invasion Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(215, 135, 95);
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
        }
        #endregion
    }
}
