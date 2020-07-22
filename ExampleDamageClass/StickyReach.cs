using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class StickyReach : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime King's Reach");
            Tooltip.SetDefault("Casts a short-ranged spell which will cover enemies in slowing slime."
            + "\n[c/00ff00:Only usable if you are mimicing King Slime.]");
        }

        public override void SafeSetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.damage = 64;
            item.knockBack = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.expert = true;
            item.useStyle = 3;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("StickyReachP");
            item.shootSpeed = 4;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(mod.BuffType("KingSlimeBuff")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}