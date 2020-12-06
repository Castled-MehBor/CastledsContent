using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace CastledsContent.Items.Accessories
{
	public class SamuraiInstincts : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Samurai's Instincts");
            Tooltip.SetDefault("'You have made an enemy out of something you have never met.'"
            + "\n40% increased melee speed"
            + "\n40% decreased melee damage");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 34;
            item.accessory = true;
            item.value = 45000;
            item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeSpeed += 0.4f;
            player.meleeDamage -= 0.4f;
        }
    }
}
