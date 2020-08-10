using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable
{
    public class SuperLauncherMK12 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Boomer");
            Tooltip.SetDefault("Pressing <right> will perform an alt attack, firing an ultra-powerful missile at the cost of mana");
        }

        public override void SetDefaults()
        {
            item.damage = 250;
            item.ranged = true;
            item.width = 70;
            item.height = 44;
            item.useTime = 1;
            item.useAnimation = 1;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 6;
            item.value = 3500000;
            item.rare = (-12);
            item.UseSound = SoundID.Item11;
            item.shoot = 134;
            item.shootSpeed = 38f;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Rocket;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 5;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 50000;
                item.shoot = 134;
                item.mana = 250;
            }
            else
            {
                item.useStyle = 5;
                item.useTime = 1;
                item.useAnimation = 1;
                item.damage = 250;
                item.shoot = 134;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                target.AddBuff(BuffID.BrokenArmor, 90);
            }
            else
            {
                target.AddBuff(BuffID.OnFire, 4);
            }
        }
    }
}