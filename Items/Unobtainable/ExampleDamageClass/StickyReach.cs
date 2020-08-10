using CastledsContent.Buffs;
using CastledsContent.Projectiles;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable.ExampleDamageClass
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
            item.useStyle = ItemUseStyleID.Stabbing;
            item.noUseGraphic = true;
            item.shoot = ProjectileType<Projectiles.Friendly.StickyReachP>();
            item.shootSpeed = 4;
        }

        public override bool CanUseItem(Player player) => player.HasBuff(BuffType<KingSlimeBuff>());
    }
}