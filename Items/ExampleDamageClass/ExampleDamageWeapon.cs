using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.ExampleDamageClass
{
    public class ExampleDamageWeapon : ExampleDamageItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime King's Solution");
            Tooltip.SetDefault("Launches bottles of Royal Gel, covering enemies in slime."
            + "\n[c/aaee21:Ethral Strike : Casts a magical spell which materializes a special potion, but curses the weapon]"
            +"\nRight click to un-curse");
        }

        public override void SafeSetDefaults()
        {
            item.noUseGraphic = true;
            item.width = 42;
            item.height = 42;
            item.damage = 9;
            item.knockBack = 3;
            item.useTime = 24;
            item.useAnimation = 24;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.rare = 2;
            item.useStyle = 1;
            item.shoot = mod.ProjectileType("ExampleDamageProjectile");
            item.shootSpeed = 8;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.useStyle = ItemUseStyleID.EatingUsing;
                item.useAnimation = 15;
                item.useTime = 15;
                player.QuickSpawnItem(mod.ItemType("SlimeKingPotion"));
                item.useAmmo = ItemType<ExampleAmmo>();
            }
            return base.CanUseItem(player);
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("ExampleDamageWeapon"));
        }
    }
}