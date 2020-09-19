using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class HarpyQueenCirclet : ModItem
    {
        public int spaceBoost;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Circlet of the Avian Monarch");
            Tooltip.SetDefault("'Now you're the ruler of the skies'"
            + "\n2 defense"
            + "\n4% increased damage"
            + "\nImmunity to fall damage"
            + "\nIncreased jump speed"
            + "\nAll above effects are doubled in space"
            + "\nYour attacks have a chance to spawn feathers from the sky"
            + "\nToggle visibility to toggle feathers");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.width = 18;
            item.value = 17500;
            item.expert = true;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (slot != i && player.armor[i].type == ItemType<Suggested.DarkCrown>())
                {
                    return false;
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneSkyHeight)
            {
                spaceBoost = 2;
            }
            else
            {
                spaceBoost = 1;
            }
            if (!hideVisual)
            {
                player.GetModPlayer<CastledPlayer>().harpyCrown = true;
            }
            player.statDefense += 2 * spaceBoost;
            player.allDamage += 0.04f * spaceBoost;
            player.noFallDmg = true;
            player.jumpSpeedBoost += 1 * spaceBoost;
            player.wingTimeMax += 50 * spaceBoost;
        }
    }
}