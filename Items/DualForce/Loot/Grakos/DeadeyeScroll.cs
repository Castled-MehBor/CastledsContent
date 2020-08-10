using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Projectiles.DualForce.Friendly;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.DualForce.Loot.Grakos
{
    public class DeadeyeScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dead-Man's Deadeye Contract");
            Tooltip.SetDefault("'The remains of a well-worth barter to the demonic overlord Grakos, gifted to you.'"
            + "\nPosition your line of sight, and then fire away!"
            + "\nIs stronger in hardmode, and even stronger in the Corruption.");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.noUseGraphic = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 90;
            item.UseSound = SoundID.DD2_KoboldIgnite;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = true;
            item.shoot = ProjectileType<LineofSightFriendly>();
            item.shootSpeed = 15f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneCorrupt)
            {
                item.damage = 70;
                item.useTime = 50;
                item.useAnimation = 50;
                item.shootSpeed = 18f;
            }
            else if (Main.hardMode)
            {
                item.damage = 60;
                item.useTime = 70;
                item.useAnimation = 70;
                item.shootSpeed = 18f;
            }
            else
            {
                item.damage = 50;
                item.useTime = 90;
                item.useAnimation = 45;
                item.shootSpeed = 15f;
            }
            return true;
        }
    }
}