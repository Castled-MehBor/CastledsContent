using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class RayGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ray Gun");
            Tooltip.SetDefault("'A discarded prototype of a handheld weapon'"
            +"\nFires a purple ray that will slide off tiles and pierce a dozen foes"
            +"\nThe ray will dimish in power the more enemies it pierces");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.width = 32;
            item.height = 16;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 25000;
            item.noMelee = false;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.DD2_LightningAuraZap.WithVolume(2f);
            item.shoot = ProjectileType<Projectiles.Friendly.RayGun>();
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
        #region Robot Invasion Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(60, 60, 60);
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
            list.Insert(num + 1, new TooltipLine(mod, "RobotInvasionTag", "Robot Database"));
            foreach (TooltipLine item2 in list)
            {
                if (item2.mod == "CastledsContent" && item2.Name == "RobotInvasionTag")
                {
                    item2.overrideColor = new Color(90, 25, 0);
                }
            }
        }
        #endregion
    }
}
