using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CastledsContent.Items.ExampleDamageClass
{
    public abstract class ExampleDamageItem : ModItem
    {
        public override bool CloneNewInstances => true;

        // Custom items should override this to set their defaults
        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            mult *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            //Knockback
            knockback += ExampleDamagePlayer.ModPlayer(player).exampleKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            //Critical Hit
            crit += ExampleDamagePlayer.ModPlayer(player).exampleCrit;
        }

        //Tooltip
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                //Custom Tooltip Text
                tt.text = damageValue + " morph " + damageWord;
            }
        }
    }
}