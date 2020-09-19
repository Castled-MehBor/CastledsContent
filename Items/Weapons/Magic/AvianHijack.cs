using CastledsContent.Projectiles.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Magic
{
    public class AvianHijack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avian Hijack");
            Tooltip.SetDefault("Prepare for a storm!"
            + "\nVertically ejects a jinxing core of magic"
            + "\nAfter colliding with a target or on death, a volley of feathers will be launched to it's position"
            + "\nThe weapon can only fire directly upwards");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
            item.magic = true;
            item.width = 38;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 27500;
            item.rare = ItemRarityID.Orange;
            item.mana = 4;
            item.autoReuse = true;
            item.UseSound = SoundID.DD2_BallistaTowerShot.WithVolume(0.1f);
            item.shoot = ProjectileType<FeatherBall>();
            item.shootSpeed = 9f;
            item.crit = 7;
        }
    }
}