using CastledsContent.Projectiles.Friendly.HarpyQueen;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.Weapons.Magic
{
    public class HarpyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avian Ruler's Discipline");
            Tooltip.SetDefault("'Rogue harpy subject firing feathers at you?'\n'Fire feathers back at them.'"
            + "\nLaunches a cartillage capsule that will fire a spinning volley of feathers"
            + "\nSpins faster and charges up in power over time"
            + "\nOnly one of these capsules may exist at a time");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.magic = true;
            item.width = 22;
            item.height = 58;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.value = 27500;
            item.rare = ItemRarityID.Orange;
            item.mana = 20;
            item.autoReuse = true;
            item.UseSound = SoundID.DD2_BookStaffCast;
            item.shoot = ModContent.ProjectileType<HarpyCapsule>();
            item.shootSpeed = 10f;
            item.crit = 5;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<HarpyCapsule>()] < 1;
        #region Harpy Queen Hook
        public override void ModifyTooltips(List<TooltipLine> list)
        {

            foreach (TooltipLine item in list)
            {
                if (item.mod == "Terraria" && item.Name == "ItemName")
                {
                    item.overrideColor = new Color(150, 0, 100);
                }
            }
        }
        #endregion
    }
}