using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class ExampleDamageAccessory : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.AnglerEarring;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased additive example damage" +
                               "\n20% more multiplicative example damage" +
                               "\n15% increased example critical strike chance" +
                               "\n5 increased increased example knockback");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(34);
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ExampleDamagePlayer modPlayer = ExampleDamagePlayer.ModPlayer(player);
            modPlayer.exampleDamageAdd += 0.2f;
            modPlayer.exampleDamageMult *= 1.2f;
            modPlayer.exampleCrit += 15;
            modPlayer.exampleKnockback += 5;
        }
    }
}