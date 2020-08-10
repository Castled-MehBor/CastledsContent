using CastledsContent.Buffs;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Morphs
{
    [AutoloadEquip(EquipType.Balloon)]
    public class KingSlimeMorphItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King Slime Morph");
            Tooltip.SetDefault("TEST ITEM: Transforms you into King Slime");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.width = 40;
            item.rare = (-12);
            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffType<KingSlimeBuff>(), 1);
        }
    }
}