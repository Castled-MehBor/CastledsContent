using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class Maradon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Mar-adon");
            Tooltip.SetDefault("Fires a high-velocity bullet"
            + "\nHas a chance to fire a homing Sharphyte tooth instead of a bullet");
        }

        public override void SetDefaults()
        {

            item.damage = 65;
            item.ranged = true;
            item.width = 62;
            item.height = 22;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 35000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item40;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18, -2);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
            if (Main.rand.Next(9) == 0)
            {
                Main.PlaySound(SoundID.NPCDeath13);
                Main.PlaySound(SoundID.NPCDeath11);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<Projectiles.Friendly.Sharkphyte>(), damage, knockBack, player.whoAmI);
                return false;
            }
            return true;
        }
    }
}