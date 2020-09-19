using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class HarpyBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Breastplate");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 16;
            item.value = 6500;
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = true;
        }
    }
}