using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using CastledsContent.Projectiles.Friendly;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.Weapons.Magic
{
    public class CarpillaryBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carpillary Blaster");
            Tooltip.SetDefault("Bloody hell!");
        }
        public override void SetDefaults()
        {
            item.damage = 54;
            item.magic = true;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 12500;
            item.autoReuse = true;
            item.rare = ItemRarityID.Pink;
            item.shootSpeed = 8f;
            item.shoot = ModContent.ProjectileType<RedBloodCell>();
            item.UseSound = SoundID.NPCDeath13;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vel = new Vector2(speedX, speedY);
            int rand = Main.rand.Next(6, 8);
            for (int i = 0; i < rand; i++)
            {
                int type2 = Main.rand.Next(5) == 0 ? ModContent.ProjectileType<WhiteBloodCell>() : type;
                float rot = MathHelper.ToRadians(Main.rand.Next(-15, 16)); 
                Projectile.NewProjectile(position, vel.RotatedBy(rot) * Main.rand.NextFloat(0.5f, 1.5f), type2, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}