using CastledsContent.Buffs;
using CastledsContent.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class SpecialAttack2 : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Rage");
            Tooltip.SetDefault("Unleashes a fragment of Cthulhu's rage right in front of you."
            + "\n[c/00ff00:Only usable if you are mimicing the Eye of Cthulhu.]");
        }

        public override void SafeSetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.damage = 75;
            item.knockBack = 15;
            item.useTime = 45;
            item.useAnimation = 45;
            item.autoReuse = true;
            item.expert = true;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.noUseGraphic = true;
            item.shoot = ProjectileType<TackleP>();
            item.shootSpeed = 2;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffType<EyeofCthulhuBuff>()))
            {
                Main.PlaySound(SoundID.Roar, player.position, 0);
                return true;
            }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 15;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(45));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.HasBuff(BuffType<EmptySyringe>()))
                player.AddBuff(BuffType<AdrenalineBuff>(), 180);
        }
    }
}