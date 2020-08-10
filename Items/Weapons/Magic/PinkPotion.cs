using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Projectiles.DualForce.Friendly;

namespace CastledsContent.Items.Weapons.Magic
{
    public class PinkPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystical Flask");
            Tooltip.SetDefault("'The mysterious flask of the legendary Nymph, gifted to you.'"
            + "\nThrows a voltaic potion that will explode in three directions."
            + "\nIs stronger in hardmode, and even stronger in the hallow.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.magic = true;
            item.noUseGraphic = true;
            item.width = 24;
            item.mana = 5;
            item.height = 24;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileType<PinkBottleFriendly>();
            item.shootSpeed = 8f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneHoly)
            {
                item.damage = 60;
                item.mana = 8;
                item.useTime = 30;
                item.useAnimation = 30;
                item.shootSpeed = 12f;
            }
            else if (Main.hardMode)
            {
                item.damage = 50;
                item.useTime = 40;
                item.mana = 8;
                item.useAnimation = 40;
                item.shootSpeed = 9f;
            }
            else
            {
                item.damage = 40;
                item.useTime = 45;
                item.mana = 8;
                item.useAnimation = 45;
                item.shootSpeed = 8f;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<PinkBottleFriendly>(), damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X - 75, position.Y, speedX, speedY, ProjectileType<PinkBottleFriendly>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
}