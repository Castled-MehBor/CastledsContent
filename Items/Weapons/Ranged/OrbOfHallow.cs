using CastledsContent.Projectiles.DualForce.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class OrbOfHallow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Orb of Light");
            Tooltip.SetDefault("'The wise and precise Orb of Light of the legendary Nymph, gifted to you.'"
            + "\nThrows an Orb of Light that will travel for a little bit, stop in place and then explode in four different directions."
            + "\nIs stronger in hardmode, and even stronger in the hallow.");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;
            item.noUseGraphic = true;
            item.width = 26;
            item.height = 26;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileType<HallowOrb1Friendly>();
            item.shootSpeed = 5f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneHoly)
            {
                item.damage = 80;
                item.useTime = 40;
                item.useAnimation = 40;
                item.knockBack = 12;
            }
            else if (Main.hardMode)
            {
                item.damage = 70;
                item.useTime = 50;
                item.useAnimation = 50;
                item.knockBack = 9;
            }
            else
            {
                item.damage = 60;
                item.useTime = 60;
                item.useAnimation = 60;
                item.knockBack = 8;
            }
            return true;
        }
    }
}