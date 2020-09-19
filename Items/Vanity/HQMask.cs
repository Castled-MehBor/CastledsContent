using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class HQMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Queen Mask");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 30;
            item.value = 17500;
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
        }
    }
}