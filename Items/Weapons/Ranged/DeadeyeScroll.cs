using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Projectiles.DualForce.Friendly;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class DeadeyeScroll : ModItem
    {
        public DualForceBuff buff = new DualForceBuff(true);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dead Man's Deadeye Pact");
            Tooltip.SetDefault("'How do you accidently give someone a pact?'"
            + "\nFires a stream of blasts");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.noUseGraphic = true;
            item.width = 34;
            item.height = 50;
            item.useTime = 90;
            item.useAnimation = 45;
            item.UseSound = SoundID.DD2_KoboldIgnite;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<LineofSightFriendly>();
            item.shootSpeed = 10f;
        }
        public override bool HoldItemFrame(Player player)
        {
            if (!UseItem(player))
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            item.damage = 15;
            item.useTime = 90;
            item.useAnimation = 45;
            item.shootSpeed = 10f;
            item.damage = buff.BuffInt(item.damage, player, 1);
            item.useTime = buff.BuffInt(item.useTime, player, 2);
            item.useAnimation = buff.BuffInt(item.useAnimation, player, 2);
            item.shootSpeed = buff.BuffFloat(item.shootSpeed, player);
            return player.ownedProjectileCounts[ModContent.ProjectileType<LineofSightFriendly>()] < 1;
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
    }
}