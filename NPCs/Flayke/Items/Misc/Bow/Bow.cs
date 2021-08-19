using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.NPCs.Flayke.Items.Misc.Bow
{
    [AutoloadEquip(EquipType.Head)]
    public class Bow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Ribbon");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.value = 12500;
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }
    }
}