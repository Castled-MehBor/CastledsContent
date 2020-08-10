using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CastledsContent.Projectiles.DualForce.Friendly.PreciousFlame;

namespace CastledsContent.Items.Weapons.Magic
{
    public class PreciousFlame : ModItem
    {
        public int flameStyle;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The First Cursed Flame");
            Tooltip.SetDefault("'The ancient spark of eternal heat, gifted by the demonic overlord Grakos.'"
            + "\nFires Cursed Flames in varying directions."
            + "\nIs stronger in hardmode, and even stronger in the Corruption.");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.noUseGraphic = true;
            item.width = 18;
            item.height = 24;
            item.useTime = 45;
            item.mana = 10;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = true;
            item.shoot = ProjectileType<FlameDeclare>();
            item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneCorrupt)
            {
                item.damage = 40;
                item.height = 24;
                item.useTime = 25;
                item.useAnimation = 25;
                item.knockBack = 6;
                item.shootSpeed = 16f;
            }
            else if (Main.hardMode)
            {
                item.damage = 35;
                item.height = 24;
                item.useTime = 35;
                item.useAnimation = 35;
                item.knockBack = 6;
                item.shootSpeed = 12f;
            }
            else
            {
                item.damage = 30;
                item.height = 24;
                item.useTime = 45;
                item.useAnimation = 45;
                item.knockBack = 4;
                item.shootSpeed = 12f;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projectileType = 0;
            switch (flameStyle++)
            {
                case 0:
                    flameStyle = 1;
                    break;
                case 1:
                    projectileType = ProjectileType<Flame1>();
                    break;
                case 2:
                    projectileType = ProjectileType<Flame2>();
                    break;
                case 3:
                    projectileType = ProjectileType<Flame3>();
                    break;
                case 4:
                    goto case 2;
                case 5:
                    projectileType = ProjectileType<Flame4>();
                    break;
                case 6:
                    projectileType = ProjectileType<Flame5>();
                    break;
                case 7:
                    flameStyle = 1;
                    goto case 5;
                default:
                    break;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, projectileType, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}