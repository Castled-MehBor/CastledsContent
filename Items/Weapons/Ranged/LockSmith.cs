using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class LockSmith : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The LockSmith");
            Tooltip.SetDefault("'No door is safe...'"
            + "\nFires coins and piercing Stellar Keys!"
            + "\n'May frighten ballistas'");
        }

        public override void SetDefaults()
        {
            item.damage = 23;
            item.ranged = true;
            item.width = 52;
            item.height = 36;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item11;
            item.shoot = ProjectileType<Projectiles.Friendly.StellarKeyProj>();
            item.shootSpeed = 32f;
            item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, -4);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.CopperCoin, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.CopperCoin, damage, knockBack, player.whoAmI);
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                player.AddBuff(BuffID.BallistaPanic, 240);
            }
        }
    }
}