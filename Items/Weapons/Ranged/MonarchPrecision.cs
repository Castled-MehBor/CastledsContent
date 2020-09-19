using CastledsContent.Projectiles.Friendly.HarpyQueen;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class MonarchPrecision : ModItem
    {
        public int shots;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monarch's Precision");
            Tooltip.SetDefault("'We've got a feather to pick with you, pal'"
            + "\nFires rounds of two bullets"
            + "\nAfter every few shots, an imprecise burst of feathers will be expelled");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.width = 38;
            item.height = 22;
            item.useAnimation = 10;
            item.useTime = 5;
            item.reuseDelay = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 27500;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 2);
        }


        public override bool ConsumeAmmo(Player player)
        {
            return !(player.itemAnimation < item.useAnimation - 2);
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            shots++;
            if (shots > 4)
            {
                Main.PlaySound(SoundID.DD2_BallistaTowerShot.WithVolume(0.1f));
                Main.PlaySound(SoundID.Item32.WithVolume(10f));
                Projectile.NewProjectile(position.X - 15, position.Y, speedX - 10, speedY, ProjectileType<HyperFeatherF>(), damage * (int)1.25, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, speedX - 10, speedY, ProjectileType<GiantFeatherF>(), damage * 2, knockBack * 2, player.whoAmI);
                Projectile.NewProjectile(position.X + 30, position.Y, speedX - 10, speedY, ProjectileType<HyperFeatherF>(), damage * (int)1.25, knockBack, player.whoAmI);
                shots = 0;
            }
            return true;
        }
    }
}
