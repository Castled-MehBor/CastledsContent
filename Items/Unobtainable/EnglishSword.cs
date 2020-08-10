using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Unobtainable
{
    public class EnglishSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smite");
            Tooltip.SetDefault("BEGONE!!!!!!");
        }

        public override void SetDefaults()
        {
            item.damage = 300000000;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 0;
            item.useAnimation = 0;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = (-12);
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = 442;
            item.shootSpeed = 38f;
        }
    }
}