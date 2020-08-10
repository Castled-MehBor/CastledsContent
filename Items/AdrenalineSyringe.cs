using CastledsContent.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CastledsContent.Items
{
    public class AdrenalineSyringe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adrenaline Syringe");
            Tooltip.SetDefault("'The eye's sporatic dashing isn't just natural energy, you know.'"
            + "\nInjects an adrenaline-filled syringe, inflicting a special 'buff'"
            + "\nIn the timespan of this buff, each time you attack an enemy (up close)"
            +"\n-with your Tackle, adrenaline will rush through your body, making you jumpy and immune."
            +"\n[c/00ff00:Only usable if you are mimicing the Eye of Cthulhu.]");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.useStyle = 3;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.consumable = false;
            item.expert = true;
            item.buffType = BuffType<EmptySyringe>();
            item.buffTime = 1800;
        }
        public override bool CanUseItem(Player player)
        {
            if (!player.HasBuff(BuffType<EmptySyringe>()))
            {
                item.buffType = BuffType<EmptySyringe>();
                item.buffTime = 1800;
                Main.PlaySound(SoundID.NPCHit, player.position, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}