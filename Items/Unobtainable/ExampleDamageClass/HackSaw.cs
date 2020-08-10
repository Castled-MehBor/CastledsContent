using CastledsContent.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Unobtainable.ExampleDamageClass
{
    public class HackSaw : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebony Hack Saw");
            Tooltip.SetDefault("'Sharp enough to cut dia-' what do you mean Fabsol already did that?"
            + "\nCuts out a portion of your body, releasing homing eaters which deal immense damage."
            + "\nHowever, if you do this, you will greatly damage yourself."
            + "\n[c/00ff00:Only usable if you are mimicing the Eater of Worlds.]");
        }

        public override void SafeSetDefaults()
        {
            item.width = 32;
            item.height = 12;
            item.damage = 150;
            item.useStyle = 3;
            item.useAnimation = 35;
            item.useTime = 35;
            item.useTurn = true;
            item.consumable = false;
            item.expert = true;
            item.shoot = ProjectileID.TinyEater;
            item.shootSpeed = 8;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffType<EaterofWorldsBuff>()))
            {
                player.AddBuff(BuffType<HackSawDebuff>(), 120);
                return true;
            }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 15;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(135));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}