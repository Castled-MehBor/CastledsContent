// Tag #1: Skyware Boss
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class QJDebuff : ModBuff
    {
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Queen's Judgement");
			Description.SetDefault("Your defensive capabilities are defective");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.defense -= 50;
		}
        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense -= 50;
		}
    }
}