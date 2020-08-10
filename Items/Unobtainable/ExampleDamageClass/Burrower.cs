using CastledsContent.Buffs;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable.ExampleDamageClass
{
    public class Burrower : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Burrower");
            Tooltip.SetDefault("[c/00ff00:Only usable if you are mimicing the Eater of Worlds]");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 35;
            item.width = 36;
            item.height = 36;
            item.useTime = 0;
            item.useAnimation = 0;
            item.noUseGraphic = true;
            item.pick = 65;
            item.useStyle = 1;
            item.knockBack = 2;
            item.expert = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player) => player.HasBuff(BuffType<EaterofWorldsBuff>());
    }
}