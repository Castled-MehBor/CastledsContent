using CastledsContent.Projectiles.DualForce.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Melee
{
    public class CrystalSpear : ModItem
    {
        public DualForceBuff buff = new DualForceBuff(false);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Spear");
            Tooltip.SetDefault("'Fragile, and fragmented; the properties a spear shouldn't have'"
            + "\nThrows a crystalline spear that fires light sparks in two directions");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.noUseGraphic = true;
            item.width = 46;
            item.height = 46;
            item.useTime = 75;
            item.useAnimation = 75;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.DD2_WitherBeastHurt;
            item.autoReuse = true;
            item.shoot = ProjectileType<CrystalSpearFriendly>();
            item.shootSpeed = 8f;
        }
        #region DualForce Hook
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Microsoft.Xna.Framework.Color(255, 100, 175);
                }
            }
        }
        #endregion
        public override bool CanUseItem(Player player)
        {
            item.damage = 45;
            item.useTime = 75;
            item.useAnimation = 75;
            item.shootSpeed = 8f;
            item.damage = buff.BuffInt(item.damage, player, 1);
            item.useTime = buff.BuffInt(item.useTime, player, 2);
            item.useAnimation = buff.BuffInt(item.useAnimation, player, 2);
            item.shootSpeed = buff.BuffFloat(item.shootSpeed, player);
            return true;
        }
    }
}