using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Special
{
    public class TOS : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tree of Smite");
            Tooltip.SetDefault("This is a developer weapon, and is only intended to be used only for fun. Also merry christmas");
        }

        public override void SetDefaults()
        {
            item.damage = 1450;
            item.ranged = true;
            item.width = 160;
            item.height = 52;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 9;
            item.rare = 10;
            item.UseSound = SoundID.Item11;
            item.shoot = 12;
            item.shootSpeed = 78f;
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
                item.useTime = 14;
                item.useAnimation = 14;
                item.useStyle = 5;
                item.damage = 550;
                item.shoot = 335;
                item.useAmmo = AmmoID.Rocket;
            }
            else
            {
                item.useStyle = 5;
                item.useTime = 14;
                item.useAnimation = 14;
                item.damage = 1450;
                item.shoot = 12;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                target.AddBuff(BuffID.Ichor, 7);
            }
            else
            {
                target.AddBuff(BuffID.BrokenArmor, 4);
            }
        }
    }
}