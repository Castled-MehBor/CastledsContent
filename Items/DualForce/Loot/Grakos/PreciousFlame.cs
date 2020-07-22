using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CastledsContent.Items.DualForce.Loot.Grakos
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
            item.shoot = mod.ProjectileType("FlameDeclare");
            item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.hardMode && player.ZoneCorrupt)
            {
                item.damage = 40;
                item.magic = true;
                item.noUseGraphic = true;
                item.width = 18;
                item.height = 24;
                item.mana = 10;
                item.useTime = 25;
                item.useAnimation = 25;
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.noMelee = true;
                item.knockBack = 6;
                item.value = 50000;
                item.rare = ItemRarityID.LightRed;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("FlameDeclare");
                item.shootSpeed = 16f;
            }
            else if (Main.hardMode)
            {
                item.damage = 35;
                item.magic = true;
                item.noUseGraphic = true;
                item.width = 18;
                item.mana = 10;
                item.height = 24;
                item.useTime = 35;
                item.useAnimation = 35;
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.noMelee = true;
                item.knockBack = 6;
                item.value = 50000;
                item.rare = ItemRarityID.LightRed;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("FlameDeclare");
                item.shootSpeed = 12f;
            }
            else
            {
                item.damage = 30;
                item.magic = true;
                item.noUseGraphic = true;
                item.width = 18;
                item.mana = 10;
                item.height = 24;
                item.useTime = 45;
                item.useAnimation = 45;
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.noMelee = true;
                item.knockBack = 4;
                item.value = 50000;
                item.rare = ItemRarityID.LightRed;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("FlameDeclare");
                item.shootSpeed = 12f;
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            flameStyle++;
            if (flameStyle < 1)
            {
                flameStyle = 1;
            }
            if (flameStyle == 1)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame1"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame2"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 3)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame3"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 4)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame2"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 5)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame4"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame5"), damage, knockBack, player.whoAmI);
                return false;
            }
            if (flameStyle == 7)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Flame4"), damage, knockBack, player.whoAmI);
                flameStyle = 1;
                return false;
            }
            return false;
        }
    }
}