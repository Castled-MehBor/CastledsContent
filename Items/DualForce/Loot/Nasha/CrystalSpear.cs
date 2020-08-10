using CastledsContent.Projectiles.DualForce.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.DualForce.Loot.Nasha
{
    public class CrystalSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Spear");
            Tooltip.SetDefault("'The divine and fragile spear of the legendary Nymph, gifted to you.'"
            + "\nThrows a crystalline spear that will create light sparks either both ways horizontally, or vertically."
            + "\nIs stronger in hardmode, and even stronger in the hallow.");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
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
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.DD2_WitherBeastHurt;
            item.autoReuse = true;
            item.shoot = ProjectileType<CrystalSpearFriendly>();
            item.shootSpeed = 8f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneHoly)
            {
                item.damage = 90;
                item.useTime = 50;
                item.useAnimation = 50;
                item.shootSpeed = 12f;
            }
            else if (Main.hardMode)
            {
                item.damage = 80;
                item.useTime = 60;
                item.useAnimation = 60;
                item.shootSpeed = 10f;
            }
            else
            {
                item.damage = 70;
                item.useTime = 75;
                item.useAnimation = 75;
                item.shootSpeed = 8f;
            }
            return true;
        }
    }
}