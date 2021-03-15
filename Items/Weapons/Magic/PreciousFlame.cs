using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Projectiles.DualForce.Friendly.PreciousFlame;

namespace CastledsContent.Items.Weapons.Magic
{
    public class PreciousFlame : ModItem
    {
        public DualForceBuff buff = new DualForceBuff(true);
        public int flameStyle;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The First Cursed Flame");
            Tooltip.SetDefault("'An ember of unyeilding age, unable to extinguish...'"
            + "\nFires Cursed Flames in various directions");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.noUseGraphic = true;
            item.width = 30;
            item.height = 46;
            item.useTime = 45;
            item.useAnimation = 45;
            item.mana = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = true;
            item.shoot = ProjectileType<FlameDeclare>();
            item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            item.damage = 30;
            item.useTime = 45;
            item.useAnimation = 45;
            item.shootSpeed = 12f;
            item.damage = buff.BuffInt(item.damage, player, 1);
            item.useTime = buff.BuffInt(item.useTime, player, 2);
            item.useAnimation = buff.BuffInt(item.useAnimation, player, 2);
            item.shootSpeed = buff.BuffFloat(item.shootSpeed, player);
            return true;
        }
        #region DualForce Hook
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Microsoft.Xna.Framework.Color(175, 115, 255);
                }
            }
        }
        #endregion
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projectileType = 0;
            switch (flameStyle++)
            {
                case 0:
                    flameStyle = 1;
                    break;
                case 1:
                    projectileType = ProjectileType<Flame1>();
                    break;
                case 2:
                    projectileType = ProjectileType<Flame2>();
                    break;
                case 3:
                    projectileType = ProjectileType<Flame3>();
                    break;
                case 4:
                    goto case 2;
                case 5:
                    projectileType = ProjectileType<Flame4>();
                    break;
                case 6:
                    projectileType = ProjectileType<Flame5>();
                    break;
                case 7:
                    flameStyle = 1;
                    goto case 5;
                default:
                    break;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, projectileType, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}