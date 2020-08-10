using CastledsContent.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Magic
{
    public class ShinyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Staff of the Ancients");
            Tooltip.SetDefault("'A staff passed on from previous generations.'"
            + "\nFires a homing bolt of pure light");
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.magic = true;
            item.mana = 35;
            item.width = 46;
            item.height = 24;
            item.useAnimation = 55;
            item.useTime = 55;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 50000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item4;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurpleLaser;
            item.shootSpeed = 22f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-5, -2);
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 24; i++)
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<Projectiles.Friendly.LightBolt>(), damage, knockBack, player.whoAmI);

            return false;
        }
    }
}
