using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Projectiles.DualForce.Friendly;

namespace CastledsContent.Items.Weapons.Magic
{
    public class PinkPotion : ModItem
    {
        public DualForceBuff buff = new DualForceBuff(false);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sickly Sweet Flask");
            Tooltip.SetDefault("'A shimmering cocktail of magic, cotton candy and sodium'"
            + "\nThrows a voltaic flask that explodes into three directions.");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.magic = true;
            item.noUseGraphic = true;
            item.width = 20;
            item.height = 34;
            item.mana = 5;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileType<PinkBottleFriendly>();
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
            item.damage = 25;
            item.useTime = 45;
            item.useAnimation = 45;
            item.shootSpeed = 8f;
            item.damage = buff.BuffInt(item.damage, player, 1);
            item.useTime = buff.BuffInt(item.useTime, player, 2);
            item.useAnimation = buff.BuffInt(item.useAnimation, player, 2);
            item.shootSpeed = buff.BuffFloat(item.shootSpeed, player);
            return true;
        }
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<PinkBottleFriendly>(), damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X - 75, position.Y, speedX, speedY, ProjectileType<PinkBottleFriendly>(), damage, knockBack, player.whoAmI);
            return false;
        }*/
    }
}