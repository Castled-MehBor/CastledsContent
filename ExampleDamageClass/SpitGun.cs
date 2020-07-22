using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class SpitGun : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater's Saliva Gland");
            Tooltip.SetDefault("Regurgitates a ball of spit."
            + "\n[c/00ff00:Only usable if you are mimicing the Eater of Worlds.]");
        }

        public override void SafeSetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.damage = 65;
            item.useTime = 25;
            item.useAnimation = 25;
            item.autoReuse = true;
            item.expert = true;
            item.useStyle = 3;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("VileSpitFriendly");
            item.shootSpeed = 10;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(mod.BuffType("EaterofWorldsBuff")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                target.AddBuff(BuffID.Slow, 600);
            }
        }
    }
}