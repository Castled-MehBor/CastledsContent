using CastledsContent.Projectiles.DualForce.Friendly;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CastledsContent.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.Weapons.Melee
{
    public class LightSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Sword");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 56;
            item.height = 62;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 10;
            item.knockBack = 1;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.shoot = ProjectileID.None;
            item.autoReuse = true;
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                target.AddBuff(BuffType<Lightful>(), 600);
            }
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			return false;
		}
    }
}