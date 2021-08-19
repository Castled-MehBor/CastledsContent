using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Items.Weapons.Melee
{
	public class CrystalShortsword : ModItem
	{
		public override void SetStaticDefaults() { DisplayName.SetDefault("Crystal Shardsword"); Tooltip.SetDefault("Fires three crystal shards at varying angles"); }
		public override void SetDefaults()
        {
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.damage = 36;
            item.UseSound = SoundID.Item1;
            item.knockBack = 2.5f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.rare = ItemRarityID.LightRed;
            item.value = 22500;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.shoot = ProjectileID.CrystalStorm;
            item.autoReuse = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int a = 0; a < 2; a++)
                Main.projectile[Projectile.NewProjectile(player.Center, new Vector2(player.direction == 1 ? -5f : 5f, 0).RotatedBy(a == 1 ? -15 : 15, default), ProjectileID.CrystalShard, item.damage / 2, item.knockBack / 2, player.whoAmI)].penetrate = 1;
            Projectile shard = Main.projectile[Projectile.NewProjectile(player.Center, new Vector2(player.direction == 1 ? 8f : -8f, 0), ProjectileID.CrystalStorm, item.damage, item.knockBack, player.whoAmI)];
            shard.penetrate = 2;
            shard.timeLeft = 75;
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(ItemID.CopperShortsword);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(ItemID.TinShortsword);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
