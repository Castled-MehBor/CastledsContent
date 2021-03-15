using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Weapons.Ranged
{
    public class Flarethrower : ModItem
    {
        int consumeAmmo = 7;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Flarethrower");
            Tooltip.SetDefault("Every seventh attack consumes gel"
            + "\nSometimes creates molten sparks that inflict Daybroken");
        }

        public override void SetDefaults()
        {

            item.damage = 85;
            item.ranged = true;
            item.width = 58;
            item.height = 24;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 125000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item34.WithVolume(0.25f);
            item.shoot = ProjectileID.Flames;
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Gel;
            item.autoReuse = true;

        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem != item)
                consumeAmmo = 7;
        }
        public override bool ConsumeAmmo(Player player) => consumeAmmo >= 7;
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset() => new Vector2(-14, -2);
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            consumeAmmo++;
            if (consumeAmmo > 7)
                consumeAmmo = 0;
            if (Main.rand.Next(2) == 0)
            {
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 15f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<Projectiles.Friendly.FireSpark>(), damage * 3, knockBack * 3, player.whoAmI);
                Main.PlaySound(SoundID.DD2_FlameburstTowerShot.WithVolume(0.45f), player.position);
            }
            return true;
        }
    }
}