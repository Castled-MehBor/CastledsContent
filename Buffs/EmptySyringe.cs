using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CastledsContent.Buffs
{
    public class EmptySyringe : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Empty Syringe");
            Description.SetDefault("Alright, now that that's over and done with, go get 'em!");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}