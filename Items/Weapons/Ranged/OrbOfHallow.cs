using CastledsContent.Projectiles.DualForce.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class OrbOfHallow : ModItem
    {
        public DualForceBuff buff = new DualForceBuff(false);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Light");
            Tooltip.SetDefault("'Don't mistake it for a gobstopper'"
            + "\nThrows an orb that grows in power until it explodes into four light sparks");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.noUseGraphic = true;
            item.width = 22;
            item.height = 30;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileType<HallowOrb1Friendly>();
            item.shootSpeed = 5f;
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
            item.damage = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.shootSpeed = 5f;
            item.damage = buff.BuffInt(item.damage, player, 1);
            item.useTime = buff.BuffInt(item.useTime, player, 2);
            item.useAnimation = buff.BuffInt(item.useAnimation, player, 2);
            item.shootSpeed = buff.BuffFloat(item.shootSpeed, player);
            return true;
        }
    }
}